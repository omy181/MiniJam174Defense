using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Holylib.UI
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] TMP_Text messageDialog;
        [SerializeField] Button ButtonNo;
        [SerializeField] TMP_Text ButtonNoText;
        [SerializeField] Button ButtonYes;
        [SerializeField] TMP_Text ButtonYesText;

        public void Initialize(string messagetext, string notext, string yestext, UnityAction YesAction)
        {
            messageDialog.text = messagetext;
            ButtonNoText.text = notext;

            ButtonNo.onClick.AddListener(CloseMessageBox);

            if (YesAction == null)
            {
                ButtonYes.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                ButtonYesText.text = yestext;
                ButtonYes.onClick.AddListener(YesAction);
                ButtonYes.onClick.AddListener(CloseMessageBox);
            }

        }

        void CloseMessageBox()
        {
            Destroy(this.gameObject);
        }
    }
}
