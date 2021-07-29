using System;
using System.Collections.Generic;
using System.Text;
using MuckVR.Utils;
using UnityEngine;

namespace MuckVR.VR.Gameplay
{
    class VRInventory : MonoBehaviour
    {
        const float minAngle = 30;
        const float camSafeSpot = 30f;
        const float handSafeSpot = 10f;
        const float minInteractableAngle = 5f;

        Transform cameraTransform;
        CanvasGroup inventoryGroup;
        Hotbar hotbarInstance;
        Transform hotbarParent;

        Transform handPivot;

        const int layerMask = 1 << 5;

        public VRInventory()
        {
            cameraTransform = VRPlayer.instance.VRCamera;
            inventoryGroup = GetComponent<CanvasGroup>();

            hotbarInstance = Hotbar.Instance;
            hotbarParent = hotbarInstance.transform.GetChild(0);

            handPivot = VRPlayer.instance.RHand.rayPivot;

            foreach (InventoryCell cell in Resources.FindObjectsOfTypeAll<InventoryCell>())
            {
                cell.gameObject.AddComponent<VR.UI.UIButtonCollider>();
            }
        }

        void Update()
        {
            // Angle from inventory forward towards camera pos
            // Range 0 to MinAngle
            float inventoryAngle = Mathf.Clamp(minAngle - (Vector3.Angle(transform.forward, transform.position - cameraTransform.position) - handSafeSpot), 0, minAngle);

            // Angle from camera forward towards inventory pos
            // Range 0 to 1
            float cameraAngle = Mathf.Clamp(minAngle - (Vector3.Angle(cameraTransform.forward, transform.position - cameraTransform.position) - camSafeSpot), 0, minAngle);
            cameraAngle /= minAngle;

            // Makes sure cameraAngle has a higher priority than inventoryAngle
            float scaledAngle = inventoryAngle * cameraAngle;

            //Set inventory alpha
            inventoryGroup.alpha = scaledAngle / minAngle;

            if (scaledAngle > minInteractableAngle) UpdateSlot();
        }

        void UpdateSlot()
        {
            RaycastHit hit;
            if (Physics.Raycast(handPivot.position, handPivot.forward, out hit, 0.35f, layerMask))
            {
                if (hit.transform.parent == hotbarParent)
                {
                    hotbarInstance.SetPrivateFieldValue("currentActive", hit.transform.GetSiblingIndex());
                    hotbarInstance.UpdateHotbar();
                }
            }
        }
    }
}
