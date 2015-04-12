using UnityEngine;
using System.Collections;

public class BasicSensor : MonoBehaviour
{
    // detect if another GameObject entered the boundary
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Character>())
        {
            Debug.Log("Hi There!");
        }
    }
}
