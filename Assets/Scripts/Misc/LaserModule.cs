using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LaserModule : NPCHealth {

    public LaserSystem laserParent;
    public bool isMidPoint=true;
    // Use this for initialization
    public void Setup()
    {
        health = maxHealth;
    }
    private void dafok()
    {
        if (!isMidPoint)
        {
            Destroy(laserParent);
        }
        laserParent.RemoveLaserPoint(transform);
        
    }
    private void OnDestroy()
    {
        dafok();
    }

    [Button("DamageObject")]
    public void TestDamage()
    {
        Debug.Log("H " + health);
  
        AffectHealth(5);

    }
    // Update is called once per frame
    void Update () {
		
	}
}
