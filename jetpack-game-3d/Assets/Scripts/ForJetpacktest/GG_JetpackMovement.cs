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
    [SerializeField] float SoftlaunchMin,SoftlaunchMax;

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
    [HideInInspector]
    public float Fuel;
    [HideInInspector]
    public float FuelForStart;
    Rigidbody rb;


    private void Awake()
    {
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
            if (DummyJetPackOn)
            {
                particleControl.StartJetpackParticle();
                DummyFalling = DummySoftlaunch = true;
                DummyJetPackOn = false;
                transform.DOLocalRotate(new Vector3(JetpackAngle, 0, 0), .5f);
            }
            Fuel -= .1f;
            rb.AddForce(Vector3.up * UpSpeed);
            rb.AddForce(Vector3.forward * ForwardSpeed);

        }
        #endregion
        #region  Jetpack is Off
        else if (FallingOn)
        {
            Fuel += .05f;
            Fuel = Mathf.Clamp(Fuel, 0f, FuelForStart);
            if (DummyFalling)
            {
                DummyJetPackOn = true;
                rb.useGravity = true;
                transform.DOLocalRotate(new Vector3(0, 0, 0), .5f).SetId("FallingAngle");
                DummyFalling = false;

            }
            //Softlaunch
            if (Vector3.Distance(transform.position, PointerTrajectory.position) > SoftlaunchMin &&
            Vector3.Distance(transform.position, PointerTrajectory.position) < SoftlaunchMax &&
            DummySoftlaunch)
            {
                DummySoftlaunch = false;
                //DOTween.Kill("FallingAngle");
                particleControl.StartJetpackParticle();
                transform.DOLocalRotate(new Vector3(-JetpackAngle, 0, 0), .5f);

            }

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
