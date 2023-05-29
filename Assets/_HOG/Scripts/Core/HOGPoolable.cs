using UnityEngine.SceneManagement;

namespace HOG.Core
{
    public class HOGPoolable : HOGMonoBehaviour
    {
        public PoolNames poolName;

        protected virtual void Awake()
        {
            SceneManager.activeSceneChanged += OnSceneChangedAction;
        }

        public virtual void OnReturnedToPool()
        {
            this.gameObject.SetActive(false);
        }
        
        public virtual void OnTakenFromPool()
        {
            this.gameObject.SetActive(true);
        }

        private void OnSceneChangedAction(Scene prevScene, Scene nextScene)
        {
            Manager.PoolManager.ReturnPoolable(this);
        }
        

        public virtual void PreDestroy()
        {
        }
    }
}