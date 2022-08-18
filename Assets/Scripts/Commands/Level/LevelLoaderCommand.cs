using UnityEngine;

namespace Commands
{
    public class LevelLoaderCommand
    {
        #region Self Variables

        #region Private Variables

        private GameObject _levelHolder;

        #endregion

        #endregion

        public LevelLoaderCommand(ref GameObject levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute(int _levelID)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {_levelID}"),
                _levelHolder.transform);
        }
    }
}