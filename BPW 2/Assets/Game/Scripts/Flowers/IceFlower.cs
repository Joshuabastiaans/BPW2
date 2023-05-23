using UnityEngine;
using UnityEngine.Events;

public class IcePlant : Flower
{
    public UnityEvent m_OnMelt;

    public override void OnRecieveSignal()
    {
        base.OnRecieveSignal();
        m_OnMelt?.Invoke();
    }

    public void Melt()
    {
        // Perform actions to simulate melting of the ice plant
    }
}
