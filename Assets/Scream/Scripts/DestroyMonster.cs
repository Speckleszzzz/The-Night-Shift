using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyMonster : MonoBehaviour
{
    public GameObject Monster;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(Monster);
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
