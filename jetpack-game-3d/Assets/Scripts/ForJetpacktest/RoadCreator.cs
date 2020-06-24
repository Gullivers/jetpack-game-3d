using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCreator : MonoBehaviour
{
    [SerializeField] GameObject Plane;
    [SerializeField] GameObject Water;
    float xpos = 60;
    void Awake()
    {   
        // Transform tempWater=Instantiate(Water,this.transform).transform;
        // tempWater.localScale=new Vector3(tempWater.localScale.x, tempWater.localScale.y,50);
        for (int i = 0; i < 25; i++)
        {
            Transform temp = Instantiate(Plane, new Vector3(0, -1.3f, xpos), Quaternion.identity).transform;
            xpos += 140;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
