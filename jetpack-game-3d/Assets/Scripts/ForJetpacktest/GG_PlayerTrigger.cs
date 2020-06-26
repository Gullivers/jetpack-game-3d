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
    [SerializeField] GG_VoidEvent FinishEvent;
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
        if (col.tag == "Finish")
        {
            ResetTrajectory();
            rb.velocity = Vector3.zero;
            Jetpack.CanTap = true;
            rb.useGravity = false;
            FinishEvent.Raise();
        }
    }

    public void SetLastCheckpoint()
    {
        Jetpack.Fuel = Jetpack.FuelForStart;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        Jetpack.CanTap = true;
        transform.position = LastCheckpoint;
        particleControl.StopJetpackParticle();
        Jetpack.FallingOn = false;

    }
    void ResetTrajectory()
    {
        Trajectory.GetComponent<GG_TrajectoryWithPhysics>().LastDotIndex = 50;
        for (int i = 0; i < Trajectory.childCount; i++)
        {
            Trajectory.GetChild(i).transform.position = new Vector3(0, 1, 0);
        }
    }

    public void Retrylevel()
    {
        transform.position = new Vector3(0, 3, 0);
        LastCheckpoint = transform.position;
    }
}
