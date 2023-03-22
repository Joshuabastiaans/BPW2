using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SignalManager : MonoBehaviour
{
    #region Instancing

    public static SignalManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SignalManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("SignalManager").AddComponent<SignalManager>();
                }
            }

            return _instance;
        }
    }

    private static SignalManager _instance;

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
    public List<PlantSignal> m_ActiveSignals = new List<PlantSignal>();
    public bool m_AllSignalsFinished;
    public LineRenderer m_BaseRendererPrefab;

    public Dictionary<int, Color> m_ColorPerFrequency = new Dictionary<int, Color>();


    private void Start()
    {
        m_ColorPerFrequency.Add(0,new Color32(165,50,0,255));
        m_ColorPerFrequency.Add(1,Color.blue);
        m_ColorPerFrequency.Add(2,Color.green);
        m_ColorPerFrequency.Add(3,Color.red);
    }

    public void Reset()
    {
        m_AllSignalsFinished = false;
        while(m_ActiveSignals.Count>0)
        {
            foreach (var flower in m_ActiveSignals[0].m_TargetedFlowers)
            {
                flower.m_SignalHitBy = null;
            }
            m_ActiveSignals.RemoveAt(0);
        }
        m_ActiveSignals.Clear();
    }

    public void CheckSignalsStillActive()
    {
        bool somethingactive = false;
        foreach (var signal in m_ActiveSignals)
        {
            if (signal.m_TargetedFlowers.Count == 0)
            {
                
            }
        }
    }
    
    public void AddSignal(PlantSignal signal)
    {
        m_ActiveSignals.Add(signal);
        foreach (var renderer in signal.m_SignalOwner.m_OwnedLineRenderers)
        {
            renderer.gameObject.SetActive(false);
        }
        for (int i = 0; i < signal.m_TargetedFlowers.Count; i++)
        {
            signal.m_TargetedFlowers[i].m_SignalHitBy = signal;
            if (signal.m_SpawnLineRenderers)
            {
                if (signal.m_SignalOwner.m_OwnedLineRenderers.Count >= i + 1)
                {
                    signal.m_SignalOwner.m_OwnedLineRenderers[i].gameObject.SetActive(true);
                    signal.m_SignalOwner.m_OwnedLineRenderers[i]
                        .SetPosition(0, signal.m_SignalOwner.transform.position);
                    signal.m_SignalOwner.m_OwnedLineRenderers[i]
                        .SetPosition(1, signal.m_TargetedFlowers[i].transform.position);
                    signal.m_SignalOwner.m_OwnedLineRenderers[i].startColor = m_ColorPerFrequency[signal.m_Frequency];
                    signal.m_SignalOwner.m_OwnedLineRenderers[i].endColor = m_ColorPerFrequency[signal.m_Frequency];
                }
                else
                {
                    LineRenderer renderer = Instantiate(m_BaseRendererPrefab, signal.m_SignalOwner.transform.position,
                        quaternion.identity, signal.m_SignalOwner.transform);
                    renderer.SetPosition(0, signal.m_SignalOwner.transform.position);
                    renderer.SetPosition(1, signal.m_TargetedFlowers[i].transform.position);
                    renderer.startColor = m_ColorPerFrequency[signal.m_Frequency];
                    renderer.endColor = m_ColorPerFrequency[signal.m_Frequency];
                    signal.m_SignalOwner.m_OwnedLineRenderers.Add(renderer);
                }
            }
        }
        foreach (var targetFlower in signal.m_TargetedFlowers)
        {
            targetFlower.OnRecieveSignal();
        }
    }
}
