using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] GameObject TextPrefab;
    [SerializeField] Transform TextParent;
    public void ShowText(string text, float time, Color color, float fontsize = 24)
    {
        var obj = Instantiate(TextPrefab, TextParent);

        obj.GetComponent<TextUI>().ShowText(text, time, color, fontsize);
    }

}
