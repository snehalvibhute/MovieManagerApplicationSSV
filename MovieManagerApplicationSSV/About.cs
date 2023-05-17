using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieManagerApplicationSSV
{
    public partial class About : Form
    {
        public string MoreInformation { get; set; }
        public About()
        {
            InitializeComponent();
        }


        private void About_Load(object sender, EventArgs e)
        {
            txtAbout.Text = MoreInformation;

            string firstLine = txtAbout.Lines[0];
            int index1 = txtAbout.Text.IndexOf(firstLine);
            int length = firstLine.Length;
            txtAbout.Select(index1, length);
            txtAbout.SelectionColor = Color.Blue;
            txtAbout.SelectionFont = new Font("Sergoe UI", 14, FontStyle.Bold);

            string secondLine = txtAbout.Lines[2];
            int index2 = txtAbout.Text.IndexOf(secondLine);
            int secondLength = secondLine.Length;
            txtAbout.Select(index2, secondLength);
            txtAbout.SelectionFont = new Font("Sergoe UI", 11);

            int dateIndex = txtAbout.Text.IndexOf("04/19/2023");
            int dateLength = 10;
            txtAbout.Select(dateIndex, dateLength);
            txtAbout.SelectionFont = new Font("Sergoe UI", 11, FontStyle.Bold);

            int authorIndex = txtAbout.Text.IndexOf("Snehal Vibhute");
            int authorLength = 14;
            txtAbout.Select(authorIndex, authorLength);
            txtAbout.SelectionFont = new Font("Sergoe UI", 11, FontStyle.Bold);

            int versionIndex = txtAbout.Text.IndexOf("1.0");
            int versionLength = 3;
            txtAbout.Select(versionIndex, versionLength);
            txtAbout.SelectionFont = new Font("Sergoe UI", 11, FontStyle.Italic);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
