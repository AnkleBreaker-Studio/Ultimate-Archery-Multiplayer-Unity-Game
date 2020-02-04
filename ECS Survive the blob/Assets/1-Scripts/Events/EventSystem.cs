using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Events;

public class EventMsg : UnityEvent<object>
{
}

public class EventSystem : ComponentSystem
{
    private static Queue<KeyValuePair<string, UnityAction<object>>> eventToStartListenQueue;
    private static Queue<KeyValuePair<string, UnityAction<object>>> eventToStopListenQueue;
    private static Queue<KeyValuePair<string, object>> eventToTriggerQueue;
    
    private Dictionary<string, EventMsg> eventDict;

    protected override void OnCreate()
    {
        base.OnCreate();
        eventDict = new Dictionary<string, EventMsg>();
        eventToStartListenQueue = new Queue<KeyValuePair<string, UnityAction<object>>>();
        eventToStopListenQueue = new Queue<KeyValuePair<string, UnityAction<object>>>();
        eventToTriggerQueue = new Queue<KeyValuePair<string, object>>();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        eventToStartListenQueue?.Clear();
        eventToStopListenQueue?.Clear();
        eventToTriggerQueue?.Clear();
        foreach (EventMsg eventMsg in this.eventDict.Values)
        {
            eventMsg?.RemoveAllListeners();
        }
        eventDict.Clear();
    }

    public static void StartListening(string eventName, UnityAction<object> eventAction)
    {
        eventToStartListenQueue.Enqueue(new KeyValuePair<string, UnityAction<object>>(eventName, eventAction));
    }

    public static void StopListening(string eventName, UnityAction<object> eventAction)
    {
        eventToStopListenQueue.Enqueue(new KeyValuePair<string, UnityAction<object>>(eventName, eventAction));
    }

    public static void TriggerEvent(string eventName) => TriggerEvent(eventName, null);
    
    public static void TriggerEvent(string eventName, object arg)
    {
        eventToTriggerQueue.Enqueue(new KeyValuePair<string, object>(eventName, arg));
    }
    
    protected override void OnUpdate()
    {
        if (eventToStartListenQueue.Count > 0)
        {
            var listener = eventToStartListenQueue.Dequeue();
            AddListener(listener.Key, listener.Value);
        }
        
        if (eventToTriggerQueue.Count > 0)
        {
            var listener = eventToTriggerQueue.Dequeue();
            Trigger(listener.Key, listener.Value);
        }
        
        if (eventToStopListenQueue.Count > 0)
        {
            var listener = eventToStopListenQueue.Dequeue();
            RemoveListener(listener.Key, listener.Value);
        }
    }

    private void AddListener(string eventName, UnityAction<object> eventAction)
    {
        EventMsg currentEvent = null;
        if (this.eventDict.TryGetValue(eventName, out currentEvent))
        {
            currentEvent.AddListener(eventAction);
        }
        else
        {
            currentEvent = new EventMsg();
            currentEvent.AddListener(eventAction);
            this.eventDict.Add(eventName, currentEvent);
        }
    }
    
    private void RemoveListener(string eventName, UnityAction<object> eventAction)
    {
        if (this.eventDict.TryGetValue(eventName, out EventMsg currentEvent))
        {
            currentEvent.RemoveListener(eventAction);
        }
    }

    private void Trigger(string eventName, object arg)
    {        
        if (this.eventDict.TryGetValue(eventName, out EventMsg currentEvent))
        {
            currentEvent.Invoke(arg);
        }
    }
}
