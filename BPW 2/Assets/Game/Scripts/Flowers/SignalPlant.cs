public class SignalPlant : Flower
{
    public bool m_OGSignalPlant = true;
    override protected void Update()
    {
        base.Update();
        Transmit();
    }

    public override void Transmit()
    {
        //if (SignalManager.Instance.m_AllSignalsFinished)
        //{
        if (m_OGSignalPlant)
            SignalManager.Instance.Reset();

        m_CurrentSignal = new PlantSignal();
        m_SignalHitBy = m_CurrentSignal;
        m_CurrentSignal.m_Frequency = m_InitialFrequency;
        m_CurrentSignal.m_SignalOwner = this;
        Flower flower = getClosestFlowerInRange();
        if (flower != null)
            m_CurrentSignal.m_TargetedFlowers.Add(flower);
        SignalManager.Instance.AddSignal(m_CurrentSignal);
        //}
    }
    public override void OnRecieveSignal()
    {
        if (!m_OGSignalPlant)
            Transmit();
    }
    protected override void RotateFlower(float valueToChangeBy)
    {

    }
}
