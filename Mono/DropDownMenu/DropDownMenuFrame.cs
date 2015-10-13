using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class DropDownMenuFrame : MonoBehaviour
    {
        private RectTransform m_transSelf;
        private GridLayoutGroup m_compGrid;


        public RectTransform cachedTransform
        {
            get
            {
                return m_transSelf;
            }
        }

        public GridLayoutGroup grid
        {
            get
            {
                return m_compGrid;
            }
        }

        void Awake()
        {
            m_transSelf = gameObject.GetComponent<RectTransform>();
            m_compGrid = gameObject.GetComponent<GridLayoutGroup>();
            if (m_compGrid == null)
            {
                m_compGrid = gameObject.AddComponent<GridLayoutGroup>();
            }
        }
    }
}
