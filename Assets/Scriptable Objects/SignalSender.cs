using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SignalSender : ScriptableObject
{
    public List<SignalListner> listners = new List<SignalListner>();

    public void Raise()
    {
        for(int i=listners.Count-1; i >= 0; i--)
        {
            listners[i].OnSignalRaise();
        }
    }

    public void RegisterListner(SignalListner listner)
    {
        listners.Add(listner);
    }

    public void DeregisterListner(SignalListner listner)
    {
        listners.Remove(listner);
    }
}
