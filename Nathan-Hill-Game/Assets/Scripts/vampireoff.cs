using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vampireoff : MonoBehaviour
{
    public GameObject vampire;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "player")
        {
            vampire.SetActive(false);
        }
    }
}
