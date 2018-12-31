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
        private NBTViewer _viewer;

        public MainForm()
        {
            InitializeComponent();

            finishCheckWorker.DoWork += FinishCheckWorker_DoWork;
            finishCheckWorker.RunWorkerCompleted += FinishCheckWorker_RunWorkerCompleted;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

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
                button.Enabled = true;
                return;
            }

            finishCheckWorker.RunWorkerAsync(tasks);
        }

        private void nBTViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _viewer = new NBTViewer();
            _viewer.Show();
        }

        private void FinishCheckWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Task[] tasks = (Task[]) e.Argument;
            Task.WaitAll(tasks);
        }

        private void FinishCheckWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateState("変換完了!");
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

        private void valueSpliterSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitSpliter spliter = new BitSpliter();
            spliter.Show();
        }
    }
}
