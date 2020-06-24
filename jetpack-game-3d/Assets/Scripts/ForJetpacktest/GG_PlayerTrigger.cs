using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_PlayerTrigger : MonoBehaviour
{
    public Vector3 LastCheckpoint;
    [SerializeField] GG_JetpackMovement Jetpack;
    private void Awake() {
        LastCheckpoint=transform.position;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Platform")
        {
            LastCheckpoint = transform.position;
            Jetpack.CanTap = true;
            Debug.Log("Girdii");
        }
        if (col.tag == "Water")
        {
            SetLastCheckpoint();
            Jetpack.CanTap = true;
            Debug.Log("Girdii Water");
        }
    }

    public void SetLastCheckpoint()
    {
        Jetpack.FallingOn=false;
        Jetpack.DotCounter=1;
        transform.position = LastCheckpoint;
    }
}
