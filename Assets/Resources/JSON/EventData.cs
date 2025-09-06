using System.Collections.Generic;

namespace Resources.JSON
{
    [System.Serializable]
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
        public string content;
    }

    [System.Serializable]
    public class EventDataList
    {
        public List<EventData> events;
    }
}