namespace QuanLyTV.FormCon
{
    partial class frmPhieuTra
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPhieuTra));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbMaDG = new System.Windows.Forms.RadioButton();
            this.rdbNgayTra = new System.Windows.Forms.RadioButton();
            this.rdbMaSach = new System.Windows.Forms.RadioButton();
            this.rdbMaPhieu = new System.Windows.Forms.RadioButton();
            this.grbTimPhieu = new System.Windows.Forms.GroupBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.paChucNangAdmin = new System.Windows.Forms.Panel();
            this.btnDaTra = new System.Windows.Forms.Button();
            this.btnDangMuon = new System.Windows.Forms.Button();
            this.btnPhat = new System.Windows.Forms.Button();
            this.btnXuatPhieu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtbNgaytra = new System.Windows.Forms.DateTimePicker();
            this.txtMaSach = new System.Windows.Forms.TextBox();
            this.txtMaPhieu = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.entityCommand1 = new System.Data.Entity.Core.EntityClient.EntityCommand();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grbTimPhieu.SuspendLayout();
            this.panel2.SuspendLayout();
            this.paChucNangAdmin.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.grbTimPhieu);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 534);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Aqua;
            this.groupBox1.Controls.Add(this.rdbMaDG);
            this.groupBox1.Controls.Add(this.rdbNgayTra);
            this.groupBox1.Controls.Add(this.rdbMaSach);
            this.groupBox1.Controls.Add(this.rdbMaPhieu);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 255);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 276);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bộ Lọc Tìm Kiếm";
            // 
            // rdbMaDG
            // 
            this.rdbMaDG.AutoSize = true;
            this.rdbMaDG.Location = new System.Drawing.Point(15, 199);
            this.rdbMaDG.Name = "rdbMaDG";
            this.rdbMaDG.Size = new System.Drawing.Size(137, 27);
            this.rdbMaDG.TabIndex = 11;
            this.rdbMaDG.TabStop = true;
            this.rdbMaDG.Text = "Mã Độc Giả";
            this.rdbMaDG.UseVisualStyleBackColor = true;
            // 
            // rdbNgayTra
            // 
            this.rdbNgayTra.AutoSize = true;
            this.rdbNgayTra.Location = new System.Drawing.Point(15, 151);
            this.rdbNgayTra.Name = "rdbNgayTra";
            this.rdbNgayTra.Size = new System.Drawing.Size(114, 27);
            this.rdbNgayTra.TabIndex = 10;
            this.rdbNgayTra.TabStop = true;
            this.rdbNgayTra.Text = "Ngày Trả";
            this.rdbNgayTra.UseVisualStyleBackColor = true;
            // 
            // rdbMaSach
            // 
            this.rdbMaSach.AutoSize = true;
            this.rdbMaSach.Location = new System.Drawing.Point(15, 106);
            this.rdbMaSach.Name = "rdbMaSach";
            this.rdbMaSach.Size = new System.Drawing.Size(108, 27);
            this.rdbMaSach.TabIndex = 9;
            this.rdbMaSach.TabStop = true;
            this.rdbMaSach.Text = "Mã Sách";
            this.rdbMaSach.UseVisualStyleBackColor = true;
            // 
            // rdbMaPhieu
            // 
            this.rdbMaPhieu.AutoSize = true;
            this.rdbMaPhieu.Location = new System.Drawing.Point(15, 62);
            this.rdbMaPhieu.Name = "rdbMaPhieu";
            this.rdbMaPhieu.Size = new System.Drawing.Size(150, 27);
            this.rdbMaPhieu.TabIndex = 8;
            this.rdbMaPhieu.TabStop = true;
            this.rdbMaPhieu.Text = "Mã Phiếu Trả";
            this.rdbMaPhieu.UseVisualStyleBackColor = true;
            // 
            // grbTimPhieu
            // 
            this.grbTimPhieu.BackColor = System.Drawing.Color.Aqua;
            this.grbTimPhieu.Controls.Add(this.btnTim);
            this.grbTimPhieu.Controls.Add(this.txtTimKiem);
            this.grbTimPhieu.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbTimPhieu.Location = new System.Drawing.Point(0, 3);
            this.grbTimPhieu.Name = "grbTimPhieu";
            this.grbTimPhieu.Size = new System.Drawing.Size(197, 246);
            this.grbTimPhieu.TabIndex = 3;
            this.grbTimPhieu.TabStop = false;
            this.grbTimPhieu.Text = "Tìm Phiếu ";
            // 
            // btnTim
            // 
            this.btnTim.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTim.BackgroundImage")));
            this.btnTim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTim.Location = new System.Drawing.Point(60, 137);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(72, 36);
            this.btnTim.TabIndex = 12;
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(22, 76);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(148, 30);
            this.txtTimKiem.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MediumAquamarine;
            this.panel2.Controls.Add(this.paChucNangAdmin);
            this.panel2.Controls.Add(this.btnXuatPhieu);
            this.panel2.Controls.Add(this.btnHuy);
            this.panel2.Location = new System.Drawing.Point(750, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 534);
            this.panel2.TabIndex = 1;
            // 
            // paChucNangAdmin
            // 
            this.paChucNangAdmin.Controls.Add(this.btnDaTra);
            this.paChucNangAdmin.Controls.Add(this.btnDangMuon);
            this.paChucNangAdmin.Controls.Add(this.btnPhat);
            this.paChucNangAdmin.Location = new System.Drawing.Point(26, 106);
            this.paChucNangAdmin.Name = "paChucNangAdmin";
            this.paChucNangAdmin.Size = new System.Drawing.Size(200, 253);
            this.paChucNangAdmin.TabIndex = 2;
            // 
            // btnDaTra
            // 
            this.btnDaTra.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDaTra.Location = new System.Drawing.Point(3, 3);
            this.btnDaTra.Name = "btnDaTra";
            this.btnDaTra.Size = new System.Drawing.Size(194, 41);
            this.btnDaTra.TabIndex = 3;
            this.btnDaTra.Text = "Đã Trả";
            this.btnDaTra.UseVisualStyleBackColor = true;
            this.btnDaTra.Click += new System.EventHandler(this.btnDaTra_Click);
            // 
            // btnDangMuon
            // 
            this.btnDangMuon.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangMuon.Location = new System.Drawing.Point(3, 90);
            this.btnDangMuon.Name = "btnDangMuon";
            this.btnDangMuon.Size = new System.Drawing.Size(194, 41);
            this.btnDangMuon.TabIndex = 4;
            this.btnDangMuon.Text = "Đang mượn";
            this.btnDangMuon.UseVisualStyleBackColor = true;
            this.btnDangMuon.Click += new System.EventHandler(this.btnDangMuon_Click);
            // 
            // btnPhat
            // 
            this.btnPhat.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhat.Location = new System.Drawing.Point(3, 176);
            this.btnPhat.Name = "btnPhat";
            this.btnPhat.Size = new System.Drawing.Size(194, 41);
            this.btnPhat.TabIndex = 5;
            this.btnPhat.Text = "Phạt";
            this.btnPhat.UseVisualStyleBackColor = true;
            this.btnPhat.Click += new System.EventHandler(this.btnPhat_Click);
            // 
            // btnXuatPhieu
            // 
            this.btnXuatPhieu.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatPhieu.Location = new System.Drawing.Point(16, 31);
            this.btnXuatPhieu.Name = "btnXuatPhieu";
            this.btnXuatPhieu.Size = new System.Drawing.Size(224, 41);
            this.btnXuatPhieu.TabIndex = 14;
            this.btnXuatPhieu.Text = "Xuất Phiếu Trả";
            this.btnXuatPhieu.UseVisualStyleBackColor = true;
            this.btnXuatPhieu.Click += new System.EventHandler(this.btnXuatPhieu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Font = new System.Drawing.Font("Times New Roman", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Location = new System.Drawing.Point(85, 392);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(89, 41);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Location = new System.Drawing.Point(209, 317);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(535, 218);
            this.panel3.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtbNgaytra);
            this.groupBox2.Controls.Add(this.txtMaSach);
            this.groupBox2.Controls.Add(this.txtMaPhieu);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(529, 212);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Phiếu Trả";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ngày Trả";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mã Sách";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mã Phiếu Trả";
            // 
            // dtbNgaytra
            // 
            this.dtbNgaytra.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtbNgaytra.Location = new System.Drawing.Point(142, 143);
            this.dtbNgaytra.Name = "dtbNgaytra";
            this.dtbNgaytra.Size = new System.Drawing.Size(322, 26);
            this.dtbNgaytra.TabIndex = 2;
            // 
            // txtMaSach
            // 
            this.txtMaSach.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaSach.Location = new System.Drawing.Point(142, 88);
            this.txtMaSach.Name = "txtMaSach";
            this.txtMaSach.Size = new System.Drawing.Size(322, 28);
            this.txtMaSach.TabIndex = 1;
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaPhieu.Location = new System.Drawing.Point(142, 43);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.Size = new System.Drawing.Size(322, 28);
            this.txtMaPhieu.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.dgv);
            this.panel4.Location = new System.Drawing.Point(209, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(535, 308);
            this.panel4.TabIndex = 3;
            // 
            // dgv
            // 
            this.dgv.BackgroundColor = System.Drawing.Color.Teal;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(3, 3);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 62;
            this.dgv.RowTemplate.Height = 28;
            this.dgv.Size = new System.Drawing.Size(529, 302);
            this.dgv.TabIndex = 13;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // entityCommand1
            // 
            this.entityCommand1.CommandTimeout = 0;
            this.entityCommand1.CommandTree = null;
            this.entityCommand1.Connection = null;
            this.entityCommand1.EnablePlanCaching = true;
            this.entityCommand1.Transaction = null;
            // 
            // frmPhieuTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 537);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmPhieuTra";
            this.Text = "frmPhieuTra";
            this.Load += new System.EventHandler(this.frmPhieuTra_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbTimPhieu.ResumeLayout(false);
            this.grbTimPhieu.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.paChucNangAdmin.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grbTimPhieu;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.RadioButton rdbNgayTra;
        private System.Windows.Forms.RadioButton rdbMaSach;
        private System.Windows.Forms.RadioButton rdbMaPhieu;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtbNgaytra;
        private System.Windows.Forms.TextBox txtMaSach;
        private System.Windows.Forms.TextBox txtMaPhieu;
        private System.Windows.Forms.DataGridView dgv;
        private System.Data.Entity.Core.EntityClient.EntityCommand entityCommand1;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnXuatPhieu;
        private System.Windows.Forms.RadioButton rdbMaDG;
        private System.Windows.Forms.Panel paChucNangAdmin;
        private System.Windows.Forms.Button btnDaTra;
        private System.Windows.Forms.Button btnDangMuon;
        private System.Windows.Forms.Button btnPhat;
    }
}