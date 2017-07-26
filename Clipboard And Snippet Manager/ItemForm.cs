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
    public partial class ItemForm : Form
    {
        public ItemForm()
        {
            InitializeComponent();
            
            okButton.Text = "追加";

        }


        public ItemForm(TextSnippetItem item)
        {
            InitializeComponent();
            
            nameTextBox.Text = item.title;
            bodyTextBox.Text = item.body;
            placeholderCheckBox.Checked = item.usePlaceholder;

            for (int i = 0; i < modeComboBox.Items.Count; i++)
            {
                if ((TextSnippetItem.Modes)((ComboItem)modeComboBox.Items[i]).Tag == item.mode)
                {
                    modeComboBox.SelectedIndex = i;
                    break;
                }
            }


            okButton.Text = "修正";

        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public TextSnippetItem getItem()
        {
            TextSnippetItem item = new TextSnippetItem();
            item.title = nameTextBox.Text;
            item.body = bodyTextBox.Text;
            item.timestamp = DateTime.Now;
            item.usePlaceholder = placeholderCheckBox.Checked;
            item.mode = (TextSnippetItem.Modes)((ComboItem)modeComboBox.SelectedItem).Tag;

            return item;
        }

        private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            modeComboBox.Items.Add(new ComboItem("Standard", TextSnippetItem.Modes.standardText));
            modeComboBox.Items.Add(new ComboItem("Send Keys", TextSnippetItem.Modes.sendKeys));
            modeComboBox.SelectedIndex = 0;
        }

    }
}
