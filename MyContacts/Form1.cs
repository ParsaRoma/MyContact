using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class Form1 : Form
    {
         
     
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            using (Contact_DBEntities db = new Contact_DBEntities())
            {
                dgContacts.AutoGenerateColumns = false;
                dgContacts.Columns[0].Visible = false;
                dgContacts.DataSource = db.MyContacts.ToList();
            }
            
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    
        private void btnNewContact_Click(object sender, EventArgs e)
        {
            frmAddOrEdit frm = new frmAddOrEdit();
            frm.ShowDialog();
            if(frm.DialogResult==DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                string name = dgContacts.CurrentRow.Cells[1].Value.ToString();
                string family = dgContacts.CurrentRow.Cells[2].Value.ToString();
                var fullname = name + " " + family;
                if (MessageBox.Show($"آیا از حذف {fullname} مطمعنید؟", "توجه", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int contactId = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                    
                    using (Contact_DBEntities db = new Contact_DBEntities())
                    {
                        MyContact contact = db.MyContacts.Single(c=>c.ContactId == contactId);
                    }
                    BindGrid();
                }
            
            
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                int ContactId = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                frmAddOrEdit frm = new frmAddOrEdit();
                frm.ContactId = ContactId;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using(Contact_DBEntities db = new Contact_DBEntities())
            {
                dgContacts.DataSource = db.MyContacts.Where(c => c.Name.Contains(txtSearch.Text) ||
                c.Family.Contains(txtSearch.Text)).ToList();
            }
        }
    }
}
