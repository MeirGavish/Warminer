using System;
using HOG.Core;

namespace HOG.GameLogic
{
    public class HOGGameLogic : IHOGBaseManager
    {
        public static HOGGameLogic Instance;

        public HOGCurrencyManager CurrencyManager;
        public HOGUpgradeManager UpgradeManager;
        
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
            
            onComplete.Invoke();
        }
    }
}