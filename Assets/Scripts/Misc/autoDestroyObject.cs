using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestroyObject : MonoBehaviour
{
    [SerializeField] float lifeTime = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
