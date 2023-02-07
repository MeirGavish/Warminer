using HOG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManualMine : HOGMonoBehaviour
{
    public int metalPerClick = 1;

    public void ClickMine()
    {
        InvokeEvent(HOGEventType.GainedMetal, metalPerClick);
    }
}
