using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_JetpackMovement : MonoBehaviour
{
    public float ForwardSpeed, UpSpeed;
    [SerializeField] float Forwardacceleration;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * UpSpeed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.forward * ForwardSpeed * Time.deltaTime, Space.World);
        ForwardSpeed += Forwardacceleration;
    }
}
