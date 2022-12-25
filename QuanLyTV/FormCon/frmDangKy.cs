
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTV.FormCon
{
    public partial class frmDangKy : Form
    {
        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();

        public frmDangKy()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            try
            {
                var checkMaDG = QLTV.DocGias.SingleOrDefault(p => p.MaDG == txtma.Text);
                var checkTK = QLTV.Accounts.SingleOrDefault(p => p.TenTK == txtTen.Text);
                var expectedPasswordPattern = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
                if (checkTK == null)
                {
                    if (checkMaDG != null)
                    {
                        if (txtMK.Text.Length < 6 || txtMK.Text.Length > 18)
                        {
                            MessageBox.Show("Mật khẩu phải từ 6 - 18 kí tự!!!");
                        }
                        else if (expectedPasswordPattern.IsMatch(txtMK.Text) == false)
                        {
                            MessageBox.Show("Mật khẩu phải có chữ thường,chữ hoa, kí tự đặc biệt, số!!!");
                        }
                        else
                        {
                            var Account = new Account()
                            {
                                TenTK = txtTen.Text,
                                MatKhau = txtMK.Text,
                                MaDG = txtma.Text,
                            };
                            QLTV.Accounts.Add(Account);
                            QLTV.SaveChanges();
                            MessageBox.Show("Đăng Ký Thành Công!!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có mã độc giả này!!!");
                    }
                }
                else
                {
                    throw new Exception("Trung Ten Tai Khoan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtma.Text = "";
            txtMK.Text = "";
            txtTen.Text = "";
        }
    }
}
