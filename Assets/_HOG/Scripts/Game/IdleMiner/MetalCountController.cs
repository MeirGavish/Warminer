using HOG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MetalCountController : HOGMonoBehaviour
{
    public TextMeshProUGUI metalText;
    
    HOG.Data.HOGPlayer player;

    private void OnEnable()
    {
        // TODO: Should be accessible differently
        player = FindObjectOfType<HOG.Data.HOGPlayer>();
        AddListener(HOGEventNames.CurrencyChanged, updateMetalCountAction);
    }

    private void OnDisable()
    {
        AddListener(HOGEventNames.CurrencyChanged, updateMetalCountAction);
    }

    void updateMetalCount(int amount)
    {
        metalText.text = "Metal: " + amount;
    }

    void updateMetalCountAction(object obj)
    {
        updateMetalCount(player.MetalCurrency);
    }
}
