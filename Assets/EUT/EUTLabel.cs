using UnityEditor;
using UnityEngine;

namespace EUT
{
	public class EUTLabel : EUTRender
	{
		public string Text;
		private GUIStyle style;
		protected TextAnchor alignment = TextAnchor.MiddleLeft;

		public EUTLabel(string text, params GUILayoutOption[] options) : base()
		{
			this.Text = text;
            this.layoutOptions = options;
		}

		public EUTLabel SetAlignment(TextAnchor anchor)
		{
			alignment = anchor;
			return this;
		}

		public EUTLabel(string text)
		{
			this.Text = text;
		}

		public EUTLabel SetText(string text)
		{
			this.Text = text;
			return this;
		}

		public EUTLabel SetColor(Color color)
		{
            AssignColor(color);
			return this;
		}

		public EUTLabel SetFontSize(int font)
		{
			fontSize = font;
			return this;
		}

		public override void Render()
		{
			if(style == null)
				style = GUI.skin.label;

			var prevColor = GUI.color;
			if(hasColor)
				GUI.color = color;
			if (fontSize > 0)
			{
				style.fontSize = fontSize;
			}
			var size = style.CalcSize(new GUIContent(Text)) * 1.1f + new Vector2(10, 4);
			style.stretchWidth = false;

			var maxX = maxWidth > 0 ? maxWidth : 512;
			// var maxY = maxHeight > 0 ? maxHeight : 512;

			style.fixedWidth = Mathf.Min(size.x, 30, maxX);
			style.alignment = alignment;
			GUILayout.Label(Text, style, layoutOptions);


			GUI.color = prevColor;
		}
	}
}