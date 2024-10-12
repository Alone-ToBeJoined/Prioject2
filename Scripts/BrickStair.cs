using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickStair : MonoBehaviour
{
    GameObject objectToActive;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToActive.SetActive(true);
        }
    }
}
