using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;

public class MainGameLoad : MonoBehaviour {



    string PlayerClass= Singleton.classname;

    public List<GameObject> Orc;
    public List<GameObject> Human;
    public List<GameObject> ButtonList;


   

    void Start ()
    {
        



        if (PlayerClass=="Human")
        {
            for(int i=0;i< ButtonList.Count;i++)
            {
                BuildElement buildinfo = ButtonList[i].GetComponent<BuildElement>();
                buildinfo.BuildModel = Human[i];
            }
        }
        else
        {
          if(  PlayerClass == "Orc")
            {
                for (int i = 0; i < ButtonList.Count; i++)
                {
                    BuildElement buildinfo = ButtonList[i].GetComponent<BuildElement>();
                    buildinfo.BuildModel = Orc[i];
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
