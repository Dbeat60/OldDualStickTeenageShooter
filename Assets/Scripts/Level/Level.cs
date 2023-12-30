using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField]
    private Transform spawnPoint;

    private bool exiting;

    private void Awake()
    {
        exiting = false;
    }

    private void Start()
    {
        player _player = FindObjectOfType<player>();
        _player.SetPosition(spawnPoint);
        UnityStandardAssets.Utility.FollowTarget followTarget = FindObjectOfType< UnityStandardAssets.Utility.FollowTarget>();
        followTarget.ResetPosition();
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    public void ExitLevel()
    {
        if (exiting)
        {
            return;
        }

        exiting = true;
        SceneMaster.GoToGameOverScreen();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawnPoint.position, 1f);
    }

}
