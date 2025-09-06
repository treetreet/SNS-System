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

        public string GetTag(string poster)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                if (tags[i].poster == poster)
                {
                    return tags[i].tag;
                }
            }
            return "ErrorAccount";
        }
    }
}