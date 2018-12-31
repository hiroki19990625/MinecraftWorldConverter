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
            long val = long.Parse(data.Text);

            result.Text = "";
            for (int i = 0; i < 16; i++)
            {
                result.Text += ((val >>= 4) & 0xfL) + Environment.NewLine;
            }
        }
    }
}
