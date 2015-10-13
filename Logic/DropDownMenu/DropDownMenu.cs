using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine.UI.Logic
{
    public class DropDownMenu<T> where T: IDropDownMenuItem
    {
        private DropDownMenu m_compMonoCtrl;

        public delegate void ON_SELECTED_CALLBACK(T item);
        public ON_SELECTED_CALLBACK onSelectedCallback;

        private List<T> m_lLogicInsList;
        private Dictionary<GameObject, T> m_dictLogicInsMapper;

        private T m_tCurSelectedItem;

        public DropDownMenu(DropDownMenu mono)
        {
            m_compMonoCtrl = mono;
            m_lLogicInsList = new List<T>();
            m_dictLogicInsMapper = new Dictionary<GameObject, T>();
            mono.AddOnSelectedCallback(OnItemSelected);
        }

        public void AddItem(T item)
        {
            DropDownMenuItem itemObj = m_compMonoCtrl.AddSingleItem(item.GetItemDesc());
            item.SetItemObj(itemObj.gameObject);
            m_lLogicInsList.Add(item);
            m_dictLogicInsMapper.Add(itemObj.gameObject, item);
        }

        public void AddItemList(List<T> itemList)
        {
            List<string> strList = new List<string>();
            for (int i = 0; i < itemList.Count; ++i)
                strList.Add(itemList[i].GetItemDesc());

            List<DropDownMenuItem> newItemList = m_compMonoCtrl.AddMultiItem(strList);
            for (int i = 0; i < newItemList.Count; ++i)
            {
                T item = itemList[i];
                item.SetItemObj(newItemList[i].gameObject);
                m_lLogicInsList.Add(item);
                m_dictLogicInsMapper.Add(newItemList[i].gameObject, item);
            }
        }

        public void RemoveItem(T item)
        {
            m_lLogicInsList.Remove(item);
            m_dictLogicInsMapper.Remove(item.GetItemObj());
        }

        public T CurSelectedItem
        {
            get
            {
                return m_tCurSelectedItem;
            }
            set
            {
                T item;
                if (!m_dictLogicInsMapper.TryGetValue(value.GetItemObj(), out item))
                {
                    throw new Exception("No Such Item in List!!");
                }
                m_tCurSelectedItem = item;
                m_compMonoCtrl.CurItemDesc = item.GetItemDesc();
            }
        }

        private void OnItemSelected(GameObject itemObj)
        {
            if (onSelectedCallback != null)
            {
                T item;
                if (m_dictLogicInsMapper.TryGetValue(itemObj, out item))
                {
                    onSelectedCallback.Invoke(item);
                }
            }
        }

        public void AddOnSelectedCallback(ON_SELECTED_CALLBACK func)
        {
            onSelectedCallback += func;
        }

        public void RemoveOnSelectedCallback(ON_SELECTED_CALLBACK func)
        {
            onSelectedCallback -= func;
        }
    }
}
