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

namespace Pikachu
{
    public partial class FormBXH : Form
    {
        public FormBXH()
        {
            InitializeComponent();
        }

        private void FormBXH_Load(object sender, EventArgs e)
        {
            try
            {
                string scoreFilePath = Application.StartupPath + "\\highscores.txt";

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("UserName", typeof(string));
                dataTable.Columns.Add("Score", typeof(int));
                dataTable.Columns.Add("Time", typeof(string));

                if (File.Exists(scoreFilePath))
                {
                    string[] lines = File.ReadAllLines(scoreFilePath);
                    var allScores = new List<PlayerScore>();

                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split(',');

                        if (parts.Length >= 2)
                        {
                            if (int.TryParse(parts[1], out int score))
                            {
                                int timeSpent = 9999; // Mặc định thời gian cực lớn nếu dữ liệu cũ bị thiếu

                                // Đọc số giây đã chơi
                                if (parts.Length >= 3 && int.TryParse(parts[2], out int parsedTime))
                                {
                                    timeSpent = parsedTime;
                                }

                                allScores.Add(new PlayerScore { UserName = parts[0], Score = score, TimeSpent = timeSpent });
                            }
                        }
                    }

                    // Lọc điểm cao nhất của mỗi người. Nếu điểm bằng nhau thì chọn lần chơi tốn ít thời gian nhất
                    var topScores = allScores
                        .GroupBy(p => p.UserName)
                        .Select(g => g.OrderByDescending(p => p.Score).ThenBy(p => p.TimeSpent).First())
                        .OrderByDescending(x => x.Score)
                        .ThenBy(x => x.TimeSpent) // Sắp xếp: Ưu tiên thời gian nhỏ hơn
                        .ToList();

                    // Đưa dữ liệu vào bảng
                    foreach (var item in topScores)
                    {
                        // Nếu là 9999 (dữ liệu cũ không có thời gian), hiển thị "Không rõ"
                        string timeDisplay = item.TimeSpent == 9999 ? "Không rõ" : $"{item.TimeSpent} giây";
                        dataTable.Rows.Add(item.UserName, item.Score, timeDisplay);
                    }
                }

                // Gán dữ liệu vào GridView
                dataGVLeaderBoard.DataSource = dataTable;

                // Đổi tên cột hiển thị tiếng Việt
                if (dataGVLeaderBoard.Columns.Contains("UserName"))
                    dataGVLeaderBoard.Columns["UserName"].HeaderText = "Tên người chơi";

                if (dataGVLeaderBoard.Columns.Contains("Score"))
                    dataGVLeaderBoard.Columns["Score"].HeaderText = "Số điểm kỷ lục";

                if (dataGVLeaderBoard.Columns.Contains("Time"))
                    dataGVLeaderBoard.Columns["Time"].HeaderText = "Thời gian hoàn thành";

                // Tự động kéo giãn các cột
                dataGVLeaderBoard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có dữ liệu điểm để hiển thị.", "Bảng xếp hạng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải bảng xếp hạng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGVLeaderBoard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Lớp phụ trợ để lưu trữ thời gian (số nguyên) giúp dễ dàng sắp xếp
        private class PlayerScore
        {
            public string UserName { get; set; }
            public int Score { get; set; }
            public int TimeSpent { get; set; }
        }
    }
}