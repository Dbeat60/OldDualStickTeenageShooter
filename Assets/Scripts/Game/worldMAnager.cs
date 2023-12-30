using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class worldMAnager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool isGameOver=false;
    public bool isendOfLevel=false;
    public void gotoScene(string scenex)
    {
        SceneManager.LoadSceneAsync(scenex);
    }
    public void gotoScene(int scenex)
    {
        SceneManager.LoadSceneAsync(scenex);
    }
    public void resetGame()
    {
        if (isGameOver)
        {
            
                gotoScene(0);
             
        }
    }
    public void gotoGameOveR()
    {
        if (isendOfLevel)
        {

            gotoScene(2) ;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if( other.transform.tag=="Player")
        {
           // Debug.LogError(other.name);
            gotoGameOveR();
        }
    }
    void Update()
    {
            if (Input.anyKeyDown)
            {
                resetGame();
            }
    }
}
