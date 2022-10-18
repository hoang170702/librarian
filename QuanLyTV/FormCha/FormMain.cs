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

namespace QuanLyTV.FormCha
{
    public partial class FormMain : Form
    {
        QuanLyThuVienEntities QLTV = new QuanLyThuVienEntities();
        public FormMain()
        {
            InitializeComponent();
        }
        string TenDN;
        string MK;
        public FormMain(string TenDN, string mK)
        {
            InitializeComponent();
            this.TenDN = TenDN;
            MK = mK;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ThongTinCaNhan(TenDN, MK).ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                lblHello.Text = TenDN;
                if (TenDN != "admin")
                {
                    PnDocGia.Hide();
                    pnNhanVien.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Form CurrentForm;
        private void childForm(Form ChildForm)
        {
            if (CurrentForm != null)
            {
                CurrentForm.Close();
            }
            CurrentForm = ChildForm;
            ChildForm.TopLevel = false;
            ChildForm.FormBorderStyle = FormBorderStyle.None;
            ChildForm.Dock = DockStyle.Fill;
            paBody.Controls.Add(ChildForm);
            paBody.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lblTieude.Text = "Sách";
                childForm(new frmSach());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPhieuMuon_Click(object sender, EventArgs e)
        {
            try
            {
                lblTieude.Text = "Phiếu Mượn";
                childForm(new frmPhieuMuon(TenDN));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPhieuTra_Click(object sender, EventArgs e)
        {
            try
            {
                lblTieude.Text = "Phiếu Trả";
                childForm(new frmPhieuTra(TenDN));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                lblTieude.Text = "Nhân Viên";
                childForm(new frmNhanVien());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDocGia_Click(object sender, EventArgs e)
        {
            try
            {
                lblTieude.Text = "Độc Giả";
                childForm(new frmDocGia());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentForm != null)
                {
                    CurrentForm.Close();
                }
                lblTieude.Text = "Home";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            var notification = MessageBox.Show("Bạn có chắc muốn đăng xuất", "Thông Báo", MessageBoxButtons.YesNo);
            if (notification == DialogResult.Yes)
            {
                this.Close();
            }

        }
    }
}
