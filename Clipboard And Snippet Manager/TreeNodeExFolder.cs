using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_And_Snippet_Manager
{
    class TreeNodeExFolder : TreeNode
    {
        public TreeNodeExFolder(string name,ContextMenuStrip contextMenuStrip, int imageIndex)
        {
            this.ImageIndex = imageIndex;
            this.SelectedImageIndex = imageIndex;
            this.ContextMenuStrip = contextMenuStrip;
            this.Text = name;
        }
    }
}
