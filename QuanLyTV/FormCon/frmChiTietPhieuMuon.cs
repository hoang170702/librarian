
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
    public partial class frmChiTietPhieuMuon : Form
    {
        QuanLyThuVienEntities QLTV = new QuanLyThuVienEntities();
        public frmChiTietPhieuMuon()
        {
            InitializeComponent();
        }
        string tenDN; long MaPhieuMuon;
        public frmChiTietPhieuMuon(string tenDN, long MaPhieuMuon)
        {
            InitializeComponent();
            this.tenDN = tenDN;
            this.MaPhieuMuon = MaPhieuMuon;
            txtMaPhieuMuon.Text = MaPhieuMuon.ToString();
            txtMaPhieuMuon.ReadOnly = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void loadAdmin()
        {
            var DsCTPM = from ctpm in QLTV.ChiTietPhieuMuons
                         where ctpm.MaPhieuMuon == MaPhieuMuon

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
        private void loadUser()
        {
            var DsCTPM = from ctpm in QLTV.ChiTietPhieuMuons
                         where ctpm.MaPhieuMuon == MaPhieuMuon
                         select new
                         {
                             MaPhieuMuon = ctpm.MaPhieuMuon,
                             MaSach = ctpm.Masach,
                             NgayMuon = ctpm.NgayMuon,
                             NgayTra = ctpm.NgayTra,
                             TrangThai = ctpm.TrangThai,
                         };
            dgv.DataSource = DsCTPM.ToList();
            paChucnang.Hide();
        }
        private void frmChiTietPhieuMuon_Load(object sender, EventArgs e)
        {
            try
            {
                if (tenDN != "admin")
                {
                    loadUser();
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

        private void AddBinding()
        {
            txtMaPhieuMuon.DataBindings.Add("Text", dgv.DataSource, "MaPhieuMuon");
            txtMaSach.DataBindings.Add("Text", dgv.DataSource, "MaSach");
            dtpNgayMuon.DataBindings.Add("Text", dgv.DataSource, "NgayMuon");
            dtpNgayTra.DataBindings.Add("Text", dgv.DataSource, "NgayTra");
        }

        private void ClearBinding()
        {
            txtMaPhieuMuon.DataBindings.Clear();
            txtMaSach.DataBindings.Clear();
            dtpNgayMuon.DataBindings.Clear();
            dtpNgayTra.DataBindings.Clear();
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                AddBinding();
                ClearBinding();
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
                var parseMaSach = long.Parse(txtMaSach.Text);
                if (dtpNgayMuon.Value.Date >= dtpNgayTra.Value.Date)
                {
                    MessageBox.Show("Ngày mượn không thể nhỏ hơn ngày trả");
                }
                else
                {
                    var AddCTPM = new ChiTietPhieuMuon()
                    {
                        MaPhieuMuon = MaPhieuMuon,
                        Masach = parseMaSach,
                        NgayMuon = dtpNgayMuon.Value.Date,
                        NgayTra = dtpNgayTra.Value.Date,
                        TrangThai = "Đang Mượn"
                    };
                    QLTV.ChiTietPhieuMuons.Add(AddCTPM);
                    QLTV.SaveChanges();
                    MessageBox.Show("Thêm Sách vào phiếu mượn " + MaPhieuMuon.ToString() + " thành công");
                    loadAdmin();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                var thongbao = MessageBox.Show("Bạn có muốn Sửa", "Thong Bao", MessageBoxButtons.YesNo);
                if (thongbao == DialogResult.Yes)
                {
                    var parseMaSach = long.Parse(txtMaSach.Text);
                    var findID = QLTV.ChiTietPhieuMuons.SingleOrDefault(p => p.MaPhieuMuon == MaPhieuMuon && p.Masach == parseMaSach);
                    if (findID != null)
                    {
                        findID.NgayMuon = dtpNgayMuon.Value.Date;
                        findID.NgayTra = dtpNgayTra.Value.Date;
                    }
                    QLTV.SaveChanges();
                    MessageBox.Show("Sửa thành công");
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
                var thongbao = MessageBox.Show("Bạn có muốn xóa", "Thong Bao", MessageBoxButtons.YesNo);
                if (thongbao == DialogResult.Yes)
                {
                    var parseMaSach = long.Parse(txtMaSach.Text);
                    var findID = QLTV.ChiTietPhieuMuons.SingleOrDefault(p => p.MaPhieuMuon == MaPhieuMuon && p.Masach == parseMaSach);
                    if (findID != null)
                    {
                        QLTV.ChiTietPhieuMuons.Remove(findID);
                        QLTV.SaveChanges();
                        MessageBox.Show("Xóa thành công");
                        loadAdmin();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaSach.Text = "";
            txtTimKiem.Text = "";
            loadAdmin();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTimKiem.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập mã sách muốn tìm kiếm");
                }
                else
                {
                    var parseMaSach = long.Parse(txtTimKiem.Text);
                    var DsCTPM = from ctpm in QLTV.ChiTietPhieuMuons
                                 where ctpm.MaPhieuMuon == MaPhieuMuon && ctpm.Masach == parseMaSach
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {

                var parseMaSach = long.Parse(txtTimKiem.Text);
                var DsCTPM = from ctpm in QLTV.ChiTietPhieuMuons
                             where ctpm.MaPhieuMuon == MaPhieuMuon && ctpm.Masach == parseMaSach
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
