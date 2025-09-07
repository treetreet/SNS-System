using System;
using System.Collections.Generic;
using Resources.JSON;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PostUploader : MonoBehaviour
{
    [SerializeField] GameObject postPrefab;
    [SerializeField] GameObject postChildPrefab;
    [SerializeField] GameObject scrollview;

    //hasttable or dictionary를 통한 posted contents 관리
    Dictionary<string, GameObject> _posts = new Dictionary<string, GameObject>();
    
    private void Awake()
    {
        postPrefab = UnityEngine.Resources.Load<GameObject>("Prefabs/PostType");
        postChildPrefab = UnityEngine.Resources.Load<GameObject>("Prefabs/PostChildType");
    }

    public void UploadPost(EventData eventData)
    {
        Debug.Log(eventData.postID + ": Upload Post!");

        if (_posts.ContainsKey(eventData.postID).Equals(true))
        {
            Debug.LogAssertion("post is already uploaded");
        }

        else
        {
            GameObject post = Instantiate(eventData.parentID == null ? postPrefab : postChildPrefab, scrollview.transform);
            post.GetComponent<PostInfo>().ChangePostInfo(eventData);
            _posts.Add(eventData.postID, post);
        }
    }

    public void EditPost(EventData eventData)
    {
        Debug.Log(eventData.postID + ": Edit Post!");

        if (_posts.ContainsKey(eventData.postID).Equals(false))
        {
            Debug.LogAssertion("post is not uploaded");
        }
        else
        {
            _posts[eventData.postID].GetComponent<PostInfo>().ChangePostInfo(eventData);
        }
        
    }

    public void DeletePost(EventData eventData)
    {
        Debug.Log(eventData.postID + ": Delete Post!");

        if (_posts.ContainsKey(eventData.postID).Equals(false))
        {
            Debug.LogAssertion("post is not uploaded");
        }
        else
        {
            GameObject deletePost = _posts[eventData.postID];
            _posts.Remove(eventData.postID);
            Destroy(deletePost);
        }
        
    }
}
