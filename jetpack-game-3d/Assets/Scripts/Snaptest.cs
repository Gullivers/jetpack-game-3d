using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snaptest : MonoBehaviour
{
    [SerializeField] Transform lastDot;
    MeshRenderer mesh;
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    void LateUpdate()
    {
        transform.position=lastDot.position;
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit))
        {
            transform.position = new Vector3(lastDot.position.x, hit.point.y+.5f, lastDot.position.z);
            transform.up = (hit.normal);
        }
      
    }
}
