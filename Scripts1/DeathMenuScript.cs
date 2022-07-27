using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }
}
