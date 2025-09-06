using System;
using System.Collections.Generic;
using Unity.VisualScripting;

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

        public List<string> GetPrecondition(string eventID)
        {
            List<string> preconditionStrings = new List<string>();
            for (int i = 0; i < preconditions.Count; i++)
            {
                if (preconditions[i].eventID == eventID)
                {
                    preconditionStrings.Add(preconditions[i].precondition);
                }
            }
            
            return preconditionStrings;
        }
    }
}