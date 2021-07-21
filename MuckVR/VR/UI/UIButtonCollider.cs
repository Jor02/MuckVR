using System.Collections;
using UnityEngine;

namespace MuckVR.VR.UI
{
    /// <summary>
    /// Adds collider for raycasts
    /// </summary>
    public class UIButtonCollider : MonoBehaviour
    {
        void Awake() => StartCoroutine(GetAddBox());

        private IEnumerator GetAddBox()
        {
            yield return new WaitForEndOfFrame();
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
            Rect scale = gameObject.GetComponent<RectTransform>().rect;
            boxCollider.size = new Vector3(scale.width, scale.height, 1);
        }
    }
}
