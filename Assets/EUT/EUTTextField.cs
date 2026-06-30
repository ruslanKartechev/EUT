using System;
using UnityEditor;
using UnityEngine;

namespace EUT
{
	public class EUTTextField : EUTRender
	{
		public string Label;
		public string Value;
		public Action<string> OnValueChanged;

		public EUTTextField(string label, string initialValue,
		                    Action<string> onValueChanged = null,
		                    params GUILayoutOption[] options)
		{
			this.layoutOptions = options;
			this.Label = label;
			this.Value = initialValue;
			this.OnValueChanged = onValueChanged;
		}

		public override void Render()
		{
			EditorGUI.BeginChangeCheck();

			if (string.IsNullOrEmpty(Label))
				Value = EditorGUILayout.TextField(Value, layoutOptions);
			else
				Value = EditorGUILayout.TextField(Label, Value, layoutOptions);

			if (EditorGUI.EndChangeCheck())
			{
				OnValueChanged?.Invoke(Value);
			}
		}
	}
}