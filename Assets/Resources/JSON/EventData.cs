using System;
using System.Collections.Generic;

namespace Resources.JSON
{
    [Serializable]
    public class EventData
    {
        public enum EventType
        {
            Add,
            Edit,
            Delete,
        }

        public EventType eventType;
        public string eventID;
        public string postID;
        public string parentID;
        public string poster;
        public string image;
        public string content;
    }

    [Serializable]
    public class EventDataList
    {
        public List<EventData> events;

        public List<EventData> GetEvents(string eventID)
        {
            List<EventData> eventList = new List<EventData>();
            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].eventID == eventID)
                {
                    eventList.Add(events[i]);
                }
            }
            
            return eventList;
        }
    }
}