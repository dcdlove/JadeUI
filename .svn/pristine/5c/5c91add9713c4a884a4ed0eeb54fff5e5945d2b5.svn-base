/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-09-11 10:31:24
 * @version 1.0.0 
 */


using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Jade.UI
{
	[DefaultEvent("CheckedChanged")]
	public class JSwitchButton : UserControl
	{
		Rectangle btnRec = new Rectangle(2, 2, 15, 15);


		public JSwitchButton()
		{
			Width = 40;
			Height = 20;
			BackColor = Color.Transparent;
			SetStyles();
			MouseClick += new MouseEventHandler(JSwitchButton_MouseClick);
			CheckedChanged += new EventHandler(OnCheckedChanged);
		}


		void JSwitchButton_MouseClick(object sender, MouseEventArgs e)
		{
			var rec = btnRec;
			rec.X = Checked ? 22 : 2;
			if (!CheckHotspot(rec, e.Location)) return;


			double g = 1.3;
			double vx = 1;//g*t
			if (!Checked)
			{
				while (true)
				{
					if (btnRec.X >= 22) { btnRec.X = 22; break; }
					System.Windows.Forms.Application.DoEvents();
					System.Threading.Thread.Sleep(10);
					vx += g;
					btnRec.X = Convert.ToInt32(vx);
					Invalidate();
					System.Windows.Forms.Application.DoEvents();
				}
			}
			else
			{
				g = 0.1;
				while (true)
				{
					if (btnRec.X <= 2) { btnRec.X = 2; break; }
					System.Windows.Forms.Application.DoEvents();
					System.Threading.Thread.Sleep(10);
					g += 0.2;
					btnRec.X = Convert.ToInt32(btnRec.X - g);
					Invalidate();
					System.Windows.Forms.Application.DoEvents();
				}

			}
			Checked = !Checked;
			CheckedChanged(this, new EventArgs());
		}


		[Description("Checked 属性的值更改时发生")]
		public event EventHandler CheckedChanged;

		private bool _checked=false;
		[Category("JadeControl _"),
		Description("是否启用")]
		public bool Checked
		{
			get { return _checked; }
			set
			{
				_checked = value;
				btnRec.X = _checked ? 22 : 2;
				Invalidate();
			}
		}

		SolidBrush bg = new SolidBrush(ColorTranslator.FromHtml("#FAFBFA"));
		SolidBrush on = new SolidBrush(ColorTranslator.FromHtml("#FFFFFF"));
		SolidBrush lk = new SolidBrush(ColorTranslator.FromHtml("#0096ff"));

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
			if (Checked)
			{
				e.Graphics.FillPath(lk, CreateRoundedRectanglePath(new Rectangle(0, 0, Width, 19), 10));
				DrawRoundRectangle(e.Graphics, new Pen(Color.FromArgb(30, 0, 0, 0)), new Rectangle(-1, 0, Width + 1, 19), 10);

				e.Graphics.FillEllipse(on, btnRec);
				e.Graphics.DrawEllipse(new Pen(Color.FromArgb(100, 0, 0, 0)), btnRec);
			}
			else
			{
				e.Graphics.FillPath(bg, CreateRoundedRectanglePath(new Rectangle(0, 0, Width, 19), 10));
				DrawRoundRectangle(e.Graphics, new Pen(Color.FromArgb(30, 0, 0, 0)), new Rectangle(-1, 0, Width + 1, 19), 10);

				e.Graphics.FillEllipse(on, btnRec);
				e.Graphics.DrawEllipse(new Pen(Color.FromArgb(70, 0, 0, 0)), btnRec);
			}

		}

		private bool CheckHotspot(Rectangle rec, Point loc)
		{
			return (loc.X >= rec.X
				&& loc.Y >= rec.Y
				&& loc.X <= rec.X + rec.Width
				&& loc.Y <= rec.Y + rec.Height);
		}


		private void SetStyles()
		{
			base.SetStyle(
			ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制  
			ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
			ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
			ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
			ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
			true);                                         // 设置以上值为 true  
			base.UpdateStyles();
		}

		public void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rect, int cornerRadius)
		{
			using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
			{
				g.DrawPath(pen, path);
			}
		}
		public void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
		{
			using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
			{
				g.FillPath(brush, path);
			}
		}
		public GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
		{
			GraphicsPath roundedRect = new GraphicsPath();
			roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
			roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
			roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
			roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
			roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
			roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
			roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
			roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
			roundedRect.CloseFigure();
			return roundedRect;
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// JSwitchButton
			// 
			this.Name = "JSwitchButton";
			this.Size = new System.Drawing.Size(50, 26);
			this.ResumeLayout(false);

		}

		protected virtual void OnCheckedChanged(object sender, EventArgs e)
		{

		}
	}


}
