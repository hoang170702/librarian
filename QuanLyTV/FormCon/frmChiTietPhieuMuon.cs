
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

        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();
        public frmChiTietPhieuMuon()
        {
            InitializeComponent();
        }
        string ten; long MaPhieuMuon; string loaiDocGia;
        public frmChiTietPhieuMuon(string ten, long MaPhieuMuon, string loaiDocGia)
        {
            InitializeComponent();
            this.ten = ten;
            this.MaPhieuMuon = MaPhieuMuon;
            this.loaiDocGia = loaiDocGia;
            txtMaPhieuMuon.Text = MaPhieuMuon.ToString();
            txtMaPhieuMuon.ReadOnly = true;
            dtpNgayMuon.Enabled = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtTongTien.ReadOnly = true;
        }

        private void loadTongTien()
        {
            int sc = dgv.Rows.Count;
            double? tongtien = 0;

            for (int i = 0; i < sc; i++)
            {
                var ngayMuon = dgv.Rows[i].Cells["NgayMuon"].Value.ToString();
                var NgayTra = dgv.Rows[i].Cells["NgayTra"].Value.ToString();


                DateTime ngaymuon = Convert.ToDateTime(ngayMuon);
                DateTime ngaytra = Convert.ToDateTime(NgayTra);
                TimeSpan Time = ngaytra - ngaymuon;
                int TongSoNgay = Time.Days;


                tongtien += double.Parse(dgv.Rows[i].Cells["GiaChoThue"].Value.ToString()) * double.Parse(TongSoNgay.ToString());
            }
            txtTongTien.Text = tongtien.ToString();
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
                             GiaChoThue = ctpm.Sach.GiaChoThue,
                         };

            dgv.DataSource = DsCTPM.ToList();
            loadTongTien();


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
            loadTongTien();
            paChucnang.Hide();
        }
        private void frmChiTietPhieuMuon_Load(object sender, EventArgs e)
        {
            try
            {
                if (loaiDocGia == "2")
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

                var sach = txtMaSach.Text;
                var TrangThai = QLTV.Saches.SingleOrDefault(p => p.Masach == sach);
                if (dtpNgayMuon.Value.Date >= dtpNgayTra.Value.Date)
                {
                    MessageBox.Show("Ngày mượn không thể nhỏ hơn ngày trả");
                }
                else
                {
                    if (TrangThai.TrangThaiSach == "Còn")
                    {
                        var AddCTPM = new ChiTietPhieuMuon()
                        {
                            MaPhieuMuon = MaPhieuMuon,
                            Masach = txtMaSach.Text,
                            NgayMuon = dtpNgayMuon.Value.Date,
                            NgayTra = dtpNgayTra.Value.Date,
                            TrangThai = "Đang Mượn"
                        };
                        QLTV.ChiTietPhieuMuons.Add(AddCTPM);
                        TrangThai.TrangThaiSach = "Không còn";
                        QLTV.SaveChanges();
                        MessageBox.Show("Thêm Sách vào phiếu mượn " + MaPhieuMuon.ToString() + " thành công");
                        loadAdmin();
                    }
                    else if (TrangThai.TrangThaiSach == "Không còn")
                    {
                        MessageBox.Show("Sách đã được khách hàng khác thuê!!!");
                    }

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
                    if (txtMaSach.Text == "" || txtMaPhieuMuon.Text == "")
                    {
                        MessageBox.Show("Vui lòng click vào phiếu mượn cần sửa");
                    }
                    else
                    {
                        var findID = QLTV.ChiTietPhieuMuons.SingleOrDefault(p => p.MaPhieuMuon == MaPhieuMuon && p.Masach == txtMaSach.Text);
                        if (findID != null)
                        {
                            findID.NgayTra = dtpNgayTra.Value.Date;
                        }
                        QLTV.SaveChanges();
                        MessageBox.Show("Sửa thành công");
                        loadAdmin();
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
                var thongbao = MessageBox.Show("Bạn có muốn xóa", "Thong Bao", MessageBoxButtons.YesNo);
                if (thongbao == DialogResult.Yes)
                {
                    var findID = QLTV.ChiTietPhieuMuons.SingleOrDefault(p => p.MaPhieuMuon == MaPhieuMuon && p.Masach == txtMaSach.Text);
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
            dtpNgayTra.Text = DateTime.Today.Date.ToString();
            dtpNgayMuon.Text = DateTime.Today.Date.ToString();
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
                    var DsCTPM = from ctpm in QLTV.ChiTietPhieuMuons
                                 where ctpm.MaPhieuMuon == MaPhieuMuon && ctpm.Masach.Contains(txtTimKiem.Text)
                                 select new
                                 {
                                     MaPhieuMuon = ctpm.MaPhieuMuon,
                                     MaSach = ctpm.Masach,
                                     NgayMuon = ctpm.NgayMuon,
                                     NgayTra = ctpm.NgayTra,
                                     TrangThai = ctpm.TrangThai,
                                     GiaChoThue = ctpm.Sach.GiaChoThue,
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
                var DsCTPM = from ctpm in QLTV.ChiTietPhieuMuons
                             where ctpm.MaPhieuMuon == MaPhieuMuon && ctpm.Masach.Contains(txtTimKiem.Text)
                             select new
                             {
                                 MaPhieuMuon = ctpm.MaPhieuMuon,
                                 MaSach = ctpm.Masach,
                                 NgayMuon = ctpm.NgayMuon,
                                 NgayTra = ctpm.NgayTra,
                                 TrangThai = ctpm.TrangThai,
                                 GiaChoThue = ctpm.Sach.GiaChoThue,
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
