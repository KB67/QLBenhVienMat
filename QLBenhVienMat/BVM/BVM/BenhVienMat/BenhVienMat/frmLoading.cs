using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace BenhVienMat
{
    public partial class frmLoading : Form
    {
        public frmLoading()
        {
            InitializeComponent();
        }

        private void frmLoading_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString);
            try
            {
                connection.Open();
                connectSuccess();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error");
                throw;
            }
        }

        private void connectSuccess()
        {
            this.Hide();
            MainForm frmForm1 = new MainForm();
            frmForm1.Show();
         
        }
    }
}
