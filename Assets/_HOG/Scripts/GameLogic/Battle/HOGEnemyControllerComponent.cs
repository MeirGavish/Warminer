
using UnityEngine;
using HOG.Core;
using UnityEngine.SceneManagement;
using System.Collections;

namespace HOG.GameLogic
{
    public class HOGEnemyControllerComponent : HOGPoolable
    {
        [SerializeField] private HOGMovementComponent movementComponent;
        [SerializeField] private HOGHealthComponent healthComponent;

        // TODO: From config?
        [SerializeField] private int coinValue = 3;
        [SerializeField] private int damage = 5;
        [SerializeField] private float damageInterval = 1;

        protected override void Awake()
        {
            base.Awake();
            healthComponent.OnDeathAction = OnDeathAction;
            
            Manager.PoolManager.InitPool("TextToast", 15);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("player"))
            {
                movementComponent.StopMovement();
                StartCoroutine(nameof(DamagePlayer), collision.gameObject);
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

        public void StartMovement() => movementComponent.StartMovement();
        public void StopMovement() => movementComponent.StopMovement();

        public override void OnReturnedToPool() 
        {
            healthComponent.Reset();
            StopCoroutine(nameof(DamagePlayer));
            base.OnReturnedToPool();
        }

        private IEnumerator DamagePlayer(GameObject playerObject)
        {
            // TODO: Use timing system?
            var playerHealthComp = playerObject.GetComponent<HOGHealthComponent>();

            while (true)
            {
                playerHealthComp.DealDamage(damage);
                yield return new WaitForSeconds(damageInterval);
            }
        }
    }
}

