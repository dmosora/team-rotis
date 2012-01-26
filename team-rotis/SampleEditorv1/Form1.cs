using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections;

namespace SampleEditorv1
{
    public partial class Form1 : Form
    {
        Image file;
        bool imageLoad = false;
        Bitmap newBitmap; // This is always the current image
        Stack undoImgStack = new Stack();
        Stack redoImgStack = new Stack();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                newBitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = file;
                imageLoad = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (imageLoad)
            {
                DialogResult dr = saveFileDialog1.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "bmp")
                    {
                        file.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                    }
                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "jpg")
                    {
                        file.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                    }
                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "png")
                    {
                        file.Save(saveFileDialog1.FileName, ImageFormat.Png);
                    }
                }
            }
            else
            {
                MessageBox.Show("Sorry, you must forst select a picture to to save.");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (undoImgStack.Count != 0)
            {
                redoImgStack.Push(new Bitmap(newBitmap));
                newBitmap = (Bitmap)undoImgStack.Pop();
                pictureBox1.Image = newBitmap;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (redoImgStack.Count != 0)
            {
                undoImgStack.Push(new Bitmap(newBitmap));
                newBitmap = (Bitmap)redoImgStack.Pop();
                pictureBox1.Image = newBitmap;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            undoImgStack.Push(new Bitmap(newBitmap));

            for(int x = 0; x < newBitmap.Width; x++)
            {
                for(int y = 0; y < newBitmap.Height; y++)
                {
                    Color originalColor = newBitmap.GetPixel(x, y);
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59) + (originalColor.B * .11));
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
                    newBitmap.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image = newBitmap;
        }
    }
}
