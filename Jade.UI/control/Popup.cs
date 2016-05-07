/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-10-23 23:15:31
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using VS = System.Windows.Forms.VisualStyles;


namespace Jade.UI
{
    [ToolboxItem(false)]
    public partial class Popup : ToolStripDropDown
    {
        #region 字段属性

        private Control content;
        /// <summary>
        /// 弹出的内容
        /// </summary>
        public Control Content
        {
            get { return content; }
        }

        private bool fade;
        /// <summary>
        /// Gets a value indicating whether the <see cref="PopupControl.Popup"/> uses the fade effect.
        /// </summary>
        /// <value><c>true</c> if pop-up uses the fade effect; otherwise, <c>false</c>.</value>
        /// <remarks>To use the fade effect, the FocusOnOpen property also has to be set to <c>true</c>.</remarks>
        public bool UseFadeEffect
        {
            get { return fade; }
            set
            {
                if (fade == value) return;
                fade = value;
            }
        }

        private bool focusOnOpen = true;
        /// <summary>
        /// 获取或设置一个值，该值指示该内容是否应在弹出式已打开后接收焦点。
        /// </summary>
        /// <value><c>true</c> if the content should be focused after the pop-up has been opened; otherwise, <c>false</c>.</value>
        /// <remarks>If the FocusOnOpen property is set to <c>false</c>, then pop-up cannot use the fade effect.</remarks>
        public bool FocusOnOpen
        {
            get { return focusOnOpen; }
            set { focusOnOpen = value; }
        }

        private bool acceptAlt = true;
        /// <summary>
        /// 获取或设置一个值，指示是否应尽快的ALT键关闭弹出。
        /// </summary>
        /// <value><c>true</c> if presing the alt key does not close the pop-up; otherwise, <c>false</c>.</value>
        public bool AcceptAlt
        {
            get { return acceptAlt; }
            set { acceptAlt = value; }
        }

        private Popup ownerPopup;
        private Popup childPopup;

        private bool _resizable;
        private bool resizable;
        /// <summary>
        /// 获取或设置一个值，指示是否可调整大小。
        /// </summary>
        /// <value><c>true</c> if resizable; otherwise, <c>false</c>.</value>
        public bool Resizable
        {
            get { return resizable && _resizable; }
            set { resizable = value; }
        }

        private ToolStripControlHost host;

        private Size minSize;
        /// <summary>
        /// 获取或设置弹出窗口的最小大小。
        /// </summary>
        /// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
        public new Size MinimumSize
        {
            get { return minSize; }
            set { minSize = value; }
        }

        private Size maxSize;
        /// <summary>
        /// 获取或设置弹出窗口的最大大小。
        /// </summary>
        /// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
        public new Size MaximumSize
        {
            get { return maxSize; }
            set { maxSize = value; }
        }

        private bool _isOpen;

        public bool IsOpen
        {
            get { return _isOpen; }
        }

