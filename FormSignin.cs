using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; // Thêm thư viện này để đọc/ghi file text
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pikachu
{
    public partial class FormSignin : Form
    {
        public FormSignin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string username = tbDangKy.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng ký.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đường dẫn file lưu tài khoản nằm trong thư mục chạy của chương trình (bin/Debug)
            string filePath = Application.StartupPath + "\\accounts.txt";

            try
            {
                // 1. Kiểm tra xem tên đăng ký đã tồn tại chưa
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        // So sánh tên nhập vào với các tên đã lưu trong file
                        if (line.Trim().Equals(username, StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Tên đăng ký đã tồn tại. Vui lòng chọn tên khác.",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // 2. Ghi tên đăng ký mới vào file text
                File.AppendAllText(filePath, username + Environment.NewLine);

                MessageBox.Show("Đăng ký thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi đăng ký: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbDangKy_TextChanged(object sender, EventArgs e)
        {

        }
    }
}