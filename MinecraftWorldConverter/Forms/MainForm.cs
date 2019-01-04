using System;
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

            WorldConvertor convertor = new WorldConvertor();
            Task[] tasks = convertor.ConvertProcess(this);
            if (tasks == null)
            {
                Logger.Error("変換に失敗しました。");
                button.Enabled = true;
                return;
            }

            finishCheckWorker.RunWorkerAsync(tasks);
        }

        private void FinishCheckWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Task[] tasks = (Task[]) e.Argument;
            Task.WaitAll(tasks);
        }

        private void FinishCheckWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateState("変換完了!");
            Logger.Info("変換が完了しました。");
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

        public NBTViewer GetNbtViewer()
        {
            return _viewer;
        }

        public Logger GetLogger()
        {
            return _logger;
        }

        private void valueSpliterSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitSpliter spliter = new BitSpliter();
            spliter.Show();
        }

        private void nBTViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_viewer == null)
                _viewer = new NBTViewer();

            _viewer.Show();
        }

        private void loggerLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_logger == null)
                _logger = new Logger();

            _logger.Show();
        }
    }
}
