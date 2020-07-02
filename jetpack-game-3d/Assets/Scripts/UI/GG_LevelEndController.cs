using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GG_CanvasController : MonoBehaviour
{
    [SerializeField] Button LoseBtn, NextBtn;
    [SerializeField] GG_JPLevels LevelAsset;

    public void Lose()
    {
        LoseBtn.gameObject.SetActive(false);
        NextBtn.gameObject.SetActive(true);
    }
    public void Win()
    {
        LoseBtn.gameObject.SetActive(false);
        NextBtn.gameObject.SetActive(true);

        LevelAsset.PlayerLevel = LevelAsset.PlayerLevel + 1;
    }
}
