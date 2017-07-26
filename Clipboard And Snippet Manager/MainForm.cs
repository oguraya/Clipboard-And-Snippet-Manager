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

        /// <summary>
        /// ホットキーの登録
        /// </summary>
        private void setupHotKey()
        {
            hotKey = new HotKey(MOD_KEY.SHIFT, Keys.None, 0.5f);
            hotKey.HotKeyPush += new EventHandler(toggleVisibule);

        }

        /// <summary>
        /// フォームの表示非表示を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 通知アイコンの設定をクリックした時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未実装");
        }

        /// <summary>
        /// 通知アイコンの終了をクリックした時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// ツリービューのノードのラベル編集開始時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // ルートノードの編集は不可とする
            if (e.Node.Parent == null)
            {
                e.CancelEdit = true;
            }
        }

        /// <summary>
        /// ツリービューのノードのラベル編集終了時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // ラベル情報を同期させます
            if (e.Node.Tag is TextSnippetItem)
            {
                TextSnippetItem item = (TextSnippetItem)e.Node.Tag;
                item.title = e.Node.Text;
            }
        }

        /// <summary>
        /// ツリービューのキー打鍵した時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// ツリービューのノードをダブルクリックした時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            doImpactItem(e.Node);
        }

        /// <summary>
        /// ノードアイテムを実行する
        /// </summary>
        /// <param name="tn"></param>
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

        /// <summary>
        /// ツリービューのフォルダノードに登録されたコンテキストメニューのアイテム追加を選択した時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// ツリービューのフォルダノードに登録されたコンテキストメニューのフォルダ追加を選択した時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode.Nodes.Add("New Folder");
            treeView1.SelectedNode = tn;
            tn.BeginEdit();
            
        }

        /// <summary>
        /// ツリービューのアイテムノードに登録されたコンテキストメニューのアイテム編集を選択した時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// ツリービューのアイテムノードに登録されたコンテキストメニューのアイテム削除を選択した時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Remove(treeView1.SelectedNode);
            
        }

        
    }
}
