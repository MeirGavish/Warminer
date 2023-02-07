using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HOG.Core;

namespace HOG.Data
{
    public class HOGPlayer : HOGMonoBehaviour
    {
        // TODO: How to make global to the game regardless of scene?
        private int _metalCurrency = 0;
        public int MetalCurrency => _metalCurrency;

        private void AddMetal(object amountObj)
        {
            int amount = (int)amountObj;
            _metalCurrency += amount;
        }

        private void OnEnable()
        {
            AddListener(HOGEventType.GainedMetal, AddMetal);
        }

        private void OnDisable()
        {
            RemoveListener(HOGEventType.GainedMetal, AddMetal);
        }

    }
}