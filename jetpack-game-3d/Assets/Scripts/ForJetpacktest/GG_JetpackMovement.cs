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
    [SerializeField] Ease LangindEase;


    [HideInInspector]
    public bool JetPackOn = false;
    [HideInInspector]
    public bool FallingOn = false;
    bool InWater = false;
    [HideInInspector]
    public int DotCounter = 1;

    bool DummyJetPackOn = true;
    bool DummySoftlaunch = true;
    bool DummySoftlaunch2 = true;
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
    [SerializeField] Transform Parent;


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

        if (Fuel <= 0 && JetPackOn) { FuelIsEmpty(); particleControl.StopJetpackParticle(); } //Falling Down when fuel is Empty
        #region  JetPack is On
        if (JetPackOn)
        {
            #region  Shaking For JetpackisOn
            aAnimator.SetFloat("ShakingSpeed", aAnimator.GetFloat("ShakingSpeed") - .03f);
            if (aAnimator.GetFloat("ShakingSpeed") <= 0) { aAnimator.SetTrigger("JPoff"); }
            #endregion
            //Increase Xdegree 
            Xdegree += DegreeIncreser;
            Xdegree = Mathf.Clamp(Xdegree, -JetpackAngle, JetpackAngle);
            transform.rotation = Quaternion.Euler(Xdegree, transform.rotation.y, transform.rotation.z);
            if (DummyJetPackOn) //Dummy just once when Jetpack is on
            {
                aAnimator.SetFloat("ShakingSpeed", 4f);
                aAnimator.SetTrigger("JPon");
                CanFillFuel = false;
                particleControl.StartJetpackParticle();
                DummyFalling = DummySoftlaunch = true;
                DummySoftlaunch2 = true;
                DummyJetPackOn = false;
                InWater = false;

            }

            Fuel -= .1f;
            
            rb.AddForce(Vector3.up * UpSpeed);
            rb.AddForce(Vector3.forward * ForwardSpeed);

        }
        #endregion
        #region  Jetpack is Off
        else if (FallingOn)

        {

            if (PointerTrajectory.localPosition.y <= -14 && !InWater) { InWater = true; } //Detect pointer in Water
            if (DummySoftlaunch2) // Dummy For Xdegree when Falling 
            {
                Xdegree -= DegreeIncreser;
                Xdegree = Mathf.Clamp(Xdegree, -JetpackAngle, JetpackAngle);
                transform.rotation = Quaternion.Euler(Xdegree, 0, 0);
            }
            #region  Fuelpart
            Fuel += FuelRegeneration;
            Fuel = Mathf.Clamp(Fuel, 0f, FuelForStart);
            // if (CanFillFuel) { Fuel += FuelRegeneration; }
            #endregion
            if (DummyFalling) //For enable gravity just once
            {
                DummyJetPackOn = true;
                rb.useGravity = true;
                //transform.DOLocalRotate(new Vector3(0, 0, 0), .5f);
                DummyFalling = false;
            }

            //SoftLanding Part and Dummy for just onces
            if (Vector3.Distance(transform.position, PointerTrajectory.position) < SoftlaunchMax && Vector3.Distance(transform.position, PointerTrajectory.position) > SoftlaunchMin
             && DummySoftlaunch && !InWater)
            {
                aAnimator.ResetTrigger("JPoff");
                aAnimator.SetFloat("ShakingSpeed", 5f);

                Debug.Log("Softlaunch");
                Debug.Log(PointerTrajectory.position + "  PointerPos");
                DummySoftlaunch = false;
                //In SotfLanding when distances is ok
                if (transform.position.y - PointerTrajectory.position.y > 4f && transform.position.z - PointerTrajectory.position.z < 2f && DummySoftlaunch2)
                {
                    DummySoftlaunch2 = false;
                    //aAnimator.SetTrigger("Soft_Landing");
                    rb.useGravity = false;
                    rb.velocity = Vector3.zero;
                    SoftlandingTween();


                }
                particleControl.StartJetpackParticle();

            }
            else
            {
                //Falling Water Part
            }
            
            #region  Shaking off When falling Down
            aAnimator.SetFloat("ShakingSpeed", aAnimator.GetFloat("ShakingSpeed") - .05f);
            if (aAnimator.GetFloat("ShakingSpeed") <= 0) { aAnimator.SetTrigger("JPoff"); }
            #endregion
           
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
    void SoftlandingTween()
    {   //Y Z pos 
        //Y pos
        //transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.DOMoveY(PointerTrajectory.position.y, SoftLaunchDuration).SetEase(LaunchEase).SetId("Softlaunch");
        //Z pos
        Sequence Zsq = DOTween.Sequence();

        Zsq.Append(transform.DOMoveZ(PointerTrajectory.position.z + .5f, SoftLaunchDuration / 2f).SetEase(LaunchEase))
        .Append(transform.DOMoveZ(PointerTrajectory.position.z, SoftLaunchDuration / 4f).SetEase(LaunchEase));
        #region OldXpos
        //.Append(transform.DOMoveZ(PointerTrajectory.position.z, SoftLaunchDuration / 4f).SetEase(LaunchEase)); //.SetId("Softlaunch");
        // //X pos 
        // Sequence XSq = DOTween.Sequence();
        // XSq.Append(transform.DOMoveX(transform.position.x + 1f, .3f).SetEase(LangindEase))
        // .Append(transform.DOMoveX(transform.position.x - 1f, .6f).SetEase(LangindEase))
        // .Append(transform.DOMoveX(transform.position.x, .3f).SetEase(LangindEase));
        #endregion
        //Rotate Z X
        Sequence RSq = DOTween.Sequence();
        RSq.Append(transform.DOLocalRotate(new Vector3(transform.rotation.x - 10, transform.rotation.y, transform.rotation.z + 15), .2f).SetEase(LangindEase))
        .Append(transform.DOLocalRotate(new Vector3(transform.rotation.x - 20, transform.rotation.y, transform.rotation.z), .3f).SetEase(LangindEase))
        .Append(transform.DOLocalRotate(new Vector3(transform.rotation.x - 10, transform.rotation.y, transform.rotation.z - 15), .3f).SetEase(LangindEase))
        .Append(transform.DOLocalRotate(new Vector3(0, transform.rotation.y, transform.rotation.z), .2f).SetEase(LangindEase));
        #region  SecondRotate
        //  Sequence RSq = DOTween.Sequence();
        // RSq.Append(transform.DOLocalRotate(new Vector3( - 25, transform.rotation.y,+5), .6f).SetEase(LangindEase))
        // .Append(transform.DOLocalRotate(new Vector3(-30, transform.rotation.y, 0), .2f).SetEase(LangindEase))
        // .Append(transform.DOLocalRotate(new Vector3(-20, transform.rotation.y,-5), .4f).SetEase(LangindEase))
        // .Append(transform.DOLocalRotate(new Vector3(0, transform.rotation.y,0), .6f).SetEase(LangindEase));
        #endregion
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
