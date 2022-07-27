using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float mouseSensivity;

    private Transform ParenTransform;

    void Start()
    {
        ParenTransform = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensivity;
        ParenTransform.Rotate(Vector3.up,mouseX);

    }
}
