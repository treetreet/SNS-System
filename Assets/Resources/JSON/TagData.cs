using System.Collections.Generic;

namespace Resources.JSON
{
    [System.Serializable]
    public class TagData
    {
        public string poster;
        public string tag;
    }

    [System.Serializable]
    public class TagDataList
    {
        public List<TagData> tags;
    }
}