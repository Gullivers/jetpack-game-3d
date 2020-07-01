using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_PlayerParentFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform Child;
    Vector3 Distance;
    void Awake()
    {
        Distance=transform.position-Child.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Child.position+Distance;
    }
}
