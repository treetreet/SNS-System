using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private PostUploader postUploader;
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
