using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Flower : MonoBehaviour
{
    public PlantSignal m_SignalHitBy;
    public float m_MaxSignalRadius = 2;
    public PlantSignal m_CurrentSignal;
    public Soil m_OnSoil;


    public UnityEvent m_OnRecieveSignal;
    public Transform m_OwnedVisualIndicator;
    public List<LineRenderer> m_OwnedLineRenderers = new List<LineRenderer>();
    public int m_InitialFrequency;
    private bool m_InInteractRange;
    public GameObject m_Particles;
    protected virtual void Start()
    {
        m_SignalHitBy = null;
        ParticleSystem particles = GetComponentInChildren<ParticleSystem>(true);
        if (particles != null)
        {
            m_Particles = particles.gameObject;
        }
    }

    protected virtual void Update()
    {
        if (m_InInteractRange)
        {
            float speed = 100f;
            if (Input.GetKey(KeyCode.E))
            {
                RotateFlower(-(speed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.Q))
            {
                RotateFlower(speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.JoystickButton4))
            {
                RotateFlower(-(speed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.JoystickButton5))
            {
                RotateFlower(speed * Time.deltaTime);
            }
        }
    }

    public virtual void Transmit()
    {
    }

    public virtual void EnableIndicator()
    {

    }

    public virtual void OnRecieveSignal()
    {
        m_OnRecieveSignal?.Invoke();
        m_OnSoil.m_OnReceiveSignal?.Invoke(this);
        if (m_Particles != null)
        {
            m_Particles.SetActive(true);
            CancelInvoke(nameof(DisableParticles));
            Invoke(nameof(DisableParticles), 0.2f);
        }
    }

    public virtual Flower getClosestFlowerInRange()
    {
        Flower closestFlower = null;

        Collider2D[] flowersInRange = Physics2D.OverlapCircleAll(transform.position, m_MaxSignalRadius, LayerMask.GetMask("Flower"));
        float maxDist = float.MaxValue;
        foreach (var col in flowersInRange)
        {
            float dist = Vector2.Distance(col.transform.position, transform.position);
            if (dist < maxDist)
            {
                if (!CheckIfValid() || col.GetComponent<Flower>().m_SignalHitBy != null)
                    continue;
                maxDist = dist;
                closestFlower = col.GetComponent<Flower>();
            }
        }
        return closestFlower;
    }

    public virtual bool CheckIfValid()
    {
        return true;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        PlantManager.Instance.ChangeLastInteractedFlower(this);

        if (col.CompareTag("Player"))
        {
            m_InInteractRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_InInteractRange = false;
        }
    }

    protected virtual void RotateFlower(float valueToChangeBy)
    {
        Vector3 rot = transform.eulerAngles;
        rot.z += valueToChangeBy;
        /*if (rot.z > 360.01f)
        {
            rot.z = 0;
        }
        else if (rot.z < -0.01f)
        {
            rot.z = 360;
        }*/
        transform.eulerAngles = rot;
        AudioManager.Instance.Play("PickFlower");
    }

    public void DisableParticles()
    {
        m_Particles.SetActive(false);
    }
}