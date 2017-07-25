using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_And_Snippet_Manager
{
    public partial class MainForm : Form
    {

        private HotKey hotKey;

        private TreeNode historyRootNode;
        private TreeNode snippetRootNode;

        public MainForm()
        {
            InitializeComponent();

            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Hide();

            setupHotKey();

            historyRootNode = treeView1.Nodes.Add("History");
            snippetRootNode = treeView1.Nodes.Add("Snippet");

            snippetRootNode.ContextMenuStrip = snippetFolderNodeContextMenuStrip;

        }

        private void setupHotKey()
        {
            hotKey = new HotKey(MOD_KEY.SHIFT, Keys.None, 0.5f);
            hotKey.HotKeyPush += new EventHandler(toggleVisibule);

        }

        private void toggleVisibule(object sender, EventArgs e)
        {
            if (Visible)
            {
                Hide();
            }
            else
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            hotKey.Dispose();
            notifyIcon1.Visible = false;
        }

        private void PreferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未実装");
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (null != treeView1.SelectedNode)
            {
                // if the Enter key was pressed
                if ((char)Keys.Return == e.KeyChar)
                {
                    doImpactItem(treeView1.SelectedNode);
                }
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            doImpactItem(e.Node);
        }

        private void doImpactItem(TreeNode tn)
        {
            Hide();

            if(tn.Tag is TextSnippetItem)
            {
                TextSnippetItem item = (TextSnippetItem)tn.Tag;
                Clipboard.SetText(item.body);
                SendKeys.SendWait("^v");

            }
            else
            {
                Clipboard.SetText(tn.Text);
                SendKeys.SendWait("^v");
            }
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemForm itemForm = new ItemForm();
            if(itemForm.ShowDialog(this) == DialogResult.OK)
            {
                TextSnippetItem item = itemForm.getItem();

                TreeNode tn = new TreeNode(item.title);
                tn.Tag = item;
                tn.ContextMenuStrip = snippetItemNodeContextMenuStrip;
                treeView1.SelectedNode.Nodes.Add(tn);

            }
            
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;

            if (tn.Tag is TextSnippetItem)
            {
                ItemForm itemForm = new ItemForm((TextSnippetItem)tn.Tag);
                if (itemForm.ShowDialog(this) == DialogResult.OK)
                {
                    TextSnippetItem item = itemForm.getItem();

                    tn.Tag = item;
                    tn.Text = item.title;

                }

            }

        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Remove(treeView1.SelectedNode);
            
        }
    }
}
