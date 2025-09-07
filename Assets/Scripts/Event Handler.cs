using System;
using System.Collections.Generic;
using UnityEngine;
using Resources.JSON;
using UnityEngine.Serialization;
using UnityEngine.UI;

//processedEvent 배열이 preconditionDataList의 길이로 초기화됨.
//만약 제대로 만든다면 hash를 써야 함.
public class EventHandler : MonoBehaviour
{
    [SerializeField] private PostUploader postUploader;

    private EventDataList _eventDataList;
    private PreconditionDataList _preconditionDataList;

    [SerializeField] private bool[] tempProcessedEvents;

    private void Awake()
    {
        LoadEvents();
        LoadPreconditions();
    }

    #region Load JSON
    void LoadEvents()
    {
        TextAsset jsonFile = UnityEngine.Resources.Load<TextAsset>("JSON/event");
        if (jsonFile != null)
        {
            _eventDataList = JsonUtility.FromJson<EventDataList>(jsonFile.text);
            //Debug.Log(jsonFile.text); ok
            Debug.Log("events count:" + _eventDataList.events.Count);
        }
    }

    void LoadPreconditions()
    {
        TextAsset jsonFile = UnityEngine.Resources.Load<TextAsset>("JSON/precondition");
        if (jsonFile != null)
        {
            _preconditionDataList = JsonUtility.FromJson<PreconditionDataList>(jsonFile.text);
            //Debug.Log(jsonFile.text);  ok
            Debug.Log("preconditions count:" + _preconditionDataList.preconditions.Count);
            
            tempProcessedEvents = new bool[_preconditionDataList.preconditions.Count];  //초기 값은 모두 false & preconditions 설정 수정 필요. 필요 이상의 배열 값.
        }
    }

   

    #endregion

    /// <summary>
    /// 이벤트를 받으면 precondition을 체크하고, postUploader로 이벤트 타입에 맞게 함수 호출을 함
    /// </summary>
    /// <param name="eventID"></param>
    public void GetEvent(string eventID)
    {
        Debug.Log("event" + eventID + ": Get Event!");
        //check processed
        if (tempProcessedEvents[int.Parse(eventID)]) return;
        
        //check precondition
        if (!CheckPrecondition(eventID))
        {
            Debug.Log("event" + eventID + ": precondition is not processed");
            return;
        }
        
        tempProcessedEvents[int.Parse(eventID)] = true;
        List<EventData> eventList = _eventDataList.GetEvents(eventID);
        
        //throw jsonData(that object has) to postUploader by eventType
        foreach (var eventData in eventList)
        {
            switch (eventData.eventType)
            {
                case EventData.EventType.Add :
                    postUploader.UploadPost(eventData);
                    break;
                case EventData.EventType.Edit :
                    postUploader.EditPost(eventData);
                    break;
                case EventData.EventType.Delete :
                    postUploader.DeletePost(eventData);
                    break;
                default: 
                    Debug.LogAssertion("eventType is not supported");
                    break;
            }
        }
    }

    /// <summary>
    /// 전제 조건으로 이전에 실행되어야 하는 이벤트가 실행되었는지 확인
    /// </summary>
    /// <param name="eventID"></param>
    /// <returns>isProcessed</returns>
    private bool CheckPrecondition(string eventID)
    {
        List<String> preconditionStrings = _preconditionDataList.GetPrecondition(eventID);
        //전제 조건이 없는 경우 true
        if (preconditionStrings.Count == 0) return true;

        //각 전제 조건을 탐색하며 모두 성립할 경우 true
        foreach (string preconditionStr in preconditionStrings)
        {
            if (int.TryParse(preconditionStr, out int precondition))
            {
                if (tempProcessedEvents[precondition] == false) return false;
            }
            else
            {
                Debug.LogError("event " + eventID + ": precondition ID is not string");
            }
        }

        return true;
    }
}