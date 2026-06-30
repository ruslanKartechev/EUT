using UnityEngine;

namespace EUT
{

	public class EUTSelector : EUTElement
	{



		protected IntGetter getter;
		protected IntSetter setter;
		protected EUTButton btnLeft;
		protected EUTButton btnRight;
		protected EUTLabel label;
		protected int value = 0;


		public EUTSelector(IntGetter getter, IntSetter setter, int value)
		{
			this.getter = getter;
			this.setter = setter;
			btnLeft = EUTButton.T_BtnSquare("<<", Prev);
			btnRight = EUTButton.T_BtnSquare(">>", Next);
			this.value = value;
			label = new EUTLabel(value.ToString());
			label.SetAlignment(TextAnchor.LowerCenter);
		}

		public EUTSelector SetButtonsColor(Color color)
		{
			btnLeft.SetColor(color);
			btnRight.SetColor(color);
			return this;
		}

		public EUTSelector SetLabelColor(Color color)
		{
			label.SetColor(color);
			return this;
		}

		public EUTSelector SetFontSize(int size)
		{
			label.SetFontSize(size);
			return this;
		}

		private void Prev()
		{
			setter?.Invoke(value - 1);
			value = getter.Invoke();
			label.SetText(value.ToString());
		}

		private void Next()
		{
			setter?.Invoke(value + 1);
			value = getter.Invoke();
			label.SetText(value.ToString());
		}

		public override void Render()
		{
			GUILayout.BeginHorizontal();
			btnLeft.Render();
			label.Render();
			btnRight.Render();
			GUILayout.EndHorizontal();

		}
	}
}