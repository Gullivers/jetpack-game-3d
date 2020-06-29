using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GG_BotJetpackMovement : MonoBehaviour
{
    GG_ParticleControl particleControl;
    [SerializeField] GG_BotTrajectory botTrajectory;
    [Header("Speed Settings")]
    public float ForwardSpeed;
    public float  UpSpeed;
    [SerializeField] float JetpackAngle;
    [SerializeField] float SoftlaunchMin;
    float MinDuration, maxDuration;




    bool DummyJetPackOn = true;
    bool DummySoftlaunch = true;
    bool DummyFalling = true;

    [HideInInspector]
    public int LoseTry;
    [HideInInspector]
    public bool MadedPass;

    Rigidbody rb;

    [Header("Bot Settings")]
    [SerializeField] BotLevel level;
    [SerializeField] int TryLimit;

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
                DummyFalling = DummySoftlaunch = true;
                DummyJetPackOn = false;
                transform.DOLocalRotate(new Vector3(JetpackAngle, 0, 0), .5f);
            }

            rb.AddForce(Vector3.up * UpSpeed);
            rb.AddForce(Vector3.forward * ForwardSpeed);

        }
        #endregion
        #region  Jetpack is Off
        else
        {

            if (DummyFalling)
            {
                DummyJetPackOn = true;
                rb.useGravity = true;
                transform.DOLocalRotate(new Vector3(0, 0, 0), .5f).SetId("FallingAngle");
                DummyFalling = false;

            }

            if (Vector3.Distance(transform.position, botTrajectory.hitPoint) > SoftlaunchMin && DummySoftlaunch)
            {
                DummySoftlaunch = false;
                particleControl.StartJetpackParticle();
                transform.DOLocalRotate(new Vector3(-JetpackAngle, 0, 0), .5f);

            }

        }
        #endregion
    }

    public IEnumerator WaitForRandom()
    {
        yield return new WaitForSeconds(Random.Range(MinDuration, maxDuration));
        botTrajectory.CanMove = false;
    }

    public void SetLevel()
    {
        MadedPass = false;
        LoseTry = 0;
        switch (level)
        {
            case BotLevel.Easy:
                MinDuration = .5f; maxDuration = 1.5f;
                break;
            case BotLevel.Middle:
                MinDuration = .4f; maxDuration = 1.3f;
                break;
            case BotLevel.Hard:
                MinDuration = .2f; maxDuration = .5f;
                break;


        }
    }
    void MakeItPassLevel() { MinDuration = .2f; maxDuration = .5f; MadedPass = true; }
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

