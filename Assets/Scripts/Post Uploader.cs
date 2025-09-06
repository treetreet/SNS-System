using Resources.JSON;
using UnityEngine;

public class PostUploader : MonoBehaviour
{
    public void UploadPost(EventData eventData)
    {
        if (eventData.parentID != null)
        {
            
        }
        
        Debug.Log(eventData.postID + ": Upload Post!");
    }

    public void EditPost(EventData eventData)
    {
        if (eventData.parentID != null)
        {
            
        }

        Debug.Log(eventData.postID + ": Edit Post!");
    }

    public void DeletePost(EventData eventData)
    {
        if (eventData.parentID != null)
        {
            
        }
        
        Debug.Log(eventData.postID + ": Delete Post!");
    }
}
