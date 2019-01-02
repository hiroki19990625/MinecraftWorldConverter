using System;
using System.Windows.Forms;
using MinecraftWorldConverter.Forms;

namespace MinecraftWorldConverter.Tools
{
    public partial class Logger : Form
    {
        public Logger()
        {
            InitializeComponent();
        }

        public static void Info(object msg)
        {
            Logger logger = MainForm.GetForm().GetLogger();
            logger?.Invoke(new LoggerDelegate(logger._Info), msg);
        }

        public static void Warn(object msg)
        {
            Logger logger = MainForm.GetForm().GetLogger();
            logger?.Invoke(new LoggerDelegate(logger._Warn), msg);
        }

        public static void Error(object msg)
        {
            Logger logger = MainForm.GetForm().GetLogger();
            logger?.Invoke(new LoggerDelegate(logger._Error), msg);
        }

        private delegate void LoggerDelegate(object msg);

        private void _Info(object msg)
        {
            loggerData.AppendText("[Info] " + msg + Environment.NewLine);
        }

        private void _Warn(object msg)
        {
            loggerData.AppendText("[Warn] " + msg + Environment.NewLine);
        }

        private void _Error(object msg)
        {
            loggerData.AppendText("[Error] " + msg + Environment.NewLine);
        }

        private void allClearCToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            loggerData.Clear();
        }
    }
}
