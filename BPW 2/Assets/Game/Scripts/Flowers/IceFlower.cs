using UnityEngine;
using UnityEngine.Events;

public class IcePlant : Flower
{
    public UnityEvent m_OnMelt;

    private bool m_IsMelted = false;

    private float m_MeltAmount;
    [SerializeField] private float m_FreezeRate = 20f;
    [SerializeField] private float m_MeltRate = 100f;

    private SpriteRenderer m_SpriteRenderer;
    private Color m_DefaultColor = Color.white;
    private Color m_MeltedColor = Color.yellow;

    public DoorManager m_Doormanager;
    [SerializeField] private int m_WhichFlower;

    protected override void Start()
    {
        base.Start();

        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = m_DefaultColor;
    }

    protected override void Update()
    {
        base.Update();

        // Gradually freeze back up
        float freezeAmount = m_FreezeRate * Time.deltaTime;
        m_MeltAmount = Mathf.Max(0f, m_MeltAmount - freezeAmount);

        ChangeColor();
    }

    public override void OnRecieveSignal()
    {
        m_OnRecieveSignal?.Invoke();
        if (m_MeltAmount >= 100f)
            m_OnSoil.m_OnReceiveSignal?.Invoke(this);
        if (m_Particles != null)
        {
            m_Particles.SetActive(true);
            CancelInvoke(nameof(DisableParticles));
            Invoke(nameof(DisableParticles), 0.2f);
        }

        Melt();
    }

    public void Melt()
    {
        m_MeltAmount = m_MeltAmount + m_MeltRate * Time.deltaTime;

        if (m_MeltAmount >= 100)
        {
            //if >100 melted, cooldown timer starts, then slowly freeze back up
            m_IsMelted = true;

        }
        
    }
    public void ChangeColor()
    {
        // Update sprite color based on melt amount
        float t = m_MeltAmount / 100f;
        Color currentColor = Color.Lerp(m_DefaultColor, m_MeltedColor, t);
        m_SpriteRenderer.color = currentColor;
    }

    public void ResetDoors()
    {
        if (m_MeltAmount < 100)
        {

            if (m_WhichFlower == 1)
            {
                m_Doormanager.Door1 = false;
            }
            if (m_WhichFlower == 2)
            {
                m_Doormanager.Door2 = false;
            }
            if (m_WhichFlower == 3)
            {
                m_Doormanager.Door3 = false;
            }
        }
    }
}
