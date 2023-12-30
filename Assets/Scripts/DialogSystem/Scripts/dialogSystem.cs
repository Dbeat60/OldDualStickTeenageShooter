using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dialogSystem : MonoBehaviour {

   
    [SerializeField]
    public Text npcNameT, dialogT;
    int currentText=0;
    [SerializeField][Range(0f,1f)]
    public float timeperLetter=0.03f;
    [HideInInspector]
    public string dialogS;
    [HideInInspector]
    public List<string> dialogLS;
    string tmp = "";
    public Image img;
    public Sprite[] sp;
    public string btnName;
    // Use this for initialization

    public void StartDialog(string npcNameS, List<string> Dialogs)
    {
        foreach (Sprite s in sp)
        {
            if (s.name == npcNameS)
                img.sprite = s;
        }
        npcNameT.text= npcNameS;
        dialogT.text = "";
        dialogLS = Dialogs;
        dialogS = dialogLS[currentText];
        StartCoroutine(showDialog());
    }
   public void resetDialogSystem()
    {
       
        currentText = 0;
        dialogT.text = "";
        tmp = "";
        dialogS = dialogLS[currentText];
        gameObject.SetActive(false);
    }
    public void nextdialog()
    {
        if (tmp.Length < dialogS.Length)
        {
            tmp = tmp + dialogS;
            dialogT.text = tmp;
        }
        else if(currentText<dialogLS.Count-1)
        {
            currentText++;
            dialogT.text = "";
            tmp = "";
            dialogS = dialogLS[currentText];
            StartCoroutine(showDialog());
        }
        else
        {
            resetDialogSystem();
        }
    }
    IEnumerator showDialog()
    {
        yield return new WaitForSeconds(timeperLetter);
        if(tmp.Length<dialogS.Length)
        {
            tmp = tmp + dialogS[tmp.Length];
            dialogT.text = tmp;
            StartCoroutine(showDialog());
        }
    }
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown(btnName))
        {
            nextdialog();
        }
		
	}
}
