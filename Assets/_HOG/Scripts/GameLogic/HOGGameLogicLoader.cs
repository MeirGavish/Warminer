using System;
using HOG.Core;
using UnityEngine;

namespace HOG.GameLogic
{
    public class HOGGameLogicLoader : HOGGameLoaderBase
    {
        [SerializeField] private HOGTriangleComponent triangleOriginal;
        
        
        public override void StartLoad(Action onComplete)
        {
            var hogGameLogic = new HOGGameLogic();
            hogGameLogic.LoadManager(() =>
            {
                Manager.PoolManager.InitPool(triangleOriginal, 30, 100);
                
                base.StartLoad(onComplete);
            });
            
        }
    }
}