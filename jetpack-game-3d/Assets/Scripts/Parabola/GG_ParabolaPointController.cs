using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_ParabolaPointController : MonoBehaviour
{
    [SerializeField] Transform StartPoint, MiddlePoint, EndPoint, Player;
    float SpeedZ, SpeedY, MiddlePointZ, MiddlePointY;
    [SerializeField] float EndPointZmultipler;
    void Start()
    {
        SpeedY = Player.GetComponent<GG_JetpackMovement>().UpSpeed;

    }

   
    void LateUpdate()
    {
        if (!Player.GetComponent<GG_JetpackMovement>().FallingOn)
        {
            SpeedZ = Player.GetComponent<GG_JetpackMovement>().ForwardSpeed;
            StartPoint.position = Player.position;
            EndPoint.position = new Vector3(Player.position.x,  EndPoint.position.y, Player.position.z + (SpeedZ * EndPointZmultipler));
            MiddlePointZ = Player.position.z + ((EndPoint.position.z - Player.position.z) / 2);
            MiddlePointY = Player.position.y - (((Player.position.y - EndPoint.position.y)*15)/100);
            MiddlePoint.position = new Vector3(Player.position.x,MiddlePointY, MiddlePointZ);
        }
    }
}
