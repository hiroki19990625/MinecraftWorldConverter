using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinecraftWorldConverter.Convertor;
using MinecraftWorldConverter.Tools;

namespace MinecraftWorldConverter.Forms
{
    public partial class MainForm : Form
    {
        private static MainForm _instance;

        private NBTViewer _viewer;
        private Logger _logger;

        private bool _error;

        internal bool TaskCancel { get; private set; }

        internal static MainForm GetForm()
        {
            if (_instance != null)
                return _instance;

            throw new Exception("MainForm is Null");
        }

        public MainForm()
        {
            _instance = this;

            InitializeComponent();

            finishCheckWorker.DoWork += FinishCheckWorker_DoWork;
            finishCheckWorker.RunWorkerCompleted += FinishCheckWorker_RunWorkerCompleted;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            oldList.Items.Add("Java Edition");
            newList.Items.Add("Nukkit");

            oldList.SelectedIndex = 0;
            newList.SelectedIndex = 0;
        }

        private void refarence_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Level Data File (*.dat)|*.dat";

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox.Text = dialog.FileName;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            button.Enabled = false;

            if (button.Tag?.ToString() == "Exec")
            {
                finishCheckWorker.CancelAsync();
            }
            else
            {
                WorldConvertor convertor = new WorldConvertor();
                Task<bool>[] tasks = convertor.ConvertProcess(this);
                if (tasks == null)
                {
                    Logger.Error("変換に失敗しました。");
                    button.Enabled = true;
                    return;
                }

                finishCheckWorker.RunWorkerAsync(tasks);
                button.Text = "キャンセル";
                button.Tag = "Exec";
                button.Enabled = true;
            }
        }

        private void FinishCheckWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Task<bool>[] tasks = (Task<bool>[]) e.Argument;
            Queue<Task<bool>> queue = new Queue<Task<bool>>(tasks);
            while (queue.Count > 0)
            {
                if (finishCheckWorker.CancellationPending)
                {
                    TaskCancel = true;
                    Task.WaitAll(tasks);
                    e.Cancel = true;
                    return;
                }

                Task<bool> task = queue.Dequeue();
                if (task.Wait(1))
                {
                    if (!task.Result)
                    {
                        TaskCancel = true;
                        Task.WaitAll(tasks);

                        if (!finishCheckWorker.CancellationPending)
                            _error = true;
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        UpdateState($"変換中... ({GetProgressValue()} / {tasks.Length})");
                        UpdateProgressAdd();
                    }
                }
                else
                {
                    queue.Enqueue(task);
                }
            }
        }

        private void FinishCheckWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                TaskCancel = false;

                if (_error)
                {
                    Logger.Info("エラーが発生しました。");
                    UpdateState("エラーが発生しました。(ツールのLoggerを使用してエラーを確認してください)");
                    UpdateProgress(0, 0);
                }
                else
                {
                    Logger.Info("キャンセルしました。");
                    UpdateState("キャンセルしました");
                    UpdateProgress(0, 0);
                }

            }
            else
            {
                UpdateState("変換完了!");
                Logger.Info("変換が完了しました。");
            }

            button.Text = "変換";
            button.Tag = "";
            button.Enabled = true;
        }

        public string GetFilePath()
        {
            return textBox.Text;
        }

        public string GetOldWorldType()
        {
            return oldList.SelectedText;
        }

        public string GetNewWorldType()
        {
            return newList.SelectedText;
        }

        public void UpdateState(string text)
        {
            state.Text = text;
        }

        public void UpdateProgress(int now, int max)
        {
            progressBar.Value = now;
            progressBar.Maximum = max;
        }

        public void UpdateProgressAdd()
        {
            progressBar.Value++;
        }

        public int GetProgressValue()
        {
            return progressBar.Value;
        }

        public int GetProgressMaxValue()
        {
            return progressBar.Maximum;
        }

        public NBTViewer GetNbtViewer()
        {
            if (!_viewer.IsDisposed)
                return _viewer;

            return null;
        }

        public Logger GetLogger()
        {
            if (!_logger.IsDisposed)
                return _logger;

            return null;
        }

        private void valueSpliterSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitSpliter spliter = new BitSpliter();
            spliter.Show();
        }

        private void nBTViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_viewer == null || _viewer.IsDisposed)
                _viewer = new NBTViewer();

            _viewer.Show();
        }

        private void loggerLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_logger == null || _logger.IsDisposed)
                _logger = new Logger();

            _logger.Show();
        }

        private void licenseLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseForm form = new LicenseForm();
            form.ShowDialog();
        }
    }
}
