using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


namespace UnityEngine.UI
{
    public class DropDownMenu : UIBehaviour
    {
        public delegate void ON_ITEM_SELECTED(GameObject item);
        private ON_ITEM_SELECTED onItemSelected;

        [HideInInspector][SerializeField] Button m_btnShowMenu;
        public Button BtnShowMenu
        {
            get
            {
                return m_btnShowMenu;
            }
            set
            {
                m_btnShowMenu = value;
            }
        }
        [HideInInspector][SerializeField] Button m_btnHideMenu;
        public Button BtnHideMenu
        {
            get
            {
                return m_btnHideMenu;
            }
            set
            {
                m_btnHideMenu = value;
            }
        }
        [HideInInspector][SerializeField] Text m_txtSelectedItem;
        public Text TxtSelectedItem
        {
            get
            {
                return m_txtSelectedItem;
            }
            set
            {
                m_txtSelectedItem = value;
            }
        }
        [HideInInspector][SerializeField] Image m_imgBackground;
        public Image Background
        {
            get
            {
                return m_imgBackground;
            }
            set
            {
                m_imgBackground = value;
            }
        }
        [HideInInspector][SerializeField] DropDownMenuFrame m_compMenuFrame;
        public DropDownMenuFrame MenuFrame
        {
            get
            {
                return m_compMenuFrame;
            }
            set
            {
                m_compMenuFrame = value;
            }
        }
        [HideInInspector][SerializeField] DropDownMenuItem m_compItemTemplate;
        public DropDownMenuItem ItemTemplate
        {
            get
            {
                return m_compItemTemplate;
            }
            set
            {
                m_compItemTemplate = value;
            }
        }
        [HideInInspector][SerializeField] Mask m_compMask;
        public Mask MenuMask
        {
            get
            {
                return m_compMask;
            }
            set
            {
                m_compMask = value;
            }
        }
        [HideInInspector][SerializeField] VerticalLayoutGroup m_compLayout;
        public VerticalLayoutGroup Layout
        {
            get
            {
                return m_compLayout;
            }
            set
            {
                m_compLayout = value;
            }
        }
        [HideInInspector][SerializeField] bool m_bIsHiding = false;
        public bool IsHiding
        {
            get
            {
                return m_bIsHiding;
            }
            set
            {
                m_bIsHiding = value;
            }
        }
        [SerializeField] public float m_fTweeningTime;

        private bool m_bIsTweening = false;
        private bool IsTweening
        {
            set
            {
                if (IsHiding)
                    return;
                if (value)
                    Layout.enabled = false;
                else
                    Layout.enabled = true;
            }
            get
            {
                return m_bIsTweening;
            }
        }
        private RectTransform m_transCachedTrans;

        private void ResizeMenu()
        {
            RectTransform maskRect = (m_compMask.transform as RectTransform);
            int childCnt = m_compMenuFrame.cachedTransform.childCount - 1;
            childCnt = childCnt == 0 ? 1 : childCnt;
            float cellY = m_compMenuFrame.grid.cellSize.y;
            float topPadding = m_compMenuFrame.grid.padding.top;
            float bottomPadding = m_compMenuFrame.grid.padding.bottom;
            float spaceY = m_compMenuFrame.grid.spacing.y;
            float resultY = childCnt * cellY + (childCnt - 1) * spaceY + topPadding + bottomPadding;
            maskRect.sizeDelta = new Vector2(maskRect.rect.width, resultY);
        }

        public DropDownMenuItem AddSingleItem(string itemDesc)
        {
            DropDownMenuItem newItem = AddItemObj(itemDesc);
            ResizeMenu();
            return newItem;
        }

        public List<DropDownMenuItem> AddMultiItem(List<string> itemDescList)
        {
            List<DropDownMenuItem> newItemList = new List<DropDownMenuItem>();
            for (int i = 0; i < itemDescList.Count; ++i)
            {
                AddItemObj(itemDescList[i]);
            }
            ResizeMenu();
            return newItemList;
        }

        private DropDownMenuItem AddItemObj(string itemDesc)
        {
            DropDownMenuItem newItem = m_compItemTemplate.Copy();
            newItem.text = itemDesc;
            newItem.AddOnItemSelectedCallback(OnItemSelected);
            return newItem;
        }

        public void RemoveItem(GameObject target)
        {
            GameObject.Destroy(target);
        }

        public string CurItemDesc
        {
            get
            {
                if (m_txtSelectedItem != null)
                    return m_txtSelectedItem.text;
                return "";
            }
            set
            {
                if (m_txtSelectedItem != null)
                    m_txtSelectedItem.text = value;
            }
        }

