using System;
using System.Collections.Generic;
using UnityEngine;

public class DropMenuItemBase : IDropDownMenuItem
{
    private GameObject m_objTargetItem;

    public string GetItemDesc()
    {
        throw new NotImplementedException();
    }

    public void SetItemObj(GameObject obj)
    {
        m_objTargetItem = obj;
    }

    public GameObject GetItemObj()
    {
        return m_objTargetItem;
    }
}
