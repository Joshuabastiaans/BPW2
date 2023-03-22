using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public AudioMixer m_AudioMixer;
    public OptionsData m_OptionsData;
    public TextMeshProUGUI m_MasterText;
    public Slider m_MasterSlider;
    public TextMeshProUGUI m_SFXText;
    public Slider m_SFXSlider;
    public TextMeshProUGUI m_VoicesText;
    public Slider m_VoicesSlider;
    public TextMeshProUGUI m_MusicText;
    public Slider m_MusicSlider;
    private void OnEnable()
    {
        if (SaveManager.JsonExists("Options"))
            m_OptionsData = SaveManager.ConvertFromJson<OptionsData>(SaveManager.LoadTheJson("Options"));
        else
        {
            m_OptionsData = new OptionsData();
        }
        SetAudioVolumes();
        m_MasterSlider.value = m_OptionsData.m_MasterVolume + 80;
        m_MusicSlider.value = m_OptionsData.m_MusicVolume+ 80;
        m_SFXSlider.value = m_OptionsData.m_SFXVolume+ 80;
        m_VoicesSlider.value = m_OptionsData.m_VoicesVolume+ 80;
    }

    public void ChangeMasterVol(float newVolume)
    {
        m_OptionsData.m_MasterVolume = newVolume - 80;
        SetAudioVolumes();
    }
    
    public void ChangeSFXVol(float newVolume)
    {
        m_OptionsData.m_SFXVolume = newVolume- 80;
        SetAudioVolumes();
    }
    
    public void ChangeVoicesVol(float newVolume)
    {
        m_OptionsData.m_VoicesVolume = newVolume- 80;
        SetAudioVolumes();
    }
    
    public void ChangeMusicVol(float newVolume)
    {
        m_OptionsData.m_MusicVolume = newVolume- 80;
        SetAudioVolumes();
    }
    
    private void OnDisable()
    {
        SaveManager.SaveTheJson("Options", SaveManager.TurnIntoJson(m_OptionsData));
    }

    private void OnDestroy()
    {
        SaveManager.SaveTheJson("Options", SaveManager.TurnIntoJson(m_OptionsData));
    }

    public void SetAudioVolumes()
    {
        m_AudioMixer.SetFloat("masterVol", m_OptionsData.m_MasterVolume);
        m_MasterText.text = (m_OptionsData.m_MasterVolume + 80).ToString();
        m_AudioMixer.SetFloat("sfxVol", m_OptionsData.m_SFXVolume);
        m_SFXText.text = (m_OptionsData.m_SFXVolume + 80).ToString();
        m_AudioMixer.SetFloat("musicVol", m_OptionsData.m_MusicVolume);
        m_MusicText.text = (m_OptionsData.m_MusicVolume +80).ToString();
        m_AudioMixer.SetFloat("voicesVol", m_OptionsData.m_VoicesVolume);
        m_VoicesText.text = (m_OptionsData.m_VoicesVolume +80).ToString();
    }
}
[System.Serializable]
public class OptionsData
{
    public float m_MasterVolume = 0;
    public float m_SFXVolume = 0;
    public float m_MusicVolume = 0;
    public float m_VoicesVolume = 0;
}
