using UnityEngine;
using System.Collections;
using UnityEditor;


namespace UnityEngine.UI
{
    [CustomEditor(typeof(DropDownMenu))]
    public class DropDownMenuInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DropDownMenu menu = (DropDownMenu)target;
            menu.BtnShowMenu = EditorGUILayout.ObjectField("Show Menu Button", menu.BtnShowMenu, typeof(Button), true) as Button;
            menu.BtnHideMenu = EditorGUILayout.ObjectField("Hide Menu Button", menu.BtnHideMenu, typeof(Button), true) as Button;
            menu.TxtSelectedItem = EditorGUILayout.ObjectField("Selected Item", menu.TxtSelectedItem, typeof(Text), true) as Text;
            menu.Background = EditorGUILayout.ObjectField("Background", menu.Background, typeof(Image), true) as Image;
            menu.MenuFrame = EditorGUILayout.ObjectField("Menu Frame", menu.MenuFrame, typeof(DropDownMenuFrame), true) as DropDownMenuFrame;
            menu.MenuMask = EditorGUILayout.ObjectField("Menu Mask", menu.MenuMask, typeof(Mask), true) as Mask;
            menu.Layout = EditorGUILayout.ObjectField("Menu Layout", menu.Layout, typeof(VerticalLayoutGroup), true) as VerticalLayoutGroup;
            menu.ItemTemplate = EditorGUILayout.ObjectField("Item Template", menu.ItemTemplate, typeof(DropDownMenuItem), true) as DropDownMenuItem;
            menu.IsHiding = EditorGUILayout.Toggle("Is Menu Hiding", menu.IsHiding);
            menu.m_fTweeningTime = EditorGUILayout.FloatField("Tweening Time", menu.m_fTweeningTime);
        }
    }
}
