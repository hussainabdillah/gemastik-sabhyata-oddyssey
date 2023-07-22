using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;

    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }

    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }
}