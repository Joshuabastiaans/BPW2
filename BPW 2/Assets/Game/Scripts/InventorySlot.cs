using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler
{
    public GameObject m_SelectedBackground;
    public GameObject m_NonSelectedBackground;
    public Image m_FlowerImage;
    public TextMeshProUGUI m_AmmountText;
    public GameObject m_TooltipParent;
    public TextMeshProUGUI m_TooltipFlowerNameText;
    public TextMeshProUGUI m_ExplanationText;
    public GameObject m_SelectedToolTipBackground;
    public GameObject m_NonSelectedToolTipBackground;
    public bool m_InSlot = false;
    
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (m_InSlot && Input.GetMouseButtonDown(0))
        {
            PlantManager.Instance.SelectFlower(PlantManager.Instance.m_InventorySlots.IndexOf(this));
        }
    }

    public void Initialize()
    {
        
    }

    public void SelectSlot()
    {
        m_SelectedBackground.SetActive(true);
        m_NonSelectedBackground.SetActive(false);
        m_SelectedToolTipBackground.SetActive(true);
        m_NonSelectedToolTipBackground.SetActive(false);
    }

    public void UnSelectSlot()
    {
        m_SelectedBackground.SetActive(false);
        m_NonSelectedBackground.SetActive(true);
        m_SelectedToolTipBackground.SetActive(false);
        m_NonSelectedToolTipBackground.SetActive(true);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("OnMouseEnter");
        m_TooltipParent.SetActive(true);
        m_InSlot = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        print("OnMouseExit");
        m_TooltipParent.SetActive(false);
        m_InSlot = false;
    }
}
