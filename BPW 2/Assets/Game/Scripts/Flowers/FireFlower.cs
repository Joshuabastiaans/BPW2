using UnityEngine;

public class FireFlower : Flower
{
    public float m_RaycastDistance = 5f;
    public LayerMask m_IcePlantLayer;

    public override void Transmit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, m_RaycastDistance, m_IcePlantLayer);
        if (hit.collider != null)
        {
            IcePlant icePlant = hit.collider.GetComponent<IcePlant>();
            if (icePlant != null)
            {
                icePlant.Melt();
                m_CurrentSignal = icePlant.m_SignalHitBy;
                OnRecieveSignal();
            }
        }
    }
}