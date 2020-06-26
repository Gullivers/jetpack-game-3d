using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GG_TrajectoryDotTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Trajectory" && other.tag != "Player")
        {
            
            

            //GetComponentInParent<GG_TrajectoryWithPhysics>().LastDotIndex = Int32.Parse(transform.name);

        }
    }
}