        /// <summary>
        /// 获取新窗口的参数。
        /// </summary>
        /// <returns>An object of type <see cref="T:System.Windows.Forms.CreateParams" /> used when creating a new window.</returns>
        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= UnsafeMethods.WS_EX_NOACTIVATE;
                return cp;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个新的实例的
        /// </summary>
        /// <param name="content">The content of the pop-up.</param>
        /// <remarks>
        /// Pop-up will be disposed immediately after disposion of the content control.
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="content" /> is <code>null</code>.</exception>
        public Popup(Control content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }
            this.content = content;
            this.fade = SystemInformation.IsMenuAnimationEnabled && SystemInformation.IsMenuFadeEnabled;
            this._resizable = true;
            AutoSize = false;
            DoubleBuffered = true;
            ResizeRedraw = true;
            host = new ToolStripControlHost(content);
            Padding = Margin = host.Padding = host.Margin = Padding.Empty;
            MinimumSize = content.MinimumSize;
            content.MinimumSize = content.Size;
            MaximumSize = content.MaximumSize;
            content.MaximumSize = content.Size;
            Size = content.Size;
            content.Location = Point.Empty;
            Items.Add(host);
            content.Disposed += delegate (object sender, EventArgs e)
            {
                content = null;
                Dispose(true);
            };
            content.RegionChanged += delegate (object sender, EventArgs e)
            {
                UpdateRegion();
            };
            content.Paint += delegate (object sender, PaintEventArgs e)
            {
                PaintSizeGrip(e);
            };
            UpdateRegion();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 处理对话框密钥.
        /// </summary>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        /// <returns>
        /// true if the key was processed by the control; otherwise, false.
        /// </returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (acceptAlt && ((keyData & Keys.Alt) == Keys.Alt))
            {
                if ((keyData & Keys.F4) != Keys.F4)
                {
                    return false;
                }
                else
                {
                    this.Close();
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 更新弹出区域.
        /// </summary>
        protected void UpdateRegion()
        {
            if (this.Region != null)
            {
                this.Region.Dispose();
                this.Region = null;
            }
            if (content.Region != null)
            {
                this.Region = content.Region.Clone();
            }
        }

        /// <summary>
        /// 显示指定控件下方的弹出窗口。
        /// </summary>
        /// <param name="control">The control below which the pop-up will be shown.</param>
        /// <remarks>
        /// When there is no space below the specified control, the pop-up control is shown above it.
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="control"/> is <code>null</code>.</exception>
        public void Show(Control control)
        {
            Show(control, control.ClientRectangle);
        }

        public void Show(Control control, bool center)
        {
            Show(control, control.ClientRectangle, center);
        }

        /// <summary>
        /// 显示指定控件的指定区域下方的弹出窗口。
        /// </summary>
        /// <param name="control">The control used to compute screen location of specified area.</param>
        /// <param name="area">The area of control below which the pop-up will be shown.</param>
        /// <remarks>
        /// When there is no space below specified area, the pop-up control is shown above it.
        /// </remarks>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="control"/> is <code>null</code>.</exception>
        public void Show(Control control, Rectangle area)
        {
            Show(control, area, false);
        }

        public void Show(Control control, Rectangle area, bool center)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            SetOwnerItem(control);
            resizableTop = resizableLeft = false;
            Point location = control.PointToScreen(new Point(area.Left, area.Top + area.Height));
            Rectangle screen = Screen.FromControl(control).WorkingArea;
            if (center)
            {
                if (location.X + (area.Width + Size.Width) / 2 > screen.Right)
                {
                    resizableLeft = true;
                    location.X = screen.Right - Size.Width;
                }
                else
                {
                    resizableLeft = true;
                    location.X = location.X - (Size.Width - area.Width) / 2;
                }
            }
            else
            {
                if (location.X + Size.Width > (screen.Left + screen.Width))
                {
                    resizableLeft = true;
                    location.X = (screen.Left + screen.Width) - Size.Width;
                }
            }

            if (location.Y + Size.Height > (screen.Top + screen.Height))
            {
                resizableTop = true;
                location.Y -= Size.Height + area.Height;
            }
            location = control.PointToClient(location);
            Show(control, location, ToolStripDropDownDirection.BelowRight);
        }

        private const int frames = 5;
        private const int totalduration = 100;
        private const int frameduration = totalduration / frames;
        /// <summary>
        /// Adjusts the size of the owner <see cref="T:System.Windows.Forms.ToolStrip" /> to accommodate the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> if the owner <see cref="T:System.Windows.Forms.ToolStrip" /> is currently displayed, or clears and resets active <see cref="T:System.Windows.Forms.ToolStripDropDown" /> child controls of the <see cref="T:System.Windows.Forms.ToolStrip" /> if the <see cref="T:System.Windows.Forms.ToolStrip" /> is not currently displayed.
        /// </summary>
        /// <param name="visible">true if the owner <see cref="T:System.Windows.Forms.ToolStrip" /> is currently displayed; otherwise, false.</param>
        protected override void SetVisibleCore(bool visible)
        {
            double opacity = Opacity;
            if (visible && fade && focusOnOpen) Opacity = 0;
            base.SetVisibleCore(visible);
            if (!visible || !fade || !focusOnOpen) return;
            for (int i = 1; i <= frames; i++)
            {
                if (i > 1)
                {
                    System.Threading.Thread.Sleep(frameduration);
                }
                Opacity = opacity * (double)i / (double)frames;
            }
            Opacity = opacity;
        }

        private bool resizableTop;
        private bool resizableLeft;

        private void SetOwnerItem(Control control)
        {
            if (control == null)
            {
                return;
            }
            if (control is Popup)
            {
                Popup popupControl = control as Popup;
                ownerPopup = popupControl;
                ownerPopup.childPopup = this;
                OwnerItem = popupControl.Items[0];
                return;
            }
            if (control.Parent != null)
            {
                SetOwnerItem(control.Parent);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            content.MinimumSize = Size;
            content.MaximumSize = Size;
            content.Size = Size;
            content.Location = Point.Empty;
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opening" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnOpening(CancelEventArgs e)
        {
            if (content.IsDisposed || content.Disposing)
            {
                e.Cancel = true;
                return;
            }
            UpdateRegion();
            base.OnOpening(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opened" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnOpened(EventArgs e)
        {
            if (ownerPopup != null)
            {
                ownerPopup._resizable = false;
            }
            if (focusOnOpen)
            {
                content.Focus();
            }
            _isOpen = true;
            base.OnOpened(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closed"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs"/> that contains the event data.</param>
        protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
        {
            if (ownerPopup != null)
            {
                ownerPopup._resizable = true;
            }
            _isOpen = false;
            base.OnClosed(e);
        }

        #endregion

        #region " Resizing Support "

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (InternalProcessResizing(ref m, false))
            {
                return;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Processes the resizing messages.
        /// </summary>
        /// <param name="m">The message.</param>
        /// <returns>true, if the WndProc method from the base class shouldn't be invoked.</returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public bool ProcessResizing(ref Message m)
        {
            return InternalProcessResizing(ref m, true);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private bool InternalProcessResizing(ref Message m, bool contentControl)
        {
            if (m.Msg == UnsafeMethods.WM_NCACTIVATE && m.WParam != IntPtr.Zero && childPopup != null && childPopup.Visible)
            {
                childPopup.Hide();
            }
            if (!Resizable)
            {
                return false;
            }
            if (m.Msg == UnsafeMethods.WM_NCHITTEST)
            {
                return OnNcHitTest(ref m, contentControl);
            }
            else if (m.Msg == UnsafeMethods.WM_GETMINMAXINFO)
            {
                return OnGetMinMaxInfo(ref m);
            }
            return false;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private bool OnGetMinMaxInfo(ref Message m)
        {
            UnsafeMethods.MINMAXINFO minmax = (UnsafeMethods.MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(UnsafeMethods.MINMAXINFO));
            minmax.maxTrackSize = this.MaximumSize;
            minmax.minTrackSize = this.MinimumSize;
            Marshal.StructureToPtr(minmax, m.LParam, false);
            return true;
        }

        private bool OnNcHitTest(ref Message m, bool contentControl)
        {
            int x = UnsafeMethods.LOWORD(m.LParam);
            int y = UnsafeMethods.HIWORD(m.LParam);
            Point clientLocation = PointToClient(new Point(x, y));

            GripBounds gripBouns = new GripBounds(contentControl ? content.ClientRectangle : ClientRectangle);
            IntPtr transparent = new IntPtr(UnsafeMethods.HTTRANSPARENT);

            if (resizableTop)
            {
                if (resizableLeft && gripBouns.TopLeft.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)UnsafeMethods.HTTOPLEFT;
                    return true;
                }
                if (!resizableLeft && gripBouns.TopRight.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)UnsafeMethods.HTTOPRIGHT;
                    return true;
                }
                if (gripBouns.Top.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)UnsafeMethods.HTTOP;
                    return true;
                }
            }
            else
            {
                if (resizableLeft && gripBouns.BottomLeft.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)UnsafeMethods.HTBOTTOMLEFT;
                    return true;
                }
                if (!resizableLeft && gripBouns.BottomRight.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)UnsafeMethods.HTBOTTOMRIGHT;
                    return true;
                }
                if (gripBouns.Bottom.Contains(clientLocation))
                {
                    m.Result = contentControl ? transparent : (IntPtr)UnsafeMethods.HTBOTTOM;
                    return true;
                }
            }
            if (resizableLeft && gripBouns.Left.Contains(clientLocation))
            {
                m.Result = contentControl ? transparent : (IntPtr)UnsafeMethods.HTLEFT;
                return true;
            }
            if (!resizableLeft && gripBouns.Right.Contains(clientLocation))
            {
                m.Result = contentControl ? transparent : (IntPtr)UnsafeMethods.HTRIGHT;
                return true;
            }
            return false;
        }

        private VS.VisualStyleRenderer sizeGripRenderer;
        /// <summary>
        /// Paints the sizing grip.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
        public void PaintSizeGrip(PaintEventArgs e)
        {
            if (e == null || e.Graphics == null || !resizable)
            {
                return;
            }
            Size clientSize = content.ClientSize;
            using (Bitmap gripImage = new Bitmap(0x10, 0x10))
            {
                using (Graphics g = Graphics.FromImage(gripImage))
                {
                    if (Application.RenderWithVisualStyles)
                    {
                        if (this.sizeGripRenderer == null)
                        {
                            this.sizeGripRenderer = new VS.VisualStyleRenderer(VS.VisualStyleElement.Status.Gripper.Normal);
                        }
                        this.sizeGripRenderer.DrawBackground(g, new Rectangle(0, 0, 0x10, 0x10));
                    }
                    else
                    {
                        ControlPaint.DrawSizeGrip(g, content.BackColor, 0, 0, 0x10, 0x10);
                    }
                }
                GraphicsState gs = e.Graphics.Save();
                e.Graphics.ResetTransform();
                if (resizableTop)
                {
                    if (resizableLeft)
                    {
                        e.Graphics.RotateTransform(180);
                        e.Graphics.TranslateTransform(-clientSize.Width, -clientSize.Height);
                    }
                    else
                    {
                        e.Graphics.ScaleTransform(1, -1);
                        e.Graphics.TranslateTransform(0, -clientSize.Height);
                    }
                }
                else if (resizableLeft)
                {
                    e.Graphics.ScaleTransform(-1, 1);
                    e.Graphics.TranslateTransform(-clientSize.Width, 0);
                }
                e.Graphics.DrawImage(gripImage, clientSize.Width - 0x10, clientSize.Height - 0x10 + 1, 0x10, 0x10);
                e.Graphics.Restore(gs);
            }
        }

        #endregion
    }
}
#region UnsafeMethods


internal class UnsafeMethods
{
    public static readonly IntPtr TRUE = new IntPtr(1);
    public static readonly IntPtr FALSE = new IntPtr(0);

    public const int WM_LBUTTONDOWN = 0x201;
    public const int WM_RBUTTONDOWN = 0x204;
    public const int WM_MBUTTONDOWN = 0x207;
    public const int WM_NCLBUTTONDOWN = 0x0A1;
    public const int WM_NCRBUTTONDOWN = 0x0A4;
    public const int WM_NCMBUTTONDOWN = 0x0A7;
    public const int WM_NCCALCSIZE = 0x0083;
    public const int WM_NCHITTEST = 0x0084;
    public const int WM_NCPAINT = 0x0085;
    public const int WM_NCACTIVATE = 0x0086;
    public const int WM_MOUSELEAVE = 0x02A3;
    public const int WS_EX_NOACTIVATE = 0x08000000;
    public const int HTTRANSPARENT = -1;
    public const int HTLEFT = 10;
    public const int HTRIGHT = 11;
    public const int HTTOP = 12;
    public const int HTTOPLEFT = 13;
    public const int HTTOPRIGHT = 14;
    public const int HTBOTTOM = 15;
    public const int HTBOTTOMLEFT = 16;
    public const int HTBOTTOMRIGHT = 17;
    public const int WM_USER = 0x0400;
    public const int WM_REFLECT = WM_USER + 0x1C00;
    public const int WM_COMMAND = 0x0111;
    public const int CBN_DROPDOWN = 7;
    public const int WM_GETMINMAXINFO = 0x0024;

    public enum TrackerEventFlags : uint
    {
        TME_HOVER = 0x00000001,
        TME_LEAVE = 0x00000002,
        TME_QUERY = 0x40000000,
        TME_CANCEL = 0x80000000
    }

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern int ReleaseDC(IntPtr hwnd, IntPtr hDC);

    [DllImport("user32")]
    public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT lpEventTrack);

    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    internal static int HIWORD(int n)
    {
        return (n >> 16) & 0xffff;
    }

    internal static int HIWORD(IntPtr n)
    {
        return HIWORD(unchecked((int)(long)n));
    }

    internal static int LOWORD(int n)
    {
        return n & 0xffff;
    }

    internal static int LOWORD(IntPtr n)
    {
        return LOWORD(unchecked((int)(long)n));
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TRACKMOUSEEVENT
    {
        public uint cbSize;
        public uint dwFlags;
        public IntPtr hwndTrack;
        public uint dwHoverTime;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MINMAXINFO
    {
        public Point reserved;
        public Size maxSize;
        public Point maxPosition;
        public Size minTrackSize;
        public Size maxTrackSize;
    }

    #region RECT structure

    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(
                    this.left,
                    this.top,
                    this.right - this.left,
                    this.bottom - this.top);
            }
        }

        public static RECT FromXYWH(int x, int y, int width, int height)
        {
            return new RECT(x,
                            y,
                            x + width,
                            y + height);
        }

        public static RECT FromRectangle(Rectangle rect)
        {
            return new RECT(rect.Left,
                             rect.Top,
                             rect.Right,
                             rect.Bottom);
        }
    }

    #endregion RECT structure

    #region WINDOWPOS

    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWPOS
    {
        internal IntPtr hwnd;
        internal IntPtr hWndInsertAfter;
        internal int x;
        internal int y;
        internal int cx;
        internal int cy;
        internal uint flags;
    }
    #endregion

    #region NCCALCSIZE_PARAMS
    //http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/windows/windowreference/windowstructures/nccalcsize_params.asp
    [StructLayout(LayoutKind.Sequential)]
    public struct NCCALCSIZE_PARAMS
    {
        /// <summary>
        /// Contains the new coordinates of a window that has been moved or resized, that is, it is the proposed new window coordinates.
        /// </summary>
        public RECT rectProposed;
        /// <summary>
        /// Contains the coordinates of the window before it was moved or resized.
        /// </summary>
        public RECT rectBeforeMove;
        /// <summary>
        /// Contains the coordinates of the window's client area before the window was moved or resized.
        /// </summary>
        public RECT rectClientBeforeMove;
        /// <summary>
        /// Pointer to a WINDOWPOS structure that contains the size and position values specified in the operation that moved or resized the window.
        /// </summary>
        public WINDOWPOS lpPos;
    }

    #endregion
}

#endregion

#region GripBounds


internal struct GripBounds
{
    private const int GripSize = 6;
    private const int CornerGripSize = GripSize << 1;

    public GripBounds(Rectangle clientRectangle)
    {
        this.clientRectangle = clientRectangle;
    }

    private Rectangle clientRectangle;
    public Rectangle ClientRectangle
    {
        get { return clientRectangle; }
        //set { clientRectangle = value; }
    }

    public Rectangle Bottom
    {
        get
        {
            Rectangle rect = ClientRectangle;
            rect.Y = rect.Bottom - GripSize + 1;
            rect.Height = GripSize;
            return rect;
        }
    }

    public Rectangle BottomRight
    {
        get
        {
            Rectangle rect = ClientRectangle;
            rect.Y = rect.Bottom - CornerGripSize + 1;
            rect.Height = CornerGripSize;
            rect.X = rect.Width - CornerGripSize + 1;
            rect.Width = CornerGripSize;
            return rect;
        }
    }

    public Rectangle Top
    {
        get
        {
            Rectangle rect = ClientRectangle;
            rect.Height = GripSize;
            return rect;
        }
    }

    public Rectangle TopRight
    {
        get
        {
            Rectangle rect = ClientRectangle;
            rect.Height = CornerGripSize;
            rect.X = rect.Width - CornerGripSize + 1;
            rect.Width = CornerGripSize;
            return rect;
        }
    }

    public Rectangle Left
    {
        get
        {
            Rectangle rect = ClientRectangle;
            rect.Width = GripSize;
            return rect;
        }
    }

    public Rectangle BottomLeft
    {
        get
        {
            Rectangle rect = ClientRectangle;
            rect.Width = CornerGripSize;
            rect.Y = rect.Height - CornerGripSize + 1;
            rect.Height = CornerGripSize;
            return rect;
        }
    }

    public Rectangle Right
    {
        get
        {
            Rectangle rect = ClientRectangle;
            rect.X = rect.Right - GripSize + 1;
            rect.Width = GripSize;
            return rect;
        }
    }

    public Rectangle TopLeft
    {
        get
        {
            Rectangle rect = ClientRectangle;
            rect.Width = CornerGripSize;
            rect.Height = CornerGripSize;
            return rect;
        }
    }
}

#endregion