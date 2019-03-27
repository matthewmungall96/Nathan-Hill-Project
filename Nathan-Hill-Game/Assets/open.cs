using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<Animator>().SetTrigger("Door_open");
        Debug.Log("Collided");
    }
}
