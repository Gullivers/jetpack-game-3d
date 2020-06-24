using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snaptest : MonoBehaviour
{
    [SerializeField] Transform lastDot;
    MeshRenderer mesh;
    float yPos;
    [SerializeField] float lastPointMinY, lastPointMaxY;
    float RaycastDistance = 3f;
    Vector3 RayPos;
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    void FixedUpdate()
    {
        RayPos = new Vector3(transform.position.x, 50f, transform.position.z + .2f);
        transform.position = lastDot.position;
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, Vector3.forward), out hit, RaycastDistance))
        {  if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
            Debug.Log("Hit forward");
            float distance = Vector3.Distance(transform.position, hit.point);
            yPos = hit.point.y;
            yPos = Mathf.Clamp(yPos, lastPointMinY, lastPointMaxY);

            transform.position = new Vector3(lastDot.position.x, yPos + .01f, lastDot.position.z);
            transform.up = (hit.normal);
            }

        }

        else if (Physics.Raycast(new Ray(RayPos, Vector3.down), out hit, 50f))
        {
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                Debug.Log("Hit down");
                float distance = Vector3.Distance(transform.position, hit.point);
                yPos = hit.point.y + .1f;
                yPos = Mathf.Clamp(yPos, lastPointMinY, lastPointMaxY);

                transform.position = new Vector3(lastDot.position.x, yPos, lastDot.position.z);
                transform.up = (hit.normal);
            }
        }
        else if (Physics.Raycast(new Ray(transform.position, Vector3.up), out hit, RaycastDistance))
        {
             if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
            Debug.Log("Hit up");
            float distance = Vector3.Distance(transform.position, hit.point);
            yPos = hit.point.y + 1f;
            yPos = Mathf.Clamp(yPos, lastPointMinY, lastPointMaxY);

            transform.position = new Vector3(lastDot.position.x, yPos, lastDot.position.z);
            transform.up = (hit.normal);
            }

        }


        // else
        // {
        //     transform.position = new Vector3(transform.position.x, transform.position.y, lastDot.position.z);
        //     transform.rotation = Quaternion.EulerAngles(0, 0, 0);
        //     Debug.Log("Hit nothing");
        // }

    }
}
