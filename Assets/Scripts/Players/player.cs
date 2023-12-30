using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    public Transform effectPoint;
    public static player _Instance;
    public  int currentplayer = 0;
    public float distbetweenplayers;
    public GameObject poolManagerPrefab;
    public static player Instance
    {
        get
        {
            return _Instance;
        }
    }

    public static event Action switchPlayer = delegate { };

    public enum PlayerState
    {
        gun,
        shield,
    }
    
  //  public AudioSource shoot;
    Quaternion extrarot;     
    public GameObject player1obj, player2obj;
  
    [SerializeField]
    PlayerSettings HealthSettings;
 
    [HideInInspector]
    public  PlayerAudioManager playerAudioManager;
    [HideInInspector]
    public PlayerAnimationManager playerAnimationManager;
    [HideInInspector] public PlayerMovementManager playerMovementManager;
    [HideInInspector] public PlayerWeaponManager playerWeaponManager;
    [HideInInspector] public playerHealthManager playerHealthManager;
    [HideInInspector] public combomanager ComboMgr;
    public bool isJumping;

    public KeyHolder keyHolder;

    public static bool HasKey(string keyId) => Instance.keyHolder != null && Instance.keyHolder.HasKey(keyId);
    
    private void Awake()
    {
        _Instance = this;
         
    }
    void Start ()
    {
        playerHealthManager = GetComponent<playerHealthManager>();
        playerHealthManager.setup(HealthSettings);
        switchPlayer += switchPlayerfn;
       
         playerAudioManager = GetComponent<PlayerAudioManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
        ComboMgr = GetComponentInChildren<combomanager>();

        //player1obj.transform.position += Vector3.forward * distbetweenplayers;
        player2obj.transform.position -= Vector3.forward * distbetweenplayers;
    }

   public void callSwitchPlayerEvent()
    {
        switchPlayer();
    }
   public void switchPlayerfn()
    {
        if(currentplayer==0)
        {
            playerHealthManager.switchPlayer(currentplayer);
            currentplayer = 1;
        }
        else
        {
            playerHealthManager.switchPlayer(currentplayer);
            currentplayer = 0;

        }
        StartCoroutine(playerHealthManager.recoverhealth(currentplayer));
    }
 

    
  
	// Update is called once per frame
	void Update ()
    {
        //LevelManager.Instance.Inputreader.SetPause();
        
       
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
       
        playerAudioManager.StepsAudio(LevelManager.Instance.Inputreader.getSpeed());
        playerAnimationManager.CalculateAnimations(LevelManager.Instance.Inputreader.getSpeed());
        playerMovementManager.CalculateDirection();
        playerMovementManager.CalculateMovement();

    }


    public void SetPosition(Transform point)
    {
        transform.position = point.position;
        transform.rotation = point.rotation;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.transform.name);
    }
}
