using System;
using System.Windows.Forms;

namespace Pikachu
{
    public class TransparentPanel : Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT: Trong suốt về hình ảnh
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Cố tình không vẽ lại nền để giữ độ trong suốt tuyệt đối
        }

        // Cho phép click chuột xuyên qua Panel xuống PictureBox bên dưới
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = -1;

            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
                return;
            }
            base.WndProc(ref m);
        }
    }
}