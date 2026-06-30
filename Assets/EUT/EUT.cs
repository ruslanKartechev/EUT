using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EUT
{
    public delegate int IntGetter();

    public delegate void IntSetter(int value);

    // --- 1. BASE ELEMENT ---
    public abstract class EUTElement
    {
        // Optional layout modifiers can be added here
        protected GUILayoutOption[] layoutOptions;

        protected float height;
        protected float width;
        protected bool hasWidth;
        protected bool hasHeight;

        protected void SetWidth(float width)
        {
            this.width = width;
            hasWidth = true;
        }

        protected void SetHeight(float h)
        {
            this.height = h;
            hasHeight = true;
        }


        public EUTElement(params GUILayoutOption[] options)
        {
            this.layoutOptions = options;
        }
        public abstract void Render();
    }

    public abstract class EUTRender : EUTElement
    {
        protected bool hasColor;
        protected Color color;
        protected bool flexWidth;
        protected bool flexHeight;
        protected int fontSize = 14;

        protected int minWidth;
        protected int minHeight;
        protected bool hasMinWidth;
        protected bool hasMinHeight;

        protected int maxWidth;
        protected int maxHeight;
        protected bool hasMaxWidth;
        protected bool hasMaxHeight;

        protected void AssignMinWidth(int val)
        {
            minWidth = val;
            hasMinWidth = true;
        }
        protected void AssignMinHeight(int val)
        {
            minHeight = val;
            hasMinHeight = true;
        }

        protected void AssignMaxWidth(int val)
        {
            maxWidth = val;
            hasMaxWidth = true;
        }
        protected void AssignMaxHeight(int val)
        {
            maxHeight = val;
            hasMaxHeight = true;
        }

        protected void AssignColor(Color color)
        {
            this.color = color;
            this.hasColor = true;
        }
    }

    public abstract class EUTContainer : EUTElement
    {
        protected List<EUTElement> children = new List<EUTElement>();

        public EUTContainer(params GUILayoutOption[] options) : base(options)
        { }
        public int Count => children.Count;

        public virtual EUTContainer AddElement(EUTElement element)
        {
            if (element != null)
                children.Add(element);
            return this;
        }

        protected void RenderChildren()
        {
            foreach (var child in children)
                child.Render();
        }
    }

    public class EUTVertical : EUTContainer
    {
        public override void Render()
        {
            EditorGUILayout.BeginVertical();
            RenderChildren();
            EditorGUILayout.EndVertical();
        }
    }
    public class EUTHorizontal : EUTContainer
    {
        public override void Render()
        {
            EditorGUILayout.BeginHorizontal();
            RenderChildren();
            EditorGUILayout.EndHorizontal();
        }
    }


    public class EUTGrid : EUTContainer
    {
        public int sizeX;
        public int sizeY;

        private int _currentX = 0;
        private int _currentY = 0;

        public List<List<EUTElement>> rowMajorGrid = new();

        public EUTGrid(int sizeX, int sizeY, params GUILayoutOption[] options) : base(options)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            // Pre-initialize the empty rows so we can safely add to them later
            for (int y = 0; y < sizeY; y++)
            {
                rowMajorGrid.Add(new List<EUTElement>(sizeX));
            }
        }

        public override EUTContainer AddElement(EUTElement element)
        {
            if (_currentY >= sizeY)
            {
                Debug.LogWarning("EUTGrid is full! Cannot add more elements.");
                return this;
            }
            rowMajorGrid[_currentY].Add(element);
            _currentX++;
            if (_currentX >= sizeX)
            {
                _currentX = 0;
                _currentY++;
            }
            return this;
        }

        public void FlipHorizontal()
        {
            foreach (var row in rowMajorGrid)
            {
                row.Reverse();
            }
        }

        public void FlipVertical()
        {
            rowMajorGrid.Reverse();
        }

        public override void Render()
        {
            EditorGUILayout.BeginVertical(layoutOptions);
            for (var y = 0; y < rowMajorGrid.Count; y++)
            {
                EditorGUILayout.BeginHorizontal();
                var row = rowMajorGrid[y];
                for (var x = 0; x < row.Count; x++)
                {
                    if (row[x] != null)
                        row[x].Render();
                    else
                        GUILayout.FlexibleSpace();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
    }

    public class EUTRoot : EUTContainer
    {
        public override void Render()
        {
            RenderChildren();
        }
    }
}