# EUT (Editor Utils Templates)

A lightweight, object-oriented wrapper for Unity IMGUI. Build custom editor inspectors using declarative tree structures instead of procedural IMGUI calls.

## Features
* **Declarative Layouts:** Chain `EUTVertical`, `EUTHorizontal`, and `EUTGrid`.
* **Standard Controls:** Build with `EUTLabel`, `EUTButton`, and `EUTTextField`.
* **Reflection Setup:** Auto-generate UI buttons for methods using `[EditorFunc]`.
* **Color Palette:** Access 100+ predefined UI colors via `EUTColors`.

## Usage

```csharp
using EUT;
using UnityEditor;

[CustomEditor(typeof(MyTarget))]
public class MyEditor : Editor
{
    private EUTElement root;

    private void OnEnable()
    {
        root = new EUTVertical("box")
            .Add(new EUTLabel("Settings"))
            .Add(new EUTHorizontal()
                .Add(new EUTButton("Action", () => Run(), null, EUTColors.lime))
                .Add(new EUTTextField("Name", "val", (v) => Update(v))));
    }

    public override void OnInspectorGUI()
    {
        root?.Render();
    }
}
```