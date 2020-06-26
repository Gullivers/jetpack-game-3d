using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GG_JetpackMovement : MonoBehaviour
{
    GG_ParticleControl particleControl;
    [SerializeField] GameObject Trajectory;
    public float ForwardSpeed, UpSpeed;
    float DefForwardSpeed, DefFallingSpeed;
    [SerializeField] float FallingSpeed = .5f;
    [SerializeField] float Forwardacceleration, JetpackAngle;

   // [HideInInspector]
    public bool JetPackOn = false;
    //[HideInInspector]
    public bool FallingOn = false;
    [HideInInspector]
    public int DotCounter = 1;
    [HideInInspector]
    bool SetDotDummy = true;
    //[HideInInspector]
    public bool CanTap = true;
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
        DefFallingSpeed = FallingSpeed;
    }
    void FixedUpdate()
    {
        if (Fuel <= 0 && JetPackOn) { FuelIsEmpty(); particleControl.StopJetpackParticle(); }
        #region  JetPack is On
        if (JetPackOn)
        {

            Trajectory.SetActive(true);
            if (!SetDotDummy)
            {

                CanTap = false;
                SetDotDummy = true;
                transform.DOLocalRotate(new Vector3(JetpackAngle, 0, 0), .5f);
            }
            Fuel -= .1f;
            rb.AddForce(Vector3.up * UpSpeed);
            rb.AddForce(Vector3.forward * ForwardSpeed);
            //ForwardSpeed += Forwardacceleration;
        }
        #endregion
        #region  Jetpack is Off
        else if (FallingOn)
        {
            Fuel += .05f;
            Fuel = Mathf.Clamp(Fuel, 0f, FuelForStart);
            if (SetDotDummy)
            {
                rb.useGravity = true;
                //GetComponent<ParabolaController>().SetDots();
                SetDotDummy = false;
                transform.DOLocalRotate(new Vector3(0, 0, 0), .5f);
            }

            // #region Following parabola Dots
            // transform.position = Vector3.MoveTowards(transform.position, GetComponent<ParabolaController>().Dots[DotCounter], FallingSpeed);
            // #endregion
            // #region DotPoint Control
            // if (Vector3.Distance(transform.position, GetComponent<ParabolaController>().Dots[DotCounter]) < .01f)
            // {
            //     if (DotCounter != GetComponent<ParabolaController>().Dots.Length - 1) { DotCounter++; }

            //     else
            //     {
            //         FallingOn = false;
            //         DotCounter = 1;
            //         Trajectory.SetActive(false);
            //         ResetSpeedValues();
            //     }

            //     FallingSpeed += .005f;
            // }
            //#endregion


        }
        #endregion
    }

    void ResetSpeedValues()
    {
        ForwardSpeed = DefForwardSpeed;
        FallingSpeed = DefFallingSpeed;
    }
    public void OnClickDown()
    {
        if (Fuel >= 0 && CanTap)

        {
            CanTap=false;
            rb.useGravity = false;
            particleControl.StartJetpackParticle();
            transform.DOLocalRotate(new Vector3(JetpackAngle, 0, 0), .5f);
            JetPackOn = true;
            FallingOn = false;
        }
    }
    public void OnClickUp()
    {
        particleControl.StopJetpackParticle();
        rb.useGravity = true;
        if (Fuel >= 0)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 0), .5f);
            JetPackOn = false;
            FallingOn = true;
        }
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
    void ClosePartice()
    {

    }
}
