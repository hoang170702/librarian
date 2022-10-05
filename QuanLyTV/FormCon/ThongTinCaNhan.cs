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
    public partial class ThongTinCaNhan : Form
    {
        QLTVEntities QLTV = new QLTVEntities();
        public ThongTinCaNhan()
        {
            InitializeComponent();
        }
        string tenDN, MK;
        public ThongTinCaNhan(string tenDN, string MK)
        {
            InitializeComponent();
            this.tenDN = tenDN;
            this.MK = MK;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtma.Text = "";
            txtten.Text = "";
            txtMatKhau.Text = "";
            txtMKMoi.Text = "";
            txtNhapLai.Text = "";
        }

        private void btncapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                var tkmk = QLTV.Account.SingleOrDefault(p => p.TenTK == txtten.Text && p.MatKhau == txtMatKhau.Text);
                var parseMA = long.Parse(txtma.Text);
                if (tenDN == txtten.Text && MK == txtMatKhau.Text && tkmk.MaDG == parseMA)
                {
                    long findID = long.Parse(txtma.Text);
                    var CapNhat = QLTV.Account.Find(findID);
                    if (CapNhat != null)
                    {
                        if (txtMKMoi.Text == txtNhapLai.Text && txtMKMoi.Text != "")
                        {
                            CapNhat.MatKhau = txtMKMoi.Text;
                            QLTV.SaveChanges();
                            MessageBox.Show("Cập Nhật Thành Công");
                        }
                        else
                        {
                            MessageBox.Show("Cập Nhật Thất Bại");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tai Khoan Hoac Mat Khau Cu Khong Dung , Vui Long Nhap Lai");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




    }
}
