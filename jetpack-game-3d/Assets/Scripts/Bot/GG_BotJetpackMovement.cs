using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GG_BotJetpackMovement : MonoBehaviour
{
    GG_ParticleControl particleControl;
    [SerializeField] GG_BotTrajectory botTrajectory;
    #region  SpeedSettings
    [Header("Speed Settings")]
    [SerializeField] float ForwardSpeed;
    [SerializeField] float UpSpeed;
    [SerializeField] float JetpackAngle;
    #endregion
    #region  SoftLandSettings
    [Header("Softland Settings")]
    [SerializeField] float SoftlaunchMin;
    [SerializeField] float SoftlaunchMax;
    [SerializeField] float SoftLaunchDuration;
    [SerializeField] Ease LaunchEase;
    [SerializeField] Ease LangindEase;
    #endregion
    #region Random Values
    float MinDuration, maxDuration;
    [HideInInspector]
    [SerializeField] float WaitTime;

    #endregion

    #region Dummys
    bool DummyJetPackOn = true;
    bool DummySoftlaunch = true;

    bool DummySoftlaunch2 = true;
    bool DummyFalling = true;
    bool InWater = false;
    #endregion
    #region  BotSettings
    [Header("Bot Settings")]
    [SerializeField] BotLevel level;
    [HideInInspector]
    public int LoseTry;
    [HideInInspector]
    public bool MadedPass;

    [SerializeField] int TryLimit;
    #endregion
    Rigidbody rb;

    private void Awake()
    {
        SetLevel();

        rb = GetComponent<Rigidbody>();
        particleControl = GetComponent<GG_ParticleControl>();

    }
    void FixedUpdate()
    {
        if (LoseTry > TryLimit) { MakeItPassLevel(); }
        #region  JetPack is On
        if (botTrajectory.CanMove)
        {
            if (DummyJetPackOn)
            {
                particleControl.StartJetpackParticle();
                DummyFalling = DummySoftlaunch = DummySoftlaunch2 = true;
                DummyJetPackOn = false;
                transform.DOLocalRotate(new Vector3(JetpackAngle, 0, 0), .5f).SetId("FallingAngle");
                InWater = false;

            }

            rb.AddForce(Vector3.up * UpSpeed);
            rb.AddForce(Vector3.forward * ForwardSpeed);

        }
        #endregion
        #region  Jetpack is Off
        else
        {
            if (botTrajectory.hitPoint.y <= -14 && !InWater) { InWater = true; }
            if (DummyFalling)
            {
                DummyJetPackOn = true;
                rb.useGravity = true;
                transform.DOLocalRotate(new Vector3(0, 0, 0), .5f).SetId("FallingAngle" + transform.name);
                DummyFalling = false;

            }

            if (Vector3.Distance(transform.position, botTrajectory.hitPoint) > SoftlaunchMin
             && Vector3.Distance(transform.position, botTrajectory.hitPoint) < SoftlaunchMax
             && DummySoftlaunch && !InWater)
            {
                DummySoftlaunch = false;
                if (transform.position.y - botTrajectory.hitPoint.y > 4f
                && transform.position.z - botTrajectory.hitPoint.z < 2f
                && DummySoftlaunch2)
                {
                    DummySoftlaunch2 = false;

                    rb.useGravity = false;
                    rb.velocity = Vector3.zero;
                    SoftlandingTween();


                }
                particleControl.StartJetpackParticle();
                transform.DOLocalRotate(new Vector3(-JetpackAngle, 0, 0), .5f).SetId("FallingAngle" + transform.name);

            }

        }
        #endregion
    }

    public IEnumerator WaitForRandom()
    {
        yield return new WaitForSeconds(WaitTime);
        botTrajectory.CanMove = false;
    }

    public void SetLevel()
    {
        MadedPass = false;  //Resetting Values
        LoseTry = 0;
        switch (level)
        {
            case BotLevel.Easy:
                MinDuration = .2f; maxDuration = 1f;
                WaitTime = Random.Range(0, 2) == 0 ? Random.Range(0, MinDuration) : Random.Range(.6f, maxDuration);

                break;
            case BotLevel.Middle:

                MinDuration = .3f; maxDuration = 1.3f;
                WaitTime = Random.Range(0, 2) == 0 ? Random.Range(0, MinDuration) : Random.Range(.4f, maxDuration);

                break;
            case BotLevel.Hard:
                MinDuration = .2f; maxDuration = .5f;
                WaitTime = Random.Range(MinDuration, maxDuration);
                break;


        }
    }
    public void SetWaitTime()
    {
        switch (level)
        {
            case BotLevel.Easy:
                MinDuration = .2f; maxDuration = 1f;
                WaitTime = Random.Range(0, 2) == 0 ? Random.Range(0, MinDuration) : Random.Range(.6f, maxDuration);

                break;
            case BotLevel.Middle:

                MinDuration = .3f; maxDuration = 1.3f;
                WaitTime = Random.Range(0, 2) == 0 ? Random.Range(0, MinDuration) : Random.Range(.4f, maxDuration);

                break;
            case BotLevel.Hard:
                MinDuration = .2f; maxDuration = .5f;
                WaitTime = Random.Range(MinDuration, maxDuration);
                break;

        }

    }
    void MakeItPassLevel() { MinDuration = .2f; maxDuration = .5f; MadedPass = true; }

    void SoftlandingTween()
    {
        //Y pos
        transform.DOMoveY(botTrajectory.hitPoint.y, SoftLaunchDuration).SetEase(LaunchEase).SetId("Softlaunch" + this.transform.name);
        //Z pos
        Sequence Zsq = DOTween.Sequence();

        Zsq.Append(transform.DOMoveZ(botTrajectory.hitPoint.z + .5f, SoftLaunchDuration / 2f).SetEase(LaunchEase))
        .Append(transform.DOMoveZ(botTrajectory.hitPoint.z, SoftLaunchDuration / 4f).SetEase(LaunchEase));

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

}
public enum BotLevel
{
    Easy, Middle, Hard
}



#region  OldCodes
// void ResetSpeedValues()
// {
//     ForwardSpeed = DefForwardSpeed;

// }
// public void OnClickDown()
// {
//     if (Fuel >= 0 && CanTap)
//     {
//         //rb.useGravity = false;

//         //transform.DOLocalRotate(new Vector3(JetpackAngle, 0, 0), .5f);
//         JetPackOn = true;
//         FallingOn = false;

//     }
// }
// public void OnClickUp()
// {

//     if (CanTap) { particleControl.StopJetpackParticle(); }
//     if (!DummySoftlaunch) { particleControl.StartJetpackParticle(); }
//     rb.useGravity = true;
//     if (Fuel >= 0 && CanTap)
//     {

//         JetPackOn = false;
//         FallingOn = true;
//     }
//     CanTap = false;

// }

// void FuelIsEmpty()
// {
//     if (JetPackOn)
//     {
//         rb.useGravity = true;
//         JetPackOn = false;
//         FallingOn = true;
//     }
// }
#endregion

