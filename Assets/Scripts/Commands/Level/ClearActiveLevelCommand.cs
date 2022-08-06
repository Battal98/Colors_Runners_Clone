using UnityEngine;

namespace Commands.Level
{
    public class ClearActiveLevelCommand
    {
        public void ClearActiveLevel(Transform levelHolder)
        {
            Object.Destroy(levelHolder.GetChild(0).gameObject);
        }
    }
}