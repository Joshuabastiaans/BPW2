using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class BasicUIFunctionality : MonoBehaviour
{
    
    #region  EnablingDisabling
    #region Enabling
    [FoldoutGroup("Enabling")] public List<MonoBehaviour> m_ComponentsToEnable = new List<MonoBehaviour>();
    [FoldoutGroup("Enabling")] public List<GameObject> m_ObjectsToEnable = new List<GameObject>();
    
    public void DoEnableDisable()
    {
        EnableThings();
        DisableThings();
    }
    public void EnableOneObject(GameObject thingToEnable)
    {
        thingToEnable.SetActive(true);
    }
    public void EnableThings()
    {
        EnableObjects();
        EnableComponents();
    }
    public void EnableObjects()
    {
        foreach (var thingToEnable in m_ObjectsToEnable)
        {
            thingToEnable.SetActive(true);
        }    
    }
    public void EnableComponents()
    {
        foreach (var thingToEnable in m_ComponentsToEnable)
        {
            thingToEnable.enabled = true;
        }    
    }
    #endregion
    #region Disabling
    [FoldoutGroup("Disabling")] public List<MonoBehaviour> m_ComponentsToDisable = new List<MonoBehaviour>();
    [FoldoutGroup("Disabling")] public List<GameObject> m_ObjectsToDisable = new List<GameObject>();
    
    public void DisableOneObject(GameObject thingToDisable)
    {
        thingToDisable.SetActive(false);
    }
    public void DisableThings()
    {
        DisableObjects();
        DisableComponents();
    }
    
    public void DisableObjects()
    {
        foreach (var thingToEnable in m_ObjectsToDisable)
        {
            thingToEnable.SetActive(false);
        }    
    }
    public void DisableComponents()
    {
        foreach (var thingToEnable in m_ComponentsToDisable)
        {
            thingToEnable.enabled = false;
        }    
    }
    #endregion
    #endregion
    public void QuitApplication()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ChangeTextToValue(float value)
    {
        GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
}
