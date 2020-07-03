using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_TrajectoryWithPhysics : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform Player;
    [SerializeField] GG_JetpackMovement JectPack;
    [SerializeField] GameObject Sphere;
    float x1, y1;
    int UnusedDotsIndex;
    [SerializeField] float dotSeparation, dotShift, dotShiftdotSeparation;
    [SerializeField] Transform[] Dots;
    [SerializeField] Transform TrajectoryLastPointer;
   

    void FixedUpdate()
    {
     
        if (JectPack.JetPackOn)
        {
            RaycastHit hit;
            for (int k = 0; k < transform.childCount; k++)
            {   //Each point of the trajectory will be given its position
                x1 = Player.position.z + (rb.velocity.z * Time.fixedDeltaTime * (dotSeparation * k + dotShift));    //X position for each point is found
                y1 = Player.position.y + (rb.velocity.y * Time.fixedDeltaTime * (dotSeparation * k + dotShift) - (-Physics2D.gravity.y / 2f * Time.fixedDeltaTime * Time.fixedDeltaTime * (dotShiftdotSeparation * k + dotShift) * (dotSeparation * k + dotShift)));  //Y position for each point is found


                if (y1 < 0) { break; }

                Dots[k].position = new Vector3(0, y1, x1);  //Position is applied to each point
            
                if (Physics.Raycast(new Ray(new Vector3(0, y1, x1), Vector3.forward), out hit, .6f))
                {
                    if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Player") && hit.transform.gameObject.layer != LayerMask.NameToLayer("Trajectory"))
                    {
                       
                        TrajectoryLastPointer.position = hit.point;
                        TrajectoryLastPointer.transform.up = hit.normal;
                        break;
                    }

                }
                else if (Physics.Raycast(new Ray(new Vector3(0, y1, x1), Vector3.down), out hit, .6f))
                {
                    if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Player") && hit.transform.gameObject.layer != LayerMask.NameToLayer("Trajectory"))
                    {
                        TrajectoryLastPointer.position = hit.point;
                        TrajectoryLastPointer.transform.up = hit.normal;
                        
                      
                        break;
                    }

                }
                else if (Physics.Raycast(new Ray(new Vector3(0, y1, x1), Vector3.up), out hit, .6f))
                {
                    if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Player") && hit.transform.gameObject.layer != LayerMask.NameToLayer("Trajectory"))
                    {
                        TrajectoryLastPointer.position = hit.point;
                        TrajectoryLastPointer.transform.up = -hit.normal;
                
                        break;
                    }

                }
            }
            // for (int i = UnusedDotsIndex; i < Dots.Length; i++)
            // {
            //     Dots[i].position = Vector3.zero;
            // }
            //transform.GetChild(LastDotIndex).GetComponent<MeshFilter>().mesh=PlaneMesh;
            //TrajectoryLastPointer.position = transform.GetChild(LastDotIndex).position;
            // for (int i =LastDot; i < 50; i++)
            // {
            //     Dots[i].gameObject.SetActive(false);
            // }
            // for (int i = 0; i < LastDot; i++)
            // {
            //    Dots[i].gameObject.SetActive(true); 
            // }
        }

    }
}
