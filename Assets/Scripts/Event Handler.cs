using System;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;


public class EventHandler : MonoBehaviour
{
    [SerializeField] private PostUploader postUploader;
    private Object _jsonObject;

    private void Awake()
    {
       // _jsonObject = Resources.Load<>("Resources/JSON/eventjson");
        //Resources.Load<GameObject>("Resources/JSON/eventjson");
    }

    void GetEvent(string eventName)
    {
        //find event -> json
        //throw json to postUploader
    }

    void UpdateJson()
    {
        //change json
        //update post -> postUploader
    }
}
