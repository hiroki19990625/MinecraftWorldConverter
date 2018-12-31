using System.Drawing;
using System.Windows.Forms;
using MinecraftWorldConverter.Tools.Controls;
using MineNET.NBT.Tags;

namespace MinecraftWorldConverter.Tools
{
    public partial class NBTViewer : Form
    {
        public NBTViewer()
        {
            InitializeComponent();
        }

        public void LoadCompoundTag(string tabName, CompoundTag tag)
        {
            Invoke(new Internal_LoadDelegate(Internal_Load), tabName, tag);
        }

        private delegate void Internal_LoadDelegate(string tabName, CompoundTag tag);

        private void Internal_Load(string tabName, CompoundTag tag)
        {
            TabPage tabPage = new TabPage(tabName);
            tabPage.ContextMenuStrip = tabControlMenu;
            tabControl.TabPages.Add(tabPage);

            ViewerTab viewer = new ViewerTab(tag);
            viewer.Location = new Point(44, 24);
            tabPage.Controls.Add(viewer);
        }

        private void 閉じるCToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            tabControl.TabPages.Remove(tabControl.SelectedTab);
        }
    }
}
