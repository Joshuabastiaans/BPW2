using System.Collections.Generic;

public class Sprout : Flower
{
    public Flower m_FlowerToGrow;
    // -1 = just any frequency
    public float m_NeededFrequency = -1;
    public List<PlantSignal> m_SignalsHitBy = new List<PlantSignal>();
    public int m_AmmountOfSignalsNeeded = 1;
    //is blooming flower removable?
    public bool m_IsRemovable = true; 

    protected override void Start()
    {
        base.Start();
        m_OnSoil = transform.parent.GetComponent<Soil>();
        m_OnSoil.m_OccupyingFlower = this;
        m_OnSoil.m_FlowerRemovable = false;
    }
    public override void OnRecieveSignal()
    {
        if ((m_NeededFrequency != -1 && m_NeededFrequency != m_SignalHitBy.m_Frequency) || m_NeededFrequency == -1)
        {
            m_SignalsHitBy.Add(m_SignalHitBy);
            m_SignalHitBy = null;
        }
    }

    protected override void RotateFlower(float valueToChangeBy)
    {

    }

    protected void LateUpdate()
    {
        if (m_SignalsHitBy.Count < m_AmmountOfSignalsNeeded)
        {
            m_SignalsHitBy.Clear();
        }
        else
        {
            if (m_FlowerToGrow != null)
            {
                Flower flower = Instantiate(m_FlowerToGrow, transform.parent.position, transform.parent.rotation, transform.parent).GetComponent<Flower>();
                flower.m_OnSoil = m_OnSoil;
                flower.m_OnSoil.m_OccupyingFlower = flower;
                flower.m_OnSoil.m_FlowerRemovable = m_IsRemovable;
                Destroy(gameObject);
            }
        }
    }
}
