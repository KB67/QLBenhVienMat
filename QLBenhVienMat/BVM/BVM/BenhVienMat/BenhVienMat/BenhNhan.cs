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
    public partial class Form1 : Form
    {
        string cnstr = "";
        SqlConnection cn;
        DataSet ds;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cn = new SqlConnection(cnstr);
            dataGridView1.DataSource = GetCustomerDataset().Tables[0];

            
        }
        public DataSet GetCustomerDataset()
        {
            try
            {
                string sql = "select * from BenhNhan";
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
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO BenhNhan(MaBN,Ho,Ten,NgaySinh,DiaChi,Gioitinh) VALUES(N'" + txtMaBN.Text + "',N'" + txtHoBN.Text + "',N'" + txtTenBN.Text + "',N'" + txtNgaySinh.Text + "',N'" + txtDiaChi.Text + "',N'" + txtGioiTinh.Text + "') ";
            using (cn) 
            try
            {
                cn.Open();
                SqlCommand cm = new SqlCommand(sql, cn);
                cm.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Loi Them Du Lieu\n" + ex.ToString());
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            string sql = @"UPDATE BenhNhan SET MaBN='" + txtMaBN.Text + "',Ho='" + txtHoBN.Text + "',Ten='" + txtTenBN.Text + "',DiaChi='" + txtDiaChi.Text + "',Gioitinh='" + txtGioiTinh.Text + "',Ngaysinh='" + txtNgaySinh.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
        }

        private void bt3_Click(object sender, EventArgs e)
        {
                cn.Open();
            string sql = @"DELETE FROM BenhNhan WHERE MaBN='" + txtMaBN.Text + "'";
            SqlCommand cm = new SqlCommand(sql, cn);
            cm.ExecuteNonQuery();
        }

        private void bt5_Click(object sender, EventArgs e)
        {
            this.Close();
            cn.Close();
            MainForm frmForm1 = new MainForm();
            frmForm1.Show();
        }

        private void bt6_Click(object sender, EventArgs e)
        {
            if (cbb.Text == "Mã Bệnh Nhân")
            {
                SqlDataAdapter sda = new SqlDataAdapter(" SELECT * From dbo.BenhNhan WHERE MaBN like '" + txtNhapTim.Text + "'", cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                if (dataGridView1.RowCount > 1)
                {
                    MessageBox.Show("Tìm thấy" + (dataGridView1.RowCount - 1) + "Bệnh Nhân!");

                }
                if (dataGridView1.RowCount == -1)
                {
                    MessageBox.Show("Không Tìm Thấy");
                }
            }
        }

        private void FormName_Click(object sender, EventArgs e)
        {

        }
    }
}
