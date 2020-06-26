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
    [SerializeField] Transform PointerTrajectory;
    [SerializeField] GG_VoidEvent FinishEvent;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        LastCheckpoint = transform.position;
        particleControl = GetComponent<GG_ParticleControl>();
    }
    private void OnTriggerEnter(Collider col)
    {
        #region Rigidbody

        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        #endregion
        Jetpack.CanTap = true;

        if (col.tag == "PlatformFront")
        {
            ResetTrajectory();
            SetLastCheckpoint();
            Debug.Log("Girdii Front");
        }
        if (col.tag == "Platform")
        {

            ResetTrajectory();
            transform.position = new Vector3(transform.position.x, col.transform.position.y + 9.5f, transform.position.z);
            Debug.Log("Platform girdi");
            LastCheckpoint = transform.position;




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
        PointerTrajectory.position = new Vector3(0, 1, -20);
        // Trajectory.GetComponent<GG_TrajectoryWithPhysics>().LastDotIndex = 50;
        for (int i = 0; i < Trajectory.childCount; i++)
        {
            Trajectory.GetChild(i).transform.position = new Vector3(0, 1, -20);
        }
    }

    public void Retrylevel()
    {
        transform.position = new Vector3(0, 3, 0);
        LastCheckpoint = transform.position;
    }
}
