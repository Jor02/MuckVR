using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.Extras;

namespace MuckVR.VR.UI
{
    /// <summary>
    /// Used for interaction with UI
    /// </summary>
    public class VRUIInput : MonoBehaviour
    {
        public SteamVR_Action_Boolean UIInput = SteamVR_Input.GetBooleanAction("InteractUI");

        public SteamVR_Behaviour_Pose controller;

        public SteamVR_LaserPointer laserPointer;
        public EventSystem eventSystem;
        public PointerEventData eventData;

        GameObject current;

        /// <summary>
        /// Initalized variables
        /// </summary>
        void Awake()
        {
            eventSystem = EventSystem.current;
            eventData = new PointerEventData(eventSystem);

            laserPointer = GetComponent<SteamVR_LaserPointer>();
            laserPointer.PointerIn += PointerIn;
            laserPointer.PointerOut += PointerOut;

            controller = GetComponent<SteamVR_Behaviour_Pose>();
            UIInput.AddOnStateDownListener(StateDown, controller.inputSource);
            UIInput.AddOnStateUpListener(StateUp, controller.inputSource);

            Debug.Log("Initiated");
        }

        /// <summary>
        /// Called when SteamVR_LaserPointer hovers over a UI Element
        /// </summary>
        private void PointerIn(object sender, PointerEventArgs e)
        {
            GameObject target = ExecuteEvents.GetEventHandler<IPointerEnterHandler>(e.target.gameObject);
            if (target != null)
            {
                //Unhover last element
                if (target != current) Unhover();

                //Hover current element
                ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerEnterHandler);
                current = target;
            }
        }

        /// <summary>
        /// Called when SteamVR_LaserPointer exits a UI Element
        /// </summary>
        private void PointerOut(object sender, PointerEventArgs e)
        {
            GameObject target = ExecuteEvents.GetEventHandler<IPointerExitHandler>(e.target.gameObject);
            if (target != null)
            {
                Unhover();
                Unhover(target);
            }
        }

        /// <summary>
        /// Stops hovering over last hovered gameObject
        /// </summary>
        void Unhover()
        {
            if (current != null)
            {
                ExecuteEvents.Execute(current, eventData, ExecuteEvents.pointerExitHandler);
                current = null;
                eventSystem.SetSelectedGameObject(null);
            }
        }

        /// <summary>
        /// Stops hovering over specified gameObject
        /// </summary>
        /// <param name="target">Target gameObject</param>
        void Unhover(GameObject target)
        {
            if (target != null)
            {
                ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerExitHandler);
            }
        }

        /// <summary>
        /// Called when input gets pressed
        /// </summary>
        private void StateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            if (current == null) return;

            ExecuteEvents.Execute(current, eventData, ExecuteEvents.pointerClickHandler);
            ExecuteEvents.Execute(current, eventData, ExecuteEvents.pointerDownHandler);
            eventSystem.SetSelectedGameObject(current);
        }

        /// <summary>
        /// Called when input gets released
        /// </summary>
        private void StateUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, eventData, ExecuteEvents.pointerUpHandler);
        }
    }

}
