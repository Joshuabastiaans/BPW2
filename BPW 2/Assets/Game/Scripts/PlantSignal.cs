using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlantSignal
{
    public Flower m_SignalOwner;
    public float m_SignalStrength;
    public int m_Frequency;
    public List<Flower> m_TargetedFlowers = new List<Flower>(0);
    public bool m_SpawnLineRenderers = true;
}
