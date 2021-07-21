using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;

namespace MuckVR.VR.UI
{
    /// <summary>
    /// UI Helper Classes
    /// </summary>
    public class VRUI
    {
        /// <summary>
        /// Previously used for UI interaction
        /// Using MuckVR.VR.UI.VRUIInput instead
        /// </summary>
        [Obsolete]
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
}
