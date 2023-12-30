using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeWeapon : MonoBehaviour
{

    private bool canshoot = true;
    private bool pointingGranade;
    private float maxgranadeDistance = 16f;
    [SerializeField]
    private Transform granadeFinalPoint;

    public GameObject granadeTarget;
    public float granadeSpeed = 150f;

    public GameObject Granade;
    


    public void Throwgranade(float leftTrigger)
    {
        if (pointingGranade && leftTrigger <= 0.05f)
        {
            canshoot = true;
            pointingGranade = false;
            ProjectileAC P = Instantiate(Granade, granadeFinalPoint.position, Quaternion.identity).GetComponent<ProjectileAC>();
            P.shoot(granadeFinalPoint.forward * granadeSpeed);
            granadeTarget.SetActive(false);


        }
        if (leftTrigger > 0.21f)
        {

            granadeTarget.SetActive(true);
            granadeTarget.transform.localPosition = Vector3.forward * maxgranadeDistance * leftTrigger;
            canshoot = false;
            pointingGranade = true;
        }
        else
        {
            granadeTarget.SetActive(false);

        }

    }
}