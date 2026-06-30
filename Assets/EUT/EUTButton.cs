using System;
using UnityEngine;

namespace EUT
{
	public class EUTButton : EUTRender
	{
		public string Text;
		public Action OnClick;
		private GUIStyle style;

		public EUTButton(string text, Action onClick,
		                 GUIStyle style = null,
		                 params GUILayoutOption[] options)
		{
			Text = text;
			OnClick = onClick;
			if (style != null)
			{
				this.style = style;
			}
			layoutOptions = options;
		}

		public EUTButton(string text, Action onClick, Color color)
		{
			Text = text;
			OnClick = onClick;
			AssignColor(color);
		}
		public EUTButton(string text, Action onClick)
		{
			Text = text;
			OnClick = onClick;
			AssignColor(color);
		}


		public static EUTButton T_BtnSquare(string text, Action onClick)
		{
			const int size = 36;
			var btn = new EUTButton(text, onClick).SetFlexWidth()
			                                      .SetMinWidth(size)
			                                      .SetMinHeight(size)
			                                      .SetMaxWidth(size)
			                                      .SetMaxHeight(size);
			return btn;
		}
		public static EUTButton T_BtnSquare(string text, Action onClick, int size)
		{
			var btn = new EUTButton(text, onClick).SetFlexWidth()
			                                      .SetMinWidth(size)
			                                      .SetMinHeight(size)
			                                      .SetMaxWidth(size)
			                                      .SetMaxHeight(size);
			return btn;
		}

		public static EUTButton T_BtnFlexMid(string text, Action onClick)
		{
			var btn = new EUTButton(text, onClick).SetFlexWidth()
			                                      .SetMinWidth(100)
			                                      .SetMinHeight(35);
			return btn;
		}

		public static EUTButton T_BtnFlexLarge(string text, Action onClick)
		{
			var btn = new EUTButton(text, onClick).SetFlexWidth()
			                                      .SetMinWidth(160)
			                                      .SetMinHeight(42);
			return btn;
		}


		public EUTButton SetColor(Color color)
		{
			AssignColor(color);
			return this;
		}

		public EUTButton SetFlexWidth()
		{
			this.hasWidth = false;
			this.flexWidth = true;
			return this;
		}
		public EUTButton SetFlexHeight()
		{
			this.hasHeight = false;
			this.flexHeight = true;
			return this;
		}
		public EUTButton SetWidth(int width)
		{
			this.width = width;
			this.hasWidth = true;
			this.flexWidth = false;
			return this;
		}
		public EUTButton SetHeight(int height)
		{
			this.height = height;
			this.hasHeight = true;
			this.flexHeight = false;
			return this;
		}

		public EUTButton SetFont(int font)
		{
			this.fontSize = font;
			return this;
		}
		public EUTButton SetMinHeight(int val)
		{
			AssignMinHeight(val);
			return this;
		}
		public EUTButton SetMinWidth(int val)
		{
            AssignMinWidth(val);
			return this;
		}
		public EUTButton SetMaxHeight(int val)
		{
			AssignMaxHeight(val);
			return this;
		}
		public EUTButton SetMaxWidth(int val)
		{
			AssignMaxWidth(val);
			return this;
		}

		public override void Render()
		{
			if(style == null)
				style = GUI.skin.button;

			var prevColor = GUI.color;
			if(hasColor)
				GUI.color = color;

			style.stretchWidth = flexWidth;
			style.stretchHeight = flexHeight;

			var labSkin = GUI.skin.label;
			if (fontSize > 0){
				labSkin.fontSize = fontSize;
                style.fontSize = fontSize;
			}
			var size = labSkin.CalcSize(new GUIContent(Text)) * 1.1f + new Vector2(10, 4);
			if (flexWidth)
			{
				style.stretchWidth = false;
				style.fixedWidth = (int)size.x;
			}
			else if (hasWidth)
			{
				style.stretchWidth = false;
				style.fixedWidth = width;
			}
			else
			{
				style.stretchWidth = true;
			}

			if (flexHeight)
			{
				style.stretchHeight = false;
				style.fixedHeight = (int)size.y;
			}
			else if (hasWidth)
			{
				style.stretchHeight = false;
				style.fixedHeight = height;
			}
			else
			{
				style.stretchHeight = true;
			}

			if (hasMinWidth)
				style.fixedWidth = Mathf.Clamp(style.fixedWidth, minWidth, int.MaxValue);
			if (hasMinHeight)
				style.fixedHeight = Mathf.Clamp(style.fixedHeight, minHeight, int.MaxValue);

			if (hasMaxWidth)
				style.fixedWidth = Mathf.Clamp(style.fixedWidth, minWidth, maxWidth);
			if (hasMaxHeight)
				style.fixedHeight = Mathf.Clamp(style.fixedHeight, minHeight, maxHeight);

			// UnityEngine.Debug.Log($"size: {style.fixedWidth}, {style.fixedHeight}. font {style.fontSize}");
			if (GUILayout.Button(Text, style, layoutOptions))
			{
				OnClick?.Invoke();
			}

			GUI.color = prevColor;
		}
	}


}