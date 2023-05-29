using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGFaceCameraComponent : HOGLogicMonoBehaviour
    {
        // TODO: Switch to single canvas and maybe delete this component
        private Camera cam;

        private void OnEnable()
        {
            cam = Camera.main;
        }

        void LateUpdate()
        {
            transform.LookAt(transform.position + cam.transform.forward);
        }
    }
}

