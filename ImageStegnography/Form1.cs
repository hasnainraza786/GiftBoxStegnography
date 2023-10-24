using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageStegnography
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png, *.jpg) | *.png; *.jpg";
            openFileDialog.InitialDirectory = @"C:\Users\hp\Downloads\Image-Steganorgraphy-AES-master";

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName.ToString();
                pictureBox1.ImageLocation = textBox1.Text;
                textBox2.Clear();
                textBox3.Clear();
            }
        }


        //encryption
        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(textBox1.Text);

            for(int i = 0; i < img.Width; i++)
            {
                for(int j = 0; j < img.Height; j++)
                {
                    Color Pixel = img.GetPixel(i, j);

                    if (i < 1 && j < textBox2.Text.Length)
                    {
                        char letter = Convert.ToChar(textBox2.Text.Substring(j, 1));
                        int val = Convert.ToInt32(letter);
                        img.SetPixel(i, j, Color.FromArgb(Pixel.R, Pixel.G, val));
                       
                    }

                }
            }

            SaveFileDialog newFile = new SaveFileDialog();
            newFile.Filter = "Image Files (*.png, *.jpg) | *.png; *.jpg";
            newFile.InitialDirectory = @"C:\Users\hp\Downloads\Image-Steganorgraphy-AES-master";

            if (newFile.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = newFile.FileName.ToString();
                pictureBox1.ImageLocation = textBox1.Text;

                img.Save(textBox1.Text);
                textBox2.Clear();
                textBox3.Clear();
            }
        }



        //decryption
        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(textBox1.Text);
            string msg = "";

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color Pixel = img.GetPixel(i, j);
                    if (i < 1 && j < 16)
                    {
                        int val = Pixel.B;
                        char c = Convert.ToChar(val);
                        string letter = Convert.ToString(c);
                        

                        msg = msg + letter;
                    }
                }
            }
            //textBox2.Text = msg;
            MessageBox.Show("decrypted text is: " + msg);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
