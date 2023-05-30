using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HOG.GameLogic
{
    public class HOGRangeComponent : HOGLogicMonoBehaviour
    {
        public Action<GameObject> OnEnterRange = null;
        public Action<GameObject> OnExitRange = null;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnEnterRange?.Invoke(collision.gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnExitRange?.Invoke(collision.gameObject);
        }
    }

}
