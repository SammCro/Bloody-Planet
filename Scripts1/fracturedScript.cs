using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fracturedScript : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Destroy(gameObject, 5f);
    }

}
