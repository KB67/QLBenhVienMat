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
            bingding();

            
        }

        void bingding()
        {
            txtMaBN.DataBindings.Clear();
            txtMaBN.DataBindings.Add("Text", dataGridView1.DataSource, "MaBN");
            txtHoBN.DataBindings.Clear();
            txtHoBN.DataBindings.Add("Text", dataGridView1.DataSource, "Ho");
            txtTenBN.DataBindings.Clear();
            txtTenBN.DataBindings.Add("Text", dataGridView1.DataSource, "Ten");
            txtNgaySinh.DataBindings.Clear();
            txtNgaySinh.DataBindings.Add("Text", dataGridView1.DataSource, "NgaySinh");
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", dataGridView1.DataSource, "DiaChi");
            txtGioiTinh.DataBindings.Clear();
            txtGioiTinh.DataBindings.Add("Text", dataGridView1.DataSource, "Gioitinh");

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
            cn.Open();
            string sql = @"UPDATE BenhNhan SET Ho=N'" + txtHoBN.Text + "',Ten=N'" + txtTenBN.Text + "',DiaChi=N'" + txtDiaChi.Text + "',Gioitinh=N'" + txtGioiTinh.Text + "',Ngaysinh='" + txtNgaySinh.Text + "'Where MaBN='" + txtMaBN.Text + "'";
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
            cn.Open();
            if (rdbMaBN.Checked == true)
            {
                string sql = "select * from BenhNhan Where MaBN like '%" + txtNhapTim.Text + "%'";
                SqlDataAdapter sda = new SqlDataAdapter(sql, cn);
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.ExecuteNonQuery();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                MessageBox.Show("Bạn vừa chọn tìm kiếm theo Mã BN ! ");
                return;

            }
            else if (rdbTenBN.Checked == true)
            {
                string sql = "select * from BenhNhan Where Ten like N'%" + txtNhapTim.Text + "%'";
                SqlDataAdapter sda = new SqlDataAdapter(sql, cn);
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.ExecuteNonQuery();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                MessageBox.Show("Bạn vừa chọn tìm kiếm theo Tên Bệnh Nhân ! ");
                return;
            }
        }

        private void FormName_Click(object sender, EventArgs e)
        {

        }

        private void bt4_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }
    }
}
