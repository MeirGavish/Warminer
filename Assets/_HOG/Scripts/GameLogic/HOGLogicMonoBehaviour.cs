using UnityEngine;
using System.Collections;
using HOG.Core;

namespace HOG.GameLogic
{
    public class HOGLogicMonoBehaviour : HOGMonoBehaviour
    {
        public HOGGameLogic GameLogic => HOGGameLogic.Instance;
    }
}