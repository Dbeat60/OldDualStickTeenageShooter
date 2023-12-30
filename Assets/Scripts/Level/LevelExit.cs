using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class LevelExit : MonoBehaviour
{
    
    private new Collider collider;

    private bool exiting;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
    private void Awake()
    {
        bool triggerFound = false;
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].isTrigger)
            {
                triggerFound = true;
                break;
            }
        }
        if (!triggerFound)
        {
            Debug.LogError("LevelExit " + name + " has no trigger");
            return;
        }

        exiting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exiting)
        {
            return;
        }

        if (other != null
            && other.tag == "Player")
        {
            exiting = true;
            Level level = FindObjectOfType<Level>();
            level.ExitLevel();
        }
    }
}
