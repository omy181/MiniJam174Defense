using UnityEngine;
using UnityEngine.Events;

namespace Holylib.UI 
{ 
    public class MessageBoxManager : MonoBehaviour
    {
        public static MessageBoxManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        [SerializeField] GameObject MessageBoxPrefab;

        public void ShowMessage(string message)
        {
            Instantiate(MessageBoxPrefab).GetComponent<MessageBox>().Initialize(message,"Close","",null);
        }

        public void ShowYesNoQuestion(string message, UnityAction OnPressedYes)
        {
            Instantiate(MessageBoxPrefab).GetComponent<MessageBox>().Initialize(message, "No", "Yes", OnPressedYes);
        }
    }
}
