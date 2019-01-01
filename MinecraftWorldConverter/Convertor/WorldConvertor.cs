using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinecraftWorldConverter.Formats.Region;
using MinecraftWorldConverter.Forms;
using MinecraftWorldConverter.Tools;
using MinecraftWorldConverter.Utils;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;
using MineNET.Utils;

namespace MinecraftWorldConverter.Convertor
{
    public class WorldConvertor
    {
        public MainForm Form { get; private set; }

        public WorldConvertor()
        {

        }

        public Task[] ConvertProcess(MainForm form)
        {
            Form = form;

            string filePath = form.GetFilePath();
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("level.datファイルを参照してください。");
                return null;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("参照されたlevel.datファイルが見つかりません。");
                return null;
            }

            try
            {
                string region = filePath.Replace(Path.GetFileName(filePath), "") + "region";
                DirectoryInfo dir = new DirectoryInfo(region);
                List<string> files = new List<string>();
                foreach (FileInfo info in dir.GetFiles())
                {
                    files.Add(info.FullName);
                }

                int max = files.Count;
                form.UpdateProgress(0, max);
                form.UpdateState($"変換中... ({form.GetProgressValue()} / {max})");

                List<Task> tasks = new List<Task>();
                foreach (string file in files)
                {
                    Task task = new Task(() =>
                    {
                        Convert(file);
                        form.Invoke(new UpdateProcessDelegate(UpdateProcess), max);
                    });
                    tasks.Add(task);

                    task.Start();
                }

                return tasks.ToArray();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private delegate void UpdateProcessDelegate(int max);

        private void UpdateProcess(int max)
        {
            Form.UpdateProgressAdd();
            Form.UpdateState($"変換中... ({Form.GetProgressValue()} / {max})");
        }

        public void Convert(string file)
        {
            OpenRegion region = new OpenRegion(file);
            ChunkData[] datas = region.Open();
            if (datas == null)
            {
                return;
            }

            if (datas.Length == 0)
            {
                return;
            }

            foreach (ChunkData data in datas)
            {
                ConvertChunkData(data);
            }

            NBTViewer viewer = Form.GetNbtViewer();
            viewer?.LoadCompoundTag(datas[0].GetHashCode().ToString(), datas[0].Data);

            string fileName = Path.GetFileName(file);
            string path = file.Replace(fileName, "");
            string newPath = path.Remove(path.Length - 1, 1) + "Convert";
            string newFile = newPath + Path.DirectorySeparatorChar + fileName;

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            region.Write(newFile, datas);
        }

        private void ConvertChunkData(ChunkData data)
        {
            CompoundTag oldTag = data.Data.GetCompound("").GetCompound("Level");
            CompoundTag newTag = (CompoundTag) oldTag.Clone();

            ListTag sections = oldTag.GetList("Sections");
            ListTag newSections = ConvertSections(sections);
            newTag.Remove("Sections");
            newTag.PutList(newSections);

            List<byte> biomes = new List<byte>();
            foreach (int b in oldTag.GetIntArray("Biomes"))
            {
                biomes.Add((byte) b);
            }
            newTag.Remove("Biomes");
            newTag.PutByteArray("Biomes", biomes.ToArray());

            data.Data = new CompoundTag("").PutCompound("Level", newTag);
        }

        private ListTag ConvertSections(ListTag sections)
        {
            ListTag list = new ListTag("Sections", NBTTagType.COMPOUND);
            foreach (Tag sectionTag in sections.Tags)
            {
                if (sectionTag is CompoundTag)
                {
                    list.Add(ConvertSection(sectionTag));
                }
            }

            return list;
        }

        private CompoundTag ConvertSection(Tag sectionTag)
        {
            CompoundTag newSection = new CompoundTag();
            byte[] blockData = new byte[4096];
            NibbleArray metaData = new NibbleArray(4096);

            CompoundTag section = (CompoundTag) sectionTag;
            long[] states = section.GetLongArray("BlockStates");
            ListTag palette = section.GetList("Palette");
            List<RuntimeTable.Table> indexs = new List<RuntimeTable.Table>();
            foreach (Tag paletteTag in palette.Tags)
            {
                if (paletteTag is CompoundTag)
                {
                    CompoundTag pt = (CompoundTag) paletteTag;
                    string name = pt.GetString("Name");
                    indexs.Add(RuntimeTable.GetNameToTable(name));
                }
            }

            int c = 0;
            int split = 64 / 16;
            foreach (long data in states)
            {
                long tmp = data;
                for (int i = 0; i < split; i++)
                {
                    int index = (int) ((tmp >>= split) & 0xf);
                    RuntimeTable.Table table = indexs[index];
                    blockData[c] = (byte) (table.Id & 0xff);
                    metaData[c] = (byte) (table.Data & 0xf);
                    c++;
                }
            }

            newSection = (CompoundTag) section.Clone();
            newSection.Remove("BlockStates");
            newSection.Remove("Palette");

            newSection.PutByteArray("Blocks", blockData);
            newSection.PutByteArray("Data", metaData.ArrayData);

            return newSection;
        }
    }
}