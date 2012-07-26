using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FastSearch
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void dropDownBtn_Click(object sender, EventArgs e)
        {
            if (dropDownPan.Visible)
            {
                dropDownPan.Visible = false;
                dropDownBtn.Image = Properties.Resources.expand;
            }
            else
            {
                dropDownPan.Visible = true;
                dropDownBtn.Image = Properties.Resources.collapse;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Backend.FillList();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();
            foreach(Item item in Backend.Get(it => it.name.ToLower().Contains(textBox1.Text))) {
                listView.Items.Add(item.name);
            }
        }
    }
}
