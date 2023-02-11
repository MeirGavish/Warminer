using System;
using System.Collections.Generic;

namespace HOG.Core
{
    public enum HOGEventNames
    {
        OnGameStart,
        CurrencyChanged
    }
    public class HOGEventsManager
    {
        private Dictionary<HOGEventNames, List<Action<object>>> activeListeners = new();

        public void AddListener(HOGEventNames gameEvent, Action<object> eventAction)
        {
            if (activeListeners.TryGetValue(gameEvent, out var listOfEvents))
            {
                listOfEvents.Add(eventAction);
                return;
            }
            
            activeListeners.Add(gameEvent, new List<Action<object>>{eventAction});
        }
        
        public void RemoveListener(HOGEventNames gameEvent, Action<object> eventAction)
        {
            if (activeListeners.TryGetValue(gameEvent, out var listOfEvents))
            {
                listOfEvents.Remove(eventAction);

                if (listOfEvents.Count <= 0)
                {
                    activeListeners.Remove(gameEvent);
                }
            }
        }
        
        public void InvokeEvent(HOGEventNames gameEvent, object obj)
        {
            if (activeListeners.TryGetValue(gameEvent, out var listOfEvents))
            {
                //TODO: Do For Loop
                foreach (var action in listOfEvents)
                {
                    action.Invoke(obj);
                }   
            }
        }
    }

    public class HOGEvent
    {
        public HOGEventNames gameEvent;
        public Action<object> eventAction;
    }
}