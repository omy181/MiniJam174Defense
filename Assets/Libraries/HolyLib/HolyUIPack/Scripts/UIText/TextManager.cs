using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Holylib.UI
{
    public class TextManager : Singleton<TextManager>
    {

        [SerializeField] GameObject TextPrefab;
        [SerializeField] Transform CanvasParent;


        public UITextElement StartTextOnWorld(Vector3 worldpos)
        {
            GameObject text = Instantiate(TextPrefab, Camera.main.WorldToScreenPoint(worldpos), Quaternion.identity, CanvasParent);
            return text.GetComponent<UITextElement>();
        }

        public UITextElement StartTextOnUI(Vector3 UIpos)
        {
            GameObject text = Instantiate(TextPrefab, UIpos, Quaternion.identity, CanvasParent);
            text.transform.localPosition = UIpos;
            return text.GetComponent<UITextElement>();
        }
    }
}

