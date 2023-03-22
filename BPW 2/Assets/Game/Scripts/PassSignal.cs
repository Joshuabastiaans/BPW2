using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassSignal : MonoBehaviour
{
    public Flower m_FlowerToPassSignalTo;
    public void PassingSignal(Flower flower)
    {
        if (m_FlowerToPassSignalTo == null)
        {
            Destroy(GetComponent<Soil>().m_OccupyingFlower.gameObject);
            GetComponent<Soil>().m_OccupyingFlower = null;
            GetComponent<Soil>().m_FlowerRemovable =true;
            Destroy(this);
            return;
        }
        PlantSignal signal = new PlantSignal();
        signal.m_SignalOwner = flower;
        signal.m_Frequency = flower.m_SignalHitBy.m_Frequency;
        signal.m_TargetedFlowers.Add(m_FlowerToPassSignalTo);
        signal.m_SpawnLineRenderers = false;
        SignalManager.Instance.AddSignal(signal);
    }
}
