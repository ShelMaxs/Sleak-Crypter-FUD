using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;

namespace SleakDirectFile
{
    public partial class DragandDrop : Form
    {

        private Timer opacityTimer;
        private const int animationDuration = 500;
        private const double targetOpacity = 1.0;
        private double currentOpacity = 0.0;
        private const double opacityIncrement = 0.05;
        private const int timerInterval = 10;
        public DragandDrop()
        {
            InitializeComponent(); 
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(encode_DragEnter);
            this.DragDrop += new DragEventHandler(encode_DragDrop);
            InitializeOpacityTimer();
            this.Load += encode_Load;
        }

        private void button1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void encode_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void encode_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "TXT File (*.txt)|*.txt";
                    saveFileDialog.Title = "Save Converted Base64 ...";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        byte[] Key = Convert.FromBase64String("TxM4zjDL+WrdkYmUb5TvoQ==");

                        byte[] IV = Convert.FromBase64String("e2iOhnoh6dc=");


                        byte[] encrypt = Algroithum.EncryptTripleDES(File.ReadAllBytes(file[0]), Key, IV);

                        string data = Convert.ToBase64String(encrypt);
                        File.WriteAllText(saveFileDialog.FileName, data);

                        MessageBox.Show("Raw File Crypted Successfully!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitializeOpacityTimer()
        {
            opacityTimer = new Timer();
            opacityTimer.Interval = timerInterval;
            opacityTimer.Tick += timer1_Tick;
        }

        private void encode_Load(object sender, EventArgs e)
        {

                opacityTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        Point mouseLocation;
        private void mouse_down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);

        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentOpacity < targetOpacity)
            {
                currentOpacity += opacityIncrement;
                if (currentOpacity > targetOpacity)
                    currentOpacity = targetOpacity;

                this.Opacity = currentOpacity;
            }
            else
            {
                opacityTimer.Stop();
            }
        }
    }
}
