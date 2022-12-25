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
    public partial class frmThongTinCaNhan : Form
    {
        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();
        public frmThongTinCaNhan()
        {
            InitializeComponent();
        }

        string ten;
        string matKhau;
        string loaiDocGia;
        public frmThongTinCaNhan(string ten, string matKhau, string loaiDocGia)
        {
            InitializeComponent();
            this.ten = ten;
            this.matKhau = matKhau;
            this.loaiDocGia = loaiDocGia;
            txtHoTen.ReadOnly = true;
            txtGioiTinh.ReadOnly = true;
            txtLoaiTaiKhoan.ReadOnly = true;
        }


        private void load()
        {
            var findMaDG = QLTV.Accounts.SingleOrDefault(p => p.TenTK == ten && p.MatKhau == matKhau);
            var findImage = QLTV.DocGias.SingleOrDefault(p => p.MaDG == findMaDG.MaDG);
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
            txtHoTen.Text = findImage.TenDG;
            txtGioiTinh.Text = findImage.GioiTinh;
            txtLoaiTaiKhoan.Text = findImage.LoaiDocGia.TenLoai;
            lblhello.Text += " " + findImage.TenDG;
        }
        private void frmThongTinCaNhan_Load(object sender, EventArgs e)
        {
            load();
        }
    }
}
