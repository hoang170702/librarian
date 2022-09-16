using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTV.FormCon
{
    public partial class frmDangKy : Form
    {
        QLTVEntities QLTV = new QLTVEntities();
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            try
            {
                int ma = int.Parse(txtma.Text);
                var checkMaDG = QLTV.DocGia.SingleOrDefault(p => p.MaDG == ma);
                var Account = new Account()
                {
                    TenTK = txtTen.Text,
                    MatKhau = txtMK.Text,
                    MaDG = ma,
                };
                var checkTK = QLTV.Account.SingleOrDefault(p => p.TenTK == txtTen.Text);
                if (checkTK == null)
                {
                    if (checkMaDG != null)
                    {
                        QLTV.Account.Add(Account);
                        QLTV.SaveChanges();
                        MessageBox.Show("Đăng Ký Thành Công!!!");

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
    }
}
