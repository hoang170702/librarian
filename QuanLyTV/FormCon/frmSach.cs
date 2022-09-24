using System;
using System.Collections.Generic;
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
        QLTVEntities QLTV = new QLTVEntities();
        public frmSach()
        {
            InitializeComponent();
            this.dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void load()
        {
            var listSach = from Lsach in QLTV.Sach
                           select new
                           {
                               MaSach = Lsach.Masach,
                               TenSach = Lsach.Tensach,
                               GiaSach = Lsach.Gia,
                               SoLuong = Lsach.Soluong,
                               MaTheLoai = Lsach.MaTL,
                               MaTacGia = Lsach.MaTG,
                               MaNXB = Lsach.MaNXB,
                           };
            dgv.DataSource = listSach.ToList();
            txtma.ReadOnly = true;
        }
        private void frmSach_Load(object sender, EventArgs e)
        {
            try
            {
                load();
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
                    txtSoLuong.Text = dgv.Rows[e.RowIndex].Cells["SoLuong"].FormattedValue.ToString();
                    txtTheLoai.Text = dgv.Rows[e.RowIndex].Cells["MaTheLoai"].FormattedValue.ToString();
                    txtMaTG.Text = dgv.Rows[e.RowIndex].Cells["MaTacGia"].FormattedValue.ToString();
                    txtMaNXB.Text = dgv.Rows[e.RowIndex].Cells["MaNXB"].FormattedValue.ToString();
                    var ParseMa = long.Parse(txtma.Text);
                    var findImage = QLTV.Sach.FirstOrDefault(p => p.Masach == ParseMa);
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
                if (txtTen.Text == "" || txtGia.Text == "" || txtSoLuong.Text == "" || txtTheLoai.Text == "" || txtMaNXB.Text == "" || txtMaTG.Text == "")
                {
                    throw new Exception("Vui lòng điền đầy đủ thông tin sách");
                }
                else
                {
                    var parseGia = float.Parse(txtGia.Text);
                    var parseSoLuong = int.Parse(txtSoLuong.Text);
                    var parseMaTheLoai = long.Parse(txtTheLoai.Text);
                    var parseMaNXB = long.Parse(txtMaNXB.Text);
                    var parseMaTG = long.Parse(txtMaTG.Text);

                    var addSach = new Sach()
                    {
                        Tensach = txtTen.Text,
                        Gia = parseGia,
                        Soluong = parseSoLuong,
                        MaTL = parseMaTheLoai,
                        MaNXB = parseMaNXB,
                        MaTG = parseMaTG,
                        HinhAnhSach = imagetobyarray(pictureBox1.Image)
                    };
                    QLTV.Sach.Add(addSach);
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
                var parseMaSach = long.Parse(txtma.Text);
                var findMaSach = QLTV.Sach.SingleOrDefault(p => p.Masach == parseMaSach);
                if (findMaSach != null)
                {
                    QLTV.Sach.Remove(findMaSach);
                    QLTV.SaveChanges();
                    MessageBox.Show("Xóa thành công");
                    load();
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
                txtma.Text = "";
                txtTen.Text = "";
                txtGia.Text = "";
                txtSoLuong.Text = "";
                txtTheLoai.Text = "";
                txtMaNXB.Text = "";
                txtMaTG.Text = "";
                txtTimKiem.Text = "";
                pictureBox1.Image = new Bitmap(Application.StartupPath + @"\\image\\NoImage.png");
                rdbGia.Checked = false;
                rdbMa.Checked = false;
                rdbNXB.Checked = false;
                rdbTacGia.Checked = false;
                rdbTen.Checked = false;
                rdbTheLoai.Checked = false;
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
                var parseGia = float.Parse(txtGia.Text);
                var parseSoLuong = int.Parse(txtSoLuong.Text);
                var parseMaTheLoai = long.Parse(txtTheLoai.Text);
                var parseMaNXB = long.Parse(txtMaNXB.Text);
                var parseMaTG = long.Parse(txtMaTG.Text);
                var parseMaSach = long.Parse(txtma.Text);
                var findMaSach = QLTV.Sach.SingleOrDefault(p => p.Masach == parseMaSach);

                if (findMaSach != null)
                {
                    findMaSach.Tensach = txtTen.Text;
                    findMaSach.Gia = parseGia;
                    findMaSach.Soluong = parseSoLuong;
                    findMaSach.MaTL = parseMaTheLoai;
                    findMaSach.MaTG = parseMaTG;
                    findMaSach.MaNXB = parseMaNXB;
                    findMaSach.HinhAnhSach = imagetobyarray(pictureBox1.Image);
                    QLTV.SaveChanges();
                    MessageBox.Show("Cập nhật thành công");
                    load();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
