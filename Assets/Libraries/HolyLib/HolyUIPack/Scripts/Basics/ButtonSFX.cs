using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaySFX);
    }
    public void PlaySFX()
    {
        //HolyFmodAudioController.PlayOneShot(HolyFmodAudioReferences.instance.UIClick2Sound, transform.position);
    }
}
