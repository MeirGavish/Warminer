
using UnityEngine;
using HOG.Core;
using UnityEngine.SceneManagement;

namespace HOG.GameLogic
{
    public class HOGEnemyControllerComponent : HOGPoolable
    {
        private HOGMovementComponent movementComponent;
        private HOGHealthComponent healthComponent;

        // TODO: From config?
        [SerializeField] private int coinValue = 3;

        void Awake()
        {
            movementComponent = GetComponent<HOGMovementComponent>();
            healthComponent = GetComponent<HOGHealthComponent>();
            healthComponent.OnDeathAction = OnDeathAction;
            SceneManager.activeSceneChanged += OnSceneChangedAction;

            Manager.PoolManager.InitPool("TextToast", 15);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("player"))
            {
                movementComponent.StopMovement();
            }
        }

        private void OnDeathAction(GameObject thisEnemyUnused)
        {
            HOGGameLogic.Instance.CurrencyManager.ChangeCurrencyByAmountByType(CurrencyTypes.CoinsCurrency, coinValue);

            var scoreText = (HOGTweenScoreComponent)Manager.PoolManager.GetPoolable(PoolNames.ScoreToast);
            scoreText.transform.position = transform.position + Vector3.forward * 5;
            scoreText.Init(coinValue);

            Manager.PoolManager.ReturnPoolable(this);
        }

        private void OnSceneChangedAction(Scene prevScene, Scene nextScene)
        {
            Manager.PoolManager.ReturnPoolable(this);
        }

        public override void OnTakenFromPool()
        {
            //movementComponent.StartMovement();
            base.OnTakenFromPool();
        }

        public override void OnReturnedToPool() 
        {
            healthComponent.Reset();
            base.OnReturnedToPool();
        }
    }
}

