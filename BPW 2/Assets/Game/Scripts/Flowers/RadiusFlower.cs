using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusFlower : Flower
{
    public override void Transmit()
    {
        
        Collider2D[] flowersInRange = Physics2D.OverlapCircleAll(transform.position, m_MaxSignalRadius, LayerMask.GetMask("Flower"));
        float maxDist = float.MaxValue;
        List<Flower> targetFlowers = new List<Flower>();
        foreach (var col in flowersInRange)
        {
                if (!CheckIfValid() || col.GetComponent<Flower>().m_SignalHitBy != null)
                    continue;
                targetFlowers.Add(col.GetComponent<Flower>());
        }

        if (targetFlowers.Count == 0)
        {
            return;
        }
        m_CurrentSignal = new PlantSignal();
        m_CurrentSignal.m_Frequency = m_SignalHitBy.m_Frequency;
        m_CurrentSignal.m_SignalOwner = this;
        
        m_CurrentSignal.m_TargetedFlowers = targetFlowers;
        SignalManager.Instance.AddSignal(m_CurrentSignal);
    }

    public override void OnRecieveSignal()
    {
        base.OnRecieveSignal();
        Transmit();
    }

    public override void EnableIndicator()
    {
        m_OwnedVisualIndicator.gameObject.SetActive(true);
        m_OwnedVisualIndicator.localScale = new Vector3(m_MaxSignalRadius, m_MaxSignalRadius, m_MaxSignalRadius);
    }
    
    protected override void RotateFlower(float valueToChangeBy)
    {
        
    }
}
