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
    public partial class frmMain : Form
    {
        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();
        public frmMain()
        {
            InitializeComponent();
        }

        string ten;
        string matKhau;
        string loaidocgia;

        public frmMain(string ten, string matKhau)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ten = ten;
            this.matKhau = matKhau;


            var checkLoaidocgia = QLTV.Accounts.SingleOrDefault(p => p.TenTK == ten && p.MatKhau == matKhau);
            this.loaidocgia = checkLoaidocgia.DocGia.MaLoaiDocGia;

            if (loaidocgia == "1")
            {
                pnNhanVien.Hide();
                thốngKêLươngToolStripMenuItem.Visible = false;
                tácVụKhácToolStripMenuItem.Visible = false;
            }
            else if (loaidocgia == "2")
            {
                pnNhanVien.Hide();
                PnDocGia.Hide();
                thốngKêToolStripMenuItem.Visible = false;
                tácVụKhácToolStripMenuItem.Visible = false;
            }

            getThongtinKhachHang();

        }

        private void getThongtinKhachHang()
        {
            var getMaKH = QLTV.Accounts.SingleOrDefault(p => p.TenTK == ten && p.MatKhau == matKhau);
            var ThongTinKH = QLTV.DocGias.SingleOrDefault(p => p.MaDG == getMaKH.MaDG);
            lblHello.Text = ThongTinKH.TenDG;
        }

        private Form currentFormChild;
        private void openChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            paBody.Controls.Add(childForm);
            paBody.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnSach_Click(object sender, EventArgs e)
        {
            lblTieude.Text = "Quản lý sách";
            openChildForm(new frmSach());
        }

        private void btnPhieuMuon_Click(object sender, EventArgs e)
        {
            lblTieude.Text = "Quản lý phiếu mượn";
            openChildForm(new frmPhieuMuon(ten, matKhau, loaidocgia));
        }

        private void btnPhieuTra_Click(object sender, EventArgs e)
        {
            lblTieude.Text = "Quản lý phiếu trả";
            openChildForm(new frmPhieuTra(ten, matKhau, loaidocgia));
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            lblTieude.Text = "Quản lý nhân viên";
            openChildForm(new frmNhanVien());
        }

        private void btnDocGia_Click(object sender, EventArgs e)
        {
            lblTieude.Text = "Quản lý độc giả";
            openChildForm(new frmDocGia());
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            var thongbao = MessageBox.Show("Bạn có muốn đăng xuất ?", "Thông báo", MessageBoxButtons.YesNo);
            if (thongbao == DialogResult.Yes)
            {
                this.Close();
            }

        }

        private void thốngKêDoanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmThongKeDoanhThu().ShowDialog();
        }

        private void thốngKêLươngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmThongKeLuong().ShowDialog();
        }

        private void thôngTinTàiKhoảnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmThongTinCaNhan(ten, matKhau, loaidocgia).ShowDialog();
        }

        private void thayĐổiQuyĐịnhPhạtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmQuyDinhPhat().ShowDialog();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ThongTinCaNhan(ten, matKhau).ShowDialog();
        }
    }
}
