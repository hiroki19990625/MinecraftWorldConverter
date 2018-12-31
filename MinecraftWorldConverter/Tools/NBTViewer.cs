using System;
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

        private void NBTViewer_Load(object sender, EventArgs e)
        {
            BuildTree();
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null)
                value.Text = e.Node.Tag.ToString();
            else
                value.Text = "プレビュー出来ません。";
        }

        private void BuildTree()
        {
            Tag.Name = "Root";

            Queue<NodeData> stack = new Queue<NodeData>(20);
            stack.Enqueue(new NodeData(treeView.Nodes, Tag));

            while (stack.Count > 0)
            {
                NodeData current = stack.Dequeue();
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
                            stack.Enqueue(new NodeData(node.Nodes, tag));
                        }
                        else
                        {
                            TreeNode child = node.Nodes.Add(tag.Name);
                            child.ToolTipText = tag.GetType().Name;
                            SetTagValue(child, tag);
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
                            stack.Enqueue(new NodeData(node.Nodes, tag, index));
                        }
                        else
                        {
                            TreeNode child = node.Nodes.Add("" + index);
                            child.ToolTipText = tag.GetType().Name;
                            SetTagValue(child, tag);
                        }

                        index++;
                    }
                }
            }
        }

        private void SetTagValue(TreeNode node, Tag tag)
        {
            if (tag is ByteArrayTag)
            {
                ByteArrayTag byteArray = (ByteArrayTag) tag;
                node.Tag = string.Join(Environment.NewLine, byteArray.Data);
            }
            else if (tag is IntArrayTag)
            {
                IntArrayTag intArray = (IntArrayTag) tag;
                node.Tag = string.Join(Environment.NewLine, intArray.Data);
            }
            else if (tag is LongArrayTag)
            {
                LongArrayTag longArray = (LongArrayTag) tag;
                node.Tag = string.Join(Environment.NewLine, longArray.Data);
            }
            else if (tag is ByteTag)
            {
                node.Tag = ((ByteTag) tag).Data;
            }
            else if (tag is ShortTag)
            {
                node.Tag = ((ShortTag) tag).Data;
            }
            else if (tag is IntTag)
            {
                node.Tag = ((IntTag) tag).Data;
            }
            else if (tag is LongTag)
            {
                node.Tag = ((LongTag) tag).Data;
            }
            else if (tag is FloatTag)
            {
                node.Tag = ((FloatTag) tag).Data;
            }
            else if (tag is DoubleTag)
            {
                node.Tag = ((DoubleTag) tag).Data;
            }
            else if (tag is StringTag)
            {
                node.Tag = ((StringTag) tag).Data;
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
