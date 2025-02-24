using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockingStop : MonoBehaviour
{
    public GameObject knocking;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(knocking);
            Destroy(this);
        }
        
    }
}
