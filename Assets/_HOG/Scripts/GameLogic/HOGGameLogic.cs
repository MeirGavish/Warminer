using System;
using HOG.Core;

namespace HOG.GameLogic
{
    public class HOGGameLogic : IHOGBaseManager
    {
        public static HOGGameLogic Instance;

        public HOGCurrencyManager CurrencyManager;
        public HOGUpgradeManager UpgradeManager;
        public HOGWaveManager WaveManager;

        public HOGGameLogic()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = this;
        }

        public void LoadManager(Action onComplete)
        {
            CurrencyManager = new HOGCurrencyManager();
            UpgradeManager = new HOGUpgradeManager();
            WaveManager = new HOGWaveManager();

            onComplete.Invoke();
        }
    }
}