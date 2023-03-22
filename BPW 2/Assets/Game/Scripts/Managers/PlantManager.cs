using MoreMountains.Tools;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlantManager : MonoBehaviour
{

    #region Instancing

    public static PlantManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlantManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("PlantManager").AddComponent<PlantManager>();
                }
            }

            return _instance;
        }
    }

    private static PlantManager _instance;

    private void Awake()
    {
        // Destroy any duplicate instances that may have been created
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    #endregion
    public List<Flower> m_SelectableFlowers;
    public List<int> m_FlowersOwned;
    public int m_SelectedFlower = 0;
    public Flower m_LastInteractedFlower;
    public List<InventorySlot> m_InventorySlots;


    private void Start()
    {
        while (m_FlowersOwned.Count < m_SelectableFlowers.Count)
        {
            m_FlowersOwned.Add(0);
        }

        for (int i = 0; i < m_FlowersOwned.Count; i++)
        {
            m_InventorySlots[i].m_AmmountText.text = m_FlowersOwned[i].ToString();
        }
    }

    private void Update()
    {
        for (int i = 0; i < m_FlowersOwned.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                SelectFlower(i);
            }
        }


        // Switch slots with D-Pad from controller
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            int selectedSlot = m_SelectedFlower;
            selectedSlot = (selectedSlot - 1 + m_FlowersOwned.Count) % m_FlowersOwned.Count;
            SelectFlower(selectedSlot);
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            int selectedSlot = m_SelectedFlower;
            selectedSlot = (selectedSlot + 1) % m_FlowersOwned.Count;
            SelectFlower(selectedSlot);
        }
    }

    public void SelectFlower(int i)
    {
        m_InventorySlots[m_SelectedFlower].UnSelectSlot();
        m_SelectedFlower = i;
        m_InventorySlots[m_SelectedFlower].SelectSlot();
    }
    public void ChangeLastInteractedFlower(Flower newInteractedFlower)
    {
        if (m_LastInteractedFlower != null)
        {
            m_LastInteractedFlower.m_OwnedVisualIndicator.gameObject.SetActive(false);
        }

        m_LastInteractedFlower = newInteractedFlower;
        m_LastInteractedFlower.EnableIndicator();
    }

    public void ReturnFlower(Flower occupyingFlower)
    {
        for (int i = 0; i < m_SelectableFlowers.Count; i++)
        {
            if (occupyingFlower.GetType() == m_SelectableFlowers[i].GetType())
            {
                m_FlowersOwned[i]++;
                m_InventorySlots[i].m_AmmountText.text = m_FlowersOwned[i].ToString();
                return;
            }
        }
    }

    public Flower GetCurrentlySelectedFlower()
    {
        if (m_FlowersOwned[m_SelectedFlower] > 0)
        {
            m_FlowersOwned[m_SelectedFlower]--;
            m_InventorySlots[m_SelectedFlower].m_AmmountText.text = m_FlowersOwned[m_SelectedFlower].ToString();
            return m_SelectableFlowers[m_SelectedFlower];
        }
        else
        {
            Debug.LogError("It does not currently have that flower");
        }

        return null;
    }
}
