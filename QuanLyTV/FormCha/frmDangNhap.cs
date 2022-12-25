using QuanLyTV.FormCon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTV.FormCha
{
    public partial class frmDangNhap : Form
    {
        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();
        public frmDangNhap()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {


                string ten = txtUser.Text;
                string matKhau = txtPass.Text;

                var TenDN = QLTV.Accounts.SingleOrDefault(p => p.TenTK == ten);
                var LoaiDocGia = QLTV.Accounts.SingleOrDefault(p => p.TenTK == ten && p.MatKhau == matKhau);

                if (TenDN == null)
                {
                    MessageBox.Show("Tài khoản không tồn tại vui lòng đăng kí tài khoản");
                }
                else if (LoaiDocGia == null)
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                }
                else if (LoaiDocGia != null)
                {
                    MessageBox.Show("Đăng nhập thành công");
                    this.Hide();
                    new frmMain(ten, matKhau).ShowDialog();
                    this.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            this.Hide();
            new frmDangKy().ShowDialog();
            this.Show();
        }

        private void CBSHowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (CBSHowPass.Checked == true)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }
    }
}
