using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-Q92DEIU\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT * FROM [dbo].[Login] where UserName='"+textBox1.Text+"' and Password='"+textBox2.Text+"'",con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)

          
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show(); 
            }
            else{
                MessageBox.Show("Invalid UserName & Password !", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnclear_Click(sender, e);
            }

        }
    }
}
