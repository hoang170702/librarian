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
    }
}
