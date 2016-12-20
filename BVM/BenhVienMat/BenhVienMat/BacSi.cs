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
    public partial class Form2 : Form
    {
        string cnstr = "";
        SqlConnection cn;
        DataSet ds; 

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cn = new SqlConnection(cnstr);
            dataGridView1.DataSource = GetCustomerDataset().Tables[0];

         
        }

        public DataSet GetCustomerDataset()
        {
            try
            {
                string sql = "select * from BacSi";
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
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bt1_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO BacSi(MaBS,Ho,Ten,NgaySinh,DiaChi,SDT) VALUES(N'" + txtMaBS.Text + "',N'" + txtHo.Text + "',N'" + txtTen.Text + "',N'" + txtSDT.Text + "',N'" + txtDiaChi.Text + "',N'"  + txtNgaySinh.Text + "') ";

            try
            {
                SqlCommand cm = new SqlCommand(sql, cn);
                cm.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Loi Them Du Lieu\n" + ex.ToString());
            }
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            string sql = @"DELETE FROM BacSi WHERE MaBS='" + txtMaBS.Text + "'";
            SqlCommand cm = new SqlCommand(sql, cn);
            cm.ExecuteNonQuery();
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            string sql = @"UPDATE BacSi SET MaBS='" + txtMaBS.Text + "',Ho='" + txtHo.Text + "',Ten='" + txtTen.Text + "',DiaChi='" + txtDiaChi.Text + "',SDT='" + txtSDT.Text + "',Ngaysinh='" + txtNgaySinh.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
        }

        private void bt5_Click(object sender, EventArgs e)
        {
            MainForm frmForm1 = new MainForm();
            frmForm1.Show();
        }
    }
}
