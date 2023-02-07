using System;
using System.Collections.Generic;

namespace HOG.Core
{
    public enum HOGEventType
    {
        GameStartEvent,
        GainedMetal
    }
    public class HOGEventsManager
    {
        private Dictionary<HOGEventType, List<Action<object>>> activeListeners = new();

        public void AddListener(HOGEventType gameEvent, Action<object> eventAction)
        {
            if (activeListeners.TryGetValue(gameEvent, out var listOfEvents))
            {
                listOfEvents.Add(eventAction);
                return;
            }
            
            activeListeners.Add(gameEvent, new List<Action<object>>{eventAction});
        }
        
        public void RemoveListener(HOGEventType gameEvent, Action<object> eventAction)
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
        
        public void InvokeEvent(HOGEventType gameEvent, object obj)
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
        public HOGEventType gameEvent;
        public Action<object> eventAction;
    }
}