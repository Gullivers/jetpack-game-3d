using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCreator : MonoBehaviour
{
    [SerializeField] GameObject Plane;
    [SerializeField] GameObject Water;
    [SerializeField] int HowManyPlatform;
    float xpos;
    void Awake()
    {
        CreateRoad();

    }

    public void CreateRoad()
    {
        ClearChilds();
        xpos=30;
        Transform tempWater = Instantiate(Water,this.transform).transform;
        tempWater.localScale = new Vector3(tempWater.localScale.x, tempWater.localScale.y, HowManyPlatform * 20);
        for (int i = 0; i < HowManyPlatform; i++)
        {
            Transform temp = Instantiate(Plane, new Vector3(0, Random.Range(-7f, 2f), xpos), Quaternion.identity).transform;
            temp.parent=this.transform;
            if (i == HowManyPlatform - 1) { temp.tag = "Finish"; }
            xpos += 50;
        }
    }
    void ClearChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


}
