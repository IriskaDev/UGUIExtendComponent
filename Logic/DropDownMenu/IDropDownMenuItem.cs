using System;
using UnityEngine;

namespace UnityEngine.UI.Logic
{
    public interface IDropDownMenuItem
    {
        string GetItemDesc();
        void SetItemObj(GameObject obj);
        GameObject GetItemObj();
    }

}
