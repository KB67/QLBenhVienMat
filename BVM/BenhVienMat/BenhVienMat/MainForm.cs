using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BenhVienMat
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frmForm1 = new Form2();
            frmForm1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frmForm1 = new Form1();
            frmForm1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HoSoBenhNhan frmForm1 = new HoSoBenhNhan();
            frmForm1.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
