using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GG_CanvasLevelEndController : MonoBehaviour
{
    [SerializeField] Button LoseBtn, NextBtn;
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
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel")+1);
        }
    }
    public void ResetDummys()
    {
        LoseBtn.gameObject.SetActive(false);
        NextBtn.gameObject.SetActive(false);
        WinDummy = LoseDummy = false;
    }

}
