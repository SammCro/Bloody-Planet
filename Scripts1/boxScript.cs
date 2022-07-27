using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxScript : MonoBehaviour
{

    [SerializeField] private string BoxType;
    [SerializeField] private float Health;
    [SerializeField] private float Booster;

    
    void Update()
    {
        transform.Rotate(Vector3.up*Time.deltaTime*50f,Space.Self);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            switch (BoxType)
            {
                case "Health":
                    
                    coll.gameObject.GetComponent<moveController>().GetHealth(Health);
                    Destroy(gameObject);
                    break;
                case "Fast":
                    
                    coll.gameObject.GetComponent<moveController>().Boost(Booster);
                    Destroy(gameObject);
                    break;

            }
            
        }
        
    }
}
