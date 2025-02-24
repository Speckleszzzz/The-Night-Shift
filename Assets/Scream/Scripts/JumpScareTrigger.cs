using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareTrigger : MonoBehaviour
{
    public GameObject corridorJumpScare;
    public GameObject colliderToEnable;
    public AudioSource knockingSound;
    public AudioSource cryingSound;
    public GameObject knockingCollider;
    public GameObject cryingCollider;

    [SerializeField] bool knocking = true;
    [SerializeField] bool crying = true;
    [SerializeField] bool monster = true;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            int temp = Random.Range(2, 7);
            if (temp == 3 && monster == true)
            {
                corridorJumpScare.SetActive(true);
                colliderToEnable.SetActive(true);
                monster = false;
            }

            else if (temp == 4 && knocking == true)
            {
                Debug.Log("Knocking is playing");
                knockingCollider.SetActive(true);
                knocking = false;
                knockingSound.Play();
            }

            else if (temp == 5 && crying == true)
            {
                Debug.Log("Crying is playing");
                cryingCollider.SetActive(true);
                crying = false;
                cryingSound.Play();
            }
        }    
    }
}
