namespace English2.Views.lopHoc
{
    partial class lh_CTLopHoc
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnMainHS = new System.Windows.Forms.Panel();
            this.tabGV = new System.Windows.Forms.TabPage();
            this.pnGV = new System.Windows.Forms.Panel();
            this.tabLichHoc = new System.Windows.Forms.TabPage();
            this.pnMainLich = new System.Windows.Forms.Panel();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lbLop = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picBack = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabGV.SuspendLayout();
            this.tabLichHoc.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBack)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1079, 756);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.materialTabControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.materialTabSelector1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.15873F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.539682F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.43386F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1079, 756);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabGV);
            this.materialTabControl1.Controls.Add(this.tabLichHoc);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTabControl1.Location = new System.Drawing.Point(0, 94);
            this.materialTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(1079, 662);
            this.materialTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pnMainHS);
            this.tabPage1.Location = new System.Drawing.Point(4, 36);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1071, 622);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Danh sách học sinh";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Enter += new System.EventHandler(this.tabPage1_Enter);
            this.tabPage1.Leave += new System.EventHandler(this.tabPage1_Leave);
            // 
            // pnMainHS
            // 
            this.pnMainHS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMainHS.Location = new System.Drawing.Point(3, 3);
            this.pnMainHS.Margin = new System.Windows.Forms.Padding(0);
            this.pnMainHS.Name = "pnMainHS";
            this.pnMainHS.Size = new System.Drawing.Size(1065, 616);
            this.pnMainHS.TabIndex = 0;
            // 
            // tabGV
            // 
            this.tabGV.Controls.Add(this.pnGV);
            this.tabGV.Location = new System.Drawing.Point(4, 36);
            this.tabGV.Name = "tabGV";
            this.tabGV.Size = new System.Drawing.Size(1071, 622);
            this.tabGV.TabIndex = 1;
            this.tabGV.Text = "Danh sách giáo viên";
            this.tabGV.UseVisualStyleBackColor = true;
            this.tabGV.Enter += new System.EventHandler(this.tabGV_Enter);
            this.tabGV.Leave += new System.EventHandler(this.tabGV_Leave);
            // 
            // pnGV
            // 
            this.pnGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnGV.Location = new System.Drawing.Point(0, 0);
            this.pnGV.Name = "pnGV";
            this.pnGV.Size = new System.Drawing.Size(1071, 622);
            this.pnGV.TabIndex = 1;
            // 
            // tabLichHoc
            // 
            this.tabLichHoc.Controls.Add(this.pnMainLich);
            this.tabLichHoc.Location = new System.Drawing.Point(4, 36);
            this.tabLichHoc.Name = "tabLichHoc";
            this.tabLichHoc.Padding = new System.Windows.Forms.Padding(3);
            this.tabLichHoc.Size = new System.Drawing.Size(1071, 622);
            this.tabLichHoc.TabIndex = 2;
            this.tabLichHoc.Text = "Lịch học";
            this.tabLichHoc.UseVisualStyleBackColor = true;
            this.tabLichHoc.Enter += new System.EventHandler(this.tabLichHoc_Enter);
            this.tabLichHoc.Leave += new System.EventHandler(this.tabLichHoc_Leave);
            // 
            // pnMainLich
            // 
            this.pnMainLich.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMainLich.Location = new System.Drawing.Point(3, 3);
            this.pnMainLich.Margin = new System.Windows.Forms.Padding(0);
            this.pnMainLich.Name = "pnMainLich";
            this.pnMainLich.Size = new System.Drawing.Size(1065, 616);
            this.pnMainLich.TabIndex = 0;
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialTabSelector1.Location = new System.Drawing.Point(3, 41);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(1073, 50);
            this.materialTabSelector1.TabIndex = 1;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1079, 38);
            this.panel2.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56F));
            this.tableLayoutPanel6.Controls.Add(this.lbLop, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.picBack, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1079, 38);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // lbLop
            // 
            this.lbLop.AutoSize = true;
            this.lbLop.BackColor = System.Drawing.Color.MediumTurquoise;
            this.lbLop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLop.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbLop.Location = new System.Drawing.Point(474, 0);
            this.lbLop.Margin = new System.Windows.Forms.Padding(0);
            this.lbLop.Name = "lbLop";
            this.lbLop.Size = new System.Drawing.Size(605, 38);
            this.lbLop.TabIndex = 6;
            this.lbLop.Text = "A";
            this.lbLop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(431, 38);
            this.label1.TabIndex = 5;
            this.label1.Text = "LỚP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picBack
            // 
            this.picBack.BackColor = System.Drawing.Color.MediumTurquoise;
            this.picBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBack.Image = global::English2.Properties.Resources.arrow;
            this.picBack.Location = new System.Drawing.Point(0, 0);
            this.picBack.Margin = new System.Windows.Forms.Padding(0);
            this.picBack.Name = "picBack";
            this.picBack.Size = new System.Drawing.Size(43, 38);
            this.picBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBack.TabIndex = 2;
            this.picBack.TabStop = false;
            this.picBack.Click += new System.EventHandler(this.picBack_Click);
            // 
            // lh_CTLopHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1079, 756);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "lh_CTLopHoc";
            this.Text = "lh_CTLopHoc";
            this.Load += new System.EventHandler(this.lh_CTLopHoc_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabGV.ResumeLayout(false);
            this.tabLichHoc.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel pnMainHS;
        private System.Windows.Forms.TabPage tabGV;
        private System.Windows.Forms.Panel pnGV;
        private System.Windows.Forms.TabPage tabLichHoc;
        private System.Windows.Forms.Panel pnMainLich;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBack;
        public MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        public System.Windows.Forms.Label lbLop;
    }
}