        private void OnShowMenu(PointerEventData evtDat)
        {
            if (IsTweening)
                return;
            IsTweening = true;
            m_btnShowMenu.gameObject.SetActive(false);
            m_btnHideMenu.gameObject.SetActive(true); 
            if (m_compMask != null)
                m_compMask.gameObject.SetActive(true);
            if (m_compMenuFrame == null)
                return;
            m_compMenuFrame.gameObject.SetActive(true);
            Vector3 targetPos = m_compMask.transform.position;
            Hashtable tweenArgs = new Hashtable();
            tweenArgs.Add("position", targetPos);
            tweenArgs.Add("time", m_fTweeningTime);
            tweenArgs.Add("oncomplete", "OnMenuShown");
            tweenArgs.Add("oncompletetarget", gameObject);
            iTween.MoveTo(m_compMenuFrame.gameObject, tweenArgs);
        }

        private void OnMenuShown()
        {
            IsHiding = false;
            IsTweening = false;
        }

        private void OnHideMenu(PointerEventData evtDat)
        {
            if (IsTweening)
                return;
            IsTweening = true;
            m_btnShowMenu.gameObject.SetActive(true);
            m_btnHideMenu.gameObject.SetActive(false);
            Vector3 targetPos = m_compMask.transform.position + 
                m_compMenuFrame.cachedTransform.up * m_compMenuFrame.cachedTransform.rect.height * m_compMenuFrame.cachedTransform.lossyScale.y +
                m_compMenuFrame.cachedTransform.up * m_transCachedTrans.rect.height * m_transCachedTrans.lossyScale.y;
            Hashtable tweenArgs = new Hashtable();
            tweenArgs.Add("position", targetPos);
            tweenArgs.Add("time", m_fTweeningTime);
            tweenArgs.Add("oncomplete", "OnMenuHidden");
            tweenArgs.Add("oncompletetarget", gameObject);
            iTween.MoveTo(m_compMenuFrame.gameObject, tweenArgs);
        }

        private void OnMenuHidden()
        {
            if (m_compMask != null)
                m_compMask.gameObject.SetActive(false);
            m_compMenuFrame.gameObject.SetActive(false);
            IsHiding = true;
            IsTweening = false;
        }

        private void HideMenuWithoutAni()
        {
            IsHiding = true;
            m_btnShowMenu.gameObject.SetActive(true);
            m_btnHideMenu.gameObject.SetActive(false);
            RectTransform maskRect = m_compMask.transform as RectTransform;
            RectTransform menuFrameRect = m_compMenuFrame.transform as RectTransform;
            Vector3 targetPos = m_compMask.transform.position +
                menuFrameRect.up * maskRect.rect.height * maskRect.lossyScale.y +
                menuFrameRect.up * m_transCachedTrans.rect.height * m_transCachedTrans.lossyScale.y;
            m_compMenuFrame.gameObject.SetActive(false);
            menuFrameRect.position = targetPos;
            if (m_compMask != null)
                m_compMask.gameObject.SetActive(false);
        }

        private void OnMenuStateChanged(PointerEventData evtDat)
        {
            if (IsHiding)
            {
                OnShowMenu(evtDat);
            }
            else
            {
                OnHideMenu(evtDat);
            }
        }

        private void OnItemSelected(DropDownMenuItem item)
        {
            if (m_txtSelectedItem != null)
                m_txtSelectedItem.text = item.text;
            OnHideMenu(null);
            if (onItemSelected != null)
                onItemSelected.Invoke(item.gameObject);
        }

        public void AddOnSelectedCallback(ON_ITEM_SELECTED func)
        {
            onItemSelected += func;
        }

        public void RemoveOnSelectedCallback(ON_ITEM_SELECTED func)
        {
            onItemSelected -= func;
        }

        protected override void Awake()
        {
            base.Awake();
            if (m_btnShowMenu != null)
            {
                UGUIEventHandler.AddListener(m_btnShowMenu.gameObject, UGUIEventType.POINTER_CLICK, OnShowMenu);
            }

            if (m_btnHideMenu != null)
            {
                UGUIEventHandler.AddListener(m_btnHideMenu.gameObject, UGUIEventType.POINTER_CLICK, OnHideMenu);
            }

            if (m_txtSelectedItem != null)
            {
                UGUIEventHandler.AddListener(m_txtSelectedItem.gameObject, UGUIEventType.POINTER_CLICK, OnMenuStateChanged);
            }

            if (m_imgBackground != null)
            {
                UGUIEventHandler.AddListener(m_imgBackground.gameObject, UGUIEventType.POINTER_CLICK, OnMenuStateChanged);
            }

            m_transCachedTrans = GetComponent<RectTransform>();

            ////---------------test code
            //List<string> itemList = new List<string>{ "Item A", "Item B", "Item C", "Item D", "Item E", "Item F", "Item G", "Item H" };
            //AddMultiItem(itemList);

            HideMenuWithoutAni();
        }

        void OnGUI()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (!IsHiding)
                {
                    OnHideMenu(null);
                }
            }
        }
    }
}
