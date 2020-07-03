using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GG_DefaultTools
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private  static void ClearPlayerPrefs(){

        PlayerPrefs.DeleteAll();
    }
}
