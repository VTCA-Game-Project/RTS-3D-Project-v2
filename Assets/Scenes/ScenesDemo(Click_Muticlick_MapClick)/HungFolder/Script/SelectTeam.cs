using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectTeam : MonoBehaviour {

	// Use this for initialization
    public void OnHumanclick()
    {
        Singleton.classname = "Human";
        SceneManager.LoadScene(3);

    }
    public void OnOrcClick()
    {
        Singleton.classname = "Orc";
        SceneManager.LoadScene(3);
    }
}

