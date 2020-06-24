using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snaptest : MonoBehaviour
{
    [SerializeField] Transform lastDot;
    MeshRenderer mesh;
    float yPos;
    [SerializeField] float lastPointMinY,lastPointMaxY;
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
            yPos=hit.point.y+.5f;
            yPos=Mathf.Clamp(yPos,lastPointMinY,lastPointMaxY);
            transform.position = new Vector3(lastDot.position.x,yPos, lastDot.position.z);
            transform.up = (hit.normal);
        }
        else if (Physics.Raycast(new Ray(transform.position, Vector3.up), out hit))
        {
            yPos=hit.point.y+1f;
            yPos=Mathf.Clamp(yPos,lastPointMinY,lastPointMaxY);
            transform.position = new Vector3(lastDot.position.x,yPos, lastDot.position.z);
            transform.up = (hit.normal);
        }
     
        else{
            transform.position = new Vector3(lastDot.position.x,0, lastDot.position.z);
        }
      
    }
}
