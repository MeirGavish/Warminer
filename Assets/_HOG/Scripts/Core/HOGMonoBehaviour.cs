using System;
using UnityEngine;

namespace HOG.Core
{
    public class HOGMonoBehaviour : MonoBehaviour
    {
        protected HOGManager Manager => HOGManager.Instance;

        protected void AddListener(HOGEventNames gameEvent, Action<object> eventAction) => Manager.EventsManager.AddListener(gameEvent, eventAction);
        protected void RemoveListener(HOGEventNames gameEvent, Action<object> eventAction) => Manager.EventsManager.RemoveListener(gameEvent, eventAction);
        protected void InvokeEvent(HOGEventNames gameEvent, object obj) => Manager.EventsManager.InvokeEvent(gameEvent, obj);
    }
}