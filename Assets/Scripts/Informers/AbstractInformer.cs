using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInformer<T>
{
    private List<T> receivers;

    protected AbstractInformer()
    {
        receivers = new List<T>();
    }

    public void RegisterReceiver(T receiver)
    {
        if (!receivers.Contains(receiver))
        {
            receivers.Add(receiver);
        }
    }

    protected void InformReceivers()
    {
        for (int i = 0; i < receivers.Count; i++)
        {
            try
            {
                InformReceiver(receivers[i]);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    protected abstract void InformReceiver(T receiver);
}
