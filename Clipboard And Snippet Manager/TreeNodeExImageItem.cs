using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Clipboard_And_Snippet_Manager
{
    class TreeNodeExImageItem : TreeNode
    {
        public Image clipboardImage { get; set; }

        public TreeNodeExImageItem(Image img, ContextMenuStrip contextMenuStrip , int imageIndex)
        {
            this.ImageIndex = imageIndex;
            this.SelectedImageIndex = imageIndex;
            this.ContextMenuStrip = contextMenuStrip;
            this.clipboardImage = img;
            this.Text = "image";
            
        }
    }
}
