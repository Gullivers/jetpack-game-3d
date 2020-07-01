using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCreator : MonoBehaviour
{
    [SerializeField] GameObject Plane;
    [SerializeField] GameObject Water;
    [SerializeField] GameObject[] Bots; //0 Easy /1 Middle /2 Hard
    [SerializeField] BotLevel[] HowManyBot;
  
    [SerializeField] int HowManyPlatform;
    Transform TempBot;
    float xpos;
    float BotxPos = 3f;

    void Awake()
    {
        CreateRoad();
        SpawnBots();

    }

    public void CreateRoad()
    {
        ClearChilds();
        xpos = 30;
        Transform tempWater = Instantiate(Water, this.transform).transform;
        tempWater.localScale = new Vector3(tempWater.localScale.x, tempWater.localScale.y, HowManyPlatform * 20);
        for (int i = 0; i < HowManyPlatform; i++)
        {
            Transform temp = Instantiate(Plane, new Vector3(0, Random.Range(-7f, 2f), xpos), Quaternion.identity).transform;
            temp.parent = this.transform;
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

   public void SpawnBots()
    {
        for (int i = 1; i < HowManyBot.Length+1; i++)
        {
            if (HowManyBot[i - 1] == BotLevel.Easy)
            {
                TempBot = Instantiate(Bots[0]).transform;

            }
            else if (HowManyBot[i - 1] == BotLevel.Middle)
            {
                TempBot = Instantiate(Bots[1]).transform;
            }
            else if (HowManyBot[i - 1] == BotLevel.Hard)
            {
                TempBot = Instantiate(Bots[2]).transform;
            }

            if(i%1==0){BotxPos=-BotxPos;}

            TempBot.transform.position = new Vector3(BotxPos, 3, 0);
            if (i % 2 == 0) { BotxPos += 3f; }
        }

    }
}

