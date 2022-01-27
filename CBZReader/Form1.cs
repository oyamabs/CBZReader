using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CBZReader
{
    public partial class Form1 : Form
    {
        private List<Image> images;
        private int pageIndex = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image = null;
                images.Clear();
                pictureBox1.Refresh();
            }

            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "CBZ File (*.cbz)|*.cbz";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                FileHandler handler = new FileHandler(fileDialog.FileName);
                images = handler.GetImages();
                pictureBox1.Image = images[0];
            }

            OpenCBZFile.Enabled = false;

        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (pageIndex < images.Count)
                pageIndex++;

            pictureBox1.Image = images[pageIndex];
        }

        private void PrevBtn_Click(object sender, EventArgs e)
        {
            if (pageIndex == 0)
                MessageBox.Show("You can't go to page " + pageIndex--.ToString());
            else
                pageIndex--;

            pictureBox1.Image = images[pageIndex];
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (pageIndex < images.Count)
                    pageIndex++;

                pictureBox1.Image = images[pageIndex];
            }
            if (e.KeyCode == Keys.Left)
            {
                if (pageIndex <= 0)
                {
                    MessageBox.Show("You can't go to page " + (pageIndex-1).ToString());
                    return;
                }
                else
                    pageIndex--;

                pictureBox1.Image = images[pageIndex];
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            NextBtn.DisableSelect();
            PrevBtn.DisableSelect();
        }
    }
}
