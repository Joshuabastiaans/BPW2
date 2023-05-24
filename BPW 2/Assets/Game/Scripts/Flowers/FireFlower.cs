using UnityEngine;

public class FireFlower : Flower
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

    protected override void Update()
    {
        base.Update();

    }

    protected override void RotateFlower(float valueToChangeBy)
    {
        base.RotateFlower(valueToChangeBy);
    }
    public override void EnableIndicator()
    {
        m_OwnedVisualIndicator.gameObject.SetActive(true);
        Vector3 spawnPosition = transform.position + new Vector3(0f, 2f, 0f);
    }


}
