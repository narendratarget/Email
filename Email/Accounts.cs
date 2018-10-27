using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Email
{
    public partial class Accounts : Form
    {
        public Accounts()
        {
            InitializeComponent();
        }
        String email="";
        private void Accounts_Load(object sender, EventArgs e)
        {

            BindAccounts();

        }

        void BindAccounts()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Email");
                dt.Columns.Add("Password");
                doc.Load(Application.CommonAppDataPath + "/emailsetup.xml");
                foreach (XmlNode node in doc.SelectNodes("Emailsetup/Account"))
                {
                    DataRow _a = dt.NewRow();
                   
                    _a["Email"] = node.SelectSingleNode("Email").InnerText;
                    _a["Password"] = "*********";
                    //node.SelectSingleNode("Password").InnerText;
                    dt.Rows.Add(_a);
                }

                dataGridView1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Error", "Some Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                email = Convert.ToString(selectedRow.Cells["Email"].Value);

            }
        }

        private void deleteAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.CommonAppDataPath + "/emailsetup.xml");
                bool f = false;
                foreach (XmlNode node in doc.SelectNodes("Emailsetup/Account"))
                {
                    if (node.SelectSingleNode("Email").InnerText == email)
                    {
                        node.RemoveAll();
                        node.ParentNode.RemoveChild(node);
                        f = true;
                        break;
                    }
                }

                doc.Save(Application.CommonAppDataPath + "/emailsetup.xml");
                if(f)
                MessageBox.Show("Account deleted Successfully");
                BindAccounts();
            }
            catch {
                MessageBox.Show("Error", "Some Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
               

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePass c = new ChangePass(email);
            c.ShowDialog();
        }
    }
}
