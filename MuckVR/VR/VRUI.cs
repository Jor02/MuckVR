using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;

namespace MuckVR.VR.UI
{
    public class VRUI
    {
        public static class Input
        {
            private static GameObject currentElement;

            public static void Down(PointerEventData eventData)
            {
                if (currentElement == null) return;
                ExecuteEvents.Execute(currentElement, eventData, ExecuteEvents.pointerClickHandler);
                ExecuteEvents.Execute(currentElement, eventData, ExecuteEvents.pointerDownHandler);
            }

            public static void Up(PointerEventData eventData)
            {
                if (currentElement == null) return;
                ExecuteEvents.Execute(currentElement, eventData, ExecuteEvents.pointerUpHandler);
            }

            public static void Hover(GameObject target, PointerEventData eventData)
            {
                if (currentElement != target) Unhover(eventData);
                ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerEnterHandler);
                currentElement = target;
            }

            public static void Unhover(PointerEventData eventData)
            {
                if (currentElement != null)
                {
                    ExecuteEvents.Execute(currentElement, eventData, ExecuteEvents.pointerExitHandler);
                    currentElement = null;
                }
            }
        }
    }

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
