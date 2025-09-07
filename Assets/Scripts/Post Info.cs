using System;
using Resources.JSON;
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
    
    private TagDataList _tagDataList;

    private void Awake()
    {
        LoadTags();
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
    
    public void ChangePostInfo(EventData eventData)
    {
        gameObject.name = eventData.postID;
        posterName.text = eventData.poster;
        posterTag.text = _tagDataList.GetTag(eventData.poster);
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
}
