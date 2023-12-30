using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{

    private bool closing;

    private void Awake()
    {
        closing = false;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            CloseGameOverMenu();
        }
    }

    public void CloseGameOverMenu()
    {
        if (closing)
        {
            return;
        }

        closing = true;
        SceneMaster.GoToTitleScreen();
    }

}
