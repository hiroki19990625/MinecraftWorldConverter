using System;
using System.Windows.Forms;

namespace MinecraftWorldConverter.Tools
{
    public partial class BitSpliter : Form
    {
        public BitSpliter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result.Text = "";

            long val = long.Parse(data.Text);

            string bin = Convert.ToString(val, 2);

            int split = decimal.ToInt32(splitValue.Value);
            int loop = 64 / split;
            for (int i = 0; i < loop; i++)
            {
                try
                {
                    result.AppendText(bin.Substring(split * i, split) + Environment.NewLine);
                }
                catch
                {
                    result.AppendText(bin.Substring(split * i));
                }
            }
        }
    }
}
