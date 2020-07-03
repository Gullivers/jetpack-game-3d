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
          
        }
        if (col.tag == "Platform")
        {  
            //Animator && Particles
            aAnimator.SetTrigger("JPoff");
            particleControl.StopJetpackParticle();
            particleControl.StartLangingPart();
            //Reset Fuel
            Jetpack.Fuel = Jetpack.FuelForStart;
            //Reset bools && tweens for fallling
            Jetpack.FallingOn = false;
            //Jetpack.CanFillFuel = true;
            DOTween.Kill("Softlaunch" + this.transform.name);

            transform.position = new Vector3(transform.position.x, col.transform.position.y + 9.5f, transform.position.z); //Set position to top the Platform
            transform.rotation = Quaternion.EulerAngles(0, 0, 0);
            Jetpack.Xdegree = 0;
            LastCheckpoint = transform.position; //Setting CheckPoint
            ResetTrajectory();
            

        }
        if (col.tag == "Water")
        {
            SetLastCheckpoint();
            ResetTrajectory();
         
        }
        if (col.tag == "Finish")
        {
           //Reset bools && tweens for fallling
            particleControl.StopJetpackParticle();
            particleControl.StartLangingPart();
            DOTween.Kill("Softlaunch" + this.transform.name);
            Jetpack.FallingOn = false;

            transform.position = new Vector3(transform.position.x, col.transform.position.y + 9.5f, transform.position.z);//Set position to top the Platform
            transform.rotation = Quaternion.EulerAngles(0, 0, 0);
            rb.velocity = Vector3.zero;
            Jetpack.Xdegree = 0;
            Jetpack.CanTap = true;
            rb.useGravity = false;
            ResetTrajectory();
            //Event
            FinishEvent.Raise();
           
          
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

    //It called with Event 
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
