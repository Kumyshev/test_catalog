using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace appForJob4
{
    public partial class Form1 : Form
    {


        string FULL_PATH;

        public Form1()
        {
            InitializeComponent();
            GetDrive();
        }

        public void GetDirect(TreeNode treeDrive)
        {
            DirectoryInfo[] directs;
            treeDrive.Nodes.Clear();
            string fullPath = treeDrive.FullPath;
            DirectoryInfo direct = new DirectoryInfo(fullPath);

            try
            {
                directs = direct.GetDirectories();
            }
            catch
            {
                return;
            }
            foreach (DirectoryInfo dirinfo in directs)
            {
                TreeNode dir = new TreeNode(dirinfo.Name, 0, 0);
                treeDrive.Nodes.Add(dir);
            }
        }


        public void GetDrive()
        {
            string[] drives = Directory.GetLogicalDrives();

            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            foreach (string drive in drives)
            {
                TreeNode treeDrive = new TreeNode(drive, 0, 0);
                treeView1.Nodes.Add(treeDrive);

                GetDirect(treeDrive);
            }
            treeView1.EndUpdate();
        }


        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeView1.BeginUpdate();
            foreach (TreeNode treeNode in e.Node.Nodes)
            {
                GetDirect(treeNode);
            }
            treeView1.EndUpdate();
        }



        void webShow(string path)
        {
            txtAddress.Text = path;
            string PATH = path.Substring(0, 3);
            if (path.Length > 3)
            {
                path = path.Remove(0, 4);
                path = PATH + path;
            }
            FULL_PATH = path;
            webBrowser1.Url = new Uri(path); 
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            webShow(e.Node.FullPath);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri(txtAddress.Text);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fullPath;
            fullPath = Path.GetFullPath(listBox1.SelectedItem.ToString());

            webBrowser1.Url = new Uri(fullPath);
            txtAddress.Text = listBox1.SelectedItem.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchFile();
        }

        void SearchFile()
        {
            string[] dirs = Directory.GetFiles(FULL_PATH, comboBox1.Text);
            listBox1.Items.Clear();
            foreach (string dir in dirs)
            {
                listBox1.Items.Add(dir);
            }
        }
    }
}
