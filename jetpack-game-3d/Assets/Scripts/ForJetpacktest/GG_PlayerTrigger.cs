using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_PlayerTrigger : MonoBehaviour
{
    public Vector3 LastCheckpoint;
    [SerializeField] GG_JetpackMovement Jetpack;
    GG_ParticleControl particleControl;
    Rigidbody rb;
    [SerializeField] Transform Trajectory;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        LastCheckpoint = transform.position;
        particleControl = GetComponent<GG_ParticleControl>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlatformFront")
        {
            ResetTrajectory();
            SetLastCheckpoint();
            Debug.Log("Girdii Front");
        }
        if (col.tag == "Platform")
        {
            ResetTrajectory();
            Debug.Log("Platform girdi");
            LastCheckpoint = transform.position;
            Jetpack.CanTap = true;
            #region Rigidbody

            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            #endregion


        }
        if (col.tag == "Water")
        {
            ResetTrajectory();
            SetLastCheckpoint();
            Debug.Log("Girdii Water");
        }
    }

    public void SetLastCheckpoint()
    {   Jetpack.Fuel=Jetpack.FuelForStart;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        Jetpack.CanTap = true;
        transform.position = LastCheckpoint;
        particleControl.StopJetpackParticle();
        Jetpack.FallingOn = false;
        //Jetpack.DotCounter = 1;
    }
    void ResetTrajectory()
    {Trajectory.GetComponent<GG_TrajectoryWithPhysics>().LastDotIndex=50;
        for (int i = 0; i < Trajectory.childCount; i++)
        {
            Trajectory.GetChild(i).transform.position = new Vector3(0,1,0);
        }
    }
}
