using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DieAfterAnimate : MonoBehaviour
{
    public void DestroyAfterAnimate()
    {
        GameObject temp = this.transform.parent.gameObject;
        Destroy(temp);
    }
}
