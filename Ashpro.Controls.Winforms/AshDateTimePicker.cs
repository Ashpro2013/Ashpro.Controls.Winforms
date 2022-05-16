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
	public partial class AshDateTimePicker : DateTimePicker
	{
		#region ComboInfoHelper
		internal class ComboInfoHelper
		{
			[DllImport("user32")]
			private static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref ComboBoxInfo info);

			#region RECT struct
			[StructLayout(LayoutKind.Sequential)]
			private struct RECT
			{
				public int Left;
				public int Top;
				public int Right;
				public int Bottom;
			}
			#endregion

			#region ComboBoxInfo Struct
			[StructLayout(LayoutKind.Sequential)]
			private struct ComboBoxInfo
			{
				public int cbSize;
				public RECT rcItem;
				public RECT rcButton;
				public IntPtr stateButton;
				public IntPtr hwndCombo;
				public IntPtr hwndEdit;
				public IntPtr hwndList;
			}
			#endregion

			public static int GetComboDropDownWidth()
			{
				ComboBox cb = new ComboBox();
				int width = GetComboDropDownWidth(cb.Handle);
				cb.Dispose();
				return width;
			}
			public static int GetComboDropDownWidth(IntPtr handle)
			{
				ComboBoxInfo cbi = new ComboBoxInfo();
				cbi.cbSize = Marshal.SizeOf(cbi);
				GetComboBoxInfo(handle, ref cbi);
				int width = cbi.rcButton.Right - cbi.rcButton.Left;
				return width;
			}
		}
		#endregion

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


		private Pen BorderPen = new Pen(Color.LightGray, 2);
		private Pen BorderPenControl = new Pen(SystemColors.ControlDark, 2);
		private bool DroppedDown = false;
		private static int DropDownButtonWidth = 17;
		static void FlatDateTimePicker()
		{
			// 2 pixel extra is for the 3D border around the pulldown button on the left and right
			DropDownButtonWidth = ComboInfoHelper.GetComboDropDownWidth();
		}
		public AshDateTimePicker() : base()
		{
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			InitializeComponent();
		}
		protected override void OnValueChanged(EventArgs eventargs)
		{
			base.OnValueChanged(eventargs);
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
		protected override void WndProc(ref Message m)
		{
			try
			{
				IntPtr hDC = GetWindowDC(m.HWnd);
				Graphics gdc = Graphics.FromHdc(hDC);
				switch (m.Msg)
				{
					case WM_NC_PAINT:

						SendPrintClientMsg();
						OverrideControlBorder(gdc);

						m.Result = (IntPtr)1; // indicate msg has been processed
						break;
					case WM_PAINT:
						base.WndProc(ref m);
						OverrideControlBorder(gdc);
						OverrideDropDown(gdc);
						break;
					case WM_NC_HITTEST:
						base.WndProc(ref m);
						if (DroppedDown)
							this.Invalidate(this.ClientRectangle, false);
						break;
					default:
						base.WndProc(ref m);
						break;
				}
				ReleaseDC(m.HWnd, hDC);
				gdc.Dispose();

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
			gClient.ReleaseHdc(ptrClientDC);
			gClient.Dispose();
		}

		private void OverrideDropDown(Graphics g)
		{
			if (!this.ShowUpDown)
			{
				Rectangle rect = new Rectangle(this.Width - DropDownButtonWidth, 0, DropDownButtonWidth, this.Height);
				//ControlPaint.DrawComboButton(g, rect, ButtonState.Flat);
			}
		}

		private void OverrideControlBorder(Graphics g)
		{
			g.DrawRectangle(new Pen(Color.White, 2), new Rectangle(0, 0, this.Width, this.Height));
			g.DrawLine(BorderPen, new Point(0, this.Height), new Point(this.Width - 33, this.Height));


			//if (this.Focused == false || this.Enabled == false)
			//    g.DrawRectangle(BorderPenControl, new Rectangle(0, 0, this.Width, this.Height));
			//else
			//    g.DrawRectangle(BorderPen, new Rectangle(0, 0, this.Width, this.Height));
		}

		protected override void OnDropDown(EventArgs eventargs)
		{
			DroppedDown = true;
			base.OnDropDown(eventargs);
		}
		protected override void OnCloseUp(EventArgs eventargs)
		{
			DroppedDown = false;
			base.OnCloseUp(eventargs);
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

	}
}
