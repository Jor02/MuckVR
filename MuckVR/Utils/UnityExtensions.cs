using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MuckVR.Utils
{
    public static class UnityExtensions
    {
        public static void ResetLocalTransform(this Transform transform) => ResetLocalTransform(transform, true, true, true);

        public static void ResetLocalTransform(this Transform transform, bool pos, bool rot, bool scale)
        {
            if (rot) transform.localRotation = Quaternion.identity;
            if (pos) transform.localPosition = Vector3.zero;
            if (scale) transform.localScale = Vector3.one;
        }
    }
}
