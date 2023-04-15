using System;
using HOG.Core;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGGameLogicLoader : HOGGameLoaderBase
    { 
        public override void StartLoad(Action onComplete)
        {
            var hogGameLogic = new HOGGameLogic();
            hogGameLogic.LoadManager(() =>
            {
                base.StartLoad(onComplete);
            });
        }
    }
}