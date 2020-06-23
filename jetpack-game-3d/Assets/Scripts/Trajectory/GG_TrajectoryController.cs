using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_TrajectoryController : MonoBehaviour
{
    [SerializeField] ParabolaController parabola;
    private void Update() {
       for (int i = 1; i < transform.childCount; i++)
       {
           transform.GetChild(i-1).transform.position= parabola.Dots[i*5];
       } 
    }
}
