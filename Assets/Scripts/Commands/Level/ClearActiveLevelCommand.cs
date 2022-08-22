using UnityEngine;

namespace Commands
{
    public class ClearActiveLevelCommand
    {
        #region Self Variables

        #region Private Variables

        private GameObject _levelholder;

        #endregion

        #endregion

        public ClearActiveLevelCommand(ref GameObject levelHolder)
        {
            _levelholder = levelHolder;
        }

        public void Execute()
        {
            Object.Destroy(_levelholder.transform.GetChild(0).gameObject);
            
            
        }
    }
}