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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        public string UserName { get; set; }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string userName = tbDangNhap.Text.Trim();

            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Đường dẫn file lưu tài khoản nằm trong thư mục chạy của chương trình (bin/Debug)
                string filePath = Application.StartupPath + "\\accounts.txt";

                // Kiểm tra xem file đã được tạo chưa
                if (!File.Exists(filePath))
                {
                    MessageBox.Show(
                        "Tên đăng nhập không tồn tại. Vui lòng đăng ký trước.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Đọc toàn bộ danh sách tài khoản từ file
                string[] lines = File.ReadAllLines(filePath);
                bool isLoginSuccess = false;

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // So sánh tên nhập vào với tên lưu trong file
                    if (line.Trim().Equals(userName, StringComparison.OrdinalIgnoreCase))
                    {
                        isLoginSuccess = true;
                        break;
                    }
                }

                if (isLoginSuccess)
                {
                    UserName = userName;

                    // Chuyển sang Form1 (Game)
                    Form1 game = new Form1(UserName);
                    this.Hide();
                    game.ShowDialog();
                    this.Show(); // Hiện lại form đăng nhập sau khi đóng game
                }
                else
                {
                    MessageBox.Show(
                        "Tên đăng nhập không tồn tại. Vui lòng đăng ký trước.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi đăng nhập: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            using (FormSignin formsignin = new FormSignin())
            {
                formsignin.ShowDialog();
            }
        }

        private void DangNhap_Click(object sender, EventArgs e)
        {

        }
    }
}