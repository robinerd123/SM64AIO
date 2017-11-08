using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace sm64aio
{
    public partial class Form2 : Form
    {

        public Form2(Form parentForm)
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Internet is unavailable!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DirectoryInfo d = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\");
            FileInfo[] Files = d.GetFiles("*.zip");
            foreach (FileInfo file in Files)
            {
                if(file.ToString() != "SM64AIO Update.zip")
                {
                    comboBox1.Items.Insert(0, file.Name.Substring(0, file.Name.Length - 4));
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + comboBox1.SelectedItem + "\\main.exe"))
            {
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + comboBox1.SelectedItem + "\\main.exe");
            }
            else
            {
                MessageBox.Show("Unable to start " + comboBox1.SelectedItem + "!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + comboBox1.SelectedItem + "\\desc.txt"))
            {
                MessageBox.Show("Unable to load info for " + comboBox1.SelectedItem + "!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string[] info = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + comboBox1.SelectedItem + "\\desc.txt");
                info[3] = info[3].Replace("(addline)", "\n");
                info[4] = info[4].Replace("(addline)", "\n");
                MessageBox.Show("---" + comboBox1.SelectedItem + "---\n" + info[1] + "\n\nVersion:\n" + info[2], "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
