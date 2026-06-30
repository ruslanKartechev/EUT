using UnityEngine;

namespace EUT
{
	/// <summary>
	/// Simply Inserts a Space between previous and the next Element (GUI.Space(size))
	/// </summary>
	public class EUTSpace : EUTElement
	{
		public int space = 10;
		public EUTSpace(){}

		public EUTSpace(int space)
		{
			this.space = space;
		}

		public EUTSpace SetSpace(int space)
		{
			this.space = space;
			return this;
		}

		public override void Render()
		{
			GUILayout.Space(space);
		}
	}
}