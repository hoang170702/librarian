﻿
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
    public partial class frmNhanVien : Form
    {

        QuanLyCHCTSEntities QLTV = new QuanLyCHCTSEntities();

        public frmNhanVien()
        {
            InitializeComponent();
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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
                         };
            dgv.DataSource = ListNV.ToList();

        }

        private void loadComboboxChucVu()
        {
            var listChucVu = QLTV.ChucVus.ToList();
            cbbChucVu.DisplayMember = "TenChucVu";
            cbbChucVu.ValueMember = "MaChucVu";
            cbbChucVu.DataSource = listChucVu;
        }

        private void AddBinding()
        {
            txtma.DataBindings.Add("Text", dgv.DataSource, "MaNV");
            txtten.DataBindings.Add("Text", dgv.DataSource, "TenNV");
            txtsdt.DataBindings.Add("Text", dgv.DataSource, "SDT");
            txtLuong.DataBindings.Add("Text", dgv.DataSource, "Luong");
            cbbChucVu.DataBindings.Add("Text", dgv.DataSource, "ChucVu");
        }

        private void ClearBinhding()
        {
            txtma.DataBindings.Clear();
            txtten.DataBindings.Clear();
            cbbChucVu.DataBindings.Clear();
            txtsdt.DataBindings.Clear();
            txtLuong.DataBindings.Clear();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                load();
                loadComboboxChucVu();
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
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                AddBinding();
                ClearBinhding();
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
                txtten.Text = "";
                txtsdt.Text = "";
                cbbChucVu.Text = "";
                txtTimKiem.Text = "";
                txtLuong.Text = "";
                rdbMa.Checked = false;
                rdbTen.Checked = false;
                rdbSDT.Checked = false;
                rdbChucVu.Checked = false;
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
                var checkSDT = QLTV.NhanViens.SingleOrDefault(p => p.SDTNV == txtsdt.Text);
                var AddMaChucVu = cbbChucVu.SelectedItem as ChucVu;
                var parseLuong = float.Parse(txtLuong.Text);
                var AddNV = new NhanVien()
                {
                    MaNV = "NV" + txtma.Text,
                    TenNV = txtten.Text,
                    Luong = parseLuong,
                    SDTNV = txtsdt.Text,
                    MaChucVu = AddMaChucVu.MaChucVu,

                };
                if (txtma.Text == "" || txtten.Text == "" || txtsdt.Text == "" || txtLuong.Text == "" || cbbChucVu.Text == "")
                {
                    throw new Exception("Không đươc để trống thông tin!!!");
                }
                else if (txtsdt.Text.Length != 10)
                {
                    throw new Exception("Số điện thoại phải là 10 số!!!");
                }
                else if (checkSDT != null)
                {
                    throw new Exception("Số điện thoại này đã đươc nhân viên " + checkSDT.MaNV + " đăng kí");
                }
                else
                {
                    QLTV.NhanViens.Add(AddNV);
                    QLTV.SaveChanges();
                    MessageBox.Show("Thêm Thành Công");
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
                    throw new Exception("Vui lòng click vào nhân viên muốn xóa!!!");
                }
                else
                {
                    var Notification = MessageBox.Show("Bạn có chắc muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (Notification == DialogResult.Yes)
                    {

                        var findID = QLTV.NhanViens.SingleOrDefault(p => p.MaNV == txtma.Text);
                        if (findID != null)
                        {
                            QLTV.NhanViens.Remove(findID);
                            QLTV.SaveChanges();
                            MessageBox.Show("Xóa thành công nhân viên [" + findID.MaNV + "] !!!");
                            load();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại!!!");
                        }
                    }
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
                    throw new Exception("Không có nhân viên này");
                }
                else
                {
                    var Notification = MessageBox.Show("Bạn có chắc muốn sửa ?", "Thông báo", MessageBoxButtons.YesNo);
                    if (Notification == DialogResult.Yes)
                    {
                        var AddMaChucVu = cbbChucVu.SelectedItem as ChucVu;
                        var parseLuong = float.Parse(txtLuong.Text);
                        var findID = QLTV.NhanViens.SingleOrDefault(p => p.MaNV == txtma.Text);
                        if (findID != null)
                        {
                            findID.TenNV = txtten.Text;
                            findID.SDTNV = txtsdt.Text;
                            findID.Luong = parseLuong;
                            findID.MaChucVu = AddMaChucVu.MaChucVu;
                            QLTV.SaveChanges();
                            MessageBox.Show("Cập nhật thông tin nhân viên [" + findID.MaNV + "] thành công");
                            load();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thông tin nhân viên thất bại");
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
                    throw new Exception("Vui lòng nhập từ tìm kiếm ");
                }
                else if (rdbMa.Checked == false && rdbTen.Checked == false && rdbSDT.Checked == false && rdbChucVu.Checked == false)
                {
                    throw new Exception("Vui lòng chọn trường muốn tìm kiếm");
                }
                else
                {
                    // find ID 
                    if (rdbMa.Checked == true)
                    {
                        var ID = (txtTimKiem.Text);
                        var findID = from id in QLTV.NhanViens
                                     where id.MaNV.Contains(ID)
                                     select new
                                     {
                                         MaNV = id.MaNV,
                                         TenNV = id.TenNV,
                                         SDT = id.SDTNV,
                                         Luong = id.Luong,
                                         ChucVu = id.ChucVu.TenChucVu,
                                     };
                        if (findID != null)
                        {
                            dgv.DataSource = findID.ToList();
                            ClearBinhding();
                        }
                    }
                    // Find name
                    else if (rdbTen.Checked == true)
                    {
                        var name = txtTimKiem.Text;
                        var findName = from NAME in QLTV.NhanViens
                                       where NAME.TenNV.Contains(name)
                                       select new
                                       {
                                           MaNV = NAME.MaNV,
                                           TenNV = NAME.TenNV,
                                           SDT = NAME.SDTNV,
                                           Luong = NAME.Luong,
                                           ChucVu = NAME.ChucVu.TenChucVu,
                                       };
                        if (findName != null)
                        {
                            dgv.DataSource = findName.ToList();
                            ClearBinhding();
                        }
                    }
                    // Find Phone Number
                    else if (rdbSDT.Checked == true)
                    {
                        var sdt = txtTimKiem.Text;
                        var findPhoneNumber = from phoneNumber in QLTV.NhanViens
                                              where phoneNumber.SDTNV.Contains(sdt)
                                              select new
                                              {
                                                  MaNV = phoneNumber.MaNV,
                                                  TenNV = phoneNumber.TenNV,
                                                  SDT = phoneNumber.SDTNV,
                                                  Luong = phoneNumber.Luong,
                                                  ChucVu = phoneNumber.ChucVu.TenChucVu,
                                              };
                        if (findPhoneNumber != null)
                        {
                            dgv.DataSource = findPhoneNumber.ToList();
                            ClearBinhding();
                        }
                    }
                    //Find positive
                    else if (rdbChucVu.Checked == true)
                    {
                        var ChucVu = txtTimKiem.Text;
                        var findChucVu = from ChucVuNV in QLTV.NhanViens
                                         where ChucVuNV.ChucVu.TenChucVu.Contains(ChucVu)
                                         select new
                                         {
                                             MaNV = ChucVuNV.MaNV,
                                             TenNV = ChucVuNV.TenNV,
                                             SDT = ChucVuNV.SDTNV,
                                             Luong = ChucVuNV.Luong,
                                             ChucVu = ChucVuNV.ChucVu.TenChucVu,
                                         };
                        if (findChucVu != null)
                        {
                            dgv.DataSource = findChucVu.ToList();
                            ClearBinhding();
                        }
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
