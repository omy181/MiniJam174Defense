using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUIManager : Singleton<InteractionUIManager>
{
    public GameObject InteractionUI;

    private void Start()
    {
        DisableInteraction();
        var obj = InteractionUI.transform.GetChild(0);
        obj.LeanScale(obj.transform.localScale* 1.2f,1).setLoopPingPong();
    }

    public void DisableInteraction()
    {
        InteractionUI.SetActive(false);
    }

    public void EnableInteraction(Vector3 pos)
    {
        InteractionUI.SetActive(true);
        InteractionUI.transform.position = pos;
    }
}
