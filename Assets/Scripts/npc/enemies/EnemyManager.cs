using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] enemies;
	// Use this for initialization
	void Start ()
    {
        enemies = new GameObject[transform.childCount];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = transform.GetChild(i).gameObject;
        } 
		
	}
	
}
