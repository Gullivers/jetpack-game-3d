using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GG_LevelEndController : MonoBehaviour
{
    [SerializeField] Button LoseBtn, NextBtn;
    [SerializeField] GG_JPLevels LevelAsset;
    bool LoseDummy, WinDummy;


    public void Lose()
    {
        if (!WinDummy)
        {
            LoseDummy = true;
            LoseBtn.gameObject.SetActive(true);
            NextBtn.gameObject.SetActive(false);
        }
    }
    public void Win()
    {
        if (!LoseDummy)
        {
            WinDummy = true;
            LoseBtn.gameObject.SetActive(false);
            NextBtn.gameObject.SetActive(true);
            LevelAsset.PlayerLevel = LevelAsset.PlayerLevel + 1;
        }
    }
    public void ResetDummys()
    {
        LoseBtn.gameObject.SetActive(false);
        NextBtn.gameObject.SetActive(false);
        WinDummy = LoseDummy = false;
    }



}
