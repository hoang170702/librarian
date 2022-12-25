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
    public partial class frmQuyDinhPhat : Form
    {
        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();
        public frmQuyDinhPhat()
        {
            InitializeComponent();
        }


        private void load()
        {
            var Quydinh = from quydinhphat in QLTV.Phats
                          select new
                          {
                              MaPhat = quydinhphat.MaPhat,
                              LyDoPhat = quydinhphat.LyDoPhat,
                              TienPhat = quydinhphat.TienPhat,
                          };
            dgv.DataSource = Quydinh.ToList();
        }
        private void frmQuyDinhPhat_Load(object sender, EventArgs e)
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var addQuydinh = new Phat()
                {
                    MaPhat = txtMaPhat.Text,
                    LyDoPhat = txtLydoPhat.Text,
                    TienPhat = float.Parse(txtTienPhat.Text),
                };
                QLTV.Phats.Add(addQuydinh);
                QLTV.SaveChanges();
                MessageBox.Show("Đã thêm quy định phạt mới thành công!!!");
                load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void addBinding()
        {
            txtMaPhat.DataBindings.Add("Text", dgv.DataSource, "MaPhat");
            txtLydoPhat.DataBindings.Add("Text", dgv.DataSource, "LyDoPhat");
            txtTienPhat.DataBindings.Add("Text", dgv.DataSource, "TienPhat");
        }

        private void clearBinding()
        {
            txtMaPhat.DataBindings.Clear();
            txtLydoPhat.DataBindings.Clear();
            txtTienPhat.DataBindings.Clear();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                addBinding();
                clearBinding();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            txtLydoPhat.Text = "";
            txtMaPhat.Text = "";
            txtTienPhat.Text = "";
            load();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                var thongbao = MessageBox.Show("Bạn có muốn sửa ?", "Thông Báo", MessageBoxButtons.YesNo);
                if (thongbao == DialogResult.Yes)
                {
                    var parseTienPhat = float.Parse(txtTienPhat.Text);
                    var findID = QLTV.Phats.SingleOrDefault(p => p.MaPhat == txtMaPhat.Text);
                    if (findID != null)
                    {

                        findID.LyDoPhat = txtLydoPhat.Text;
                        findID.TienPhat = parseTienPhat;
                    }
                    QLTV.SaveChanges();
                    MessageBox.Show("Cập nhật quy định " + findID.MaPhat + " thành công");
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
                var thongbao = MessageBox.Show("Bạn có muốn xóa ?", "Thông Báo", MessageBoxButtons.YesNo);
                if (thongbao == DialogResult.Yes)
                {

                    var findID = QLTV.Phats.SingleOrDefault(p => p.MaPhat == txtMaPhat.Text);
                    if (findID != null)
                    {
                        QLTV.Phats.Remove(findID);
                    }
                    QLTV.SaveChanges();
                    MessageBox.Show("Xóa quy định " + findID.MaPhat + " thành công");
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
