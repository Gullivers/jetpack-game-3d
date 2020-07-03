using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GG_PlayerTrigger : MonoBehaviour
{
    public Vector3 LastCheckpoint;
    [SerializeField] GG_JetpackMovement Jetpack;
    GG_ParticleControl particleControl;
    Rigidbody rb;
    [SerializeField] Transform Trajectory;
    [SerializeField] Transform PointerTrajectory;
    [SerializeField] GG_VoidEvent FinishEvent;
    Animator aAnimator;
    bool CanCollide = true;
    private void Awake()
    {
        aAnimator = GetComponent<Animator>();
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

            SetLastCheckpoint();
            ResetTrajectory();
            Debug.Log("PGirdii Front");
            return;
        }
        if (col.tag == "Platform")
        {
            Debug.Log("PGirdii Platform");

            aAnimator.SetTrigger("JPoff");
            particleControl.StopJetpackParticle();
            particleControl.StartLangingPart();
            Jetpack.Fuel = Jetpack.FuelForStart;
            //DOTween.Kill("SoftlaunchZ");
            Jetpack.FallingOn = false;
            Jetpack.CanFillFuel = true;
            DOTween.Kill("Softlaunch" + this.transform.name);

            // DOTween.Kill("SoftlaunchAngle");
            //DOTween.Kill("Softlaunch");
            transform.position = new Vector3(transform.position.x, col.transform.position.y + 9.5f, transform.position.z);
            transform.rotation = Quaternion.EulerAngles(0, 0, 0);
            Jetpack.Xdegree = 0;
            LastCheckpoint = transform.position;
            ResetTrajectory();
            return;

        }
        if (col.tag == "Water")
        {

            SetLastCheckpoint();
            Debug.Log("Girdii Water");
            ResetTrajectory();
            return;
        }
        if (col.tag == "Finish")
        {

            particleControl.StopJetpackParticle();
            particleControl.StartLangingPart();
            DOTween.Kill("Softlaunch" + this.transform.name);

            Jetpack.FallingOn = false;
            transform.position = new Vector3(transform.position.x, col.transform.position.y + 9.5f, transform.position.z);
            transform.rotation = Quaternion.EulerAngles(0, 0, 0);
            rb.velocity = Vector3.zero;
            Jetpack.Xdegree = 0;
            Jetpack.CanTap = true;
            rb.useGravity = false;
            ResetTrajectory();
            FinishEvent.Raise();
            Debug.Log("Finish raisee");
            return;
        }

    }

    public void SetLastCheckpoint()
    {

        aAnimator.SetTrigger("JPoff");
        Jetpack.Xdegree = 0;

        transform.rotation = Quaternion.EulerAngles(0, 0, 0);
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

        for (int i = 0; i < Trajectory.childCount; i++)
        {
            Trajectory.GetChild(i).transform.position = new Vector3(0, 1, -20);
        }

    }

    public void Retrylevel()
    {
        ResetTrajectory();
        DOTween.KillAll();
        aAnimator.SetTrigger("JPoff");
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        Jetpack.FallingOn = false;
        Jetpack.Fuel = Jetpack.FuelForStart;
        Jetpack.JetPackOn = false;
        Jetpack.CanTap = true;
        transform.position = new Vector3(0, 3, 0);
        transform.rotation = Quaternion.EulerAngles(0, 0, 0);
        LastCheckpoint = transform.position;
    }
}
