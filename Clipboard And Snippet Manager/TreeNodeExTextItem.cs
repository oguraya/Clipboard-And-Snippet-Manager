using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_And_Snippet_Manager
{
    class TreeNodeExTextItem : TreeNode
    {
        private TextSnippetItem _item;
        public TextSnippetItem item {
            get { return _item; }
            set { _item = value;refresh(); }
        }

        public TreeNodeExTextItem(TextSnippetItem item, ContextMenuStrip contextMenuStrip , int imageIndex)
        {
            this.ImageIndex = imageIndex;
            this.SelectedImageIndex = imageIndex;
            this.ContextMenuStrip = contextMenuStrip;
            this.item = item;
            
        }

        public void refresh()
        {
            this.Text = item.title;
        }

        public void rename(string text)
        {
            item.title = text;
            refresh();
        }
    }
}
