using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionManager : Singleton<AttentionManager>
{
    [SerializeField] private GameObject _prefab;

    private Dictionary<object, GameObject> _attentions = new();
    [SerializeField] private Sprite _repairIcon;
    public void ShowAttention(object self,Vector3 pos,bool isRepair = false)
    {
        if (_attentions.ContainsKey(self)) return;

        var obj = Instantiate(_prefab, pos,Quaternion.identity);
        obj.transform.position = pos + new Vector3(0, 0.6f, 0);

        if (isRepair)
        {
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _repairIcon;
        }

        obj.LeanMoveLocalY(obj.transform.localPosition.y + 1f, 1f).setEaseInCirc().setLoopPingPong();

        _attentions.Add(self,obj);
    }

    public void HideAttention(object self)
    {
        if(_attentions.TryGetValue(self, out var attention))
        {
            attention.LeanCancel();
            Destroy(attention);

            _attentions.Remove(self);
        }
        
    }
}
