using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

    // Use this for initialization

    public Image imageHealthValue;

    private float counter;

    void Start() { 
   
		
	}
	
	// Update is called once per frame
	void Update ()

    {
        transform.LookAt(Camera.main.transform);
    }

    public void SetValue(float value)
    {
        imageHealthValue.fillAmount = value;

    }
}
