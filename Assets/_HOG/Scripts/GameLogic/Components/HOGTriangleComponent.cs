using HOG.Core;
using UnityEngine;

namespace  HOG.GameLogic
{
    public class HOGTriangleComponent : HOGPoolable
    {
        private void OnMouseDown()
        {
            Manager.PoolManager.DestroyPool(PoolNames.TrianglePool);
        }
        
        public override void OnReturnedToPool()
        {
            transform.position = Vector3.zero;
            Manager.EventsManager.RemoveListener(HOGEventNames.TriangleTaken, OnTriangleTaken);
            base.OnReturnedToPool();
        }
        
        public override void OnTakenFromPool()
        {
            Manager.EventsManager.AddListener(HOGEventNames.TriangleTaken, OnTriangleTaken);
            base.OnTakenFromPool();
            
            Manager.EventsManager.InvokeEvent(HOGEventNames.TriangleTaken, this);
        }

        private void OnTriangleTaken(object obj)
        {
            transform.position += Vector3.right;
        }
    }
}
