using UnityEngine;
using System.Collections;
using UnityEditor;


namespace UnityEngine.UI
{
    [CustomEditor(typeof(DropDownMenuItem))]
    public class DropDownMenuItemInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DropDownMenuItem item = (DropDownMenuItem)target;
            item.ItemText = EditorGUILayout.ObjectField("Item Desc", item.ItemText, typeof(Text), true) as Text;
        }
    }
}
