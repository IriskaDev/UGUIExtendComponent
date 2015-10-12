using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


namespace UnityEngine.UI
{
    public class DropDownMenu : UIBehaviour
    {
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
                if (value != null)
                    m_vec3MenuFrameOriPos = value.transform.position;
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
        [SerializeField] float m_fTweeningTime = 0.3f;

        [SerializeField] private Vector3 m_vec3MenuFrameOriPos;
        private RectTransform m_transCachedTrans;

        public DropDownMenuItem AddItem(string itemDesc)
        {
            DropDownMenuItem newItem = m_compItemTemplate.Copy();
            newItem.text = itemDesc;
            newItem.AddOnItemSelectedCallback(OnItemSelected);
            return newItem;
        }

        private void OnShowMenu(PointerEventData evtDat)
        {
            IsHiding = false;
            m_btnShowMenu.gameObject.SetActive(false);
            m_btnHideMenu.gameObject.SetActive(true); 
            if (m_compMask != null)
                m_compMask.gameObject.SetActive(true);
            if (m_compMenuFrame == null)
                return;
            m_compMenuFrame.gameObject.SetActive(true);
            Vector3 targetPos = m_vec3MenuFrameOriPos;
            Hashtable tweenArgs = new Hashtable();
            tweenArgs.Add("position", targetPos);
            tweenArgs.Add("time", m_fTweeningTime);
            tweenArgs.Add("oncomplete", "OnMenuShown");
            tweenArgs.Add("oncompletetarget", gameObject);
            iTween.MoveTo(m_compMenuFrame.gameObject, tweenArgs);
        }

        private void OnMenuShown()
        {
            
        }

        private void OnHideMenu(PointerEventData evtDat)
        {
            IsHiding = true;
            m_btnShowMenu.gameObject.SetActive(true);
            m_btnHideMenu.gameObject.SetActive(false);
            Vector3 targetPos = m_vec3MenuFrameOriPos + 
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
        }

        public void Awake()
        {
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

            //---------------test code
            AddItem("Item A");
            AddItem("Item B");
            AddItem("Item C");
            AddItem("Item D");
            AddItem("Item E");
        }
    }
}
