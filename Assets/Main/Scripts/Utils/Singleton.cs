using System.Collections.Generic;
using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    private static System.Action<T> s_onAwake;

    public static void WhenInstantiated(System.Action<T> action)
    {
        if (Instance != null)
            action(Instance);
        else
            s_onAwake += action;
    }

    protected virtual void Awake()
    {
        if (!enabled)
            return;

        if (Instance != null)
        {
            Debug.LogWarning($"Another instance of Singleton {typeof(T).Name} is being instantiated, destroying...", this);
            Destroy(gameObject);
            return;
        }

        Instance = (T)this;

        InternalAwake();

        s_onAwake?.Invoke(Instance);
        s_onAwake = null;
    }

    protected void OnEnable()
    {
        if (Instance != this)
            Awake();
    }

    protected virtual void InternalAwake() { }
}