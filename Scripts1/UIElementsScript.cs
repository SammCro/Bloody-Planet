using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementsScript : MonoBehaviour
{

    [SerializeField] private GameObject PauseCanvas;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            PauseCanvas.SetActive(true);
        }
    }
}
