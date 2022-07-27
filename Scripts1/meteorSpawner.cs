using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rock;

    

    // Start is called before the first frame update
    void Start()
    {
        
        

        InvokeRepeating("RockSpawner",0,5);
    }

    public void RockSpawner()
    {
        float xAxis = Random.Range(0, 100);
        float zAxis = Random.Range(0, 100);

        Instantiate(rock, new Vector3(xAxis, 65, zAxis), Quaternion.identity);
    }

 
}
