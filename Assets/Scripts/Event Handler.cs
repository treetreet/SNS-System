using System;
using System.Collections.Generic;
using UnityEngine;
using Resources.JSON;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private PostUploader postUploader;

    private EventDataList _eventDataList;
    private PreconditionDataList _preconditionDataList;
    private TagDataList _tagDataList;

    private bool[] _processedEvents;

    private void Awake()
    {
        LoadEvents();
        LoadPreconditions();
        LoadTags();
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

            _processedEvents = new bool[_eventDataList.events.Count]; //초기 값은 모두 false
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
        }
    }

    void LoadTags()
    {
        TextAsset jsonFile = UnityEngine.Resources.Load<TextAsset>("JSON/tag");
        if (jsonFile != null)
        {
            _tagDataList = JsonUtility.FromJson<TagDataList>(jsonFile.text);
            //Debug.Log(jsonFile.text);  ok
            Debug.Log("tags count:" + _tagDataList.tags.Count);
        }
    }

    #endregion

    /// <summary>
    /// 이벤트를 받으면 precondition을 체크하고, postUploader로 이벤트 타입에 맞게 함수 호출을 함
    /// </summary>
    /// <param name="eventID"></param>
    void GetEvent(string eventID)
    {
        //check precondition
        if (!CheckPrecondition(eventID))
        {
            Debug.Log("precondition is not processed");
            return;
        }

        List<EventData> eventList = _eventDataList.GetEvents(eventID);
        
        //throw json to postUploader by eventType **** 만들 부분
    }

    /// <summary>
    /// 전제 조건으로 이전에 실행되어야 하는 이벤트가 실행되었는지 확인
    /// </summary>
    /// <param name="eventID"></param>
    /// <returns>isProcessed</returns>
    bool CheckPrecondition(string eventID)
    {
        List<String> preconditionStrings = _preconditionDataList.GetPrecondition(eventID);
        //전제 조건이 없는 경우 true
        if (preconditionStrings.Count == 0) return true;

        //각 전제 조건을 탐색하며 모두 성립할 경우 true
        foreach (string preconditionStr in preconditionStrings)
        {
            if (int.TryParse(preconditionStr, out int precondition))
            {
                if (_processedEvents[precondition] == false) return false;
            }
            else
            {
                Debug.LogError("event " + eventID + ": precondition ID is not string");
            }
        }

        return true;
    }
}