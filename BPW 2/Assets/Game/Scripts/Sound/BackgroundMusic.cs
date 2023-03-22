using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("BackgroundMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
