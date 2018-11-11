﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTagetButton : MonoBehaviour {

	public void LoadSceneNum(int num)
    {
      
        if (num<0||num>=SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Can't load SCence num" + num + ", SceneManager only has" + SceneManager.sceneCountInBuildSettings + "scenes in BuildSettings!");
            return;
        }

        LoadingScreenManager.LoadScene(num);

    }

	
}
