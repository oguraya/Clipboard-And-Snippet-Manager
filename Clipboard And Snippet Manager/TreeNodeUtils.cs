using System.Windows.Forms;

namespace Clipboard_And_Snippet_Manager
{
    public class TreeNodeUtils
    {
        public static TreeNode findRootNode(TreeNode node)
        {
            if (node.Parent == null)
            {
                return node;
            }

            return findRootNode(node.Parent);
        }
    }
}
