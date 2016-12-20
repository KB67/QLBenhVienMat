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
    public partial class HoSoBenhNhan : Form
    {
        string cnstr = "";
        SqlConnection cn;
        DataSet ds;

        public HoSoBenhNhan()
        {
            InitializeComponent();
        }

        private void HoSoBenhNhan_Load(object sender, EventArgs e)
        {
            cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cn = new SqlConnection(cnstr);
            dataGridView1.DataSource = GetCustomerDataset().Tables[0];
        }
        public DataSet GetCustomerDataset()
        {
            try
            {
                string sql = "select * from HoSoBenhNhan";
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                ds = new DataSet();//đã có dataset khai báo ở ngoài dataset ds=new dataset();
                da.Fill(ds); // sao chép 
                return ds;
                // xóa sửa thông tin thực chất  sẽ xóa kh đó trên dataset (trên máy mình chưa tácđộng đên csdl)
                //phải cập nhật để xóa trong csdl
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
                //throw;
            }
            finally
            {
                cn.Close();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainForm frmForm1 = new MainForm();
            frmForm1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO HoSoBenhNhan(MaHSBN,MaBN,NgayKham,MaLop,TieuSuBenhLy) VALUES(N'" + txtMaHSBN.Text + "',N'" + txtMaBN.Text + "',N'" + txtNgayKham.Text + "',N'" + txtTieuSuBenhLy.Text + "') ";
           
            try
            {
                SqlCommand cm = new SqlCommand(sql,cn);
                cm.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Loi Them Du Lieu\n" + ex.ToString());
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = @"DELETE FROM HoSoBenhNhan WHERE MaHSBN='" + txtMaHSBN.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.ExecuteNonQuery();

        }
    }
}
