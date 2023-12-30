using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class playerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public static playerCamera instance;
    Camera playerCam;
    private void Awake()
    {
        if (!playerCamera.instance)
            instance = this;
        else
            Destroy(gameObject);

        playerCam = Camera.main;
    }
    void Start()
    {
        
    }
    public void shake(float duration=0.5f)
    {
        playerCam.DOShakePosition(duration);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
