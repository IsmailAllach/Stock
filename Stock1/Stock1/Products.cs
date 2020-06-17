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
    public partial class products : Form
    {
        public products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-Q92DEIU\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            // insertion logic
            con.Open();
            bool status = false;
            if (comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else {
                status = false;
            }
            var sqlquery = "";

            if (IfProductsExists(con,textBox1.Text))
            {
                sqlquery = @"UPDATE [dbo].[Products] SET [ProductName] = '" + textBox2.Text + "' ,[ProductStatut] = '" + status + "' WHERE [ProductCode] = '" + textBox1.Text + "'";
            }
            else
            {
               sqlquery = @"INSERT INTO [dbo].[Products] ([ProductCode],[ProductName],[ProductStatut])  VALUES
                  ('" + textBox1.Text + "','" + textBox2.Text + "','" + status + "')";
            }


            SqlCommand cmd = new SqlCommand(sqlquery, con);
            cmd.ExecuteNonQuery();
            con.Close();

            // reading data

            LoadData();
        }

        private bool IfProductsExists(SqlConnection con,string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("select 1 from Products where [ProductCode] ='" + productCode + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public void LoadData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-Q92DEIU\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("select * from Products", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();

                if ((bool)item["ProductStatut"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }

            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString()== "Active")
            {
                comboBox1.SelectedIndex = 0;

            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
            

            }

        private void btndelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-Q92DEIU\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            var sqlquery = "";

            if (IfProductsExists(con, textBox1.Text))
            {
                con.Open();
                sqlquery = @"delete from [dbo].[Products] WHERE [ProductCode] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Record Not Exists !", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // reading data

            LoadData();
        }
    }
}
