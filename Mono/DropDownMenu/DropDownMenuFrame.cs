using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class DropDownMenuFrame : MonoBehaviour
    {
        [SerializeField] private RectTransform m_transSelf;
        [SerializeField] private GridLayoutGroup m_compGrid;


        public RectTransform cachedTransform
        {
            get
            {
                return m_transSelf;
            }
            set
            {
                m_transSelf = value;
            }
        }

        public GridLayoutGroup grid
        {
            get
            {
                return m_compGrid;
            }
            set
            {
                m_compGrid = value;
            }
        }

        void Awake()
        {
            
        }
    }
}
