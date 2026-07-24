using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    public GameObject selected;
    public void OnTriggerStay(Collider other)
    {
        if (other.transform.tag != "Player")
        {
            if (other.gameObject.GetComponent<IInteractable>() != null)
            {
                selected = other.transform.gameObject;

            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (selected == other.gameObject)
        {
            selected = null;
        }
    }
}
