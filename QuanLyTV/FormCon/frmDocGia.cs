
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
    public partial class frmDocGia : Form
    {
        QuanLyThuVienEntities QLTV = new QuanLyThuVienEntities();

        public frmDocGia()
        {
            InitializeComponent();
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            txtma.ReadOnly = true;
            this.AcceptButton = btnTim;

        }

        public void load()
        {
            var ListDG = from dg in QLTV.DocGias
                         select new
                         {
                             MaDG = dg.MaDG,
                             TenDG = dg.TenDG,
                             GioiTinh = dg.GioiTinh,
                         };
            dgv.DataSource = ListDG.ToList();
        }
        private void frmDocGia_Load(object sender, EventArgs e)
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

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dgv.CurrentCell.Selected = true;
                    txtma.Text = dgv.Rows[e.RowIndex].Cells["MaDG"].FormattedValue.ToString();
                    txtten.Text = dgv.Rows[e.RowIndex].Cells["TenDG"].FormattedValue.ToString();
                    txtGioiTinh.Text = dgv.Rows[e.RowIndex].Cells["GioiTinh"].FormattedValue.ToString();

                    var ParseMa = long.Parse(txtma.Text);
                    var findImage = QLTV.DocGias.FirstOrDefault(p => p.MaDG == ParseMa);
                    byte[] HinhSach = findImage.HinhDG;
                    if (findImage.HinhDG == null)
                    {
                        pictureDG.Image = new Bitmap(Application.StartupPath + @"\\image\\NoImage.png");
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(HinhSach);
                        pictureDG.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureDG_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureDG.Image = Image.FromFile(ofd.FileName);
                    this.Text = ofd.FileName;
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                txtma.Text = "";
                txtten.Text = "";
                txtGioiTinh.Text = "";
                txtTimKiem.Text = "";
                rdbMa.Checked = false;
                rdbTen.Checked = false;
                pictureDG.Image = new Bitmap(Application.StartupPath + @"\\image\\NoImage.png");
                load();
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
                if (txtGioiTinh.Text != "Nam" && txtGioiTinh.Text != "Nữ")
                {
                    MessageBox.Show("Giới tính chỉ có thể là Nam Hoặc Nữ");
                }
                else
                {
                    var AddDG = new DocGia()
                    {
                        TenDG = txtten.Text,
                        GioiTinh = txtGioiTinh.Text,
                        HinhDG = imagetobyarray(pictureDG.Image)
                    };
                    QLTV.DocGias.Add(AddDG);
                    QLTV.SaveChanges();
                    MessageBox.Show("Thêm Thành Công!!!");
                    load();
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
                if (txtma.Text == "")
                {
                    MessageBox.Show("Vui lòng Click vào đối tượng muốn sửa");
                }
                else
                {
                    var Notification = MessageBox.Show("Bạn Có chắc muốn sửa", "Thông báo", MessageBoxButtons.YesNo);
                    if (Notification == DialogResult.Yes)
                    {
                        var parseMaDG = long.Parse(txtma.Text);
                        var findID = QLTV.DocGias.SingleOrDefault(p => p.MaDG == parseMaDG);
                        if (findID != null)
                        {
                            findID.TenDG = txtten.Text;
                            findID.GioiTinh = txtGioiTinh.Text;
                            findID.HinhDG = imagetobyarray(pictureDG.Image);
                            QLTV.SaveChanges();
                            MessageBox.Show("Cập nhật độc giả có mã độc giả [" + findID.MaDG + "] thành công!!!");
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtma.Text == "")
                {
                    MessageBox.Show("Vui lòng Click vào đối tượng muốn xóa");
                }
                else
                {
                    var Notification = MessageBox.Show("Bạn Có chắc muốn Xóa", "Thông báo", MessageBoxButtons.YesNo);
                    if (Notification == DialogResult.Yes)
                    {
                        var parseMaDG = long.Parse(txtma.Text);
                        var findID = QLTV.DocGias.SingleOrDefault(p => p.MaDG == parseMaDG);
                        if (findID != null)
                        {
                            QLTV.DocGias.Remove(findID);
                            QLTV.SaveChanges();
                            MessageBox.Show("Đã Xóa độc giả có mã độc giả [" + findID.MaDG + "] thành công!!!");
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
                    MessageBox.Show("Vui lòng nhập từ muốn tìm kiếm");
                }
                else if (rdbMa.Checked == false && rdbTen.Checked == false)
                {
                    MessageBox.Show("Vui lòng chọn trường muốn tìm kiếm");
                }
                else
                {
                    // tim Ma doc gia
                    if (rdbMa.Checked == true)
                    {
                        var parseMaDG = long.Parse(txtTimKiem.Text);
                        var findDG = from dg in QLTV.DocGias
                                     where dg.MaDG == parseMaDG
                                     select new
                                     {
                                         MaDG = dg.MaDG,
                                         TenDG = dg.TenDG,
                                         GioiTinh = dg.GioiTinh,
                                     };
                        dgv.DataSource = findDG.ToList();
                    }
                    // tim ten doc gia
                    else if (rdbTen.Checked == true)
                    {
                        var findTen = from Tendg in QLTV.DocGias
                                      where Tendg.TenDG.Contains(txtTimKiem.Text)
                                      select new
                                      {
                                          MaDG = Tendg.MaDG,
                                          TenDG = Tendg.TenDG,
                                          GioiTinh = Tendg.GioiTinh,
                                      };
                        dgv.DataSource = findTen.ToList();
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
