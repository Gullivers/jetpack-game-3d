using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GG_JetpackMovement : MonoBehaviour
{
    GG_ParticleControl particleControl;
    [SerializeField] GameObject Trajectory;
    [SerializeField] Transform PointerTrajectory;
    public float ForwardSpeed, UpSpeed;
    float DefForwardSpeed;
    //[SerializeField] float FallingSpeed = .5f;
    [SerializeField] float JetpackAngle;
    public float Xdegree;
    [SerializeField] float DegreeIncreser;
    [SerializeField] float SoftlaunchMin, SoftlaunchMax;
    [SerializeField] float SoftLaunchDuration;
    [SerializeField] Ease LaunchEase;

    [HideInInspector]
    public bool JetPackOn = false;
    [HideInInspector]
    public bool FallingOn = false;
    [HideInInspector]
    public int DotCounter = 1;

    bool DummyJetPackOn = true;
    bool DummySoftlaunch = true;
    bool DummyFalling = true;

    [HideInInspector]
    public bool CanTap = true;

    public float Fuel;
    public bool CanFillFuel = false;

    [HideInInspector]
    public float FuelForStart;
    [SerializeField] float FuelRegeneration;
    Rigidbody rb;

    Animator aAnimator;


    private void Awake()
    {
        aAnimator = GetComponent<Animator>();
        FuelForStart = Fuel;
        rb = GetComponent<Rigidbody>();
        particleControl = GetComponent<GG_ParticleControl>();
        DefForwardSpeed = ForwardSpeed;

    }
    void FixedUpdate()
    {

        if (Fuel <= 0 && JetPackOn) { FuelIsEmpty(); particleControl.StopJetpackParticle(); }
        #region  JetPack is On
        if (JetPackOn)
        {
            aAnimator.SetFloat("ShakingSpeed", aAnimator.GetFloat("ShakingSpeed") - .03f);
            if (aAnimator.GetFloat("ShakingSpeed") <= 0) { aAnimator.SetTrigger("JPoff"); }

            Xdegree += DegreeIncreser;
            Xdegree = Mathf.Clamp(Xdegree, -20, 20);
            if (DummyJetPackOn)
            {
                aAnimator.SetFloat("ShakingSpeed", 4f);
                aAnimator.SetTrigger("JPon");
                CanFillFuel = false;
                particleControl.StartJetpackParticle();
                DummyFalling = DummySoftlaunch = true;
                DummyJetPackOn = false;
                //transform.DOLocalRotate(new Vector3(JetpackAngle, 0, 0), .5f);
            }
            transform.rotation = Quaternion.Euler(Xdegree, 0, 0);
            Fuel -= .1f;
            rb.AddForce(Vector3.up * UpSpeed);
            rb.AddForce(Vector3.forward * ForwardSpeed);

        }
        #endregion
        #region  Jetpack is Off
        else if (FallingOn)
        {

            Xdegree -= DegreeIncreser;
            Xdegree = Mathf.Clamp(Xdegree, -15, 15);
            transform.rotation = Quaternion.Euler(Xdegree, 0, 0);
            Fuel += FuelRegeneration;
            Fuel = Mathf.Clamp(Fuel, 0f, FuelForStart);
            if (DummyFalling)
            {


                DummyJetPackOn = true;
                rb.useGravity = true;
                //transform.DOLocalRotate(new Vector3(0, 0, 0), .5f);
                DummyFalling = false;

            }

            //Softlaunch
            if (Vector3.Distance(transform.position, PointerTrajectory.position) < SoftlaunchMax && Vector3.Distance(transform.position, PointerTrajectory.position) > SoftlaunchMin
             && DummySoftlaunch)
            {
                aAnimator.ResetTrigger("JPoff");
                aAnimator.SetFloat("ShakingSpeed", 5f);
                aAnimator.SetTrigger("JPon");
                Debug.Log("Softlaunch");
                Debug.Log(PointerTrajectory.position + "  PointerPos");
                DummySoftlaunch = false;
                rb.useGravity = false;

                rb.velocity = Vector3.zero;
                transform.DOMove(PointerTrajectory.position, SoftLaunchDuration).SetEase(LaunchEase).SetId("Softlaunch");
                particleControl.StartJetpackParticle();
                //transform.DOLocalRotate(new Vector3(-10, 0, 0), 1f).SetId("SoftlaunchAngle");

            }
            aAnimator.SetFloat("ShakingSpeed", aAnimator.GetFloat("ShakingSpeed") - .05f);
            if (aAnimator.GetFloat("ShakingSpeed") <= 0) { aAnimator.SetTrigger("JPoff"); }
            if (CanFillFuel) { Fuel += FuelRegeneration; }
        }
        #endregion
    }

    void ResetSpeedValues()
    {
        ForwardSpeed = DefForwardSpeed;

    }
    public void OnClickDown()
    {
        if (Fuel >= 0 && CanTap)
        {
            //rb.useGravity = false;

            //transform.DOLocalRotate(new Vector3(JetpackAngle, 0, 0), .5f);
            JetPackOn = true;
            FallingOn = false;

        }
    }
    public void OnClickUp()
    {

        if (CanTap) { particleControl.StopJetpackParticle(); }
        if (!DummySoftlaunch) { particleControl.StartJetpackParticle(); }
        rb.useGravity = true;
        if (Fuel >= 0 && CanTap)
        {

            JetPackOn = false;
            FallingOn = true;
        }
        CanTap = false;

    }

    void FuelIsEmpty()
    {
        if (JetPackOn)
        {
            rb.useGravity = true;
            JetPackOn = false;
            FallingOn = true;
        }
    }

}
