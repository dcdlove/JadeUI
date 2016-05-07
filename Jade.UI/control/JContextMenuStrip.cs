/**
 * Copyroght jaderd.com 2011-2015
 * 
 * 
 * 
 * @author jaly
 * @date 2015-09-16 15:55:37
 * @version 1.0.0 
 */


using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Media;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Jade.UI
{
	public class JContextMenuStrip : ContextMenuStrip
	{
		public JContextMenuStrip()
		{
			this.Renderer = new JContextMenuStripRender();
		}

		public JContextMenuStrip(IContainer container)
		{
			container.Add(this);
			this.Renderer = new JContextMenuStripRender();//设置渲染
		}
	}

	public class JContextMenuStripRender : ToolStripProfessionalRenderer
	{
		ColorConfig colorconfig = new ColorConfig();//创建颜色配置类  
		/// <summary>  
		/// 渲染整个背景  
		/// </summary>  
		/// <param name="e"></param>  
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{

			e.ToolStrip.ForeColor = colorconfig.FontColor;
			//如果是下拉  
			if (e.ToolStrip is ToolStripDropDown)
			{
				e.Graphics.FillRectangle(new SolidBrush(colorconfig.DropDownItemBackColor), e.AffectedBounds);
			}
			//如果是菜单项  
			else if (e.ToolStrip is MenuStrip)
			{
				Blend blend = new Blend();
				float[] fs = new float[5] { 0f, 0.3f, 0.5f, 0.8f, 1f };
				float[] f = new float[5] { 0f, 0.5f, 0.9f, 0.5f, 0f };
				blend.Positions = fs;
				blend.Factors = f;
				//FillLineGradient(e.Graphics, e.AffectedBounds, colorconfig.MainMenuStartColor, colorconfig.MainMenuEndColor, 90f, blend);
			}
			else
			{
				base.OnRenderToolStripBackground(e);
			}
		}
		/// <summary>  
		/// 渲染下拉左侧图标区域  
		/// </summary>  
		/// <param name="e"></param>  
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
			FillLineGradient(e.Graphics, e.AffectedBounds, colorconfig.MarginStartColor, colorconfig.MarginEndColor, 0f, null);
		}
		/// <summary>  
		/// 渲染菜单项的背景  
		/// </summary>  
		/// <param name="e"></param>  
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			if (e.ToolStrip is MenuStrip)
			{
				//如果被选中或被按下  
				if (e.Item.Selected || e.Item.Pressed)
				{

					Blend blend = new Blend();
					float[] fs = new float[5] { 0f, 0.3f, 0.5f, 0.8f, 1f };
					float[] f = new float[5] { 0f, 0.5f, 1f, 0.5f, 0f };
					blend.Positions = fs;
					blend.Factors = f;
					FillLineGradient(e.Graphics, new Rectangle(0, 0, e.Item.Size.Width, e.Item.Size.Height), colorconfig.MenuItemStartColor, colorconfig.MenuItemEndColor, 90f, blend);
				}
				else
					base.OnRenderMenuItemBackground(e);
			}

			else if (e.ToolStrip is ToolStripDropDown)
			{
				if (e.Item.Selected)
				{
					e.Item.ForeColor = Color.White;
					FillLineGradient(e.Graphics, new Rectangle(0, 0, e.Item.Size.Width, e.Item.Size.Height), colorconfig.DropDownItemStartColor, colorconfig.DropDownItemEndColor, 90f, null);
				}
				else {
					e.Item.ForeColor = colorconfig.FontColor;
				}
			}
			else
			{
				base.OnRenderMenuItemBackground(e);
			}
		}
		/// <summary>  
		/// 渲染菜单项的分隔线  
		/// </summary>  
		/// <param name="e"></param>  
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			e.Graphics.DrawLine(new Pen(colorconfig.SeparatorColor), 0, 2, e.Item.Width, 2);
		}
		/// <summary>  
		/// 渲染边框  
		/// </summary>  
		/// <param name="e"></param>  
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is ToolStripDropDown)
			{
				e.Graphics.DrawRectangle(new Pen(colorconfig.DropDownBorder), new Rectangle(0, 0, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
			}
			else
			{
				base.OnRenderToolStripBorder(e);
			}
		}
		/// <summary>  
		/// 渲染箭头  
		/// </summary>  
		/// <param name="e"></param>  
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			e.ArrowColor = Color.Red;//设置为红色，当然还可以 画出各种形状  
			base.OnRenderArrow(e);
		}
		/// <summary>  
		/// 填充线性渐变  
		/// </summary>  
		/// <param name="g">画布</param>  
		/// <param name="rect">填充区域</param>  
		/// <param name="startcolor">开始颜色</param>  
		/// <param name="endcolor">结束颜色</param>  
		/// <param name="angle">角度</param>  
		/// <param name="blend">对象的混合图案</param>  
		private void FillLineGradient(Graphics g, Rectangle rect, Color startcolor, Color endcolor, float angle, Blend blend)
		{
			LinearGradientBrush linebrush = new LinearGradientBrush(rect, startcolor, endcolor, angle);
			if (blend != null)
			{
				linebrush.Blend = blend;
			}
			GraphicsPath path = new GraphicsPath();
			path.AddRectangle(rect);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillPath(linebrush, path);
		}
	}
	public class ColorConfig
	{

		private Color _marginstartcolor = ColorTranslator.FromHtml("#ededed");
		/// <summary>  
		/// 下拉菜单坐标图标区域开始颜色  
		/// </summary>  
		public Color MarginStartColor
		{
			get { return this._marginstartcolor; }
			set { this._marginstartcolor = value; }
		}
		private Color _marginendcolor = ColorTranslator.FromHtml("#ededed");

		/// <summary>  
		/// 下拉菜单坐标图标区域结束颜色  
		/// </summary>  
		public Color MarginEndColor
		{
			get { return this._marginendcolor; }
			set { this._marginendcolor = value; }
		}


		private Color _fontcolor = ColorTranslator.FromHtml("#333");
		/// <summary>  
		/// 菜单字体颜色  
		/// </summary>  
		public Color FontColor
		{
			get { return this._fontcolor; }
			set { this._fontcolor = value; }
		}

		private Color _separatorcolor = ColorTranslator.FromHtml("#F5F2F2");
		/// <summary>  
		/// 分割线颜色  
		/// </summary>  
		public Color SeparatorColor //没用
		{
			get { return this._separatorcolor; }
			set { this._separatorcolor = value; }
		}

		private Color _dropdownitembackcolor = ColorTranslator.FromHtml("#fafafa");
		/// <summary>  
		/// 下拉项背景颜色  
		/// </summary>  
		public Color DropDownItemBackColor
		{
			get { return this._dropdownitembackcolor; }
			set { this._dropdownitembackcolor = value; }
		}

		private Color _dropdownitemstartcolor = ColorTranslator.FromHtml("#3385ff");
		/// <summary>  
		/// 下拉项选中时开始颜色  
		/// </summary>  
		public Color DropDownItemStartColor
		{
			get { return this._dropdownitemstartcolor; }
			set { this._dropdownitemstartcolor = value; }
		}


		private Color _dorpdownitemendcolor = ColorTranslator.FromHtml("#2d78f4");
		/// <summary>  
		/// 下拉项选中时结束颜色  
		/// </summary>  
		public Color DropDownItemEndColor
		{
			get { return this._dorpdownitemendcolor; }
			set { this._dorpdownitemendcolor = value; }
		}

		private Color _menuitemstartcolor = ColorTranslator.FromHtml("#000");//无用
		/// <summary>  
		/// 主菜单项选中时的开始颜色  
		/// </summary>  
		public Color MenuItemStartColor
		{
			get { return this._menuitemstartcolor; }
			set { this._menuitemstartcolor = value; }
		}
		private Color _menuitemendcolor = Color.Red;//无用

		/// <summary>  
		/// 主菜单项选中时的结束颜色  
		/// </summary>  
		public Color MenuItemEndColor
		{
			get { return this._menuitemendcolor; }
			set { this._menuitemendcolor = value; }
		}


		private Color _mainmenubackcolor = ColorTranslator.FromHtml("#f0f2f5");
		/// <summary>  
		/// 主菜单背景色  
		/// </summary>  
		public Color MainMenuBackColor
		{
			get { return this._mainmenubackcolor; }
			set { this._mainmenubackcolor = value; }
		}
		private Color _mainmenustartcolor = Color.Blue;
		/// <summary>  
		/// 主菜单背景开始颜色  
		/// </summary>  
		public Color MainMenuStartColor//没用
		{
			get { return this._mainmenustartcolor; }
			set { this._mainmenustartcolor = value; }
		}
		private Color _mainmenuendcolor = Color.Red;//没用
		/// <summary>  
		/// 主菜单背景结束颜色  
		/// </summary>  
		public Color MainMenuEndColor
		{
			get { return this._mainmenuendcolor; }
			set { this._mainmenuendcolor = value; }
		}


		private Color _dropdownborder = ColorTranslator.FromHtml("#f0f2f5");
		/// <summary>  
		/// 下拉区域边框颜色  
		/// </summary>  
		public Color DropDownBorder
		{
			get { return this._dropdownborder; }
			set { this._dropdownborder = value; }
		}
	}
}
