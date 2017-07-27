using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Clipboard_And_Snippet_Manager
{
    class TreeNodeExStackRoot : TreeNode
    {
        public bool enabled { get; private set; }
        private int enabledImageIndex;
        private int disabledImageIndex;
        public TreeNodeExStackRoot(int enabledImageIndex,int disabledImageIndex)
        {
            
            this.enabledImageIndex = enabledImageIndex;
            this.disabledImageIndex = disabledImageIndex;
            disable();
        }


        public void enable()
        {
            enabled = true;
            this.ImageIndex = enabledImageIndex;
            this.SelectedImageIndex = enabledImageIndex;
            this.ForeColor = SystemColors.WindowText;
            this.Text = "Stack[on]";
        }

        public void disable()
        {
            this.enabled = false;
            this.ImageIndex = disabledImageIndex;
            this.SelectedImageIndex = disabledImageIndex;
            this.ForeColor = SystemColors.ControlDark;
            this.Text = "Stack[off]";
        }
    }
}
