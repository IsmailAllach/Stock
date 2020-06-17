using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock1
{
    public partial class StockMain : Form
    {
        

        public StockMain()
        {
            InitializeComponent();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            products pro = new products();
            pro.MdiParent = this;
            pro.Show();
        }

        private void StockMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

      

       

       
        }
    }

