using UnityEngine;

public class NPCHealth : Health
{
    [SerializeField]
    bool canReceiveDamage;
	// Use this for initialization
	void Start ()
    {

        SetupHealth();
		
	}

    public override void AffectHealth(int hdelta)
    {
        base.AffectHealth(hdelta);
        if (health <= 0)
 
        Destroy(gameObject);
     }
   
}
