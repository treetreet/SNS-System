using System.Collections.Generic;

namespace Resources.JSON
{
    [System.Serializable]
    public class PreconditionData
    {
        public string eventID;
        public string precondition;
    }

    [System.Serializable]
    public class PreconditionDataList
    {
        public List<PreconditionDataList> preconditions;
    }
}