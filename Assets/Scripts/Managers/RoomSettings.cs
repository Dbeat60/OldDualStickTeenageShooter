using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="RoomSetting",menuName ="Settings/Room",order= 0)]
public class RoomSettings : ScriptableObject
{
    int ID;
    public enum WinningRule
    {
        Time,
        KillAllEnemies,
        Key,
        Puzzle
    }
    public WinningRule roomRule;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
