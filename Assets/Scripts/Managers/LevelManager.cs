using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


    public int score;
    public static event Action<int> UpdateScore = delegate { };
    public static LevelManager Instance { get; private set; }

    [HideInInspector]
    public InputReader Inputreader;

    List<RoomManager> Rooms;

    private void Awake()
    {
        Instance = this;
    }
     
   
    // Use this for initialization
    void Start ()
    {
        Inputreader = GetComponent<InputReader>();
        UpdateScore += addscore;
	}
	public void UpdateTheScore(int i)
    {
        UpdateScore(i);
    }
    public void addscore(int i)
    {
        
        score+=i;
     }
	
}
