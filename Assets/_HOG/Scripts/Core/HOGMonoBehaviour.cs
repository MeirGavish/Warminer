using System;
using UnityEngine;

namespace HOG.Core
{
    public class HOGMonoBehaviour : MonoBehaviour
    {
        protected HOGManager Manager => HOGManager.Instance;

        protected void AddListener(HOGEventType gameEvent, Action<object> eventAction) => Manager.EventsManager.AddListener(gameEvent, eventAction);
        protected void RemoveListener(HOGEventType gameEvent, Action<object> eventAction) => Manager.EventsManager.RemoveListener(gameEvent, eventAction);
        protected void InvokeEvent(HOGEventType gameEvent, object obj) => Manager.EventsManager.InvokeEvent(gameEvent, obj);
    }
}