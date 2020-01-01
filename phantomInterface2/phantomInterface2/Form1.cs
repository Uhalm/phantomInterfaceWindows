using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace phantomInterface2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Class for connecting to server
        private void connectToServer(string adress)
        {
            
            //play.fallentech.io:19132
            string comand = "/k phantom-windows.exe -server " + adress;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = comand;
            process.StartInfo = startInfo;
            process.Start();
        }


        private void disconectFromServer()
        {
            foreach (var process in Process.GetProcessesByName("phantom-windows"))
            {
                process.Kill();
            }
        }


        //When File-Exit is clicked the program will execute this ending the program
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveListBox();
            disconectFromServer();
            this.Close();
        }

        //When the About button is clicked the program will execute this displaying a message box with credits and version number
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Interface by Uhalm\nPhantom by jhead\nVersion: 0.0.1", "About");
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            connectToServer(textBox1.Text);
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                connectToServer(textBox1.Text);
             }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            saveListBox();
            disconectFromServer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveListBox();
            disconectFromServer();
        }

        private void ListBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.AppendText(listBox1.GetItemText(listBox1.SelectedItem));
        }

        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.AppendText(listBox1.GetItemText(listBox1.SelectedItem));
            connectToServer(listBox1.GetItemText(listBox1.SelectedItem));
        }


        private void saveListBox()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Servers.ini"))
            {
                foreach (var item in listBox1.Items)
                {
                    file.WriteLine(item.ToString());
                }
            }
        }

        private void loadListBox()
        {
            String path = @"servers.ini";
            using (System.IO.StreamReader sr = System.IO.File.OpenText(path))
            {
                String s = "";

                while ((s = sr.ReadLine()) != null)
                {
                    listBox1.Items.Add(s);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadListBox();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }
    }
}
