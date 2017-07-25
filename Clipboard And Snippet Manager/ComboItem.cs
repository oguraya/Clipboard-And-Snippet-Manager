using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clipboard_And_Snippet_Manager
{
    class ComboItem
    {
        public string Id = null;
        public string Name = null;
        public object Tag = null;

        public ComboItem(string id,string name, object tag)
        {
            this.Id = id;
            this.Name = name;
            this.Tag = tag;
        }

        public ComboItem(string name, object tag)
        {
            this.Name = name;
            this.Tag = tag;
        }

        public ComboItem(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
