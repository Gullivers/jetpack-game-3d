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
    [SerializeField] float dotSeparation, dotShift, dotShiftdotSeparation;
    [SerializeField] Transform[] Dots;
    [SerializeField] Transform TrajectoryLastPointer;
    [HideInInspector]
    public int LastDotIndex = 50;

    private void Awake()
    {
        for (int i = 0; i < 51; i++)
        {


            Transform temp = Instantiate(Sphere, this.transform).transform;
            temp.name = i.ToString();
        }
    }
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Dots[i] = transform.GetChild(i).transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (JectPack.JetPackOn)
        {
            for (int k = 0; k < transform.childCount; k++)
            {   //Each point of the trajectory will be given its position
                x1 = Player.position.z + (rb.velocity.z * Time.fixedDeltaTime * (dotSeparation * k + dotShift));    //X position for each point is found
                y1 = Player.position.y + (rb.velocity.y * Time.fixedDeltaTime * (dotSeparation * k + dotShift) - (-Physics2D.gravity.y / 2f * Time.fixedDeltaTime * Time.fixedDeltaTime * (dotShiftdotSeparation * k + dotShift) * (dotSeparation * k + dotShift)));  //Y position for each point is found
                Dots[k].position = new Vector3(0, y1, x1);  //Position is applied to each point
            }
            //transform.GetChild(LastDotIndex).GetComponent<MeshFilter>().mesh=PlaneMesh;
            TrajectoryLastPointer.position = transform.GetChild(LastDotIndex).position;
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
