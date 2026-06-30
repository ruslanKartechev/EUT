using UnityEditor;
using UnityEngine;

namespace EUT
{
	public class EUT_Test_Mono : UnityEngine.MonoBehaviour
	{
		public int selectedIndex;

		public int GetIdx() => selectedIndex;

		public void SetIdx(int val)
		{
			selectedIndex = val;
			if(selectedIndex < 0)
				selectedIndex += 10;
			else
				selectedIndex %= 10;
		}

	}

	[CustomEditor(typeof(EUT_Test_Mono))]
	public class EUT_Test_MonoEditor : UnityEditor.Editor
	{
		private EUTRoot root;

		public void OnEnable()
		{
			var me = target as EUT_Test_Mono;

			root = new ();
			var row1 = new EUTHorizontal();
			var row2 = new EUTHorizontal();
			var row3 = new EUTHorizontal();

            row1.AddElement(EUTButton.T_BtnFlexMid("Clear", () => {}).SetColor(EUTColors.amber));
            row1.AddElement(EUTButton.T_BtnFlexMid("Populate", () => {}).SetColor(EUTColors.amber));

            row2.AddElement(EUTButton.T_BtnFlexMid("Mono", () => {}).SetColor(EUTColors.bisque));
            row2.AddElement(EUTButton.T_BtnFlexMid("IL2CPP", () => {}).SetColor(EUTColors.bisque));

            row3.AddElement(EUTButton.T_BtnFlexMid("Kill", () => {}).SetColor(EUTColors.blood));
            row3.AddElement(EUTButton.T_BtnFlexMid("Bill", () => {}).SetColor(EUTColors.blood));

			root.AddElement(row1);
			root.AddElement(row2);
			root.AddElement(row3);

			const int sizeX = 4;
			const int sizeY = 4;

			var grid = new EUTGrid(sizeX, sizeY);
            const int btnSize = 50;
			for (var y = 0; y < sizeY; y++)
			{
				for (var x = 0; x < sizeX; x++)
				{
					var btn = EUTButton.T_BtnSquare($"({x},{y})", () => {}, btnSize).SetColor(EUTColors.emerald);
					grid.AddElement(btn);
				}
			}

			root.AddElement(new EUTSpace(15));

			root.AddElement(grid);

			root.AddElement(new EUTSpace(15));

			var selector = new EUTSelector(me.GetIdx, me.SetIdx, me.GetIdx());
			selector.SetButtonsColor(EUTColors.floral);
			selector.SetLabelColor(EUTColors.peach);
			selector.SetFontSize(16);
			root.AddElement(selector);

		}


		public override void OnInspectorGUI()
		{
			root.Render();
		}
	}

}