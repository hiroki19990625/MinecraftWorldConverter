using System;
using System.Collections;
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

            Logger.Info("変換を開始しました。");

            try
            {
                Logger.Info("--- 認識された RegionFile ---");

                string region = filePath.Replace(Path.GetFileName(filePath), "") + "region";
                DirectoryInfo dir = new DirectoryInfo(region);
                List<string> files = new List<string>();
                foreach (FileInfo info in dir.GetFiles())
                {
                    Logger.Info(info.FullName);
                    files.Add(info.FullName);
                }

                int max = files.Count;
                form.UpdateProgress(0, max);
                form.UpdateState($"変換中... ({form.GetProgressValue()} / {max})");

                Logger.Info("タスクの作成を開始。");

                List<Task> tasks = new List<Task>();
                foreach (string file in files)
                {
                    Task task = new Task(() =>
                    {
                        Logger.Info("タスクが開始されました。");
                        try
                        {
                            Convert(file);
                            form.Invoke(new UpdateProcessDelegate(UpdateProcess), max);
                        }
                        catch (Exception e)
                        {
                            Logger.Error("エラーが発生しました。");
                            Logger.Error(e);
                        }
                        Logger.Info("タスクが終了しました。");
                    });
                    tasks.Add(task);

                    Logger.Info("タスクが作成されました。");

                    task.Start();
                }

                Logger.Info("タスクを全て実行中です。");
                return tasks.ToArray();
            }
            catch (Exception e)
            {
                Logger.Error("エラーが発生しました。");
                Logger.Error(e);
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
                Logger.Info("データが存在しないチャンク。");
                return;
            }

            if (datas.Length == 0)
            {
                Logger.Info("データが存在しないチャンク。");
                return;
            }

            Logger.Info("データを変換中。 >> " + region.RegionPosition);
            foreach (ChunkData data in datas)
            {
                ConvertChunkData(data);
            }
            Logger.Info("データの変換が完了しました。 >> " + region.RegionPosition);

            // NBTViewer viewer = Form.GetNbtViewer();
            // viewer?.LoadCompoundTag(datas[0].GetHashCode().ToString(), datas[0].Data);

            string fileName = Path.GetFileName(file);
            string path = file.Replace(fileName, "");
            string newPath = path.Remove(path.Length - 1, 1) + "Convert";
            string newFile = newPath + Path.DirectorySeparatorChar + fileName;

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            Logger.Info("データの書き込み中。 >> " + newFile);
            region.Write(newFile, datas);
            Logger.Info("データの書き込み完了。 >> " + newFile);
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

            newTag.Remove("Heightmaps");
            int[] map = new int[256];
            for (int i = 0; i < 256; i++)
            {
                map[i] = 0xff;
            }
            newTag.PutIntArray("Heightmap", map);

            newTag.PutBool("TerrainPopulated", true);
            newTag.PutBool("TerrainGenerated", true);

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

            int bits = CheckMostBit(indexs.Count - 1);
            List<byte> fixStates = new List<byte>();
            foreach (long state in states)
            {
                fixStates.AddRange(BitConverter.GetBytes((ulong) state));
            }

            BitArray stateBits = new BitArray(fixStates.ToArray());
            for (int i = 0; i < 4096; i++)
            {
                int bitOffset = i * bits;
                uint index = stateBits.Get(bitOffset + bits - 1) ? 1u : 0u;
                for (int j = bits - 2; j >= 0; j--)
                {
                    index <<= 1;
                    index |= stateBits.Get(bitOffset + j) ? 1u : 0u;
                }

                try
                {
                    RuntimeTable.Table table = indexs[(int) index];
                    blockData[i] = (byte) (table.Id & 0xff);
                    metaData[i] = (byte) (table.Data & 0xf);
                }
                catch (Exception e)
                {
                    Logger.Info(indexs.Count + " = " + states[0] + " >>> " + states[1]);
                    throw e;
                }
            }

            newSection = (CompoundTag) section.Clone();
            newSection.Remove("BlockStates");
            newSection.Remove("Palette");

            newSection.PutByteArray("Blocks", blockData);
            newSection.PutByteArray("Data", metaData.ArrayData);

            return newSection;
        }

        private int CheckMostBit(int count)
        {
            int data = count << 20;
            for (int i = 12; i >= 5; i--)
            {
                if ((data & 0x80000000) == 0x80000000)
                    return i;
                data <<= 1;
            }

            return 4;
        }
    }
}