using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Soil : MonoBehaviour, IInteractible
{
    public Flower m_OccupyingFlower;
    public bool m_FlowerRemovable = true;
    public UnityEvent<Flower> m_OnReceiveSignal;

    public bool m_InInteractRange = false;

    public void Interact()
    {
        if (m_OccupyingFlower == null)
        {
            Flower flowerPrefab = PlantManager.Instance.GetCurrentlySelectedFlower();
            if (flowerPrefab != null)
            {
                m_OccupyingFlower = Instantiate(flowerPrefab, transform.position, transform.rotation,transform)
                    .GetComponent<Flower>();
                m_OccupyingFlower.m_OnSoil = this;
                PlantManager.Instance.ChangeLastInteractedFlower(m_OccupyingFlower);
                AudioManager.Instance.Play("PlaceFlower");
            }
            else
            {
                Debug.LogError("Does not have that flower");
            }
        }
        else if(m_FlowerRemovable)
        {
            PlantManager.Instance.ReturnFlower(m_OccupyingFlower);
            Destroy(m_OccupyingFlower.gameObject);
            m_OccupyingFlower = null;
            AudioManager.Instance.Play("PickFlower");
        }
    }

    private void OnMouseOver()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Interact();
        }*/
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
