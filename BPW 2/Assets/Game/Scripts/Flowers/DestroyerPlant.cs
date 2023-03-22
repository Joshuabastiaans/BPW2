using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerPlant : Flower
{
    public LayerMask m_mask;
    public LineRenderer m_Lazor;
    public override void Transmit()
    {
        
        m_CurrentSignal = new PlantSignal();
        m_CurrentSignal.m_Frequency = m_SignalHitBy.m_Frequency;
        m_CurrentSignal.m_SignalOwner = this;
        
       
        if (m_Lazor != null)
        {
            m_Lazor.gameObject.SetActive(true);
            CancelInvoke(nameof(DisableLazor));
            Invoke(nameof(DisableLazor),0.2f);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up,m_MaxSignalRadius,m_mask);
        if (hit.collider != null)
        {
            m_Lazor.SetPosition(1,new Vector3(0,hit.distance,0));
            DestroyableTarget target = hit.transform.GetComponent<DestroyableTarget>();
            if (target != null)
                target.DestroyTarget();

        }
        SignalManager.Instance.AddSignal(m_CurrentSignal);

        AudioManager.Instance.Play("Destroyer");
    }

    public override void OnRecieveSignal()
    {
        /*transform.up = m_SignalHitBy.m_SignalOwner.transform.position - transform.position;
        transform.up *= -1;*/
        transform.up = m_SignalHitBy.m_SignalOwner.transform.up;
        Transmit();

    }

    protected override void RotateFlower(float valueToChangeBy)
    {
        
    }

    public override void EnableIndicator()
    {
        m_OwnedVisualIndicator.gameObject.SetActive(true);
        m_OwnedVisualIndicator.GetComponent<LineRenderer>().SetPosition(1,new Vector3(0,m_MaxSignalRadius,0));
    }
    
    public void DisableLazor()
    {
        m_Particles.SetActive(false);
    }
}
