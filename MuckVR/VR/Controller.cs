using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Valve.VR;

namespace MuckVR.VR
{
    public class Controller : MonoBehaviour
    {
        public Transform rayPivot { get; private set; }

        public Controller()
        {
            rayPivot = transform.Find("HandDir");
        }
    }
}
