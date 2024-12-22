using TMPro;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    [SerializeField] TMP_Text textUI;

    public void ShowText(string text, float time, Color color, float fontsize)
    {
        transform.localScale = Vector3.zero;
        transform.LeanScale(Vector3.one, 0.3f).setEaseOutBack();
        textUI.text = text;
        textUI.color = color;
        textUI.fontSize = fontsize;

        Invoke("DestroySelf", time);
    }

    void DestroySelf()
    {
        transform.LeanScale(Vector3.zero, 0.5f).setEaseInBack().setOnComplete(() => Destroy(this.gameObject));

    }

    private void Update()
    {
        int index = transform.parent.childCount - transform.GetSiblingIndex();

        transform.GetChild(0).localScale = Vector3.one - Vector3.one * (index * 0.2f);
    }
}
