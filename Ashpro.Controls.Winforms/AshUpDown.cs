using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ashpro.Controls.Winforms
{
    public partial class AshUpDown : NumericUpDown
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, object lParam);

        [DllImport("user32")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        const int WM_ERASEBKGND = 0x14;
        const int WM_PAINT = 0xF;
        const int WM_NC_HITTEST = 0x84;
        const int WM_NC_PAINT = 0x85;
        const int WM_PRINTCLIENT = 0x318;
        const int WM_SETCURSOR = 0x20;
        public AshUpDown()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        private Pen BorderPen = new Pen(Color.DarkGray, 2);
        protected override void WndProc(ref Message m)
        {
            try
            {
                IntPtr hDC = IntPtr.Zero;
                Graphics gdc = null;
                switch (m.Msg)
                {
                    case WM_NC_PAINT:
                        hDC = GetWindowDC(m.HWnd);
                        gdc = Graphics.FromHdc(hDC);
                        SendMessage(this.Handle, WM_ERASEBKGND, hDC, 0);
                        SendPrintClientMsg();
                        SendMessage(this.Handle, WM_PAINT, IntPtr.Zero, 0);
                        OverrideControlBorder(gdc);
                        m.Result = (IntPtr)1;   // indicate msg has been processed
                        ReleaseDC(m.HWnd, hDC);
                        gdc.Dispose();
                        break;
                    case WM_PAINT:
                        base.WndProc(ref m);
                        hDC = GetWindowDC(m.HWnd);
                        gdc = Graphics.FromHdc(hDC);
                        OverrideControlBorder(gdc);
                        ReleaseDC(m.HWnd, hDC);
                        gdc.Dispose();
                        break;
                    /*
                    // We don't need this anymore, handle by WM_SETCURSOR
                    case WM_NC_HITTEST: 
                        base.WndProc(ref m);
                        if (DroppedDown)
                        {
                            OverrideDropDown(gdc);
                            OverrideControlBorder(gdc);
                        }
                        break;
                    */
                    case WM_SETCURSOR:
                        base.WndProc(ref m);
                        // The value 3 is discovered by trial on error, and cover all kinds of scenarios
                        // InvalidateSince < 2 wil have problem if the control is not in focus and dropdown is clicked

                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            catch (Exception)
            {


            }

        }
        private void SendPrintClientMsg()
        {
            // We send this message for the control to redraw the client area
            Graphics gClient = this.CreateGraphics();
            IntPtr ptrClientDC = gClient.GetHdc();
            SendMessage(this.Handle, WM_PRINTCLIENT, ptrClientDC, 0);
            gClient.ReleaseHdc(ptrClientDC);
            gClient.Dispose();
        }
        private void OverrideControlBorder(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.White, 2), new Rectangle(0, 0, this.Width, this.Height));
            g.DrawLine(BorderPen, new Point(0, this.Height), new Point(this.Width - 18, this.Height));
            g.DrawLine(BorderPen, new Point(0, this.Height), new Point(this.Width - 17, this.Height));
        }
        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }
        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Handled == false)
            {
                if (e.KeyCode == Keys.Return)
                {
                    SendKeys.Send("{TAB}");
                }
            }
        }
    }

}
