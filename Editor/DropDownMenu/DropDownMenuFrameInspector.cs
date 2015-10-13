using UnityEngine;
using System.Collections;
using UnityEditor;


namespace UnityEngine.UI
{
    [CustomEditor(typeof(DropDownMenuFrame))]
    public class DropDownMenuFrameInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DropDownMenuFrame item = (DropDownMenuFrame)target;
            item.cachedTransform = EditorGUILayout.ObjectField("Rect Trans", item.cachedTransform, typeof(RectTransform), true) as RectTransform;
            item.grid = EditorGUILayout.ObjectField("Grid", item.grid, typeof(GridLayoutGroup), true) as GridLayoutGroup;
        }
    }
}
