using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListner : MonoBehaviour
{
    public SignalSender signal;
    public UnityEvent signalEvent;

    public void OnSignalRaise()
    {
        signalEvent.Invoke();
    }

    private void OnEnable()
    {
        signal.RegisterListner(this);
    }

    private void OnDisable()
    {
        signal.DeregisterListner(this);
    }
}
