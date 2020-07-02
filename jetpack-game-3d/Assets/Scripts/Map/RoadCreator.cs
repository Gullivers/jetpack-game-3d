using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCreator : MonoBehaviour
{
    [SerializeField] GG_JPLevels LevelAsset;
    [SerializeField] GameObject Plane;
    [SerializeField] GameObject Water;
    [SerializeField] GameObject[] Bots; //0 Easy /1 Middle /2 Hard


    int HowManyPlatform;
    int PlayerLevel;
    Transform TempBot;
    float zpos;
    float BotxPos = 3f;

    void Awake()
    {
        //PlayerLevel = LevelAsset.PlayerLevel;
        //HowManyPlatform = LevelAsset.Levels[PlayerLevel].HowManyPlatform;
        CreateRoad();
        SpawnBots();
        Debug.Log(PlayerPrefs.GetInt("PlayerLevel"));


    }

    public void CreateRoad()
    {
        PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
        HowManyPlatform = LevelAsset.Levels[PlayerLevel].HowManyPlatform;
        ClearChilds();
        zpos = 30;
        Transform tempWater = Instantiate(Water, this.transform).transform;
        tempWater.localScale = new Vector3(tempWater.localScale.x, tempWater.localScale.y, HowManyPlatform * 20);
        for (int i = 0; i < HowManyPlatform; i++)
        {
            Transform temp = Instantiate(Plane, new Vector3(0, Random.Range(-7f, -1f), zpos), Quaternion.identity).transform;
            temp.parent = this.transform;
            if (i == HowManyPlatform - 1) { temp.tag = "Finish"; }
            zpos += 50;
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
        BotxPos = 3f;
        PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
        for (int i = 1; i < LevelAsset.Levels[PlayerLevel].Bots.Length + 1; i++)
        {
            if (LevelAsset.Levels[PlayerLevel].Bots[i - 1] == BotLevel.Easy)
            {
                TempBot = Instantiate(Bots[0]).transform;

            }
            else if (LevelAsset.Levels[PlayerLevel].Bots[i - 1] == BotLevel.Middle)
            {
                TempBot = Instantiate(Bots[1]).transform;
            }
            else if (LevelAsset.Levels[PlayerLevel].Bots[i - 1] == BotLevel.Hard)
            {
                TempBot = Instantiate(Bots[2]).transform;
            }

            BotxPos = -BotxPos;

            TempBot.transform.position = new Vector3(BotxPos, 3, 0);
            if (i % 2 == 0) { BotxPos += 3f; }
        }

    }
}

