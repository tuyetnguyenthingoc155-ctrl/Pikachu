using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pikachu
{
    // ==========================================
    // PHẦN 1: CLASS FORM CHÍNH (NẰM TRÊN CÙNG)
    // ==========================================
    public partial class Form1 : Form
    {
        int currentLevel = 1; // Bắt đầu từ màn 1
        int rows = 6;
        int cols = 10;
        int tileSize = 35;
        Image[] congiapImages;
        PictureBox[,] congiappictureBox;
        Random random = new Random();

        int[,] boardMap;

        PictureBox selected1 = null;
        PictureBox selected2 = null;
        bool isProcessing = false;
        private List<Point> connectionPoints;

        private string userName;
        private bool daLuuDiem = false;
        private int score = 0;
        private int timeLeft = 300;
        private Timer countdownTimer;

        private SoundPlayer bgMusicPlayer;
        private bool isMusicPlaying = false;
        private bool isPaused = false;

        private string scoreFilePath = Application.StartupPath + "\\highscores.txt";

        public Form1(string currentUsername)
        {
            userName = currentUsername;
            InitializeComponent();

            this.FormClosing += Form1_FormClosing;
            this.Resize += Form1_Resize;

            panelMiddle.Size = new Size((cols + 2) * tileSize, (rows + 2) * tileSize);

            Panel oldPanel = panelOverPlay;
            panelOverPlay = new TransparentPanel();
            panelOverPlay.Name = "panelOverPlay";
            panelOverPlay.Location = new Point(0, 0);
            panelOverPlay.Size = panelMiddle.Size;
            panelOverPlay.Paint += panelOverPlay_Paint;

            if (oldPanel != null && oldPanel.Parent != null)
            {
                oldPanel.Parent.Controls.Remove(oldPanel);
            }
            panelMiddle.Controls.Add(panelOverPlay);
            panelOverPlay.BringToFront();

            connectionPoints = new List<Point>();
            LoadConGiapImage();
            Initializeboard();

            countdownTimer = new System.Windows.Forms.Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();

            PlayBackgroundMusic();
            UpdateBoardSize();

            this.Text = "Game Pikachu - Màn " + currentLevel;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            UpdateBoardSize();
        }

        private void UpdateBoardSize()
        {
            if (congiappictureBox == null || panelMiddle == null) return;

            int availableWidth = this.ClientSize.Width - 40;
            int availableHeight = this.ClientSize.Height - panelMiddle.Top - 20;

            if (availableWidth <= 0 || availableHeight <= 0) return;

            int newTileSize = Math.Min(availableWidth / (cols + 2), availableHeight / (rows + 2));
            if (newTileSize < 20) newTileSize = 20;

            tileSize = newTileSize;

            panelMiddle.Size = new Size((cols + 2) * tileSize, (rows + 2) * tileSize);
            if (panelOverPlay != null)
            {
                panelOverPlay.Size = panelMiddle.Size;
            }

            panelMiddle.Left = (this.ClientSize.Width - panelMiddle.Width) / 2;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (congiappictureBox[i, j] != null)
                    {
                        congiappictureBox[i, j].Width = tileSize;
                        congiappictureBox[i, j].Height = tileSize;
                        congiappictureBox[i, j].Left = (j + 1) * tileSize;
                        congiappictureBox[i, j].Top = (i + 1) * tileSize;
                    }
                }
            }

            if (panelOverPlay != null) panelOverPlay.Invalidate();
        }

        private void PlayBackgroundMusic()
        {
            try
            {
                if (Properties.Resources.nhacnen != null)
                {
                    bgMusicPlayer = new SoundPlayer(Properties.Resources.nhacnen);
                    bgMusicPlayer.PlayLooping();
                    isMusicPlaying = true;

                    btnToggleMusic.BackgroundImage = Properties.Resources.mở_nhạc;
                    btnToggleMusic.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Không thể phát nhạc nền: " + ex.Message);
            }
        }

        private void LoadConGiapImage()
        {
            congiapImages = new Image[]
            {
                Properties.Resources.conchuot,
                Properties.Resources.contrau,
                Properties.Resources.conho,
                Properties.Resources.contho,
                Properties.Resources.conrong,
                Properties.Resources.conran,
                Properties.Resources.conngua,
                Properties.Resources.concuu,
                Properties.Resources.conkhi,
                Properties.Resources.conga,
                Properties.Resources.concho,
                Properties.Resources.conheo,
            };
        }

        private void Initializeboard()
        {
            congiappictureBox = new PictureBox[rows, cols];
            boardMap = new int[rows, cols];

            List<int> congiapIndexes = new List<int>();
            for (int i = 0; i < (rows * cols) / 2; i++)
            {
                int index = i % congiapImages.Length;
                congiapIndexes.Add(index);
                congiapIndexes.Add(index);
            }

            ShuffleList(congiapIndexes);

            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Width = tileSize;
                    pb.Height = tileSize;

                    pb.Left = (j + 1) * tileSize;
                    pb.Top = (i + 1) * tileSize;

                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.BorderStyle = BorderStyle.FixedSingle;

                    int imangeIndex = congiapIndexes[k++];

                    pb.Image = congiapImages[imangeIndex];
                    boardMap[i, j] = imangeIndex;

                    panelMiddle.Controls.Add(pb);
                    congiappictureBox[i, j] = pb;

                    pb.Click += picturebox_Click;
                }
            }

            while (!HasAnyValidMove())
            {
                ShuffleBoardValues();
            }

            if (panelOverPlay != null)
            {
                panelOverPlay.BringToFront();
            }
        }

        private void ShuffleList<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void ShuffleBoardValues()
        {
            List<int> remainingValues = new List<int>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (boardMap[i, j] != -1)
                    {
                        remainingValues.Add(boardMap[i, j]);
                    }
                }
            }

            ShuffleList(remainingValues);

            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (boardMap[i, j] != -1)
                    {
                        boardMap[i, j] = remainingValues[k++];
                        congiappictureBox[i, j].Image = congiapImages[boardMap[i, j]];
                    }
                }
            }
        }

        private bool HasAnyValidMove()
        {
            for (int r1 = 0; r1 < rows; r1++)
            {
                for (int c1 = 0; c1 < cols; c1++)
                {
                    if (boardMap[r1, c1] == -1) continue;
                    for (int r2 = 0; r2 < rows; r2++)
                    {
                        for (int c2 = 0; c2 < cols; c2++)
                        {
                            if (boardMap[r2, c2] == -1) continue;
                            if (r1 == r2 && c1 == c2) continue;

                            if (boardMap[r1, c1] == boardMap[r2, c2])
                            {
                                Point p1 = new Point(r1, c1);
                                Point p2 = new Point(r2, c2);
                                var path = ConnectStraight(p1, p2) ?? ConnectL(p1, p2) ?? Connect3Line(p1, p2);
                                if (path != null) return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private async Task ResetSelectionAsync()
        {
            await Task.Delay(400);

            if (selected1 != null) selected1.BorderStyle = BorderStyle.FixedSingle;
            if (selected2 != null) selected2.BorderStyle = BorderStyle.FixedSingle;

            selected1 = null;
            selected2 = null;
        }

        // ===============================================
        // ĐÃ THÊM: HÀM DỒN Ô (LÙI TRÁI, PHẢI, TRÊN, DƯỚI)
        // ===============================================
        private void ShiftTiles(int level)
        {
            bool hasChanged = false;

            if (level == 2) // Lùi xuống dưới
            {
                for (int c = 0; c < cols; c++)
                {
                    for (int r = rows - 1; r >= 0; r--)
                    {
                        if (boardMap[r, c] == -1)
                        {
                            for (int k = r - 1; k >= 0; k--)
                            {
                                if (boardMap[k, c] != -1)
                                {
                                    boardMap[r, c] = boardMap[k, c];
                                    boardMap[k, c] = -1;
                                    hasChanged = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (level == 3) // Lùi lên trên
            {
                for (int c = 0; c < cols; c++)
                {
                    for (int r = 0; r < rows; r++)
                    {
                        if (boardMap[r, c] == -1)
                        {
                            for (int k = r + 1; k < rows; k++)
                            {
                                if (boardMap[k, c] != -1)
                                {
                                    boardMap[r, c] = boardMap[k, c];
                                    boardMap[k, c] = -1;
                                    hasChanged = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (level == 4) // Lùi sang trái
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (boardMap[r, c] == -1)
                        {
                            for (int k = c + 1; k < cols; k++)
                            {
                                if (boardMap[r, k] != -1)
                                {
                                    boardMap[r, c] = boardMap[r, k];
                                    boardMap[r, k] = -1;
                                    hasChanged = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (level >= 5) // Lùi sang phải
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int c = cols - 1; c >= 0; c--)
                    {
                        if (boardMap[r, c] == -1)
                        {
                            for (int k = c - 1; k >= 0; k--)
                            {
                                if (boardMap[r, k] != -1)
                                {
                                    boardMap[r, c] = boardMap[r, k];
                                    boardMap[r, k] = -1;
                                    hasChanged = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // Nếu có sự dịch chuyển ô, thì vẽ lại tất cả các hình
            if (hasChanged)
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (boardMap[r, c] == -1)
                        {
                            congiappictureBox[r, c].Image = null;
                        }
                        else
                        {
                            congiappictureBox[r, c].Image = congiapImages[boardMap[r, c]];
                        }
                    }
                }
            }
        }


        private async void picturebox_Click(object sender, EventArgs e)
        {
            if (isProcessing || isPaused) return;

            PictureBox selected = sender as PictureBox;

            if (selected == null || selected.Image == null)
            {
                return;
            }

            if (selected1 == selected)
            {
                selected1.BorderStyle = BorderStyle.FixedSingle;
                selected1 = null;
                return;
            }

            if (selected1 == null)
            {
                selected1 = selected;
                selected1.BorderStyle = BorderStyle.Fixed3D;
                return;
            }

            if (selected2 == null && selected != selected1)
            {
                selected2 = selected;
                selected2.BorderStyle = BorderStyle.Fixed3D;

                isProcessing = true;

                Point pos1 = GetPictureBoxPosition(selected1);
                Point pos2 = GetPictureBoxPosition(selected2);

                if (boardMap[pos1.X, pos1.Y] == boardMap[pos2.X, pos2.Y])
                {
                    var path = ConnectStraight(pos1, pos2) ?? ConnectL(pos1, pos2) ?? Connect3Line(pos1, pos2);
                    if (path != null)
                    {
                        connectionPoints = path.Select(p =>
                        {
                            if (p.X >= 0 && p.X < rows && p.Y >= 0 && p.Y < cols)
                            {
                                PictureBox pb = congiappictureBox[p.X, p.Y];
                                if (pb != null)
                                {
                                    return new Point(pb.Left + pb.Width / 2, pb.Top + pb.Height / 2);
                                }
                            }
                            return new Point((p.Y + 1) * tileSize + tileSize / 2, (p.X + 1) * tileSize + tileSize / 2);
                        }).ToList();

                        panelOverPlay.Invalidate();
                        panelOverPlay.Update();

                        await Task.Delay(300);

                        boardMap[pos1.X, pos1.Y] = -1;
                        boardMap[pos2.X, pos2.Y] = -1;

                        selected1.Image = null;
                        selected2.Image = null;

                        selected1.BorderStyle = BorderStyle.FixedSingle;
                        selected2.BorderStyle = BorderStyle.FixedSingle;

                        selected1 = null;
                        selected2 = null;

                        // ===============================================
                        // GỌI HÀM DỒN Ô SAU KHI XÓA CẶP HÌNH THÀNH CÔNG
                        // ===============================================
                        ShiftTiles(currentLevel);

                        connectionPoints.Clear();
                        panelOverPlay.Invalidate();
                        panelMiddle.Invalidate(true);
                        panelMiddle.Update();

                        score += 10;
                        diem.Text = "Điểm: " + score;

                        if (IsBoardCleaned())
                        {
                            countdownTimer.Stop();
                            SavePlayerScore(userName, score);

                            DialogResult result = MessageBox.Show(
                                "Chúc mừng bạn đã vượt qua màn " + currentLevel + "!\nĐiểm hiện tại: " + score + "\nBạn có muốn chơi tiếp màn sau không?",
                                "Qua màn thành công",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );

                            if (result == DialogResult.Yes)
                            {
                                ResetGame(true);
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                        else
                        {
                            // Kiểm tra lại đường đi hợp lệ sau khi dồn ô
                            while (!IsBoardCleaned() && !HasAnyValidMove())
                            {
                                ShuffleBoardValues();
                                MessageBox.Show("Hết nước đi! Bàn chơi đã được tự động xáo trộn lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        await ResetSelectionAsync();
                    }
                }
                else
                {
                    await ResetSelectionAsync();
                }

                isProcessing = false;
            }
        }

        private int GetBoardValue(int x, int y)
        {
            if (x == -1 || y == -1 || x == rows || y == cols)
            {
                return -1;
            }
            if (x < -1 || x > rows || y < -1 || y > cols)
            {
                return -2;
            }
            return boardMap[x, y];
        }

        private List<Point> ConnectStraight(Point a, Point b)
        {
            if (a.X == b.X)
            {
                int minY = Math.Min(a.Y, b.Y);
                int maxY = Math.Max(a.Y, b.Y);
                for (int y = minY + 1; y < maxY; y++)
                {
                    if (GetBoardValue(a.X, y) != -1)
                    {
                        return null;
                    }
                }
                return new List<Point> { a, b };
            }
            else if (a.Y == b.Y)
            {
                int minX = Math.Min(a.X, b.X);
                int maxX = Math.Max(a.X, b.X);
                for (int x = minX + 1; x < maxX; x++)
                {
                    if (GetBoardValue(x, a.Y) != -1)
                    {
                        return null;
                    }
                }
                return new List<Point> { a, b };
            }
            return null;
        }

        private Point GetPictureBoxPosition(PictureBox pb)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (congiappictureBox[i, j] == pb)
                    {
                        return new Point(i, j);
                    }
                }
            }
            return Point.Empty;
        }

        private List<Point> ConnectL(Point a, Point b)
        {
            Point c1 = new Point(a.X, b.Y);
            if (Isempty(c1) && ConnectStraight(a, c1) != null && ConnectStraight(c1, b) != null)
            {
                return new List<Point> { a, c1, b };
            }

            Point c2 = new Point(b.X, a.Y);
            if (Isempty(c2) && ConnectStraight(a, c2) != null && ConnectStraight(c2, b) != null)
            {
                return new List<Point> { a, c2, b };
            }

            return null;
        }

        private bool Isempty(Point p)
        {
            return GetBoardValue(p.X, p.Y) == -1;
        }

        private List<Point> Connect3Line(Point a, Point b)
        {
            for (int i = -1; i <= rows; i++)
            {
                Point mid1 = new Point(i, a.Y);
                Point mid2 = new Point(i, b.Y);
                if (Isempty(mid1) && Isempty(mid2))
                {
                    if (ConnectStraight(a, mid1) != null && ConnectStraight(mid1, mid2) != null && ConnectStraight(mid2, b) != null)
                    {
                        return new List<Point> { a, mid1, mid2, b };
                    }
                }
            }

            for (int j = -1; j <= cols; j++)
            {
                Point mid1 = new Point(a.X, j);
                Point mid2 = new Point(b.X, j);
                if (Isempty(mid1) && Isempty(mid2))
                {
                    if (ConnectStraight(a, mid1) != null && ConnectStraight(mid1, mid2) != null && ConnectStraight(mid2, b) != null)
                    {
                        return new List<Point> { a, mid1, mid2, b };
                    }
                }
            }

            return null;
        }

        private bool IsBoardCleaned()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (boardMap[i, j] != -1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            diem.Text = "Điểm: " + score;
            Time.Text = "Thời gian: " + timeLeft;
            this.Text = "Game Pikachu - Màn " + currentLevel;

            GetHighScore(userName);
        }

        private void diemkyluc_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Điểm hiện tại: " + score);
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            Time.Text = "Thời gian: " + timeLeft;

            if (timeLeft <= 0)
            {
                countdownTimer.Stop();

                SavePlayerScore(userName, score);

                DialogResult result = MessageBox.Show(
                    "Hết thời gian! Bạn dừng lại ở màn " + currentLevel + " với số điểm: " + score + ".\nBạn có muốn chơi lại từ đầu không?",
                    "Game Over",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    ResetGame(false);
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (countdownTimer != null)
            {
                countdownTimer.Stop();
            }
            SavePlayerScore(userName, score);
        }

        private void SavePlayerScore(string userName, int score)
        {
            if (daLuuDiem)
                return;

            daLuuDiem = true;

            try
            {
                int timeSpent = 300 - timeLeft;
                string record = $"{userName},{score},{timeSpent}{Environment.NewLine}";
                File.AppendAllText(scoreFilePath, record);
                GetHighScore(userName);
            }
            catch (Exception ex)
            {
                daLuuDiem = false;
                MessageBox.Show("Có lỗi khi lưu điểm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetHighScore(string userName)
        {
            try
            {
                int highScore = 0;

                if (File.Exists(scoreFilePath))
                {
                    string[] lines = File.ReadAllLines(scoreFilePath);

                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split(',');

                        if (parts.Length >= 2 && parts[0] == userName)
                        {
                            if (int.TryParse(parts[1], out int currentScore))
                            {
                                if (currentScore > highScore)
                                {
                                    highScore = currentScore;
                                }
                            }
                        }
                    }
                }

                diemkyluc.Text = "Điểm Kỷ Lục: " + highScore;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lấy điểm kỷ lục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBXH_Click(object sender, EventArgs e)
        {
            FormBXH formBXH = new FormBXH();
            formBXH.ShowDialog();
        }

        private void ResetGame(bool isContinue = false)
        {
            if (isContinue)
            {
                currentLevel++;
                if (currentLevel == 2) { rows = 8; cols = 12; }
                else if (currentLevel == 3) { rows = 10; cols = 14; }
                else if (currentLevel == 4) { rows = 10; cols = 16; }
                else if (currentLevel >= 5) { rows = 12; cols = 18; }
            }
            else
            {
                currentLevel = 1;
                score = 0;
                rows = 6;
                cols = 10;
            }

            int calculatedTime = 300 - (currentLevel > 4 ? (currentLevel - 4) * 30 : 0);
            timeLeft = Math.Max(calculatedTime, 60);

            panelMiddle.Controls.Clear();

            if (panelOverPlay != null)
            {
                panelMiddle.Controls.Add(panelOverPlay);
            }

            congiappictureBox = new PictureBox[rows, cols];
            boardMap = new int[rows, cols];
            selected1 = null;
            selected2 = null;
            isProcessing = false;
            daLuuDiem = false;

            diem.Text = "Điểm: " + score;
            Time.Text = "Thời gian: " + timeLeft + "s";
            this.Text = "Game Pikachu - Màn " + currentLevel;

            Initializeboard();
            UpdateBoardSize();

            if (countdownTimer != null)
            {
                countdownTimer.Stop();
                countdownTimer.Start();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetGame(false);
        }

        private void panelBoard_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panelOverPlay_Paint(object sender, PaintEventArgs e)
        {
            if (connectionPoints == null || connectionPoints.Count < 2)
                return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (Pen redPen = new Pen(Color.Red, 4))
            {
                for (int i = 0; i < connectionPoints.Count - 1; i++)
                {
                    e.Graphics.DrawLine(
                        redPen,
                        connectionPoints[i],
                        connectionPoints[i + 1]
                    );
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bgMusicPlayer == null) return;

            if (isMusicPlaying)
            {
                bgMusicPlayer.Stop();
                isMusicPlaying = false;
                btnToggleMusic.BackgroundImage = Properties.Resources.tắt_nhạc;
            }
            else
            {
                bgMusicPlayer.PlayLooping();
                isMusicPlaying = true;
                btnToggleMusic.BackgroundImage = Properties.Resources.mở_nhạc;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string luatChoi = "=== LUẬT CHƠI GAME PIKACHU ===\n\n" +
                              "1. Nhiệm vụ: Tìm và nối hai hình con giáp giống nhau để làm sạch bàn chơi trước khi hết thời gian.\n" +
                              "2. Cách nối:\n" +
                              "   - Đường nối thẳng: Không bị hình nào cản giữa hai điểm.\n" +
                              "   - Đường nối hình chữ L: Gập tối đa 1 góc vuông (1 lần đổi hướng).\n" +
                              "   - Đường nối hình chữ Z / 3 đoạn thẳng: Gập tối đa 2 góc vuông (2 lần đổi hướng).\n" +
                              "3. Điểm số: Mỗi cặp nối thành công được cộng 10 điểm.\n" +
                              "4. Hỗ trợ: Khi bàn chơi không còn cặp nào có thể ăn được, hệ thống sẽ tự động xáo trộn lại để bạn tiếp tục chơi!";

            MessageBox.Show(luatChoi, "Luật Chơi Pikachu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pause_Click(object sender, EventArgs e)
        {
            isPaused = !isPaused;

            Button btnPause = sender as Button;

            if (isPaused)
            {
                countdownTimer.Stop();
                panelMiddle.Visible = false;

                if (btnPause != null)
                {
                    btnPause.Text = "";
                    btnPause.BackgroundImage = Properties.Resources.play_buttton;
                    btnPause.BackgroundImageLayout = ImageLayout.Stretch;
                }

                if (isMusicPlaying && bgMusicPlayer != null)
                {
                    bgMusicPlayer.Stop();
                }
            }
            else
            {
                countdownTimer.Start();
                panelMiddle.Visible = true;

                if (btnPause != null)
                {
                    btnPause.Text = "";
                    btnPause.BackgroundImage = Properties.Resources.pause;
                    btnPause.BackgroundImageLayout = ImageLayout.Stretch;
                }

                if (isMusicPlaying && bgMusicPlayer != null)
                {
                    bgMusicPlayer.PlayLooping();
                }
            }
        }

        private async void hint_Click_1(object sender, EventArgs e)
        {
            if (isPaused || isProcessing) return;

            for (int r1 = 0; r1 < rows; r1++)
            {
                for (int c1 = 0; c1 < cols; c1++)
                {
                    if (boardMap[r1, c1] == -1) continue;

                    for (int r2 = 0; r2 < rows; r2++)
                    {
                        for (int c2 = 0; c2 < cols; c2++)
                        {
                            if (boardMap[r2, c2] == -1) continue;
                            if (r1 == r2 && c1 == c2) continue;

                            if (boardMap[r1, c1] == boardMap[r2, c2])
                            {
                                Point p1 = new Point(r1, c1);
                                Point p2 = new Point(r2, c2);
                                var path = ConnectStraight(p1, p2) ?? ConnectL(p1, p2) ?? Connect3Line(p1, p2);

                                if (path != null)
                                {
                                    await ShowHintAsync(p1, p2);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void shuffle_Click(object sender, EventArgs e)
        {
            if (isPaused || isProcessing) return;

            if (selected1 != null) selected1.BorderStyle = BorderStyle.FixedSingle;
            if (selected2 != null) selected2.BorderStyle = BorderStyle.FixedSingle;
            selected1 = null;
            selected2 = null;

            ShuffleBoardValues();

            while (!IsBoardCleaned() && !HasAnyValidMove())
            {
                ShuffleBoardValues();
            }
        }

        private async Task ShowHintAsync(Point p1, Point p2)
        {
            isProcessing = true;

            PictureBox pb1 = congiappictureBox[p1.X, p1.Y];
            PictureBox pb2 = congiappictureBox[p2.X, p2.Y];

            Color originalColor = pb1.BackColor;
            BorderStyle originalBorder = pb1.BorderStyle;

            pb1.BackColor = Color.Yellow;
            pb2.BackColor = Color.Yellow;
            pb1.BorderStyle = BorderStyle.Fixed3D;
            pb2.BorderStyle = BorderStyle.Fixed3D;

            pb1.Refresh();
            pb2.Refresh();

            await Task.Delay(800);

            pb1.BackColor = originalColor;
            pb2.BackColor = originalColor;
            pb1.BorderStyle = originalBorder;
            pb2.BorderStyle = originalBorder;

            isProcessing = false;
        }

    }
}