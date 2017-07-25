using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clipboard_And_Snippet_Manager
{
    public class TextSnippetItem
    {
        public enum Modes { standardText,sendKeys }
        public string title { get; set; }
        public Modes mode { get; set; }
        public bool usePlaceholder { get; set; }
        public string body { get; set; }
        public DateTime timestamp { get; set; }

        public TextSnippetItem()
        {
            title = "";
            mode = Modes.standardText;
            usePlaceholder = false;
            body = "";
            timestamp = DateTime.Now;

        }

        public TextSnippetItem(string title,string body)
        {
            this.title = title;
            this.body = body;
            mode = Modes.standardText;
            usePlaceholder = false;
            timestamp = DateTime.Now;
        }
    }
}
