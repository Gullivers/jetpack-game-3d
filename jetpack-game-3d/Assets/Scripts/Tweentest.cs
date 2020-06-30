using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tweentest : MonoBehaviour
{

    void Start()
    {
        transform.DOMoveY(transform.position.y - 5f, 5f);

    }
    void TweentestInv()
    {

    }
}
