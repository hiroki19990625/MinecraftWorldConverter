using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinecraftWorldConverter.Formats.Region;
using MinecraftWorldConverter.Forms;
using MinecraftWorldConverter.Tools;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;

namespace MinecraftWorldConverter.Convertor
{
    public class WorldConvertor
    {
        public MainForm Form { get; private set; }

        public WorldConvertor()
        {
            ListTag list = new ListTag("list", NBTTagType.COMPOUND);

            CompoundTag com = new CompoundTag();
            com.PutInt("int", 123456);
            com.PutLongArray("longArray", new long[5]);

            list.Add(com);
            list.Add(com);

            CompoundTag tag = new CompoundTag();
            tag.PutByte("byte", 0xfe);
            tag.PutInt("int", 0xffffff);
            tag.PutString("string", "ABC1234");
            tag.PutFloat("float", 0.1245f);
            tag.PutDouble("double", 0.456789d);
            tag.PutCompound("compound", com);
            tag.PutList(list);

            //NBTViewer viewer = new NBTViewer(tag);
            //viewer.Show();
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

            NBTViewer viewer = new NBTViewer(datas[0].Data);
            viewer.Show();
        }
    }
}