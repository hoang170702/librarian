
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTV.FormCon
{
    public partial class frmSach : Form
    {
        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();


        public frmSach()
        {
            InitializeComponent();
            this.dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void load()
        {
            var listSach = from Lsach in QLTV.Saches
                           select new
                           {
                               MaSach = Lsach.Masach,
                               TenSach = Lsach.Tensach,
                               GiaSach = Lsach.GiaSach,
                               GiaChoThue = Lsach.GiaChoThue,
                               TheLoai = Lsach.TheLoai.TenTL,
                               TacGia = Lsach.TacGia.TenTG,
                               NXB = Lsach.NXB.TenNXB,
                               TrangThaiSach = Lsach.TrangThaiSach,

                           };
            dgv.DataSource = listSach.ToList();
        }

        private void loadCbbTheLoai()
        {
            var ListCBB = QLTV.TheLoais.ToList();
            cbbTheLoai.DisplayMember = "TenTL";
            cbbTheLoai.ValueMember = "MaTL";
            cbbTheLoai.DataSource = ListCBB;
        }

        private void loadCbbTacGia()
        {
            var ListCBB = QLTV.TacGias.ToList();
            cbbTacGia.DisplayMember = "TenTG";
            cbbTacGia.ValueMember = "MaTG";
            cbbTacGia.DataSource = ListCBB;
        }


        private void loadCbbNXB()
        {

            var ListCBB = QLTV.NXBs.ToList();
            cbbNXB.DisplayMember = "TenNXB";
            cbbNXB.ValueMember = "MaNXB";
            cbbNXB.DataSource = ListCBB;
        }
        private void frmSach_Load(object sender, EventArgs e)
        {
            try
            {
                load();
                loadCbbTheLoai();
                loadCbbTacGia();
                loadCbbNXB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    this.Text = ofd.FileName;
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
                if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dgv.CurrentCell.Selected = true;
                    txtma.Text = dgv.Rows[e.RowIndex].Cells["MaSach"].FormattedValue.ToString();
                    txtTen.Text = dgv.Rows[e.RowIndex].Cells["TenSach"].FormattedValue.ToString();
                    txtGia.Text = dgv.Rows[e.RowIndex].Cells["GiaSach"].FormattedValue.ToString();
                    txtGiaChoThue.Text = dgv.Rows[e.RowIndex].Cells["GiaChoThue"].FormattedValue.ToString();
                    cbbTheLoai.Text = dgv.Rows[e.RowIndex].Cells["TheLoai"].FormattedValue.ToString();
                    cbbTacGia.Text = dgv.Rows[e.RowIndex].Cells["TacGia"].FormattedValue.ToString();
                    cbbNXB.Text = dgv.Rows[e.RowIndex].Cells["NXB"].FormattedValue.ToString();

                    var findImage = QLTV.Saches.FirstOrDefault(p => p.Masach == txtma.Text);
                    byte[] HinhSach = findImage.HinhAnhSach;
                    if (findImage.HinhAnhSach == null)
                    {
                        pictureBox1.Image = new Bitmap(Application.StartupPath + @"\\image\\NoImage.png");
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(HinhSach);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        byte[] imagetobyarray(Image img)
        {
            MemoryStream mms = new MemoryStream();
            img.Save(mms, System.Drawing.Imaging.ImageFormat.Png);
            img.Save(mms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return mms.ToArray();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtTen.Text == "" || txtGia.Text == "" || txtGiaChoThue.Text == "" || cbbTheLoai.Text == "" || cbbNXB.Text == "" || cbbTacGia.Text == "" || txtGiaChoThue.Text == "")
                {
                    throw new Exception("Vui lòng điền đầy đủ thông tin sách");
                }
                else
                {
                    var parseGia = float.Parse(txtGia.Text);
                    var parseGiaChoThue = int.Parse(txtGiaChoThue.Text);
                    var AddMaTheLoai = cbbTheLoai.SelectedItem as TheLoai;
                    var AddMaNXB = cbbNXB.SelectedItem as NXB;
                    var AddMaTG = cbbTacGia.SelectedItem as TacGia;

                    var addSach = new Sach()
                    {
                        Masach = "S" + txtma.Text,
                        Tensach = txtTen.Text,
                        GiaSach = parseGia,
                        GiaChoThue = parseGiaChoThue,
                        MaTL = AddMaTheLoai.MaTL,
                        MaNXB = AddMaNXB.MaNXB,
                        MaTG = AddMaTG.MaTG,
                        HinhAnhSach = imagetobyarray(pictureBox1.Image),
                        TrangThaiSach = "Còn",
                    };
                    QLTV.Saches.Add(addSach);
                    QLTV.SaveChanges();
                    MessageBox.Show("Thêm Sách Thành Công");
                    load();
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
                if (txtma.Text == "")
                {
                    MessageBox.Show("Vui lòng Click vào sách muốn xóa");
                }
                else
                {
                    var Notification = MessageBox.Show("Bạn có chắc muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (Notification == DialogResult.Yes)
                    {

                        var findMaSach = QLTV.Saches.SingleOrDefault(p => p.Masach == txtma.Text);
                        if (findMaSach != null)
                        {
                            QLTV.Saches.Remove(findMaSach);
                            QLTV.SaveChanges();
                            MessageBox.Show("Xóa thành công");
                            load();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void reset()
        {

            txtma.Text = "";
            txtTen.Text = "";
            txtGia.Text = "";
            txtGiaChoThue.Text = "";
            cbbTheLoai.Text = "";
            cbbNXB.Text = "";
            cbbTacGia.Text = "";
            txtTimKiem.Text = "";
            pictureBox1.Image = new Bitmap(Application.StartupPath + @"\\image\\NoImage.png");
            rdbGia.Checked = false;
            rdbMa.Checked = false;
            rdbNXB.Checked = false;
            rdbTacGia.Checked = false;
            rdbTen.Checked = false;
            rdbTheLoai.Checked = false;
            load();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                reset();
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
                if (txtma.Text == "")
                {
                    MessageBox.Show("Vui lòng Click vào sách muốn sửa!!!");
                }
                else
                {
                    var Notification = MessageBox.Show("Bạn có chắc muốn Sửa ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (Notification == DialogResult.Yes)
                    {
                        var parseGia = float.Parse(txtGia.Text);
                        var parseGiaChoThue = int.Parse(txtGiaChoThue.Text);
                        var AddMaTheLoai = cbbTheLoai.SelectedItem as TheLoai;
                        var AddMaNXB = cbbNXB.SelectedItem as NXB;
                        var AddMaTG = cbbTacGia.SelectedItem as TacGia;
                        var findMaSach = QLTV.Saches.SingleOrDefault(p => p.Masach == txtma.Text);

                        if (findMaSach != null)
                        {
                            findMaSach.Tensach = txtTen.Text;
                            findMaSach.GiaSach = parseGia;
                            findMaSach.GiaChoThue = parseGiaChoThue;
                            findMaSach.MaTL = AddMaTheLoai.MaTL;
                            findMaSach.MaTG = AddMaTG.MaTG;
                            findMaSach.MaNXB = AddMaNXB.MaNXB;
                            findMaSach.HinhAnhSach = imagetobyarray(pictureBox1.Image);
                            QLTV.SaveChanges();
                            MessageBox.Show("Cập nhật thành công");
                            load();
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
                    throw new Exception("Vui lòng nhập từ muốn tìm kiếm!!!");
                }
                else if (rdbMa.Checked == false && rdbGia.Checked == false && rdbNXB.Checked == false &&
                    rdbTacGia.Checked == false && rdbTen.Checked == false && rdbTheLoai.Checked == false)
                {
                    throw new Exception("Vui lòng chọn trường muốn tìm kiếm!!!");
                }
                else
                {
                    // Tim Ma Sach
                    if (rdbMa.Checked == true)
                    {
                        var MaSach = txtTimKiem.Text;
                        var findMaSach = from fMaSach in QLTV.Saches
                                         where fMaSach.Masach.Contains(MaSach)
                                         select new
                                         {
                                             MaSach = fMaSach.Masach,
                                             TenSach = fMaSach.Tensach,
                                             GiaSach = fMaSach.GiaSach,
                                             GiaChoThue = fMaSach.GiaChoThue,
                                             TheLoai = fMaSach.TheLoai.TenTL,
                                             TacGia = fMaSach.TacGia.TenTG,
                                             NXB = fMaSach.NXB.TenNXB,
                                         };
                        dgv.DataSource = findMaSach.ToList();
                    }
                    // Tim Gia Sach
                    else if (rdbGia.Checked == true)
                    {
                        var parseGia = float.Parse(txtTimKiem.Text);
                        var findGiaSach = from fGiaSach in QLTV.Saches
                                          where fGiaSach.GiaSach <= parseGia
                                          select new
                                          {
                                              MaSach = fGiaSach.Masach,
                                              TenSach = fGiaSach.Tensach,
                                              GiaSach = fGiaSach.GiaSach,
                                              GiaChoThue = fGiaSach.GiaChoThue,
                                              TheLoai = fGiaSach.TheLoai.TenTL,
                                              TacGia = fGiaSach.TacGia.TenTG,
                                              NXB = fGiaSach.NXB.TenNXB,
                                          };
                        dgv.DataSource = findGiaSach.ToList();
                    }
                    // Tim Ten Sach
                    else if (rdbTen.Checked == true)
                    {
                        var findTenSach = from fTenSach in QLTV.Saches
                                          where fTenSach.Tensach.Contains(txtTimKiem.Text)
                                          select new
                                          {
                                              MaSach = fTenSach.Masach,
                                              TenSach = fTenSach.Tensach,
                                              GiaSach = fTenSach.GiaSach,
                                              GiaChoThue = fTenSach.GiaChoThue,
                                              TheLoai = fTenSach.TheLoai.TenTL,
                                              TacGia = fTenSach.TacGia.TenTG,
                                              NXB = fTenSach.NXB.TenNXB,
                                          };
                        dgv.DataSource = findTenSach.ToList();
                    }
                    // Tim  The Loai
                    else if (rdbTheLoai.Checked == true)
                    {

                        var findMaTheLoai = from fMaTheLoai in QLTV.Saches
                                            where fMaTheLoai.TheLoai.TenTL.Contains(txtTimKiem.Text)
                                            select new
                                            {
                                                MaSach = fMaTheLoai.Masach,
                                                TenSach = fMaTheLoai.Tensach,
                                                GiaSach = fMaTheLoai.GiaSach,
                                                GiaChoThue = fMaTheLoai.GiaChoThue,
                                                TheLoai = fMaTheLoai.TheLoai.TenTL,
                                                TacGia = fMaTheLoai.TacGia.TenTG,
                                                NXB = fMaTheLoai.NXB.TenNXB,
                                            };
                        dgv.DataSource = findMaTheLoai.ToList();
                    }
                    //Tim  Tac Gia
                    else if (rdbTacGia.Checked == true)
                    {

                        var findMaTacGia = from fMaTG in QLTV.Saches
                                           where fMaTG.TacGia.TenTG.Contains(txtTimKiem.Text)
                                           select new
                                           {
                                               MaSach = fMaTG.Masach,
                                               TenSach = fMaTG.Tensach,
                                               GiaSach = fMaTG.GiaSach,
                                               GiaChoThue = fMaTG.GiaChoThue,
                                               TheLoai = fMaTG.TheLoai.TenTL,
                                               TacGia = fMaTG.TacGia.TenTG,
                                               NXB = fMaTG.NXB.TenNXB,
                                           };
                        dgv.DataSource = findMaTacGia.ToList();
                    }
                    //Tim  NXB
                    else if (rdbNXB.Checked == true)
                    {

                        var findMaNXB = from fMaNXB in QLTV.Saches
                                        where fMaNXB.NXB.TenNXB.Contains(txtTimKiem.Text)
                                        select new
                                        {
                                            MaSach = fMaNXB.Masach,
                                            TenSach = fMaNXB.Tensach,
                                            GiaSach = fMaNXB.GiaSach,
                                            GiaChoThue = fMaNXB.GiaChoThue,
                                            TheLoai = fMaNXB.TheLoai.TenTL,
                                            TacGia = fMaNXB.TacGia.TenTG,
                                            NXB = fMaNXB.NXB.TenNXB,
                                        };
                        dgv.DataSource = findMaNXB.ToList();
                    }
                }
                //reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
