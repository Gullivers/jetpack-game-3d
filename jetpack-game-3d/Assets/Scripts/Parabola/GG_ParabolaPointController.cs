using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_ParabolaPointController : MonoBehaviour
{
    [SerializeField] Transform StartPoint, MiddlePoint, EndPoint, Player;
    float SpeedZ, SpeedY, MiddlePointZ;
    [SerializeField] float EndPointZmultipler;
    void Start()
    {
        SpeedY = Player.GetComponent<GG_JetpackMovement>().UpSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        SpeedZ = Player.GetComponent<GG_JetpackMovement>().ForwardSpeed;
        StartPoint.position = Player.position;
        EndPoint.position = new Vector3(Player.position.x, 0, Player.position.z + (SpeedZ * EndPointZmultipler));
        MiddlePointZ = Player.position.z+((EndPoint.position.z - Player.position.z)/2);
        MiddlePoint.position = new Vector3(Player.position.x, (Player.position.y + 5f), MiddlePointZ);

    }
}
