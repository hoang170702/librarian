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
    public partial class FrmThongKeLuong : Form
    {
        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();
        public FrmThongKeLuong()
        {
            InitializeComponent();
            txtmanv.ReadOnly = true;
            txtchucvu.ReadOnly = true;
            txtluong.ReadOnly = true;
            txtsdt.ReadOnly = true;
            txttennhanvien.ReadOnly = true;

        }

        private void load()
        {
            var ListNV = from nhanvien in QLTV.NhanViens
                         select new
                         {
                             MaNV = nhanvien.MaNV,
                             TenNV = nhanvien.TenNV,
                             SDT = nhanvien.SDTNV,
                             Luong = nhanvien.Luong,
                             ChucVu = nhanvien.ChucVu.TenChucVu,
                             TongLuong = nhanvien.TongLuong,
                         };
            dgv.DataSource = ListNV.ToList();

        }

        private void AddBinding()
        {
            txtmanv.DataBindings.Add("Text", dgv.DataSource, "MaNV");
            txttennhanvien.DataBindings.Add("Text", dgv.DataSource, "TenNV");
            txtsdt.DataBindings.Add("Text", dgv.DataSource, "SDT");
            txtluong.DataBindings.Add("Text", dgv.DataSource, "Luong");
            txtchucvu.DataBindings.Add("Text", dgv.DataSource, "ChucVu");
            txtTongluong.DataBindings.Add("Text", dgv.DataSource, "TongLuong");

        }

        private void ClearBinhding()
        {
            txtmanv.DataBindings.Clear();
            txttennhanvien.DataBindings.Clear();
            txtsdt.DataBindings.Clear();
            txtluong.DataBindings.Clear();
            txtchucvu.DataBindings.Clear();
            txtTongluong.DataBindings.Clear();
        }

        private void setLuongtheothang()
        {
            int countRows = dgv.Rows.Count;
            if (dateTimePicker1.Value.Day == 18)
            {
                for (int i = 0; i < countRows; i++)
                {
                    string ID = dgv.Rows[i].Cells["MaNV"].Value.ToString();
                    var findID = QLTV.NhanViens.SingleOrDefault(p => p.MaNV == ID);
                    if (findID != null)
                    {
                        findID.TongLuong = 0;
                        QLTV.SaveChanges();
                    }
                }

            }
        }

        private void FrmThongKeLuong_Load(object sender, EventArgs e)
        {
            try
            {
                load();
                setLuongtheothang();

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
                AddBinding();
                ClearBinhding();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            try
            {
                var parseThuong = float.Parse(txtthuong.Text);
                var parsePhat = float.Parse(txtphat.Text);
                var findID = QLTV.NhanViens.SingleOrDefault(p => p.MaNV == txtmanv.Text);
                if (findID != null)
                {
                    findID.TongLuong = findID.Luong + parseThuong - parsePhat;
                    MessageBox.Show("Lương của nhân viên trong tháng, " + dateTimePicker1.Value.Month + ", có ID :" + findID.MaNV + ", họ tên :" + findID.TenNV + ", là: " + findID.TongLuong.ToString());
                    QLTV.SaveChanges();
                    load();
                }
                txtthuong.Text = "0";
                txtphat.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
