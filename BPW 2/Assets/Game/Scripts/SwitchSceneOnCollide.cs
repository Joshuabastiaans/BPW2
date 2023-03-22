using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneOnCollide : MonoBehaviour
{
    public int m_SceneToSwitchTo = 2;
    public bool autoLoadNextScene = true; 

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (autoLoadNextScene == false)
        {
            if (col.CompareTag("Player"))
                SceneManager.LoadScene(m_SceneToSwitchTo);
            AudioManager.Instance.Play("LevelComplete");
        }

        if (autoLoadNextScene == true)
        {
            if (col.CompareTag("Player"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            AudioManager.Instance.Play("LevelComplete");

        }
    }
}
