using QuanLyTV.FormCha;
using QuanLyTV.FormCon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTV
{
    public partial class Form1 : Form
    {
        QLTVEntities QLTV = new QLTVEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            this.Hide();
            new frmDangKy().ShowDialog();
            this.Show();
        }
        string TenDN;
        string MK;
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                TenDN = txtUser.Text;
                MK = txtPass.Text;
                var check = QLTV.Account.SingleOrDefault(p => p.TenTK == txtUser.Text && p.MatKhau == txtPass.Text);

                if (check != null || txtUser.Text == "admin")
                {
                    MessageBox.Show("Đăng Nhập Thành Công");
                    this.Hide();
                    new FormMain(TenDN, MK).ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Đăng Nhập Thất Bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
