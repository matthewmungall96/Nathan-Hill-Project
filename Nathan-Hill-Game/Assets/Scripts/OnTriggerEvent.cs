using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]//requires that the script is placed on an object with a box collider
public class OnTriggerEvent : MonoBehaviour
{
    public UnityEvent onTriggered;
    public bool trigger_once;
    private bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (trigger_once && isTriggered)//player activates trigger
            return;

        if (other.tag == "Player") //requires the player tag to be connected to the player
        {
            onTriggered.Invoke();
            isTriggered = true;
        }
    }
}
