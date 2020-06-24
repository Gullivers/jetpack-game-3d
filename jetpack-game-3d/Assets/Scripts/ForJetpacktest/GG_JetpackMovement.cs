using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_JetpackMovement : MonoBehaviour
{
    [SerializeField] GameObject Trajectory;
    public float ForwardSpeed, UpSpeed;
    float DefForwardSpeed, DefFallingSpeed;
    [SerializeField] float FallingSpeed = .5f;
    [SerializeField] float Forwardacceleration;
    public bool JetPackOn = false;
    public bool FallingOn = false;
    int DotCounter = 1;
    bool SetDotDummy = true;
    public float Fuel;


    private void Awake()
    {
        DefForwardSpeed = ForwardSpeed;
        DefFallingSpeed = FallingSpeed;
    }
    void FixedUpdate()
    {
        if (Fuel <= 0 && JetPackOn) { FuelIsEmpty(); }
        #region  JetPack is On
        if (JetPackOn)
        {

            Trajectory.SetActive(true);
            if (!SetDotDummy) { SetDotDummy = true; }
            Fuel -= .1f;
            transform.Translate(Vector3.up * UpSpeed * Time.deltaTime, Space.World);
            transform.Translate(Vector3.forward * ForwardSpeed * Time.deltaTime, Space.World);
            ForwardSpeed += Forwardacceleration;
        }
        #endregion
        #region  Jetpack is Off
        else if (FallingOn)
        {

            if (SetDotDummy) { GetComponent<ParabolaController>().SetDots(); SetDotDummy = false; }
            #region Following parabola Dots
            transform.position = Vector3.MoveTowards(transform.position, GetComponent<ParabolaController>().Dots[DotCounter], FallingSpeed);
            #endregion
            #region DotPoint Control
            if (Vector3.Distance(transform.position, GetComponent<ParabolaController>().Dots[DotCounter]) < .01f)
            {
                if (DotCounter != GetComponent<ParabolaController>().Dots.Length - 1) { DotCounter++; }

                else
                {
                    FallingOn = false;
                    DotCounter = 1;
                    Trajectory.SetActive(false);
                    ResetSpeedValues();
                }

                FallingSpeed += .005f;
            }
            #endregion


        }
        #endregion
    }

    void ResetSpeedValues()
    {
        ForwardSpeed = DefForwardSpeed;
        FallingSpeed = DefFallingSpeed;
    }
    private void OnMouseDown()
    {
        if (Fuel >= 0)
        {
            JetPackOn = true;
            FallingOn = false;
        }
    }
    private void OnMouseUp()
    {
        if (Fuel >= 0)
        {
            JetPackOn = false;
            FallingOn = true;
        }
    }

    void FuelIsEmpty()
    {
        if (JetPackOn)
        {
            JetPackOn = false;
            FallingOn = true;
        }

    }
}
