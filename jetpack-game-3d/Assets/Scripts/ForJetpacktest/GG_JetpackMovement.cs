using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GG_JetpackMovement : MonoBehaviour
{

    GG_ParticleControl particleControl;
    [SerializeField] GameObject Trajectory;
    [SerializeField] Transform PointerTrajectory;
    Rigidbody rb;
    Animator aAnimator;

    #region Speed
    [Header("Speed Elements")]
    public float ForwardSpeed;
    public float UpSpeed;
    #endregion
    //[SerializeField] float FallingSpeed = .5f;
    #region  LaunchDegree
    [Header("Degree Elements")]
    [SerializeField] float JetpackAngle;
    [HideInInspector]
    public float Xdegree;
    [SerializeField] float DegreeIncreser;
    #endregion
    #region  SoftLands
    [Header("SoftLand Elements")]
    [SerializeField] float SoftlaunchMin;
    [SerializeField] float SoftlaunchMax;
    [SerializeField] float SoftLaunchDuration;
    [SerializeField] Ease LaunchEase;
    [SerializeField] Ease LangindEase;
    bool InWater = false;
    #endregion

    #region JetPack switch values
    [HideInInspector]
    public bool JetPackOn = false;
    [HideInInspector]
    public bool FallingOn = false;
    public bool CanTap = true;
    #endregion

    #region Dummys
    bool DummyJetPackOn = true;
    bool DummySoftlaunch = true;
    bool DummySoftlaunch2 = true;
    bool DummyFalling = true;
    #endregion



    #region FuelValues
    [Header("Fuel Elements")]
    public float Fuel;
    [HideInInspector]
    public bool CanFillFuel = false;

    [HideInInspector]
    public float FuelForStart;
    [SerializeField] float FuelRegeneration;
    #endregion



    private void Awake()
    {
        aAnimator = GetComponent<Animator>();
        FuelForStart = Fuel;
        rb = GetComponent<Rigidbody>();
        particleControl = GetComponent<GG_ParticleControl>();

    }
    void FixedUpdate()
    {

        if (Fuel <= 0 && JetPackOn) { FuelIsEmpty(); particleControl.StopJetpackParticle(); } // Falling Down when fuel is Empty
        #region  JetPack is On
        if (JetPackOn)
        {
            #region  Shaking For JetpackisOn
            aAnimator.SetFloat("ShakingSpeed", aAnimator.GetFloat("ShakingSpeed") - .03f);
            if (aAnimator.GetFloat("ShakingSpeed") <= 0) { aAnimator.SetTrigger("JPoff"); }
            #endregion
            #region  Increase Xdegree  when going up
            Xdegree += DegreeIncreser;
            Xdegree = Mathf.Clamp(Xdegree, -JetpackAngle, JetpackAngle);
            transform.rotation = Quaternion.Euler(Xdegree, transform.rotation.y, transform.rotation.z);
            #endregion
            if (DummyJetPackOn) //Dummy just once when Jetpack is on
            {
                aAnimator.SetFloat("ShakingSpeed", 4f);  //Setting animation speed 
                aAnimator.SetTrigger("JPon");
                CanFillFuel = false;        //Cant fill fuel when going up
                particleControl.StartJetpackParticle(); //Jetpack Particle 
                DummySoftlaunch2 = DummyFalling = DummySoftlaunch = true;  //Setting Dummys for FallingOn

                DummyJetPackOn = false; // Setting dummy to false 
                InWater = false;

            }

            Fuel -= .1f; // Increase Fuel each frame

            //Adding force to land
            rb.AddForce(Vector3.up * UpSpeed);
            rb.AddForce(Vector3.forward * ForwardSpeed);

        }
        #endregion
        #region  Jetpack is Off
        else if (FallingOn)

        {

            if (PointerTrajectory.localPosition.y <= -14 && !InWater) { InWater = true; } //Detect pointer in Water
            if (DummySoftlaunch2) // Increase X degree 
            {
                Xdegree -= DegreeIncreser;
                Xdegree = Mathf.Clamp(Xdegree, -JetpackAngle, JetpackAngle);
                transform.rotation = Quaternion.Euler(Xdegree, 0, 0);
            }
            #region  Fuelpart
            //Regenerate Fuel when Jetpack is off
            Fuel += FuelRegeneration;
            Fuel = Mathf.Clamp(Fuel, 0f, FuelForStart);

            #endregion
            if (DummyFalling) //For enable gravity just once
            {
                DummyJetPackOn = true;  //Setting Dummy for JetpackOn
                rb.useGravity = true;
                DummyFalling = false;
            }

            //SoftLanding Part and Dummy for just once  // Eğer mesafeler uyar ve PointerTrajectory suda değilse softLanding kısmına girecek
            if (Vector3.Distance(transform.position, PointerTrajectory.position) < SoftlaunchMax 
            && Vector3.Distance(transform.position, PointerTrajectory.position) > SoftlaunchMin
             && DummySoftlaunch && !InWater)
            {

                aAnimator.ResetTrigger("JPoff");        //Resetting animator trigger
                aAnimator.SetFloat("ShakingSpeed", 4f);

                Debug.Log("Softlaunch");
                Debug.Log(PointerTrajectory.position + "  PointerPos");
                DummySoftlaunch = false;
                //In SotfLanding when Y distances is ok
                if (transform.position.y - PointerTrajectory.position.y > 4f 
                && transform.position.z - PointerTrajectory.position.z < 2f 
                && DummySoftlaunch2)
                {
                    DummySoftlaunch2 = false; //Dummy for Softlanding
                    rb.useGravity = false;
                    rb.velocity = Vector3.zero;
                    SoftlandingTween();  //Tweens for Softland
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


    public void OnClickDown()
    {
        if (Fuel >= 0 && CanTap)
        {
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
    {
        //Y pos
        transform.DOMoveY(PointerTrajectory.position.y, SoftLaunchDuration).SetEase(LaunchEase).SetId("Softlaunch" + this.transform.name);
        //Z pos
        Sequence Zsq = DOTween.Sequence();
        Zsq.Append(transform.DOMoveZ(PointerTrajectory.position.z + .5f, SoftLaunchDuration / 2f).SetEase(LaunchEase))
       .Append(transform.DOMoveZ(PointerTrajectory.position.z, SoftLaunchDuration / 4f).SetEase(LaunchEase));

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
        #region OldXpos
        //.Append(transform.DOMoveZ(PointerTrajectory.position.z, SoftLaunchDuration / 4f).SetEase(LaunchEase)); //.SetId("Softlaunch");
        // //X pos 
        // Sequence XSq = DOTween.Sequence();
        // XSq.Append(transform.DOMoveX(transform.position.x + 1f, .3f).SetEase(LangindEase))
        // .Append(transform.DOMoveX(transform.position.x - 1f, .6f).SetEase(LangindEase))
        // .Append(transform.DOMoveX(transform.position.x, .3f).SetEase(LangindEase));
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
