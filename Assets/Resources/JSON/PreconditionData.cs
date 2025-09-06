using System;
using System.Collections.Generic;

namespace Resources.JSON
{
    [Serializable]
    public class PreconditionData
    {
        public string eventID;
        public string precondition;
    }

    [Serializable]
    public class PreconditionDataList
    {
        public List<PreconditionData> preconditions;

        public string GetPrecondition(string eventID)
        {
            for (int i = 0; i < preconditions.Count; i++)
            {
                if (preconditions[i].eventID == eventID)
                {
                    return preconditions[i].precondition;
                }
            }
            
            return null;
        }
    }
}