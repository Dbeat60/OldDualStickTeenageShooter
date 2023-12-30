using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{

    [SerializeField]
    private string levelSceneName;

    public void OnStartGame()
    {
        SceneMaster.ChangeActiveScene(levelSceneName);
    }

}
