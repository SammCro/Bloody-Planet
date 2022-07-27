using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryCanvas : MonoBehaviour
{
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
