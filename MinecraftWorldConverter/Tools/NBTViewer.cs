using System;
using System.Windows.Forms;
using MineNET.NBT.Tags;

namespace MinecraftWorldConverter.Tools
{
    public partial class NBTViewer : Form
    {
        public CompoundTag Tag { get; }

        public NBTViewer(CompoundTag tag)
        {
            InitializeComponent();

            Tag = tag;
        }

        private void NBTViewer_Load(object sender, System.EventArgs e)
        {
            try
            {
                Tag.Name = "Root";
                AddCompoundTagNode(treeView.Nodes, Tag);
            }
            catch (StackOverflowException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddCompoundTagNode(TreeNodeCollection parentNodes, CompoundTag compound)
        {
            TreeNode node = parentNodes.Add(compound.Name);
            node.ToolTipText = compound.GetType().Name;
            foreach (Tag tag in compound.Tags.Values)
            {
                if (tag is CompoundTag)
                {
                    AddCompoundTagNode(node.Nodes, (CompoundTag) tag);
                }
                else if (tag is ListTag)
                {
                    AddListTagNode(node.Nodes, (ListTag) tag);
                }
                else
                {
                    TreeNode child = node.Nodes.Add(tag.Name);
                    child.ToolTipText = tag.GetType().Name;
                }
            }
        }

        private void AddListTagNode(TreeNodeCollection parentNodes, ListTag list)
        {
            int index = 0;
            TreeNode node = parentNodes.Add(list.Name);
            node.ToolTipText = list.GetType().Name;
            foreach (Tag tag in list.Tags)
            {
                if (tag is CompoundTag)
                {
                    AddCompoundTagNode(node.Nodes, (CompoundTag) tag);
                }
                else if (tag is ListTag)
                {
                    AddListTagNode(node.Nodes, (ListTag) tag);
                }
                else
                {
                    TreeNode child = node.Nodes.Add("" + index);
                    child.ToolTipText = tag.GetType().Name;
                }

                index++;
            }
        }
    }
}
