using UnityEngine;
using Keys;
using Signals;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            SaveSignals.Instance.onSaveData += OnSaveData;
        }

        private void Unsubscribe()
        {
            SaveSignals.Instance.onSaveData -= OnSaveData;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void OnSaveData()
        {
            OnSaveGame(
                new SaveGameDataParams()
                {
                    Level = SaveSignals.Instance.onGetLevel(),
                }
            );
        }

        private void OnSaveGame(SaveGameDataParams saveDataParams)
        {
            if (saveDataParams.Level != null) ES3.Save("Level", saveDataParams.Level);
        }
    }
}