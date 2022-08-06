using UnityEngine;
using Keys;
using Signals;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        private void SaveData()
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