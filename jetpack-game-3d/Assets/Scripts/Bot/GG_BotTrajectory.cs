using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_BotTrajectory : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform Player;
    [SerializeField] GG_BotJetpackMovement JectPack;
    [SerializeField] GG_BotTrigger BotTrigger;
    public Vector3 hitPoint;
    float x1, y1;

    float dotSeparation = 1, dotShift = 1, dotShiftdotSeparation = 1;

    public bool CanMove;

    void FixedUpdate()
    {
        if (CanMove)
        {
            RaycastHit hit;
            for (int k = 0; k < 200; k++)
            {   //Each point of the trajectory will be given its position
                x1 = Player.position.z + (rb.velocity.z * Time.fixedDeltaTime * (dotSeparation * k + dotShift));    //X position for each point is found
                y1 = Player.position.y + (rb.velocity.y * Time.fixedDeltaTime * (dotSeparation * k + dotShift) - (-Physics2D.gravity.y / 2f * Time.fixedDeltaTime * Time.fixedDeltaTime * (dotShiftdotSeparation * k + dotShift) * (dotSeparation * k + dotShift)));  //Y position for each point is found


                if (y1 < 0) { break; }

                if (Physics.Raycast(new Ray(new Vector3(Player.position.x, y1, x1), Vector3.forward), out hit, .6f))
                {
                    if ((hit.transform.tag == "Platform" || hit.transform.tag == "Finish")
                    && hit.transform != BotTrigger.LastTransform && BotTrigger.LastTransform != null)
                    {
                        hitPoint = hit.point;
                       // Debug.Log(hit.transform.tag);
                        StartCoroutine(JectPack.WaitForRandom());
                        break;
                    }

                }
                else if (Physics.Raycast(new Ray(new Vector3(Player.position.x, y1, x1), Vector3.down), out hit, .6f))
                {
                    if ((hit.transform.tag == "Platform" || hit.transform.tag == "Finish")
                    && hit.transform != BotTrigger.LastTransform && BotTrigger.LastTransform != null)
                    {
                        hitPoint = hit.point;
                       // Debug.Log(hit.transform.tag);
                        StartCoroutine(JectPack.WaitForRandom());
                        break;
                    }

                }
                else if (Physics.Raycast(new Ray(new Vector3(Player.position.x, y1, x1), Vector3.up), out hit, .6f))
                {
                    if ((hit.transform.tag == "Platform" || hit.transform.tag == "Finish")
                     && hit.transform != BotTrigger.LastTransform && BotTrigger.LastTransform != null)
                    {
                        hitPoint = hit.point;
                       // Debug.Log(hit.transform.tag);
                        StartCoroutine(JectPack.WaitForRandom());
                        break;
                    }

                }
            }

        }

    }
}


