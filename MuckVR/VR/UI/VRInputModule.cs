using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MuckVR.VR.UI;
using Valve.VR;

namespace MuckVR.VR
{
    /// <summary>
    /// [Obsolete] System to interact with UI
    /// Using MuckVR.VR.UI.VRUIInput instead
    /// </summary>
    [System.Obsolete]
    class VRInputModule : MonoBehaviour
    {
        PointerEventData eventData;
        public Camera Cam;
        public MenuController menu;
        GameObject current;

        EventSystem eventSystem;

        Transform sphere;

        List<RaycastResult> results = new List<RaycastResult>();

        private void Awake()
        {
            eventSystem = EventSystem.current;
            eventData = new PointerEventData(eventSystem);

            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            sphere.localScale = Vector3.one * 0.3f;
            sphere.GetComponent<Renderer>().material.color = Color.red;

            Cam = new GameObject("UI Camera").AddComponent<Camera>();
            Cam.stereoTargetEye = StereoTargetEyeMask.None;
            Cam.nearClipPlane = 0.01f;
            Cam.cullingMask = 0;
            Cam.enabled = false;

        }

        public float Distance
        {
            get
            {
                if (eventData != null)
                    return eventData.pointerCurrentRaycast.distance;
                else
                    return 0f;
            }
        }

        public void Update()
        {
            eventData.Reset();
            eventData.position = new Vector2(Cam.pixelWidth / 2, Cam.pixelHeight / 2);

            eventSystem.RaycastAll(eventData, results);

            if (results.Count > 0) {

                bool isValid = false;
                foreach (RaycastResult r in results)
                {
                    if (r.isValid)
                    {
                        eventData.pointerCurrentRaycast = r;
                        isValid = true;
                        break;
                    }
                }

                if (isValid)
                {
                    current = eventData.pointerCurrentRaycast.gameObject;

                    VRUI.Input.Hover(current, eventData);
                }
                else
                    VRUI.Input.Unhover(eventData);
            }
            else
                VRUI.Input.Unhover(eventData);

            results.Clear();

            //Should be vr input
            //menu.GetInputs(eventData);

            //if (Input.GetKeyDown(KeyCode.Space)) ProcessDown(eventData);
            //if (Input.GetKeyUp(KeyCode.Space)) ProcessUp(eventData);
        }

        public void ProcessDown(PointerEventData data)
        {
            data.pointerPressRaycast = data.pointerCurrentRaycast;

            GameObject pointerPress = ExecuteEvents.ExecuteHierarchy(current, eventData, ExecuteEvents.pointerClickHandler);

            if (pointerPress == null)
                pointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(current);

            eventData.pressPosition = eventData.position;
            eventData.pointerPress = pointerPress;
            eventData.rawPointerPress = current;
        }

        public void ProcessUp(PointerEventData data)
        {
            data.pointerPressRaycast = data.pointerCurrentRaycast;

            ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);

            GameObject pointer = ExecuteEvents.GetEventHandler<IPointerClickHandler>(current);

            if (eventData.pointerPress == pointer)
                ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerClickHandler);

            eventData.pressPosition = Vector2.zero;
            eventData.pointerPress = null;
            eventData.rawPointerPress = null;

            eventSystem.SetSelectedGameObject(null);
        }
    }
}
