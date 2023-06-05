
using System;
using DG.Tweening;
using HOG.Core;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace  HOG.GameLogic
{
    public class HOGUpgradeButtonComponent : HOGLogicMonoBehaviour
    {
        [SerializeField] private UpgradeablesTypeID UpgradeType;

        [SerializeField] private TMP_Text NameText;
        [SerializeField] private TMP_Text PowerText;
        [SerializeField] private TMP_Text PriceText;

        [SerializeField] private Button buttonComponent;

        private void Start()
        {
            UpdateTexts();
            UpdateButtonInteractibility();
        }

        private void OnEnable()
        {
            Manager.EventsManager.AddListener(HOGEventNames.OnCurrencyChanged, UpdateButtonInteractibilityAction);
            Manager.EventsManager.AddListener(HOGEventNames.OnUpgraded, UpdateButtonInteractibilityAction);
        }

        private void OnDisable()
        {
            Manager.EventsManager.RemoveListener(HOGEventNames.OnCurrencyChanged, UpdateButtonInteractibilityAction);
            Manager.EventsManager.RemoveListener(HOGEventNames.OnUpgraded, UpdateButtonInteractibilityAction);
        }

        private void UpdateButtonInteractibilityAction(object obj)
        {
            // Wrapper to avoid having calls with an unused parameter
            UpdateButtonInteractibility();
        }

        private void UpdateButtonInteractibility()
        {
            HOGUpgradeableLevelData upgradableNextLevel = GameLogic.UpgradeManager.GetUpgradableDataAtNextLevel(UpgradeType);

            if (upgradableNextLevel == null)
            {
                buttonComponent.interactable = false;
                return;
            }

            GameLogic.CurrencyManager.TryGetCurrencyByType(upgradableNextLevel.CurrencyType, out int playerCurrencyAmount);

            buttonComponent.interactable = (playerCurrencyAmount >= upgradableNextLevel.CurrencyAmountNeeded);
        }

        public void OnUpgradePressed()
        {
            GameLogic.UpgradeManager.UpgradeItemByID(UpgradeType);
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            HOGUpgradeableLevelData upgradableCurrLevel = GameLogic.UpgradeManager.GetUpgradableDataAtCurrLevel(UpgradeType);
            HOGUpgradeableLevelData upgradableNextLevel = GameLogic.UpgradeManager.GetUpgradableDataAtNextLevel(UpgradeType);

            NameText.text = GameLogic.UpgradeManager.GetHogUpgradeableConfigByID(UpgradeType).UserVisibleName;
            PowerText.text = upgradableCurrLevel.Power.ToString();
            if (upgradableNextLevel != null)
            {
                PriceText.text = $"<sprite=0>{upgradableNextLevel.CurrencyAmountNeeded:N0}";
            }
            else
            {
                PriceText.text = "-";
            }
            
        }
    }
}
