using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject[] canvasToShow;
    [SerializeField] private GameObject mainMenuCanvas;


    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume"); 
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"), true);
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClick(int index)
    {
        mainMenuCanvas.SetActive(false);
        canvasToShow[index].SetActive(true);
    }

    public void OnBack(int index)
    {
        mainMenuCanvas.SetActive(true);
        canvasToShow[index].SetActive(false);

     
    }


    
}

