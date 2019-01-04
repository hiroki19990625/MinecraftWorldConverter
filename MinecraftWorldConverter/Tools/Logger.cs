using System;
using System.Threading;
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
            logger?.Invoke(new LoggerDelegate(logger._Info), msg, Thread.CurrentThread.ManagedThreadId);
        }

        public static void Warn(object msg)
        {
            Logger logger = MainForm.GetForm().GetLogger();
            logger?.Invoke(new LoggerDelegate(logger._Warn), msg, Thread.CurrentThread.ManagedThreadId);
        }

        public static void Error(object msg)
        {
            Logger logger = MainForm.GetForm().GetLogger();
            logger?.Invoke(new LoggerDelegate(logger._Error), msg, Thread.CurrentThread.ManagedThreadId);
        }

        private delegate void LoggerDelegate(object msg, int threadId);

        private void _Info(object msg, int threadId)
        {
            loggerData.AppendText("[Info][" + threadId + "] " + msg + Environment.NewLine);
        }

        private void _Warn(object msg, int threadId)
        {
            loggerData.AppendText("[Warn][" + threadId + "] " + msg + Environment.NewLine);
        }

        private void _Error(object msg, int threadId)
        {
            loggerData.AppendText("[Error][" + threadId + "] " + msg + Environment.NewLine);
        }

        private void allClearCToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            loggerData.Clear();
        }
    }
}
