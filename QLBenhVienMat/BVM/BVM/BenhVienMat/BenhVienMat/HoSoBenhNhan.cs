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
using System.Globalization;

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
            bingding();

        }

        void bingding()
        {
            txtMaHSBN.DataBindings.Clear();
            txtMaHSBN.DataBindings.Add("Text", dataGridView1.DataSource, "MaHSBN");
            txtMaBN.DataBindings.Clear();
            txtMaBN.DataBindings.Add("Text", dataGridView1.DataSource, "MaBN");
            txtTieuSuBenhLy.DataBindings.Clear();
            txtTieuSuBenhLy.DataBindings.Add("Text", dataGridView1.DataSource, "TieusuBenhLy");
            dtbNgayKham.DataBindings.Clear();
            dtbNgayKham.DataBindings.Add("Text", dataGridView1.DataSource, "NgayKham");
            
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
            this.Close();
            cn.Close();
            MainForm frmForm1 = new MainForm();
            frmForm1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //using (cn)
                {
                    cn.Open();
                    string sql = @"INSERT INTO HoSoBenhNhan(MaHSBN,MaBN,NgayKham,TieuSuBenhLy) VALUES(N'" + txtMaHSBN.Text + "',N'" + txtMaBN.Text + "','" + dtbNgayKham.Text + "',N'" + txtTieuSuBenhLy.Text + "') ";
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

        private void button4_Click(object sender, EventArgs e)
        {
            cn.Open();
            string sql = @"DELETE FROM HoSoBenhNhan WHERE MaHSBN='" + txtMaHSBN.Text + "'";
            SqlCommand cm = new SqlCommand(sql, cn);
            cm.ExecuteNonQuery();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cn.Open();
            string sql = @"UPDATE HoSoBenhNhan SET MaBN='" + txtMaBN.Text + "',TieusuBenhLy=N'" + txtTieuSuBenhLy.Text + "' where MaHSBN='" + txtMaHSBN.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.ExecuteNonQuery();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            HoSoBenhNhan_Load(sender, e);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
                cn.Open();
                if (rdbMaHSBN.Checked == true)
                {
                    string sql = "select * from HoSoBenhNhan Where MaHSBN like '%" + txtNhapTim.Text + "%'";
                    SqlDataAdapter sda = new SqlDataAdapter(sql, cn);
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.ExecuteNonQuery();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                    MessageBox.Show("Bạn vừa chọn tìm kiếm theo Mã HSBN ! ");
                    return;

                }
                else if (rdbMaBN.Checked == true)
                {
                    string sql = "select * from HoSoBenhNhan Where MaBN like '%" + txtNhapTim.Text + "%'";
                    SqlDataAdapter sda = new SqlDataAdapter(sql, cn);
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.ExecuteNonQuery();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                    MessageBox.Show("Bạn vừa chọn tìm kiếm theo Mã BN ! ");
                    return;
                }
                //string sql = "select * from HoSoBenhNhan Where MaHSBN like '%" + txtNhapTim.Text + "%'";
                //SqlDataAdapter sda = new SqlDataAdapter(sql, cn);
                //DataTable dt = new DataTable();
                //SqlCommand cmd = new SqlCommand(sql, cn);
                //cmd.ExecuteNonQuery();
                //sda.Fill(dt);
                
                //dataGridView1.DataSource = dt;
                //cn.Close();
        }
    }
}
