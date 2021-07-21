using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MuckVR.VR.UI
{
    /// <summary>
    /// Changes position of object when hovered over
    /// </summary>
    class UIHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Vector3 pressedPos;
        private Vector3 defaultPos;

        public UIHover()
        {
            Vector3 pos = GetComponent<RectTransform>().anchoredPosition3D;
            defaultPos = pos + transform.position;
            pressedPos = transform.localPosition + transform.forward * 0.1f;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            transform.localPosition += transform.forward * 0.1f;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            transform.localPosition = defaultPos;
        }
    }
}
