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
    public partial class FrmThongKeDoanhThu : Form
    {
        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();
        public FrmThongKeDoanhThu()
        {
            InitializeComponent();
            txtTongDoanhThu.ReadOnly= true;
        }

        private void loadDoanhThu()
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
            txtTongDoanhThu.Text = tongtien.ToString();
        }
        private void btnTinh_Click(object sender, EventArgs e)
        {
            try
            {
                var ListDoanhThu = from doanhthu in QLTV.ChiTietPhieuMuons
                                   where doanhthu.NgayMuon >= dtbNgayBatDau.Value.Date && doanhthu.NgayTra <= dtbNgayKetThuc.Value.Date
                                   select new
                                   {
                                       MaPhieuMuon = doanhthu.MaPhieuMuon,
                                       MaSach = doanhthu.Masach,
                                       NgayMuon = doanhthu.NgayMuon,
                                       NgayTra = doanhthu.NgayTra,
                                       TrangThai = doanhthu.TrangThai,
                                       GiaChoThue = doanhthu.Sach.GiaChoThue,
                                   };
                dgv.DataSource = ListDoanhThu.ToList();
                loadDoanhThu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
