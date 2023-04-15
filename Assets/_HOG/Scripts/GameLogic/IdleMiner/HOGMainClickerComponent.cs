
using HOG.Core;
using UnityEngine;

namespace  HOG.GameLogic
{
    public class HOGMainClickerComponent : HOGLogicMonoBehaviour
    {
        HOGUpgradeableData clickUpgradeData;

        private void Awake()
        {
            Manager.PoolManager.InitPool("TextToast", 15);
            clickUpgradeData = GameLogic.UpgradeManager.GetUpgradeableByID(UpgradeablesTypeID.ClickPowerUpgrade);
        }

        public void OnMouseUpAsButton()
        {
            //TODO: Convert level to power from config
            var power = GameLogic.UpgradeManager.GetPowerByIDAndLevel(clickUpgradeData.upgradableTypeID, clickUpgradeData.CurrentLevel);
            
            GameLogic.CurrencyManager.ChangeCurrencyByAmountByType(CurrencyTypes.MetalCurrency, power);

            var scoreText = (HOGTweenScoreComponent) Manager.PoolManager.GetPoolable(PoolNames.ScoreToast);
            scoreText.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 5;
            scoreText.Init(power);
        }
    }
}
