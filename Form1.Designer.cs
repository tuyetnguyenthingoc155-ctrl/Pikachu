using System;

namespace Pikachu
{
    partial class Form1
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
            this.panelBoard = new System.Windows.Forms.Panel();
            this.bxh = new System.Windows.Forms.Button();
            this.choilai = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.Label();
            this.diem = new System.Windows.Forms.Label();
            this.diemkyluc = new System.Windows.Forms.Label();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelOverPlay = new System.Windows.Forms.Panel();
            this.btnToggleMusic = new System.Windows.Forms.Button();
            this.huongdan = new System.Windows.Forms.Button();
            this.panelBoard.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBoard
            // 
            this.panelBoard.Controls.Add(this.huongdan);
            this.panelBoard.Controls.Add(this.btnToggleMusic);
            this.panelBoard.Controls.Add(this.bxh);
            this.panelBoard.Controls.Add(this.choilai);
            this.panelBoard.Controls.Add(this.Time);
            this.panelBoard.Controls.Add(this.diem);
            this.panelBoard.Controls.Add(this.diemkyluc);
            this.panelBoard.Location = new System.Drawing.Point(4, 4);
            this.panelBoard.Name = "panelBoard";
            this.panelBoard.Size = new System.Drawing.Size(790, 105);
            this.panelBoard.TabIndex = 0;
            // 
            // bxh
            // 
            this.bxh.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bxh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bxh.Location = new System.Drawing.Point(508, 15);
            this.bxh.Name = "bxh";
            this.bxh.Size = new System.Drawing.Size(165, 75);
            this.bxh.TabIndex = 4;
            this.bxh.Text = "Bảng Xếp Hạng";
            this.bxh.UseVisualStyleBackColor = false;
            this.bxh.Click += new System.EventHandler(this.btnBXH_Click);
            // 
            // choilai
            // 
            this.choilai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.choilai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choilai.Location = new System.Drawing.Point(679, 16);
            this.choilai.Name = "choilai";
            this.choilai.Size = new System.Drawing.Size(94, 74);
            this.choilai.TabIndex = 3;
            this.choilai.Text = "Chơi Lại";
            this.choilai.UseVisualStyleBackColor = false;
            this.choilai.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // Time
            // 
            this.Time.AutoSize = true;
            this.Time.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time.ForeColor = System.Drawing.Color.Red;
            this.Time.Location = new System.Drawing.Point(264, 20);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(132, 25);
            this.Time.TabIndex = 2;
            this.Time.Text = "Thời gian: 0";
            // 
            // diem
            // 
            this.diem.AutoSize = true;
            this.diem.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.diem.Location = new System.Drawing.Point(10, 67);
            this.diem.Name = "diem";
            this.diem.Size = new System.Drawing.Size(90, 25);
            this.diem.TabIndex = 1;
            this.diem.Text = "Điểm: 0";
            // 
            // diemkyluc
            // 
            this.diemkyluc.AutoSize = true;
            this.diemkyluc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.diemkyluc.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diemkyluc.ForeColor = System.Drawing.Color.Indigo;
            this.diemkyluc.Location = new System.Drawing.Point(8, 15);
            this.diemkyluc.Name = "diemkyluc";
            this.diemkyluc.Size = new System.Drawing.Size(204, 32);
            this.diemkyluc.TabIndex = 0;
            this.diemkyluc.Text = "Điểm Kỷ Lục: 0";
            this.diemkyluc.Click += new System.EventHandler(this.diemkyluc_Click);
            // 
            // panelMiddle
            // 
            this.panelMiddle.Controls.Add(this.flowLayoutPanel1);
            this.panelMiddle.Location = new System.Drawing.Point(6, 121);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new System.Drawing.Size(787, 374);
            this.panelMiddle.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(692, 59);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(7, 6);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // panelOverPlay
            // 
            this.panelOverPlay.Location = new System.Drawing.Point(11, 114);
            this.panelOverPlay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelOverPlay.Name = "panelOverPlay";
            this.panelOverPlay.Size = new System.Drawing.Size(14, 8);
            this.panelOverPlay.TabIndex = 0;
            this.panelOverPlay.Paint += new System.Windows.Forms.PaintEventHandler(this.panelOverPlay_Paint);
            // 
            // btnToggleMusic
            // 
            this.btnToggleMusic.BackgroundImage = global::Pikachu.Properties.Resources.mở_nhạc;
            this.btnToggleMusic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnToggleMusic.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleMusic.Location = new System.Drawing.Point(443, 31);
            this.btnToggleMusic.Name = "btnToggleMusic";
            this.btnToggleMusic.Size = new System.Drawing.Size(46, 45);
            this.btnToggleMusic.TabIndex = 5;
            this.btnToggleMusic.UseVisualStyleBackColor = true;
            this.btnToggleMusic.Click += new System.EventHandler(this.button1_Click);
            // 
            // huongdan
            // 
            this.huongdan.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.huongdan.Location = new System.Drawing.Point(269, 56);
            this.huongdan.Name = "huongdan";
            this.huongdan.Size = new System.Drawing.Size(113, 36);
            this.huongdan.TabIndex = 6;
            this.huongdan.Text = "Hướng dẫn";
            this.huongdan.UseVisualStyleBackColor = true;
            this.huongdan.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 507);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelOverPlay);
            this.Controls.Add(this.panelBoard);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelBoard.ResumeLayout(false);
            this.panelBoard.PerformLayout();
            this.panelMiddle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        
        
        #endregion

        private System.Windows.Forms.Panel panelBoard;
        private System.Windows.Forms.Button bxh;
        private System.Windows.Forms.Button choilai;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Label diem;
        private System.Windows.Forms.Label diemkyluc;
        private System.Windows.Forms.Panel panelMiddle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelOverPlay;
        private System.Windows.Forms.Button btnToggleMusic;
        private System.Windows.Forms.Button huongdan;
    }
}

