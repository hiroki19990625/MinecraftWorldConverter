﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinecraftWorldConverter.Formats.Region;
using MinecraftWorldConverter.Forms;

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


        }
    }
}