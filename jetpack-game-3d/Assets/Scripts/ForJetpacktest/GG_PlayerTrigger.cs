using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_PlayerTrigger : MonoBehaviour
{
    [SerializeField] Vector3 LastCheckpoint=Vector3.zero;
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Platform")
        {
            LastCheckpoint=transform.position;
            Debug.Log("Girdii");
        }
        if(col.tag=="Water")
        {
            transform.position=LastCheckpoint;
             Debug.Log("Girdii Water");
        }
    }
}
