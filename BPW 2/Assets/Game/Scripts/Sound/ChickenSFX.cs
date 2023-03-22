using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSFX : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter2D()
    {
        audioManager.Play("Chickin");
    }
}
