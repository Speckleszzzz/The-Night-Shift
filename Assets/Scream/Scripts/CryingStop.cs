using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CryingStop : MonoBehaviour
{
    public GameObject crying;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(crying);
            Destroy(this);
        }
    }
}
