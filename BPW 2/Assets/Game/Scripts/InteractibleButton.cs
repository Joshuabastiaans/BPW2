using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractibleButton : MonoBehaviour, IInteractible
{
    public UnityEvent m_OnInteract;
    public GameObject m_Indicator;
    public void Interact()
    {
        m_OnInteract?.Invoke();
        m_Indicator.SetActive(true);
        AudioManager.Instance.Play("PlaceFlower");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if(!col.transform.GetComponent<PlayerController>().m_InteractiblesInRange.Contains(transform))
                col.transform.GetComponent<PlayerController>().m_InteractiblesInRange.Add(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.transform.GetComponent<PlayerController>().m_InteractiblesInRange.Contains(transform))
                other.transform.GetComponent<PlayerController>().m_InteractiblesInRange.Remove(transform);
        }
    }
}
