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
    }
}
