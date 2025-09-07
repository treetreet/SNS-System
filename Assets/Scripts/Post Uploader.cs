using System.Collections;
using System.Collections.Generic;
using Resources.JSON;
using UnityEngine;

public class PostUploader : MonoBehaviour
{
    [SerializeField] GameObject postPrefab;
    [SerializeField] GameObject postChildPrefab;
    [SerializeField] GameObject postScrollView;
    [SerializeField] GameObject loadingImage;

    //hastTable or dictionary를 통한 posted contents 관리
    private readonly Dictionary<string, GameObject> _posts = new Dictionary<string, GameObject>();

    private string _currentPoster = "main";
    
    private void Awake()
    {
        postPrefab = UnityEngine.Resources.Load<GameObject>("Prefabs/PostType");
        postChildPrefab = UnityEngine.Resources.Load<GameObject>("Prefabs/PostChildType");
        
        loadingImage.SetActive(false);
    }

    public void UploadPost(EventData eventData)
    {
        Debug.Log(eventData.postID + ": Upload Post!");
        
        GameObject post;
        
        if (_posts.ContainsKey(eventData.postID).Equals(true))
        {
            Debug.LogAssertion("post is already uploaded");
            return;
        }

        if (eventData.parentID != null && _posts.ContainsKey(eventData.parentID).Equals(false))
        {
            Debug.LogAssertion("post's parent ID is invalid");
            return;
        }

        loadingImage.SetActive(true);
        if(eventData.parentID == null)
        {
            post = Instantiate(postPrefab, postScrollView.transform);
        }
        else
        {
            GameObject parentPost = _posts[eventData.parentID];
            
            post = Instantiate(postChildPrefab, parentPost.transform);
            post.GetComponent<PostInfo>().GetParentPostInfo(parentPost);
        }
        
        post.GetComponent<PostInfo>().ChangePostInfo(eventData);
        post.GetComponent<PostInfo>().ChangeActivePost(_currentPoster);
        _posts.Add(eventData.postID, post);
        
        StartCoroutine(LoadPosts());
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
            loadingImage.SetActive(true);
            GameObject deletePost = _posts[eventData.postID];
            _posts.Remove(eventData.postID);
            Destroy(deletePost);
            StartCoroutine(LoadPosts());
        }
    }

    /// <summary>
    /// poster가 main인 경우 모두 active를 활성화. 그 외는 poster 이름에 따라 active를 활성화
    /// </summary>
    /// <param name="poster"></param>
    public void ChangeActivePost(string poster)
    {
        loadingImage.SetActive(true);
        _currentPoster = poster;
        foreach (var values in _posts)
        {
            values.Value.GetComponent<PostInfo>().ChangeActivePost(poster);
        }

        StartCoroutine(LoadPosts());
    }

    private IEnumerator LoadPosts()
    {
        for (int i = 0; i < 20; ++i)
        {
            postScrollView.SetActive(false);
            yield return new WaitForEndOfFrame();
            postScrollView.SetActive(true);
            yield return new WaitForEndOfFrame();
        }
        
        loadingImage.SetActive(false);
    }
    
}
