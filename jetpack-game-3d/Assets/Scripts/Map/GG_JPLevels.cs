using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JP Level", menuName = "GG_Database/LevelAsset")]
public class GG_JPLevels : ScriptableObject
{   
    public int PlayerLevel;
    public List<LevelInfo> Levels=new List<LevelInfo>();
}
[System.Serializable]
public class LevelInfo
{
    public int HowManyPlatform;
    public BotLevel[] Bots;


}



