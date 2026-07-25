using TMPro;
using UnityEngine;

namespace FPS
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI promptText;

        public void UpdateText(string promptMessage)
        {
            promptText.text = promptMessage;
        }
    }
}