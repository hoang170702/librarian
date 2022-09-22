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
    public partial class frmPhieuMuon : Form
    {
        QLTVEntities QLTV = new QLTVEntities();
        public frmPhieuMuon()
        {
            InitializeComponent();
        }
        string tenDN;
        public frmPhieuMuon(string tenDN)
        {
            InitializeComponent();
            this.tenDN = tenDN;
            txtMaPhieu.ReadOnly = true;
        }

        private void loadAdmin()
        {
            var ListPhieuMuon = from pt in QLTV.PhieuMuon
                                select new
                                {
                                    MaPhieuMuon = pt.MaPhieuMuon,
                                    MaDG = pt.MaDG,
                                    MaNV = pt.MaNV,
                                    NgayMuon = pt.NgayMuon,
                                };
            dgv.DataSource = ListPhieuMuon.ToList();
        }

        private void loadUser()
        {
            var ListPhieuTra = from pt in QLTV.PhieuMuon
                               from acc in QLTV.Account
                               where acc.TenTK == tenDN && pt.MaDG == acc.MaDG
                               select new
                               {
                                   MaPhieuMuon = pt.MaPhieuMuon,
                                   MaDG = pt.MaDG,
                                   MaNV = pt.MaNV,
                                   NgayMuon = pt.NgayMuon,
                               };
            dgv.DataSource = ListPhieuTra.ToList();
            grbChucNang.Visible = false;
        }
        private void getSizeColumns()
        {
            dgv.Columns["MaPhieuMuon"].Width = 50;
            dgv.Columns["MaDG"].Width = 50;
            dgv.Columns["MaNV"].Width = 50;
            dgv.Columns["NgayMuon"].Width = 150;
        }

        private void AddBinding()
        {
            txtMaPhieu.DataBindings.Add("Text", dgv.DataSource, "MaPhieuMuon");
            txtMaDocGia.DataBindings.Add("Text", dgv.DataSource, "MaDG");
            txtMaNhanVien.DataBindings.Add("Text", dgv.DataSource, "MaNV");
            date.DataBindings.Add("Text", dgv.DataSource, "NgayMuon");
        }

        private void ClearBinhding()
        {
            txtMaPhieu.DataBindings.Clear();
            txtMaDocGia.DataBindings.Clear();
            txtMaNhanVien.DataBindings.Clear();
            date.DataBindings.Clear();
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                AddBinding();
                ClearBinhding();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmPhieuMuon_Load(object sender, EventArgs e)
        {
            try
            {
                if (tenDN != "admin")
                {
                    loadUser();
                    getSizeColumns();
                }
                else
                {
                    loadAdmin();
                    getSizeColumns();
                }

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

                var parseMaDG = long.Parse(txtMaDocGia.Text);
                var parseMaNV = long.Parse(txtMaNhanVien.Text);
                var CheckMaDG = QLTV.DocGia.Where(p => p.MaDG == parseMaDG);
                var checkMaNV = QLTV.NhanVien.Where(p => p.MaNV == parseMaNV);
                if (CheckMaDG == null)
                {
                    throw new Exception("Không có mã độc giả này!!!");
                }
                else if (checkMaNV == null)
                {
                    throw new Exception("Không có mã nhân viên này!!!");
                }
                else
                {
                    var Phieu = new PhieuMuon()
                    {
                        MaDG = parseMaDG,
                        MaNV = parseMaNV,
                        NgayMuon = date.Value.Date,
                    };
                    QLTV.PhieuMuon.Add(Phieu);
                    QLTV.SaveChanges();
                    MessageBox.Show("Thêm phiếu mượn thành công!!!");
                    loadAdmin();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                if (tenDN != "admin")
                {
                    txtMaPhieu.Text = "";
                    txtMaDocGia.Text = "";
                    txtMaNhanVien.Text = "";
                    txtTimKiem.Text = "";
                    date.Text = DateTime.Now.ToString();
                    rdbMaDocGia.Checked = false;
                    rdbMaNhanVien.Checked = false;
                    rdbMaPhieu.Checked = false;
                    rdbNgayMuon.Checked = false;
                    loadUser();
                    getSizeColumns();
                }
                else
                {
                    txtMaPhieu.Text = "";
                    txtMaDocGia.Text = "";
                    txtMaNhanVien.Text = "";
                    txtTimKiem.Text = "";
                    date.Text = DateTime.Now.ToString();
                    rdbMaDocGia.Checked = false;
                    rdbMaNhanVien.Checked = false;
                    rdbMaPhieu.Checked = false;
                    rdbNgayMuon.Checked = false;
                    loadAdmin();
                    getSizeColumns();
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
                if (txtMaPhieu.Text == "")
                {
                    throw new Exception("Không có mã phiếu này!!!");
                }
                else
                {
                    var Notification = MessageBox.Show("Bạn có chắc muốn sửa ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (Notification == DialogResult.Yes)
                    {
                        var parseMaPhieu = long.Parse(txtMaPhieu.Text);
                        var parseMaDG = long.Parse(txtMaDocGia.Text);
                        var parseMaNV = long.Parse(txtMaNhanVien.Text);
                        var checkMaPhieu = QLTV.PhieuMuon.SingleOrDefault(p => p.MaPhieuMuon == parseMaPhieu);
                        if (checkMaPhieu != null)
                        {
                            checkMaPhieu.MaDG = parseMaDG;
                            checkMaPhieu.MaNV = parseMaNV;
                            checkMaPhieu.NgayMuon = date.Value.Date;
                            QLTV.SaveChanges();
                            MessageBox.Show("Cập nhật phiếu mượn thành công!!!");
                            loadAdmin();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật phiếu mượn thất bại!!!");
                        }
                    }
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
                if (txtMaPhieu.Text == "")
                {
                    throw new Exception("Không có phiếu mượn này");
                }
                else
                {
                    var Notification = MessageBox.Show("Bạn có chắc muốn Xóa ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (Notification == DialogResult.Yes)
                    {
                        var parseMaPhieu = long.Parse(txtMaPhieu.Text);
                        var checkMaPhieu = QLTV.PhieuMuon.SingleOrDefault(p => p.MaPhieuMuon == parseMaPhieu);
                        if (checkMaPhieu != null)
                        {
                            QLTV.PhieuMuon.Remove(checkMaPhieu);
                            QLTV.SaveChanges();
                            MessageBox.Show("Xóa thành công!!!");
                            loadAdmin();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại!!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTimKiem.Text == "")
                {
                    throw new Exception("Vui lòng nhập phiếu muốn tìm kiếm!!!");
                }
                else if (rdbMaDocGia.Checked == false && rdbMaNhanVien.Checked == false && rdbMaPhieu.Checked == false && rdbNgayMuon.Checked == false)
                {
                    throw new Exception("Vui lòng chọn trường muốn tìm kiếm!!!");
                }
                else
                {
                    //tim Ma phieu
                    if (rdbMaPhieu.Checked == true)
                    {
                        var parseMaPhieu = long.Parse(txtTimKiem.Text);
                        var findMaPhieuAdmin = from MaPhieu in QLTV.PhieuMuon
                                               where MaPhieu.MaPhieuMuon == parseMaPhieu
                                               select new
                                               {
                                                   MaPhieuMuon = MaPhieu.MaPhieuMuon,
                                                   MaDG = MaPhieu.MaDG,
                                                   MaNV = MaPhieu.MaNV,
                                                   NgayMuon = MaPhieu.NgayMuon,
                                               };
                        var findMaPhieuUser = from pt in QLTV.PhieuMuon
                                              from acc in QLTV.Account
                                              where acc.TenTK == tenDN && pt.MaDG == acc.MaDG && pt.MaPhieuMuon == parseMaPhieu
                                              select new
                                              {
                                                  MaPhieuMuon = pt.MaPhieuMuon,
                                                  MaDG = pt.MaDG,
                                                  MaNV = pt.MaNV,
                                                  NgayMuon = pt.NgayMuon,
                                              };
                        if (tenDN != "admin")
                        {
                            dgv.DataSource = findMaPhieuUser.ToList();
                            ClearBinhding();
                        }
                        else
                        {
                            dgv.DataSource = findMaPhieuAdmin.ToList();
                            ClearBinhding();
                        }



                    }
                    //Tim Ma Doc Gia
                    else if (rdbMaDocGia.Checked == true)
                    {
                        var parseMaDocGia = long.Parse(txtTimKiem.Text);
                        var findMaMaDocGia = from MaDocGia in QLTV.PhieuMuon
                                             where MaDocGia.MaDG == parseMaDocGia
                                             select new
                                             {
                                                 MaPhieuMuon = MaDocGia.MaPhieuMuon,
                                                 MaDG = MaDocGia.MaDG,
                                                 MaNV = MaDocGia.MaNV,
                                                 NgayMuon = MaDocGia.NgayMuon,
                                             };
                        var findMaUser = from dg in QLTV.PhieuMuon
                                         from acc in QLTV.Account
                                         where acc.TenTK == tenDN && dg.MaDG == acc.MaDG && dg.MaDG == parseMaDocGia
                                         select new
                                         {
                                             MaPhieuMuon = dg.MaPhieuMuon,
                                             MaDG = dg.MaDG,
                                             MaNV = dg.MaNV,
                                             NgayMuon = dg.NgayMuon,
                                         };
                        if (tenDN != "admin")
                        {
                            dgv.DataSource = findMaUser.ToList();
                            ClearBinhding();
                        }
                        else
                        {
                            dgv.DataSource = findMaMaDocGia.ToList();
                            ClearBinhding();
                        }
                    }
                    //Tim Ma Nhan Vien
                    else if (rdbMaNhanVien.Checked == true)
                    {
                        var parseMaNhanVien = long.Parse(txtTimKiem.Text);
                        var findNhanVien = from MaNhanVien in QLTV.PhieuMuon
                                           where MaNhanVien.MaNV == parseMaNhanVien
                                           select new
                                           {
                                               MaPhieuMuon = MaNhanVien.MaPhieuMuon,
                                               MaDG = MaNhanVien.MaDG,
                                               MaNV = MaNhanVien.MaNV,
                                               NgayMuon = MaNhanVien.NgayMuon,
                                           };
                        var findMaNVUser = from nv in QLTV.PhieuMuon
                                           from acc in QLTV.Account
                                           where acc.TenTK == tenDN && nv.MaDG == acc.MaDG && nv.MaNV == parseMaNhanVien
                                           select new
                                           {
                                               MaPhieuMuon = nv.MaPhieuMuon,
                                               MaDG = nv.MaDG,
                                               MaNV = nv.MaNV,
                                               NgayMuon = nv.NgayMuon,
                                           };
                        if (tenDN != "admin")
                        {
                            dgv.DataSource = findMaNVUser.ToList();
                            ClearBinhding();
                        }
                        else
                        {
                            dgv.DataSource = findNhanVien.ToList();
                            ClearBinhding();
                        }
                    }
                    //Tim Ngay
                    else if (rdbNgayMuon.Checked == true)
                    {
                        var parseDate = DateTime.Parse(txtTimKiem.Text);
                        var findDate = from ngay in QLTV.PhieuMuon
                                       where ngay.NgayMuon == parseDate
                                       select new
                                       {
                                           MaPhieuMuon = ngay.MaPhieuMuon,
                                           MaDG = ngay.MaDG,
                                           MaNV = ngay.MaNV,
                                           NgayMuon = ngay.NgayMuon,
                                       };
                        var findDateUser = from dateUser in QLTV.PhieuMuon
                                           from acc in QLTV.Account
                                           where acc.TenTK == tenDN && dateUser.MaDG == acc.MaDG && dateUser.NgayMuon == parseDate
                                           select new
                                           {
                                               MaPhieuMuon = dateUser.MaPhieuMuon,
                                               MaDG = dateUser.MaDG,
                                               MaNV = dateUser.MaNV,
                                               NgayMuon = dateUser.NgayMuon,
                                           };
                        if (tenDN != "admin")
                        {
                            dgv.DataSource = findDateUser.ToList();
                            ClearBinhding();
                        }
                        else
                        {
                            dgv.DataSource = findDate.ToList();
                            ClearBinhding();
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
