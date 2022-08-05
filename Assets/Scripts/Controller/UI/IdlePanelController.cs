using UnityEngine;
using TMPro;


namespace Controllers
{
    public class IdlePanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private TextMeshProUGUI playerScoreText;

        #endregion

        #endregion

        public void SetScoreText(int value)
        {
            playerScoreText.text = value.ToString();
        }
    }
}
