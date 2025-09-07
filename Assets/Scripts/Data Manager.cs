using Resources.JSON;
using UnityEngine;

namespace Scripts
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        
        private EventDataList _eventDataList;
        private PreconditionDataList _preconditionDataList;
        private TagDataList _tagDataList;
        
        public EventDataList EventDataList => _eventDataList;
        public PreconditionDataList PreconditionDataList => _preconditionDataList;
        public TagDataList TagDataList => _tagDataList;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this.gameObject);
            }

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
            else
            {
                Debug.Log("precondition is null");
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
    }
}