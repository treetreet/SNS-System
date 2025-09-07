using Resources.JSON;
using Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostInfo : MonoBehaviour
{
    [SerializeField] Image profileImage;
    [SerializeField] Image postImage;

    [SerializeField] TextMeshProUGUI posterName;
    [SerializeField] TextMeshProUGUI posterTag;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] private GameObject parentPost;
    
    public void ChangePostInfo(EventData eventData)
    {
        gameObject.name = eventData.postID;
        posterName.text = eventData.poster;
        posterTag.text = DataManager.Instance.TagDataList.GetTag(eventData.poster);
        switch (eventData.poster)
        {
            case "red": 
                profileImage.color = Color.red;
                break;
            case "green": 
                profileImage.color = Color.green;
                break;
            case "yellow": 
                profileImage.color = Color.yellow;
                break;
        }

        if (eventData.image != null)
        {
            postImage.sprite = UnityEngine.Resources.Load<Sprite>("Images/" + eventData.image);
        }
        else
        {
            postImage.gameObject.SetActive(false);
        }
        
        content.text = eventData.content;
    }

    public void GetParentPostInfo(GameObject parent)
    {
        parentPost = parent;
    }

    /// <summary>
    /// poster가 main인 경우 모두 active를 활성화. 그 외는 poster 이름에 따라 active를 활성화
    /// </summary>
    /// <param name="poster"></param>
    public void ChangeActivePost(string poster)
    {
        if (poster == "main")
        {
            gameObject.SetActive(true);
        }
        else if (parentPost == null)
        {
            gameObject.SetActive(posterName.text == poster);
        }
        else
        {
            string parentPoster = parentPost.GetComponent<PostInfo>().posterName.text;
            gameObject.SetActive(parentPoster == poster);
        }
    }
}
