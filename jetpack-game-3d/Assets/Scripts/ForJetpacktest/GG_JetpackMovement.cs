using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_JetpackMovement : MonoBehaviour
{
    public float ForwardSpeed, UpSpeed;
    [SerializeField] float FallingSpeed = .5f;
    [SerializeField] float Forwardacceleration;
    public bool JetPackOn = false;
    public bool FallingOn = false;
    int DotCounter = 1;
    

 
    void FixedUpdate()
    {   
        #region  JetPack is On
        if (JetPackOn)
        {
            transform.Translate(Vector3.up * UpSpeed * Time.deltaTime, Space.World);
            transform.Translate(Vector3.forward * ForwardSpeed * Time.deltaTime, Space.World);
            ForwardSpeed += Forwardacceleration;
        }
        #endregion
        #region  Jetpack is Off
        else if (FallingOn)
        {
            #region DotPoint Control
            if (Vector3.Distance(transform.position, GetComponent<ParabolaController>().Dots[DotCounter]) < .1f)
            {
                if (DotCounter != GetComponent<ParabolaController>().Dots.Length - 1) { DotCounter++; }

                else
                {
                    FallingOn = false;
                    DotCounter = 1;
                }

                FallingSpeed += .005f;
            }
            #endregion
            #region Following parabola Dots
            transform.position = Vector3.MoveTowards(transform.position,GetComponent<ParabolaController>().Dots[DotCounter], FallingSpeed);
            #endregion

        }
        #endregion
    }


    private void OnMouseDown()
    {
        JetPackOn = true;
        FallingOn = false;
    }
    private void OnMouseUp()
    {
        JetPackOn = false;
        FallingOn = true;
    }
}
