using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

using excel = Microsoft.Office.Interop.Excel;

namespace QuanLyTV.FormCon
{
    public partial class frmPhieuTra : Form
    {
        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();

        public frmPhieuTra()
        {
            InitializeComponent();
        }

        string ten;
        string matKhau;
        string loaiDocGia;
        public frmPhieuTra(string ten, string matKhau, string loaiDocGia)
        {
            InitializeComponent();
            this.ten = ten;
            this.matKhau = matKhau;
            this.loaiDocGia = loaiDocGia;

            if (loaiDocGia == "2")
            {
                paChucNangAdmin.Hide();
            }
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private async void loadTrangThaiPhat()
        {
            var dtnow = DateTime.Today.Date;
            var updateStatus = await QLTV.ChiTietPhieuMuons.Where(p => p.NgayTra < dtnow && p.TrangThai != "Đã Trả").ToArrayAsync();
            if (updateStatus != null)
                foreach (var item in updateStatus)
                {
                    item.TrangThai = "Phạt";
                }
            QLTV.SaveChanges();
        }
        private void loadAdmin()
        {
            var DsCTPM = from ctpm in QLTV.ChiTietPhieuMuons
                         select new
                         {
                             MaPhieuMuon = ctpm.MaPhieuMuon,
                             MaSach = ctpm.Masach,
                             NgayMuon = ctpm.NgayMuon,
                             NgayTra = ctpm.NgayTra,
                             TrangThai = ctpm.TrangThai,
                         };
            dgv.DataSource = DsCTPM.ToList();
            loadTrangThaiPhat();
        }

        private void loadUser()
        {
            var DsCTPM = from ctpm in QLTV.ChiTietPhieuMuons
                         from acc in QLTV.Accounts
                         where acc.TenTK == ten && ctpm.PhieuMuon1.MaDG == acc.MaDG
                         select new
                         {
                             MaPhieuMuon = ctpm.MaPhieuMuon,
                             MaSach = ctpm.Masach,
                             NgayMuon = ctpm.NgayMuon,
                             NgayTra = ctpm.NgayTra,
                             TrangThai = ctpm.TrangThai,
                         };
            dgv.DataSource = DsCTPM.ToList();
        }



        private void AddBindDing()
        {
            txtMaPhieu.DataBindings.Add("Text", dgv.DataSource, "MaPhieuMuon");
            txtMaSach.DataBindings.Add("Text", dgv.DataSource, "MaSach");
            dtbNgaytra.DataBindings.Add("Text", dgv.DataSource, "NgayTra");

        }

        private void clearBinding()
        {
            txtMaPhieu.DataBindings.Clear();
            txtMaSach.DataBindings.Clear();
            dtbNgaytra.DataBindings.Clear();

        }

        private void frmPhieuTra_Load(object sender, EventArgs e)
        {
            try
            {
                if (loaiDocGia == "2")
                {
                    loadUser();
                    paChucNangAdmin.Visible = false;
                    rdbMaDG.Visible = false;
                }
                else
                {
                    loadAdmin();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                AddBindDing();
                clearBinding();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void Huy()
        {
            txtMaPhieu.Text = "";
            txtMaSach.Text = "";
            txtTimKiem.Text = "";
            rdbMaPhieu.Checked = false;
            rdbMaSach.Checked = false;
            rdbNgayTra.Checked = false;
            rdbMaDG.Checked = false;

        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (loaiDocGia == "2")
            {
                Huy();
                loadUser();
            }
            else
            {
                Huy();
                loadAdmin();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTimKiem.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập thông tin muốn tìm kiếm!!!");
                }
                else if (rdbMaPhieu.Checked == false && rdbMaSach.Checked == false && rdbNgayTra.Checked == false && rdbMaDG.Checked == false)
                {
                    MessageBox.Show("Vui lòng chọn trường muốn tìm kiếm!!!");
                }
                else
                {
                    //tim Ma Phieu
                    if (rdbMaPhieu.Checked == true)
                    {
                        var parseMaPhieu = long.Parse(txtTimKiem.Text);
                        var findMaPhieuAdmin = from fAdmin in QLTV.ChiTietPhieuMuons
                                               where fAdmin.MaPhieuMuon == parseMaPhieu
                                               select new
                                               {
                                                   MaPhieu = fAdmin.MaPhieuMuon,
                                                   MaSach = fAdmin.Masach,
                                                   MaDocGia = fAdmin.PhieuMuon1.MaDG,
                                                   NgayTra = fAdmin.NgayTra,
                                                   TrangThai = fAdmin.TrangThai,
                                               };
                        var findMaPhieuUser = from fUser in QLTV.ChiTietPhieuMuons
                                              from acc in QLTV.Accounts
                                              where acc.MaDG == fUser.PhieuMuon1.MaDG && acc.TenTK == ten && fUser.MaPhieuMuon == parseMaPhieu
                                              select new
                                              {
                                                  MaPhieu = fUser.MaPhieuMuon,
                                                  MaSach = fUser.Masach,
                                                  MaDocGia = fUser.PhieuMuon1.MaDG,
                                                  NgayTra = fUser.NgayTra,
                                                  TrangThai = fUser.TrangThai,
                                              };
                        if (loaiDocGia == "2")
                        {
                            dgv.DataSource = findMaPhieuUser.ToList();
                            clearBinding();
                        }
                        else
                        {
                            dgv.DataSource = findMaPhieuAdmin.ToList();
                            clearBinding();
                        }
                    }
                    // Tim Ma Doc Gia
                    else if (rdbMaDG.Checked == true)
                    {
                        var findDGAdmin = from AdminDG in QLTV.ChiTietPhieuMuons
                                          where AdminDG.PhieuMuon1.MaDG == txtTimKiem.Text
                                          select new
                                          {
                                              MaPhieu = AdminDG.MaPhieuMuon,
                                              MaSach = AdminDG.Masach,
                                              MaDocGia = AdminDG.PhieuMuon1.MaDG,
                                              NgayTra = AdminDG.NgayTra,
                                              TrangThai = AdminDG.TrangThai,
                                          };
                        dgv.DataSource = findDGAdmin.ToList();
                        clearBinding();
                    }
                    // Tim Ma Sach 
                    else if (rdbMaSach.Checked == true)
                    {

                        var findMaSachAdmin = from AdminSach in QLTV.ChiTietPhieuMuons
                                              where AdminSach.Masach == txtTimKiem.Text
                                              select new
                                              {
                                                  MaPhieu = AdminSach.MaPhieuMuon,
                                                  MaSach = AdminSach.Masach,
                                                  MaDocGia = AdminSach.PhieuMuon1.MaDG,
                                                  NgayTra = AdminSach.NgayTra,
                                                  TrangThai = AdminSach.TrangThai,
                                              };
                        var findMaSachUser = from UserSach in QLTV.ChiTietPhieuMuons
                                             from acc in QLTV.Accounts
                                             where acc.MaDG == UserSach.PhieuMuon1.MaDG && acc.TenTK == ten && UserSach.Masach == txtTimKiem.Text
                                             select new
                                             {
                                                 MaPhieu = UserSach.MaPhieuMuon,
                                                 MaSach = UserSach.Masach,
                                                 MaDocGia = UserSach.PhieuMuon1.MaDG,
                                                 NgayTra = UserSach.NgayTra,
                                                 TrangThai = UserSach.TrangThai,
                                             };
                        if (loaiDocGia == "2")
                        {
                            dgv.DataSource = findMaSachUser.ToList();
                            clearBinding();
                        }
                        else
                        {
                            dgv.DataSource = findMaSachAdmin.ToList();
                            clearBinding();
                        }
                    }
                    // tim ngay tra
                    else if (rdbNgayTra.Checked == true)
                    {
                        var parseNgayTra = DateTime.Parse(txtTimKiem.Text);
                        var findNgayTraAdmin = from adminNgayTra in QLTV.ChiTietPhieuMuons
                                               where adminNgayTra.NgayTra == parseNgayTra
                                               select new
                                               {
                                                   MaPhieu = adminNgayTra.MaPhieuMuon,
                                                   MaSach = adminNgayTra.Masach,
                                                   MaDocGia = adminNgayTra.PhieuMuon1.MaDG,
                                                   NgayTra = adminNgayTra.NgayTra,
                                                   TrangThai = adminNgayTra.TrangThai,
                                               };
                        var findNgayTraUser = from userNgayTra in QLTV.ChiTietPhieuMuons
                                              from acc in QLTV.Accounts
                                              where acc.MaDG == userNgayTra.PhieuMuon1.MaDG && acc.TenTK == ten && userNgayTra.NgayTra == parseNgayTra
                                              select new
                                              {
                                                  MaPhieu = userNgayTra.MaPhieuMuon,
                                                  MaSach = userNgayTra.Masach,
                                                  MaDocGia = userNgayTra.PhieuMuon1.MaDG,
                                                  NgayTra = userNgayTra.NgayTra,
                                                  TrangThai = userNgayTra.TrangThai,
                                              };
                        if (loaiDocGia == "2")
                        {
                            dgv.DataSource = findNgayTraUser.ToList();
                            clearBinding();
                        }
                        else
                        {
                            dgv.DataSource = findNgayTraAdmin.ToList();
                            clearBinding();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void xuat(string str)
        {
            excel.Application application = new excel.Application();
            application.Application.Workbooks.Add(Type.Missing);
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                application.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    application.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value;
                }
            }
            application.Columns.AutoFit();
            application.ActiveWorkbook.SaveCopyAs(str);
            application.ActiveWorkbook.Saved = true;

        }
        private void btnXuatPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Phiếu Trả";
                saveFileDialog.Filter = "Excel (*xlsx)|*.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        xuat(saveFileDialog.FileName);
                        MessageBox.Show("Xuất phiếu trả thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDaTra_Click(object sender, EventArgs e)
        {
            try
            {
                var parseMaPhieuMuon = long.Parse(txtMaPhieu.Text);
                var Trasach = QLTV.ChiTietPhieuMuons.SingleOrDefault(p =>
                p.MaPhieuMuon == parseMaPhieuMuon && p.Masach == txtMaSach.Text);

                if (Trasach != null)
                {
                    Trasach.TrangThai = "Đã Trả";
                    Trasach.Sach.TrangThaiSach = "Còn";
                    QLTV.SaveChanges();
                    MessageBox.Show("Độc giả " + Trasach.PhieuMuon1.MaDG + " đã trả sách " + Trasach.Masach);
                    loadAdmin();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDangMuon_Click(object sender, EventArgs e)
        {
            try
            {
                var parseMaPhieuMuon = long.Parse(txtMaPhieu.Text);
                var Trasach = QLTV.ChiTietPhieuMuons.SingleOrDefault(p =>
                p.MaPhieuMuon == parseMaPhieuMuon && p.Masach == txtMaSach.Text);

                if (Trasach != null)
                {
                    Trasach.TrangThai = "Đang Mượn";
                    Trasach.Sach.TrangThaiSach = "Không còn";
                    QLTV.SaveChanges();
                    MessageBox.Show("Độc giả " + Trasach.PhieuMuon1.MaDG + " đang muợn sách " + Trasach.Masach);
                    loadAdmin();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void load()
        private void btnPhat_Click(object sender, EventArgs e)
        {
            try
            {
                var parseMaPhieuMuon = long.Parse(txtMaPhieu.Text);

                var Trasach = QLTV.ChiTietPhieuMuons.SingleOrDefault(p =>
                p.MaPhieuMuon == parseMaPhieuMuon && p.Masach == txtMaSach.Text);

                if (Trasach != null)
                {
                    Trasach.TrangThai = "Phạt";
                    QLTV.SaveChanges();
                    MessageBox.Show("Độc giả " + Trasach.PhieuMuon1.MaDG + " đang muợn sách " + Trasach.Masach + " đã bị phạt");
                    loadAdmin();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnphieuPhat_Click(object sender, EventArgs e)
        {
            new frmPhieuPhat().ShowDialog();
        }
    }
}
