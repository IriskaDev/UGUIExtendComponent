using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class DropDownMenuItem : Selectable
    {
        [HideInInspector][SerializeField] Text m_txtItemText;
        public Text ItemText
        {
            get
            {
                return m_txtItemText;
            }
            set
            {
                m_txtItemText = value;
            }
        }
        public string text
        {
            get
            {
                if (m_txtItemText == null)
                    return "";
                return m_txtItemText.text;
            }
            set
            {
                if (m_txtItemText == null)
                    return;
                m_txtItemText.text = value;
            }
        }

        public delegate void ON_ITEM_SELECTED(DropDownMenuItem item);
        private ON_ITEM_SELECTED onItemSelected;

        protected override void Awake()
        {
            base.Awake();
        }

        public DropDownMenuItem Copy(ScrollRect scrollRect = null)
        {
            GameObject newItemObj = GameObject.Instantiate(gameObject) as GameObject;
            newItemObj.transform.SetParent(gameObject.transform.parent, false);
            newItemObj.SetActive(true);
            DropDownMenuItem newItemComp = newItemObj.GetComponent<DropDownMenuItem>();
            UGUIEventHandler.AddListener(newItemComp.ItemText.gameObject, UGUIEventType.POINTER_CLICK, newItemComp.OnItemSelected);
            if (scrollRect != null)
            {

            }
            return newItemComp;
        }

        private void OnItemSelected(PointerEventData evtDat)
        {
            if (onItemSelected != null)
            {
                onItemSelected.Invoke(this);
            }
        }

        public void AddOnItemSelectedCallback(ON_ITEM_SELECTED func)
        {
            onItemSelected = func;
        }
    }
}
