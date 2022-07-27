using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sectorCompleted : MonoBehaviour
{
    [SerializeField] private GameObject WonScreen;
    [SerializeField] private GameObject PlayerUI;

    void OnEnable()
    {
        Invoke("SetActiveFunc", 5f);
    }

    void OnDisable()
    {
        WonScreen.SetActive(true);
        Time.timeScale = 0;
    }



    void SetActiveFunc()
    {
        
        gameObject.SetActive(false);
        PlayerUI.SetActive(false);
    }
}
