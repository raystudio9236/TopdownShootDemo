using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Event
{
    using EventDic = Dictionary<short, EventDispatcher.EventData>;
    using EventDataList = List<EventDispatcher.EventData>;

    public delegate void EventHandler(short eventType);

    public delegate void EventHandler<in T>(short eventType, T data);

    public class EventDispatcher
    {
        public class EventData
        {
            public short EventType { get; }

            private readonly List<Delegate> _handlers;

            public EventData(short eventType, int size = 4)
            {
                EventType = eventType;
                _handlers = new List<Delegate>(size);
            }

            public void Send()
            {
                foreach (var handler in _handlers)
                {
                    if (handler == null)
                        continue;

                    var eventHandler = (EventHandler) handler;
                    eventHandler(EventType);
                }
            }

            public void Send<T>(T msgData)
            {
                foreach (var handler in _handlers)
                {
                    if (handler == null)
                        continue;

                    var eventHandler = (EventHandler<T>) handler;
                    eventHandler(EventType, msgData);
                }
            }

            public void Clear()
            {
                _handlers.Clear();
            }

            public void ClearNull()
            {
                var len = _handlers.Count;
                var newLen = 0;
                for (var i = 0; i < len; i++)
                {
                    if (_handlers[i] == null)
                        continue;

                    if (newLen != i)
                        _handlers[newLen] = _handlers[i];

                    newLen++;
                }

                _handlers.RemoveRange(newLen, len - newLen);
            }

            public bool TryAdd(Delegate handler)
            {
                var newEvent = true;

#if DEBUG
                if (_handlers.Contains(handler))
                {
                    Debug.LogError($"重复添加事件 {handler}");
                    newEvent = false;
                }
#endif

                if (newEvent)
                {
                    _handlers.Add(handler);
                }

                return newEvent;
            }

            public bool TryRemove(Delegate handler)
            {
                var index = _handlers.IndexOf(handler);
                if (index != -1)
                {
                    _handlers[index] = null;
                    return true;
                }

                return false;
            }
        }

        private readonly EventDic _eventDatas = new EventDic();

        private readonly EventDataList _waitClearList = new EventDataList();

        public void AddHandler(short type, EventHandler handler)
        {
            InnerAddHandler(type, handler);
        }

        public void AddHandler<T>(short type, EventHandler<T> handler)
        {
            InnerAddHandler(type, handler);
        }

        public void RemoveHandler(short type, EventHandler handler)
        {
            InnerRemoveHandler(type, handler);
        }

        public void RemoveHandler<T>(short type, EventHandler<T> handler)
        {
            InnerRemoveHandler(type, handler);
        }

        public void Send(short type)
        {
            var eventData = GetEventData(type);
            eventData?.Send();
        }

        public void Send<T>(short type, T msgData)
        {
            var eventData = GetEventData(type);
            eventData?.Send(msgData);
        }

        public EventData GetEventData(short type, bool autoCreate = false)
        {
            if (!_eventDatas.TryGetValue(type, out var ret) && autoCreate)
            {
                ret = new EventData(type);
                _eventDatas.Add(type, ret);
            }

            return ret;
        }

        public void Clear()
        {
            foreach (var eventData in _eventDatas)
            {
                eventData.Value.Clear();
            }

            _eventDatas.Clear();
        }

        public void ClearNull()
        {
            foreach (var eventData in _waitClearList)
            {
                eventData.ClearNull();
            }

            _waitClearList.Clear();
        }

        public void PreCreate(short type, int size)
        {
            if (!_eventDatas.ContainsKey(type))
                _eventDatas.Add(type, new EventData(type, size));
        }

        private void InnerAddHandler(short type, Delegate handler)
        {
            var eventData = GetEventData(type, true);
            eventData?.TryAdd(handler);
        }

        private void InnerRemoveHandler(short type,
            Delegate handler)
        {
            var eventData = GetEventData(type);
            if (eventData == null) return;

            if (eventData.TryRemove(handler))
            {
                if (_waitClearList.Contains(eventData))
                    _waitClearList.Add(eventData);
            }
        }
    }
}