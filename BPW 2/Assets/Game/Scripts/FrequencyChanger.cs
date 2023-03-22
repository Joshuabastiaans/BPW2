using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyChanger : MonoBehaviour
{
    public Flower m_FlowerFrequencyToChange;
    public int m_FrequencyToChangeTo;
    public void ChangeFrequency()
    {
        m_FlowerFrequencyToChange.m_InitialFrequency = m_FrequencyToChangeTo;
    }
}
