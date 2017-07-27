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

        private bool runningItem = false;

        private HotKey hotKey;

        private ClipBoardWatcher cbw;

        private TreeNodeExStackRoot stackRootNode;
        private TreeNodeExHistoryRoot historyRootNode;
        private TreeNodeExSnippetRoot snippetRootNode;

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

            stackRootNode = new TreeNodeExStackRoot(1, 8);
            historyRootNode = new TreeNodeExHistoryRoot(0);
            snippetRootNode = new TreeNodeExSnippetRoot(snippetFolderNodeContextMenuStrip,2);

            treeView1.Nodes.Add(stackRootNode);
            treeView1.Nodes.Add(historyRootNode);
            treeView1.Nodes.Add(snippetRootNode);

            
            cbw = new ClipBoardWatcher();
            cbw.DrawClipBoard += (sender2, e2) => {

                if (Clipboard.ContainsText())
                {
                    string text = Clipboard.GetText();
                    
                    if (historyRootNode.Nodes.Count == 0 || historyRootNode.Nodes[0].Text != text)
                    {
                        TreeNodeExTextItem textItemNode = new TreeNodeExTextItem(new TextSnippetItem(text), snippetItemNodeContextMenuStrip, 5);
                        
                        historyRootNode.Nodes.Insert(0, textItemNode);
                    }

                    if (stackRootNode.enabled)
                    {
                        if (!runningItem)
                        {
                            if (stackRootNode.Nodes.Count == 0 || stackRootNode.Nodes[0].Text != text)
                            {
                                TreeNodeExTextItem textItemNode = new TreeNodeExTextItem(new TextSnippetItem(text), snippetItemNodeContextMenuStrip, 5);

                                stackRootNode.Nodes.Insert(0, textItemNode);

                                textItemNode.EnsureVisible();
                            }
                        }
                        
                        
                    }
                    
                }
            };
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
            if (e.Node is TreeNodeExTextItem)
            {
                TreeNodeExTextItem textItemNode = (TreeNodeExTextItem)e.Node;
                textItemNode.rename(e.Node.Text);
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
            runningItem = true;
            try
            {
                if (tn is TreeNodeExStackRoot)
                {
                    TreeNodeExStackRoot n = (TreeNodeExStackRoot)tn;
                    if (n.enabled)
                    {
                        n.disable();
                    }
                    else
                    {
                        n.enable();
                    }
                }
                else if (tn is TreeNodeExTextItem)
                {
                    Hide();
                    TreeNodeExTextItem textItemNode = (TreeNodeExTextItem)tn;

                    string body = textItemNode.item.body;
                    switch (textItemNode.item.mode)
                    {
                        case TextSnippetItem.Modes.standardText:
                            Clipboard.SetText(body);
                            SendKeys.SendWait("^v");
                            break;
                        case TextSnippetItem.Modes.sendKeys:
                            SendKeys.SendWait(body);
                            break;
                        default:
                            break;
                    }

                    // スタックノードのアイテムの場合、選択アイテムの移動と削除を行う
                    if (TreeNodeUtils.findRootNode(tn) == stackRootNode)
                    {
                        if (tn.NextNode != null)
                        {
                            treeView1.SelectedNode = tn.NextNode;
                        }
                        else if (tn.PrevNode != null)
                        {
                            treeView1.SelectedNode = tn.PrevNode;
                        }
                        else
                        {
                            treeView1.SelectedNode = tn.Parent;
                        }
                        tn.Remove();
                    }

                }
            }
            finally
            {
                runningItem = false;
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

                TreeNodeExTextItem textItemNode = new TreeNodeExTextItem(item, snippetItemNodeContextMenuStrip, 5);

                treeView1.SelectedNode.Nodes.Add(textItemNode);
                treeView1.SelectedNode = textItemNode;
            }
            
        }

        /// <summary>
        /// ツリービューのフォルダノードに登録されたコンテキストメニューのフォルダ追加を選択した時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNodeExFolder folderNode = new TreeNodeExFolder("New Folder", snippetFolderNodeContextMenuStrip, 7);

            treeView1.SelectedNode.Nodes.Add(folderNode);
            treeView1.SelectedNode = folderNode;
            folderNode.BeginEdit();
            
        }

        /// <summary>
        /// ツリービューのアイテムノードに登録されたコンテキストメニューのアイテム編集を選択した時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;

            if (tn is TreeNodeExTextItem)
            {
                TreeNodeExTextItem textItemNode = (TreeNodeExTextItem)tn;
                ItemForm itemForm = new ItemForm(textItemNode.item);
                if (itemForm.ShowDialog(this) == DialogResult.OK)
                {
                    textItemNode.item = itemForm.getItem();
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

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
         
               
        }

        /// <summary>
        /// ノードが選択された時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            // ステータスバーに選択されたアイテムの内容を表示する

            TreeNode tn = treeView1.SelectedNode;

            if (tn != null && tn is TreeNodeExTextItem)
            {
                toolStripStatusLabel1.Text = ((TreeNodeExTextItem)tn).item.title;
            }
            else
            {
                toolStripStatusLabel1.Text = "";
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // 右クリックでコンテキストメニューが表示されたタイミングではSelectedNodeが古いままなので、
            // 右クリックでコンテキストメニューが表示される前に、clickされたノードを選択する
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
            }
        }

        
        
    }
}
