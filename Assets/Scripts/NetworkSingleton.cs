using Mirror;
using UnityEngine;

public abstract class NetworkSingleton<T> : NetworkBehaviour where T : class
{
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning(this +" has already an istance.");
        }
        else
        {
            instance = this as T;
        }
        
    }

    public static T instance;

    protected virtual void OnDestroy()
    {
        instance = null;
    }
}
