namespace Pikachu
{
    partial class FormBXH
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
            this.dataGVLeaderBoard = new System.Windows.Forms.DataGridView();
            this.BXH = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVLeaderBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGVLeaderBoard
            // 
            this.dataGVLeaderBoard.AllowUserToAddRows = false;
            this.dataGVLeaderBoard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGVLeaderBoard.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGVLeaderBoard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGVLeaderBoard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVLeaderBoard.Location = new System.Drawing.Point(11, 44);
            this.dataGVLeaderBoard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGVLeaderBoard.Name = "dataGVLeaderBoard";
            this.dataGVLeaderBoard.ReadOnly = true;
            this.dataGVLeaderBoard.RowHeadersWidth = 62;
            this.dataGVLeaderBoard.RowTemplate.Height = 28;
            this.dataGVLeaderBoard.Size = new System.Drawing.Size(690, 306);
            this.dataGVLeaderBoard.TabIndex = 0;
            this.dataGVLeaderBoard.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGVLeaderBoard_CellContentClick);
            // 
            // BXH
            // 
            this.BXH.AutoSize = true;
            this.BXH.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BXH.Location = new System.Drawing.Point(241, 7);
            this.BXH.Name = "BXH";
            this.BXH.Size = new System.Drawing.Size(196, 29);
            this.BXH.TabIndex = 1;
            this.BXH.Text = "Bảng Xếp Hạng";
            // 
            // FormBXH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 360);
            this.Controls.Add(this.BXH);
            this.Controls.Add(this.dataGVLeaderBoard);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormBXH";
            this.Text = "FormBXH";
            this.Load += new System.EventHandler(this.FormBXH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGVLeaderBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGVLeaderBoard;
        private System.Windows.Forms.Label BXH;
    }
}