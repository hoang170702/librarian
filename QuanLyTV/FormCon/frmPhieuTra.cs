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
    public partial class frmPhieuTra : Form
    {
        QLTVEntities QLTV = new QLTVEntities();
        string tenDN;
        public frmPhieuTra()
        {
            InitializeComponent();
        }

        public frmPhieuTra(string tenDN)
        {
            InitializeComponent();
            this.tenDN = tenDN;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void loadAdmin()
        {
            var ListPhieuTra = from phieutra in QLTV.ChiTietPhieuMuon
                               select new
                               {
                                   MaPhieu = phieutra.MaPhieuMuon,
                                   MaSach = phieutra.Masach,
                                   MaDocGia = phieutra.PhieuMuon.MaDG,
                                   NgayTra = phieutra.NgayTra,
                               };
            dgv.DataSource = ListPhieuTra.ToList();
        }

        private void loadUser()
        {
            var ListPhieuTra = from phieutra in QLTV.ChiTietPhieuMuon
                               from acc in QLTV.Account
                               where acc.MaDG == phieutra.PhieuMuon.MaDG && acc.TenTK == tenDN
                               select new
                               {
                                   MaPhieu = phieutra.MaPhieuMuon,
                                   MaSach = phieutra.Masach,
                                   MaDocGia = phieutra.PhieuMuon.MaDG,
                                   NgayTra = phieutra.NgayTra,
                               };
            dgv.DataSource = ListPhieuTra.ToList();
        }

        private void getsizeColumns()
        {
            dgv.Columns["MaPhieu"].Width = 60;
            dgv.Columns["MaSach"].Width = 60;
            dgv.Columns["MaDocGia"].Width = 60;
            dgv.Columns["NgayTra"].Width = 120;
        }

        private void AddBindDing()
        {
            txtMaPhieu.DataBindings.Add("Text", dgv.DataSource, "MaPhieu");
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
                if (tenDN != "admin")
                {
                    loadUser();
                    getsizeColumns();
                    paChucNangAdmin.Visible = false;
                    rdbMaDG.Visible = false;
                }
                else
                {
                    loadAdmin();
                    getsizeColumns();
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var parseMaPhieu = long.Parse(txtMaPhieu.Text);
                var parseMaSach = long.Parse(txtMaSach.Text);
                var checkMaPhieuMuon = QLTV.ChiTietPhieuMuon.SingleOrDefault(p => p.MaPhieuMuon == parseMaPhieu);
                var checkMaSach = QLTV.ChiTietPhieuMuon.SingleOrDefault(p => p.Masach == parseMaSach);
                if (txtMaPhieu.Text == "" || txtMaSach.Text == "" || dtbNgaytra.Text == "")
                {
                    throw new Exception("Vui lòng nhập đầy đủ thông tin");
                }
                else if (checkMaPhieuMuon != null && checkMaSach != null)
                {
                    throw new Exception("Phiếu này đã được trả!!!");
                }
                else
                {
                    var AddPhieuTra = new ChiTietPhieuMuon()
                    {
                        MaPhieuMuon = parseMaPhieu,
                        Masach = parseMaSach,
                        NgayTra = dtbNgaytra.Value.Date
                    };
                    QLTV.ChiTietPhieuMuon.Add(AddPhieuTra);
                    QLTV.SaveChanges();
                    MessageBox.Show("Thêm Phiếu Trả Thành Công!!!");
                    loadAdmin();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var parseMaPhieu = long.Parse(txtMaPhieu.Text);
                var parseMaSach = long.Parse(txtMaSach.Text);
                var checkMaPhieuMuon = QLTV.ChiTietPhieuMuon.SingleOrDefault(p => p.MaPhieuMuon == parseMaPhieu);
                if (checkMaPhieuMuon != null)
                {
                    QLTV.ChiTietPhieuMuon.Remove(checkMaPhieuMuon);
                    QLTV.SaveChanges();
                    MessageBox.Show("Xóa thành công!!!");
                    loadAdmin();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                var parseMaPhieu = long.Parse(txtMaPhieu.Text);
                var parseMaSach = long.Parse(txtMaSach.Text);
                var checkMaPhieuMuon = QLTV.ChiTietPhieuMuon.SingleOrDefault(p => p.MaPhieuMuon == parseMaPhieu);
                if (checkMaPhieuMuon != null)
                {
                    checkMaPhieuMuon.Masach = parseMaSach;
                    checkMaPhieuMuon.NgayTra = dtbNgaytra.Value.Date;
                    QLTV.SaveChanges();
                    MessageBox.Show("Cập nhật phiếu trả thành công");
                    loadAdmin();
                }
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
            if (tenDN != "admin")
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
                        var findMaPhieuAdmin = from fAdmin in QLTV.ChiTietPhieuMuon
                                               where fAdmin.MaPhieuMuon == parseMaPhieu
                                               select new
                                               {
                                                   MaPhieu = fAdmin.MaPhieuMuon,
                                                   MaSach = fAdmin.Masach,
                                                   MaDocGia = fAdmin.PhieuMuon.MaDG,
                                                   NgayTra = fAdmin.NgayTra,
                                               };
                        var findMaPhieuUser = from fUser in QLTV.ChiTietPhieuMuon
                                              from acc in QLTV.Account
                                              where acc.MaDG == fUser.PhieuMuon.MaDG && acc.TenTK == tenDN && fUser.MaPhieuMuon == parseMaPhieu
                                              select new
                                              {
                                                  MaPhieu = fUser.MaPhieuMuon,
                                                  MaSach = fUser.Masach,
                                                  MaDocGia = fUser.PhieuMuon.MaDG,
                                                  NgayTra = fUser.NgayTra,
                                              };
                        if (tenDN != "admin")
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
                        var parseMaDG = long.Parse(txtTimKiem.Text);
                        var findDGAdmin = from AdminDG in QLTV.ChiTietPhieuMuon
                                          where AdminDG.PhieuMuon.MaDG == parseMaDG
                                          select new
                                          {
                                              MaPhieu = AdminDG.MaPhieuMuon,
                                              MaSach = AdminDG.Masach,
                                              MaDocGia = AdminDG.PhieuMuon.MaDG,
                                              NgayTra = AdminDG.NgayTra,
                                          };
                        dgv.DataSource = findDGAdmin.ToList();
                        clearBinding();
                    }
                    // Tim Ma Sach 
                    else if (rdbMaSach.Checked == true)
                    {
                        var parseMaSach = long.Parse(txtTimKiem.Text);
                        var findMaSachAdmin = from AdminSach in QLTV.ChiTietPhieuMuon
                                              where AdminSach.Masach == parseMaSach
                                              select new
                                              {
                                                  MaPhieu = AdminSach.MaPhieuMuon,
                                                  MaSach = AdminSach.Masach,
                                                  MaDocGia = AdminSach.PhieuMuon.MaDG,
                                                  NgayTra = AdminSach.NgayTra,
                                              };
                        var findMaSachUser = from UserSach in QLTV.ChiTietPhieuMuon
                                             from acc in QLTV.Account
                                             where acc.MaDG == UserSach.PhieuMuon.MaDG && acc.TenTK == tenDN && UserSach.Masach == parseMaSach
                                             select new
                                             {
                                                 MaPhieu = UserSach.MaPhieuMuon,
                                                 MaSach = UserSach.Masach,
                                                 MaDocGia = UserSach.PhieuMuon.MaDG,
                                                 NgayTra = UserSach.NgayTra,
                                             };
                        if (tenDN != "admin")
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
                        var findNgayTraAdmin = from adminNgayTra in QLTV.ChiTietPhieuMuon
                                               where adminNgayTra.NgayTra == parseNgayTra
                                               select new
                                               {
                                                   MaPhieu = adminNgayTra.MaPhieuMuon,
                                                   MaSach = adminNgayTra.Masach,
                                                   MaDocGia = adminNgayTra.PhieuMuon.MaDG,
                                                   NgayTra = adminNgayTra.NgayTra,
                                               };
                        var findNgayTraUser = from userNgayTra in QLTV.ChiTietPhieuMuon
                                              from acc in QLTV.Account
                                              where acc.MaDG == userNgayTra.PhieuMuon.MaDG && acc.TenTK == tenDN && userNgayTra.NgayTra == parseNgayTra
                                              select new
                                              {
                                                  MaPhieu = userNgayTra.MaPhieuMuon,
                                                  MaSach = userNgayTra.Masach,
                                                  MaDocGia = userNgayTra.PhieuMuon.MaDG,
                                                  NgayTra = userNgayTra.NgayTra,
                                              };
                        if (tenDN != "admin")
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
    }
}
