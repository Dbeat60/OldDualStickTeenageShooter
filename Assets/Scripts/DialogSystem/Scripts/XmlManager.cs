using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
public class XmlManager : MonoBehaviour
{

      Dictionary<string, List<string>> Dialogs = new Dictionary<string, List<string>>();

   public List<string> ReadData(string NpcName)
    {
        XmlDocument xmlDoc = new XmlDocument();
        string s = File.ReadAllText(Application.dataPath+"/DialogSystem/dialogs.xml");
        xmlDoc.LoadXml(s);

        foreach(XmlNode Dialog in xmlDoc["Dialogs"].ChildNodes)
        {
            if (NpcName == Dialog.Attributes["name"].Value)
            {
                List<string> Lines = new List<string>();

                foreach (XmlNode Line in Dialog["Lines"].ChildNodes)
                {
                    Lines.Add(Line.InnerText);
                }

                Dialogs[NpcName] = Lines;
            }
        }

        return Dialogs[NpcName];
    }

	// Use this for initialization
	void Start ()
    {
        
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
