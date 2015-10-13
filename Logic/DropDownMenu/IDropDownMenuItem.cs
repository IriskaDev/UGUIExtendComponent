using System;
using UnityEngine;

public interface IDropDownMenuItem
{
    string GetItemDesc();
    void SetItemObj(GameObject obj);
    GameObject GetItemObj();
}

