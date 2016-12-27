﻿using System;
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
            bingding();

         
        }

        void bingding()
        {
            txtMaBS.DataBindings.Clear();
            txtMaBS.DataBindings.Add("Text", dataGridView1.DataSource, "MaBS");
            txtHo.DataBindings.Clear();
            txtHo.DataBindings.Add("Text", dataGridView1.DataSource, "Ho");
            txtTen.DataBindings.Clear();
            txtTen.DataBindings.Add("Text", dataGridView1.DataSource, "Ten");
            dtbNgaySinh.DataBindings.Clear();
            dtbNgaySinh.DataBindings.Add("Text", dataGridView1.DataSource, "Ngaysinh");
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", dataGridView1.DataSource, "DiaChi");
            txtSDT.DataBindings.Clear();
            txtSDT.DataBindings.Add("Text", dataGridView1.DataSource, "SDT");

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
            

            try
            {
                using (cn)
            {
                cn.Open();
                //string ngay = dtbNgaySinh.Value.ToShortDateString();
                string sql = @"INSERT INTO BacSi(MaBS,Ho,Ten,NgaySinh,DiaChi,SDT) VALUES(N'" + txtMaBS.Text + "',N'" + txtHo.Text + "',N'" + txtTen.Text + "','" + dtbNgaySinh.Text + "','" + txtSDT.Text + "',N'" + txtDiaChi.Text + "') ";
                SqlCommand cm = new SqlCommand(sql, cn);
                cm.ExecuteNonQuery();

            }
            }
            catch (SqlException ex)
            
            {
                MessageBox.Show("Loi Them Du Lieu\n" + ex.ToString());
                cn.Close();
            }
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            cn.Open();
            string sql = @"DELETE FROM BacSi WHERE MaBS='" + txtMaBS.Text + "'";
            SqlCommand cm = new SqlCommand(sql, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            cn.Open();
            string sql = @"UPDATE BacSi SET MaBS='" + txtMaBS.Text + "',Ho='" + txtHo.Text + "',Ten='" + txtTen.Text + "',DiaChi='" + txtDiaChi.Text + "',SDT='" + txtSDT.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
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
            if (cbb.Text == "Mã Bác Sĩ")
            {
                cn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(" SELECT * From dbo.BacSi WHERE MaBS like '" + txtNhapTim.Text + "'", cn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                if (dataGridView1.RowCount > 1)
                {
                    MessageBox.Show("Tìm thấy" + (dataGridView1.RowCount - 1) + "Bác Sĩ!");

                }
                if (dataGridView1.RowCount == -1)
                {
                    MessageBox.Show("Không Tìm Thấy");
                }
            }
        }

        private void txtNgaySinh_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtbNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
