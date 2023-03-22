using UnityEngine;

public class ConnectionPlant : Flower
{
    public LayerMask m_mask;
    public override void Transmit()
    {
        m_CurrentSignal = new PlantSignal();
        m_CurrentSignal.m_SignalOwner = this;
        m_CurrentSignal.m_Frequency = m_SignalHitBy.m_Frequency;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, m_MaxSignalRadius, m_mask);
        if (hit.collider != null)
        {
            Flower flower = hit.transform.GetComponent<Flower>();
            if (flower != null && flower.m_SignalHitBy == null)
                m_CurrentSignal.m_TargetedFlowers.Add(flower);
        }
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
        m_OwnedVisualIndicator.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, m_MaxSignalRadius, 0));
    }
}
