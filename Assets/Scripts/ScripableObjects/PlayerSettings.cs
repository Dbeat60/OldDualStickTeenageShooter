using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerSettings", menuName ="Settings",order =1)]
public class PlayerSettings : ScriptableObject {

    public string objectName = "PlayerSettings";
    public float health = 0;
    public float maxHealth = 100;
}
