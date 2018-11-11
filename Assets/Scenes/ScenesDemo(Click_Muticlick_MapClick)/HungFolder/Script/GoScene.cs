using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoScene : MonoBehaviour {

	

    public void Go_select_Scene()
    {
        new LoadSceneTagetButton().LoadSceneNum(1);
       
    }
}
