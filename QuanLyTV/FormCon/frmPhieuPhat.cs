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
    public partial class frmPhieuPhat : Form
    {
        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();
        public frmPhieuPhat()
        {
            InitializeComponent();
        }
        private void load()
        {
            var ListPhieuPhat = from phieuphat in QLTV.PhieuPhats
                                select new
                                {
                                    MaPhieuPhat = phieuphat.MaPhieuPhat,
                                    MaPhieuMuon = phieuphat.MaPhieuMuon,
                                    MaSach = phieuphat.Masach,
                                    LyDoPhat = phieuphat.Phat.LyDoPhat,
                                    TienPhat = phieuphat.Phat.TienPhat,

                                };
            dgv.DataSource = ListPhieuPhat.ToList();

        }

        private void loadCBB()
        {
            var ListPhat = QLTV.Phats.ToList();
            cbbLyDoPhat.DisplayMember = "LyDoPhat";
            cbbLyDoPhat.ValueMember = "MaPhat";
            cbbLyDoPhat.DataSource = ListPhat;
        }
        private void frmPhieuPhat_Load(object sender, EventArgs e)
        {
            try
            {
                load();
                loadCBB();
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
                if (txtMaPhieuTra.Text == "" || txtMaSach.Text == "" || cbbLyDoPhat.Text == "")
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!!!");
                }
                else
                {
                    var parseMaPhieuMuon = long.Parse(txtMaPhieuTra.Text);
                    var addMaphat = cbbLyDoPhat.SelectedItem as Phat;
                    var setTrangThaiPhat = QLTV.ChiTietPhieuMuons.SingleOrDefault(p => p.MaPhieuMuon == parseMaPhieuMuon && p.Masach == txtMaSach.Text);
                    var addPhieuPhat = new PhieuPhat()
                    {
                        MaPhieuMuon = parseMaPhieuMuon,
                        Masach = txtMaSach.Text,
                        MaPhat = addMaphat.MaPhat,
                    };
                    QLTV.PhieuPhats.Add(addPhieuPhat);
                    setTrangThaiPhat.TrangThai = "Đã Trả";
                    QLTV.SaveChanges();
                    load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddBinding()
        {
            txtMaPhieuPhat.DataBindings.Add("Text", dgv.DataSource, "MaPhieuPhat");
            txtMaPhieuTra.DataBindings.Add("Text", dgv.DataSource, "MaPhieuMuon");
            txtMaSach.DataBindings.Add("Text", dgv.DataSource, "MaSach");
            txtTienPhat.DataBindings.Add("Text", dgv.DataSource, "TienPhat");
            cbbLyDoPhat.DataBindings.Add("Text", dgv.DataSource, "LyDoPhat");
        }

        private void clearBinding()
        {
            txtMaPhieuPhat.DataBindings.Clear();
            txtMaPhieuTra.DataBindings.Clear();
            txtMaSach.DataBindings.Clear();
            cbbLyDoPhat.DataBindings.Clear();
            txtTienPhat.DataBindings.Clear();
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                AddBinding();
                clearBinding();
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

                if (txtMaPhieuTra.Text == "" || txtMaSach.Text == "" || cbbLyDoPhat.Text == "")
                {
                    MessageBox.Show("Vui lòng click vào phiếu phạt muốn sửa!!!");
                }
                else
                {
                    var thongbao = MessageBox.Show("Bạn có muốn sửa ?", "ThongBao", MessageBoxButtons.YesNo);
                    if (thongbao == DialogResult.Yes)
                    {
                        var parseMaPhieuPhat = long.Parse(txtMaPhieuPhat.Text);
                        var findMaPhieuPhat = QLTV.PhieuPhats.SingleOrDefault(p => p.MaPhieuPhat == parseMaPhieuPhat);
                        var parseMaPhieuMuon = long.Parse(txtMaPhieuTra.Text);
                        var addMaphat = cbbLyDoPhat.SelectedItem as Phat;
                        if (findMaPhieuPhat != null)
                        {
                            findMaPhieuPhat.MaPhieuMuon = parseMaPhieuMuon;
                            findMaPhieuPhat.Masach = txtMaSach.Text;
                            findMaPhieuPhat.MaPhat = addMaphat.MaPhat;
                            QLTV.SaveChanges();
                            MessageBox.Show("Cập nhật phiếu phạt thành công!!!");
                            load();
                        };
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
            if (txtMaPhieuTra.Text == "" || txtMaSach.Text == "" || cbbLyDoPhat.Text == "")
            {
                MessageBox.Show("Vui lòng click vào phiếu phạt muốn xóa!!!");
            }
            else
            {
                var ThongBao = MessageBox.Show("Bạn có muốn xóa ?", "ThongBao", MessageBoxButtons.YesNo);
                if (ThongBao == DialogResult.Yes)
                {
                    var parseMaPhieuPhat = long.Parse(txtMaPhieuPhat.Text);
                    var findMaPhieuPhat = QLTV.PhieuPhats.SingleOrDefault(p => p.MaPhieuPhat == parseMaPhieuPhat);
                    if (findMaPhieuPhat != null)
                    {
                        QLTV.PhieuPhats.Remove(findMaPhieuPhat);
                        QLTV.SaveChanges();
                        MessageBox.Show("Xóa thành công");
                        load();
                    }
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaPhieuPhat.Text = "";
            txtMaPhieuTra.Text = "";
            txtMaSach.Text = "";
            txtTienPhat.Text = "";
            txtTimKiem.Text = "";
            rdbLyDoPhat.Checked = false;
            rdbMaSach.Checked = false;
            rdbMaPhieuPhat.Checked = false;
            rdbMaPhieuTra.Checked = false;
            load();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTimKiem.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập từ muốn tìm kiếm!!!");
                }
                else if (rdbMaPhieuPhat.Checked == false && rdbMaPhieuTra.Checked == false && rdbMaSach.Checked == false && rdbLyDoPhat.Checked == false)
                {
                    MessageBox.Show("Vui lòng chọn trường muốn tìm kiếm!!!");
                }
                else
                {
                    //tim ma phieu phat
                    if (rdbMaPhieuPhat.Checked == true)
                    {
                        var parseMaphieuPhat = long.Parse(txtTimKiem.Text);
                        var phieuphat = from Maphieuphat in QLTV.PhieuPhats
                                        where Maphieuphat.MaPhieuPhat == parseMaphieuPhat
                                        select new
                                        {
                                            MaPhieuPhat = Maphieuphat.MaPhieuPhat,
                                            MaPhieuMuon = Maphieuphat.MaPhieuMuon,
                                            MaSach = Maphieuphat.Masach,
                                            LyDoPhat = Maphieuphat.Phat.LyDoPhat,
                                        };
                        dgv.DataSource = phieuphat.ToList();
                    }
                    // tim ma phieu tra
                    else if (rdbMaPhieuTra.Checked == true)
                    {
                        var parseMaphieutra = long.Parse(txtTimKiem.Text);
                        var phieutra = from Maphieutra in QLTV.PhieuPhats
                                       where Maphieutra.MaPhieuMuon == parseMaphieutra
                                       select new
                                       {
                                           MaPhieuPhat = Maphieutra.MaPhieuPhat,
                                           MaPhieuMuon = Maphieutra.MaPhieuMuon,
                                           MaSach = Maphieutra.Masach,
                                           LyDoPhat = Maphieutra.Phat.LyDoPhat,
                                       };
                        dgv.DataSource = phieutra.ToList();
                    }
                    // Tim Ma sach
                    else if (rdbMaSach.Checked == true)
                    {

                        var Sach = from MaSach in QLTV.PhieuPhats
                                   where MaSach.Masach.Contains(txtTimKiem.Text)
                                   select new
                                   {
                                       MaPhieuPhat = MaSach.MaPhieuPhat,
                                       MaPhieuMuon = MaSach.MaPhieuMuon,
                                       MaSach = MaSach.Masach,
                                       LyDoPhat = MaSach.Phat.LyDoPhat,
                                   };
                        dgv.DataSource = Sach.ToList();
                    }
                    //Tim Ly do phat
                    else if (rdbLyDoPhat.Checked == true)
                    {
                        var LyDoPhat = from lydo in QLTV.PhieuPhats
                                       where lydo.Phat.LyDoPhat.Contains(txtTimKiem.Text)
                                       select new
                                       {
                                           MaPhieuPhat = lydo.MaPhieuPhat,
                                           MaPhieuMuon = lydo.MaPhieuMuon,
                                           MaSach = lydo.Masach,
                                           LyDoPhat = lydo.Phat.LyDoPhat,
                                       };
                        dgv.DataSource = LyDoPhat.ToList();
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
