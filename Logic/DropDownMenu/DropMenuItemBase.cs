using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI.Logic
{
    public class DropMenuItemBase : IDropDownMenuItem
    {
        private GameObject m_objTargetItem;

        public virtual string GetItemDesc()
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
}