using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace sm64aio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int a;
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label7.Text = "- Added offline support";
            cl = label7.Text;
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                MessageBox.Show("Another instance is already running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio");
            }
            if (CheckForInternetConnection())
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile("http://chirucities.servehttp.com/~robinerd/prog.txt", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/prog.txt");
            }
            else
            {
                Form f1 = this;
                Form2 frm = new Form2(this);
                f1.Hide();
                frm.ShowDialog();
                this.Close();
            }
            string[] prog = System.IO.File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/prog.txt");
            bool gaye = false;
            a = -2;
            foreach (var line in prog)
            {
                string installed = "";
                if (gaye == false)
                {
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + line + ".zip"))
                    {
                        installed = " ✓";
                    }
                    if (line != "SM64AIO Update")
                    {
                        checkedListBox1.Items.Insert(0, line + installed);
                    }
                    gaye = true;
                }
                else
                {
                    gaye = false;
                }a++;
            }
            checkedListBox1.SelectedIndex = 0;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] prog = System.IO.File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/prog.txt");
            MessageBox.Show("\"" + prog[a - checkedListBox1.SelectedIndex * 2] + "\" will now be installed.", "Updating", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button1.Text = "Installing...";
            Application.DoEvents();
            if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2] + ".zip"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2] + ".zip");
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2], true);
            }
            using (WebClient client = new WebClient())
             {
                 client.DownloadFile("http://" + prog[a - checkedListBox1.SelectedIndex * 2 + 1], Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex*2 ] + ".zip");
             }
            System.IO.Compression.ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2] + ".zip", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2]);
            button1.Text = "Update";
            button2.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + checkedListBox1.SelectedItem + ".zip"))
            {
                button1.Text = "Install";
                button2.Enabled = false;
            }
            else
            {
                button1.Text = "Update";
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + checkedListBox1.SelectedItem + "\\main.exe");
        }
        int si = 1023;
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string val = "";
            if (first == true)
            {
                first = false;
                checkedListBox1.SelectedIndex = -1;
                return;
            }
            else
            {
                button8.PerformClick();
            }
            if (si < 1023)
            {
                
                checkedListBox1.SelectedIndex = si - 1;
                return;
            }
            else if (si == 1024) {
                return;
            } else
            {
                if (checkedListBox1.SelectedIndex != -1){
                    val = checkedListBox1.SelectedItem.ToString();
                }
                else
                {
                    return;
                }
            }
            if (val[val.Length - 1] == '✓')
            {
                val = val.Remove(val.Length - 2, 2);
            }
            label2.Text = val; WebClient webClient = new WebClient();
            if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + val))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + val);
            }
            webClient.DownloadFile("http://chirucities.servehttp.com/~robinerd/new/" + val + "/desc.txt", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + val + "\\desc.txt");
            string[] info = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + val + "\\desc.txt");
                info[3] = info[3].Replace("(addline)", "\n");
                info[4] = info[4].Replace("(addline)", "\n");
                label3.Text = info[0];
                label9.Text = info[1];
                label6.Text = info[2];
                label7.Text = info[3];
                label8.Text = info[4];
                cl = label7.Text;
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + val + ".zip"))
            {
                button4.Show();
                button4.Text = "Install " + val;
            }
            else
            {
                button4.Hide();
            }
            button5.Text = "Batch install " + checkedListBox1.CheckedItems.Count.ToString() + " elements";
        }
        string cl;
        private void button7_Click(object sender, EventArgs e)
        {
            //UPDATE
            string[] prog = System.IO.File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/prog.txt");
            MessageBox.Show("\"" + prog[a - checkedListBox1.SelectedIndex * 2] + "\" will now be updated.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button7.Text = "Updating...";
            Application.DoEvents();
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2] + ".zip"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2] + ".zip");
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2]))
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2], true);
                }
            }
            using (WebClient client = new WebClient())
            {
                client.DownloadFile("http://" + prog[a - checkedListBox1.SelectedIndex * 2 + 1], Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2] + ".zip");
            }
            System.IO.Compression.ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2] + ".zip", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2]);
            button7.Text = "Update";
        }
        bool first = true;
        private void button6_Click(object sender, EventArgs e)
        {
            //START
            string val = checkedListBox1.SelectedItem.ToString();
            if (val[val.Length - 1] == '✓')
            {
                val = val.Remove(val.Length - 2, 2);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + val + "\\main.exe"))
            {
                if(!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/git.txt")){
                    File.Delete("");
                }
                string[] f = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/git.txt");
                int g = 0;
                while (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + val + "\\" + f[g] + "\\" + val + ".exe")) {
                    g = g + 1;
                    
                }
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + val + "\\" + f[g] + "\\" + val + ".exe");
            }
            else
            {
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + val + "\\main.exe");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("----SM64AIO----\n\nAn all-in-one\ntool for Mario 64\nROM hacking.\n\nMade by:\n-RobiNERD\n-Biobak\n\nSpecial thanks:\n-IceFairyAmy", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        int image = 2;
        private void button4_Click(object sender, EventArgs e)
        {
            if(button4.Text == "Update SM64AIO")
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/path.txt", Directory.GetCurrentDirectory());
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/SM64AIO Update.zip"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/SM64AIO Update.zip");
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/SM64AIO Update", true);
                }
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile("http://chirucities.servehttp.com/~robinerd/aio.zip", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + " /sm64aio/SM64AIO Update.zip");
                }
                System.IO.Compression.ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/SM64AIO Update.zip", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/SM64AIO Update");
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\SM64AIO Update\\main.exe");
                Environment.Exit(1);
                return;
            }
            //INSTALL
            
            string[] prog = System.IO.File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/prog.txt");
            MessageBox.Show("\"" + prog[a - checkedListBox1.SelectedIndex * 2] + "\" will now be installed.", "Install", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button4.Text = "Installing " + checkedListBox1.SelectedItem + "...";
            Application.DoEvents();
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2] + ".zip"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2] + ".zip");
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2], true);
            }
            if(!prog[a - checkedListBox1.SelectedIndex * 2 + 1].Contains("chirucities"))
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/git.txt", Path.GetFileNameWithoutExtension(prog[a - checkedListBox1.SelectedIndex * 2 + 1]) + "\r\n");
            }
            using (WebClient client = new WebClient())
            {
                client.DownloadFile("http://" + prog[a - checkedListBox1.SelectedIndex * 2 + 1], Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2] + ".zip");
            }
            System.IO.Compression.ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2] + ".zip", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/sm64aio/" + prog[a - checkedListBox1.SelectedIndex * 2]);
            button4.Hide();
            button4.Text = "Done.";
            string old = checkedListBox1.SelectedItem.ToString();
            si = checkedListBox1.SelectedIndex;
            checkedListBox1.Items.RemoveAt(si);
            checkedListBox1.Items.Insert(si, old + " ✓");
            si = si + 1;
            if (checkedListBox1.Items.Count <= si)
            {
                Application.Restart();
                Environment.Exit(1);
            }
            checkedListBox1.SelectedIndex = si;
            si = 1023;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                if (image != 1)
                {
                    image = image - 1;
                }
                string val = checkedListBox1.SelectedItem.ToString();
                if (val[val.Length - 1] == '✓')
                {
                    val = val.Remove(val.Length - 2, 2);
                }
                pictureBox1.Load("http://chirucities.servehttp.com/~robinerd/new/" + val + "/" + image.ToString() + ".png");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                if (image != 3)
                {
                    image = image + 1;
                }
                string val = checkedListBox1.SelectedItem.ToString();
                if (val.Length > 1)
                {
                    if (val[val.Length - 1] == '✓')
                    {
                        val = val.Remove(val.Length - 2, 2);
                    }
                    pictureBox1.Load("http://chirucities.servehttp.com/~robinerd/new/" + val + "/" + image.ToString() + ".png");
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cl, "Changelog", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string val = checkedListBox1.SelectedItem.ToString();
            if (val[val.Length - 1] == '✓')
            {
                val = val.Remove(val.Length - 2, 2);
            }
            int bb = 0;
            si = 1023;
            while (bb < checkedListBox1.Items.Count)
            {
                checkedListBox1.SelectedIndex = bb;
                val = checkedListBox1.SelectedItem.ToString();
                if (val[val.Length - 1] == '✓')
                {
                    val = val.Remove(val.Length - 2, 2);
                }
                if (checkedListBox1.GetItemCheckState(bb) == CheckState.Checked)
                {
                    if(!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\sm64aio\\" + val + "\\main.exe"))
                    {
                        button4.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show(val + " is already installed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                bb++;
            }
        }

        private void suggestAToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contact Robin#0133 (Discord) for feature requests.", "Suggestions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
