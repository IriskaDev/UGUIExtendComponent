using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class DropDownMenuFrame : MonoBehaviour
    {
        private RectTransform m_transSelf;
        //private bool m_bExtendable = false;


        public RectTransform cachedTransform
        {
            get
            {
                return m_transSelf;
            }
        }

        void Awake()
        {
            m_transSelf = GetComponent<RectTransform>();
        }
    }
}
