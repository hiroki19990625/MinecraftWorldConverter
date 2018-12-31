using System.Collections.Generic;
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
            Tag.Name = "Root";

            Stack<NodeData> stack = new Stack<NodeData>(20);
            stack.Push(new NodeData(treeView.Nodes, Tag));

            while (stack.Count > 0)
            {
                NodeData current = stack.Pop();
                TreeNodeCollection nodes = current.Nodes;
                Tag data = current.Tag;

                if (data is CompoundTag)
                {
                    CompoundTag compound = (CompoundTag) data;
                    TreeNode node = nodes.Add(current.ListIndex != -1 ? current.ListIndex + "" : (compound.Name == "" ? "NoNameTag" : compound.Name));
                    node.ToolTipText = compound.GetType().Name;
                    foreach (Tag tag in compound.Tags.Values)
                    {
                        if (tag is CompoundTag || tag is ListTag)
                        {
                            stack.Push(new NodeData(node.Nodes, tag));
                        }
                        else
                        {
                            TreeNode child = node.Nodes.Add(tag.Name);
                            child.ToolTipText = tag.GetType().Name;
                        }
                    }
                }
                else if (data is ListTag)
                {
                    int index = 0;
                    ListTag list = (ListTag) data;
                    TreeNode node = nodes.Add(current.ListIndex != -1 ? current.ListIndex + "" : list.Name);
                    node.ToolTipText = list.GetType().Name;
                    foreach (Tag tag in list.Tags)
                    {
                        if (tag is CompoundTag || tag is ListTag)
                        {
                            stack.Push(new NodeData(node.Nodes, tag, index));
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

        private class NodeData
        {
            public TreeNodeCollection Nodes { get; }
            public Tag Tag { get; }
            public int ListIndex { get; }

            public NodeData(TreeNodeCollection nodes, Tag tag, int index = -1)
            {
                Nodes = nodes;
                Tag = tag;
                ListIndex = index;
            }
        }
    }
}
