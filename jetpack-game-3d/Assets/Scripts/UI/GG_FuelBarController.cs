using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GG_FuelBarController : MonoBehaviour
{
    [SerializeField] GG_JetpackMovement jectpack;
    float StartFuel;
    Image Image;
    private void Awake()
    {
        Image = GetComponent<Image>();
        StartFuel = jectpack.Fuel;
    }
    void Update()
    {
        Image.fillAmount = (jectpack.Fuel/StartFuel);
    }
}
