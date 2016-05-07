/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-09-22 15:42:59
 * @version 1.0.0 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Jade.UI
{
	public partial class JIconListView : Control
	{

		public delegate void JIconListViewEventHandler(object sender, JIconListViewEventArgs e);
		public event JIconListViewEventHandler DoubleClickItem;
		private List<JIconListViewItem> _Items = new List<JIconListViewItem>();

		private Rectangle _HoverItem;
		private JIconListViewItem _SelectItem;
		private Color _HoverItemBackgroundColor = Color.FromArgb(30, 0, 0, 255);
		private Color _SelectItemBackgroundColor = Color.FromArgb(50, 200, 0, 0);

		private Point m_ptMousePos;		 //鼠标的位置
		private JVScroll vscroll;        //滚动条
		private int _ColumnCount = 0;    //列表列数
		private int _ItemSpace = 10;
		private Size _ItemSize = new Size(70, 70);
		private Size _IconSize = new Size(48, 48);
		private StringFormat stringformat = new StringFormat();

		[Category("JadeControl"),
		Description("控件中所有项的集合"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<JIconListViewItem> Items
		{
			get { return _Items; }
		}

		[Category("JadeControl"),
		Description("列表选中项的背景颜色")]
		public Color SelectItemBackgroundColor
		{
			get { return _SelectItemBackgroundColor; }
			set { _SelectItemBackgroundColor = value; }
		}

		[Category("JadeControl"),
		Description("列表悬停项的背景颜色")]
		public Color HoverItemBackgroundColor
		{
			get { return _HoverItemBackgroundColor; }
			set { _HoverItemBackgroundColor = value; }
		}

		[Category("JadeControl"),
		Description("控件中列表项之间的间隔距离")]
		public int ItemSpace
		{
			get { return _ItemSpace; }
			set { _ItemSpace = value; }
		}

		[Category("JadeControl"),
		Description("控件中列表项之间的大小")]
		public Size ItemSize
		{
			get { return _ItemSize; }
			set { _ItemSize = value; }
		}

		[Category("JadeControl"), Description("当前选中项"), Browsable(false)]
		public JIconListViewItem SelectItem
		{
			get { return _SelectItem; }
			set { _SelectItem = value; }
		}


		public JIconListView()
		{
			InitializeComponent();
			vscroll = new JVScroll(this);
			stringformat.Alignment = StringAlignment.Center;
			//stringformat.FormatFlags = StringFormatFlags.FitBlackBox;


			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		public void AddItem(JIconListViewItem item)
		{
			_Items.Add(item);

		}

		/// <summary>
		/// 清空当前列表选择的项
		/// </summary>
		private void ClearSelectItem()
		{
			if (_SelectItem != null)
			{
				this.Invalidate(new Rectangle(
					_SelectItem.Bounds.X, _SelectItem.Bounds.Y - vscroll.Value,
					_SelectItem.Bounds.Width, _SelectItem.Bounds.Height));
				_SelectItem = null;
			}
		}

		/// <summary>
		/// 根据坐标点 找到对应的Item区域
		/// </summary>
		/// <param name="mousePos"></param>
		/// <returns></returns>
		private Rectangle GetItemRectangle(Point mousePos)
		{
			var c = (mousePos.X / (_ItemSize.Width + _ItemSpace));
			var r = (mousePos.Y / (_ItemSize.Height + _ItemSpace));
			var b = (c * (_ItemSize.Width + _ItemSpace));
			if (c > _ColumnCount || (c == _ColumnCount && mousePos.X > b))
			{
				return Rectangle.Empty;
			}
			return new Rectangle(c * (_ItemSize.Width + _ItemSpace), r * (_ItemSize.Height + _ItemSpace), _ItemSize.Width, _ItemSize.Height);
		}

		/// <summary>
		/// 绘制列表项
		/// </summary>
		/// <author>jaly</author>
		/// <date>2015-09-23 09:28:43</date>
		/// <param name="g">绘图表面</param>
		/// <param name="item">要绘制的列表项</param>
		/// <param name="rect"></param>
		/// <returns></returns>
		protected virtual void DrawItem(Graphics g, JIconListViewItem item, Rectangle rect)
		{
			//g.DrawRectangle(Pens.Azure, rect);
			if (item.Icon != null)
			{
				g.DrawImage(item.Icon, new Rectangle(rect.X + ((_ItemSize.Width - _IconSize.Width) / 2), rect.Y, _IconSize.Width, _IconSize.Height), new Rectangle(new Point(), item.Icon.Size), GraphicsUnit.Pixel);
			}
			var fr = rect;
			fr.Y += _IconSize.Height + 4;
			g.DrawString(item.Text, Font, new SolidBrush(ForeColor), fr, stringformat);

			//TextFormatFlags format = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;
			//if (RightToLeft == RightToLeft.Yes)
			//{
			//    format |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
			//}
			//TextRenderer.DrawText(g, item.Text, Font, rect, ForeColor, format);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			//g.FillRectangle(new SolidBrush(Color.Pink), this.ClientRectangle);
			g.TranslateTransform(0, -vscroll.Value);//偏移坐标系
			if (!_HoverItem.IsEmpty)
			{
				g.FillRectangle(new SolidBrush(HoverItemBackgroundColor), _HoverItem);
			}
			if (_SelectItem != null)
			{
				g.FillRectangle(new SolidBrush(_SelectItemBackgroundColor), _SelectItem.Bounds);
			}

			var rect = new Rectangle(0, 0, _ItemSize.Width, _ItemSize.Height);
			for (int i = 0; i < _Items.Count; i++)
			{
				var item = _Items[i];
				if (i > 0 && i % _ColumnCount == 0)
				{
					rect.Y += _ItemSize.Height + _ItemSpace;
					rect.X = 0;
				}
				item.Index = i;
				_Items[i].Bounds = rect;
				DrawItem(e.Graphics, item, rect);
				rect.X += _ItemSize.Width + _ItemSpace;
			}
			g.ResetTransform();             //重置坐标系
			vscroll.VirtualHeight = rect.Bottom;//绘制完成计算虚拟高度决定是否绘制滚动条
			if (vscroll.ShouldBeDraw)   //是否绘制滚动条
				vscroll.ReDrawScroll(g);
			base.OnPaint(e);

		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (e.Delta > 0) vscroll.Value -= 50;
			if (e.Delta < 0) vscroll.Value += 50;
			base.OnMouseWheel(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{        //如果左键在滚动条滑块上点击
				if (vscroll.SliderBounds.Contains(e.Location))
				{
					vscroll.IsMouseDown = true;
					vscroll.MouseDownY = e.Y;
				}
			}
			this.Focus();
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				vscroll.IsMouseDown = false;
			base.OnMouseUp(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			m_ptMousePos = e.Location;
			if (vscroll.IsMouseDown)
			{          //如果滚动条的滑块处于被点击 那么移动
				vscroll.MoveSliderFromLocation(e.Y);
				return;
			}
			if (vscroll.ShouldBeDraw)
			{         //如果控件上有滚动条 判断鼠标是否在滚动条区域移动
				if (vscroll.Bounds.Contains(m_ptMousePos))
				{

					if (vscroll.SliderBounds.Contains(m_ptMousePos))
						vscroll.IsMouseOnSlider = true;
					else
						vscroll.IsMouseOnSlider = false;
					if (vscroll.UpBounds.Contains(m_ptMousePos))
						vscroll.IsMouseOnUp = true;
					else
						vscroll.IsMouseOnUp = false;
					if (vscroll.DownBounds.Contains(m_ptMousePos))
						vscroll.IsMouseOnDown = true;
					else
						vscroll.IsMouseOnDown = false;
					return;
				}
				else
					vscroll.ClearAllMouseOn();
			}
			m_ptMousePos.Y += vscroll.Value;        //如果不在滚动条范围类 那么根据滚动条当前值计算虚拟的一个坐标
			_HoverItem = GetItemRectangle(m_ptMousePos);
			Invalidate();
			base.OnMouseMove(e);
		}

		protected override void OnClick(EventArgs e)
		{
			if (vscroll.IsMouseDown) return;    //MouseUp事件触发在Click后 滚动条滑块为点下状态 单击无效
			if (vscroll.ShouldBeDraw)
			{         //如果有滚动条 判断是否在滚动条类点击
				if (vscroll.Bounds.Contains(m_ptMousePos))
				{        //判断在滚动条那个位置点击
					if (vscroll.UpBounds.Contains(m_ptMousePos))
						vscroll.Value -= 50;
					else if (vscroll.DownBounds.Contains(m_ptMousePos))
						vscroll.Value += 50;
					else if (!vscroll.SliderBounds.Contains(m_ptMousePos))
						vscroll.MoveSliderToLocation(m_ptMousePos.Y);
					return;
				}
			}//否则 如果在列表上点击 展开或者关闭 在子项上面点击则选中
			ClearSelectItem();
			foreach (JIconListViewItem item in _Items)
			{
				if (item.Bounds.Contains(m_ptMousePos))
				{
					_SelectItem = item;
					Invalidate();
				}
			}
			base.OnClick(e);
		}

		protected override void OnDoubleClick(EventArgs e)
		{
			this.OnClick(e);
			if (vscroll.Bounds.Contains(PointToClient(MousePosition))) return;  //如果双击在滚动条上返回
			if (_SelectItem != null)     //如果选中项不为空 那么触发用户绑定的双击事件
				OnDoubleClickItem(new JIconListViewEventArgs(_SelectItem));
			base.OnDoubleClick(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			//ClearSelectItem();
			_HoverItem = Rectangle.Empty;
			base.OnMouseLeave(e);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			_ColumnCount = (Width / (_ItemSize.Width + _ItemSpace));
			base.OnSizeChanged(e);
		}

		protected virtual void OnDoubleClickItem(JIconListViewEventArgs e)
		{
			if (this.DoubleClickItem != null)
				DoubleClickItem(this, e);
		}
	}

	public class JIconListViewItem
	{
		public JIconListViewItem() { }
		public JIconListViewItem(string text)
		{
			Text = text;
		}
		public JIconListViewItem(string text, Image icon)
		{
			Text = text;
			Icon = icon;
		}

		public string Text { get; set; }
		public Image Icon { get; set; }
		public int Index { get; set; }
		public string ToolTipText { get; set; }
		public object Tag { get; set; }

		private Rectangle _bounds;
		/// <summary>
		/// 获取列表项的显示区域
		/// </summary>
		[Browsable(false)]
		public Rectangle Bounds
		{
			get { return _bounds; }
			internal set { _bounds = value; }
		}

		private JIconListView _ownerJIconListView;
		/// <summary>
		/// 获取列表项所在的控件
		/// </summary>
		[Browsable(false)]
		public JIconListView OwnerJIconListView
		{
			get { return _ownerJIconListView; }
			internal set { _ownerJIconListView = value; }
		}
	}


	//自定义事件参数类
	public class JIconListViewEventArgs
	{
		private JIconListViewItem _selectItem;
		public JIconListViewItem SelectItem
		{
			get { return _selectItem; }
		}

		public JIconListViewEventArgs(JIconListViewItem selectitem)
		{
			_selectItem = selectitem;
		}
	}

}
