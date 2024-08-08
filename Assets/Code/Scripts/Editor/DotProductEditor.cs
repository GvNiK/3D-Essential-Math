using UnityEngine;
using UnityEditor;

public class DotProductEditor : EditorWindow
{
    [MenuItem("Tools/Dot Product")]
    public static void ShowWindow()
    {
        DotProductEditor window = (DotProductEditor)GetWindow(typeof(DotProductEditor), true, "Dot Product");
        window.Show();
    }
}
