using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Holylib.UI
{
    public class UITextElement : MonoBehaviour
    {
        [SerializeField] TMP_Text textmesh;

        Vector3 DefaultScale;

        private void Awake()
        {
            DefaultScale = textmesh.transform.localScale;
        }
        public void SetText(string text, bool bob = false, float bobtime = 0.1f, float bobendtime = 0.5f, float bobsize = 1.2f, float destroytime = -1, float textsize = 1)
        {
            if (!textmesh) return;

            transform.localScale = DefaultScale * textsize;

            textmesh.gameObject.LeanCancel();
            if (bob)
            {
                textmesh.gameObject.LeanScale(DefaultScale * bobsize, bobtime).setIgnoreTimeScale(true);
                textmesh.gameObject.LeanScale(DefaultScale, bobendtime).setIgnoreTimeScale(true).setDelay(bobtime);
            }

            textmesh.text = text;

            if (destroytime != -1)
            {
                textmesh.gameObject.LeanScale(Vector3.zero, 0.8f).setDelay(destroytime).setOnComplete(Destroyself).setIgnoreTimeScale(true);
                textmesh.gameObject.GetComponent<CanvasGroup>().LeanAlpha(0, 0.3f).setDelay(destroytime).setIgnoreTimeScale(true);
            }
        }

        void Destroyself()
        {
            Destroy(this.gameObject);
        }
    }
}