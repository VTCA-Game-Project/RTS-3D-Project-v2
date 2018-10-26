using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPos : MonoBehaviour {

    public Material m1;
    public Material m2;

    public GameObject mytaget;
    public GameObject Mychild;
    MeshRenderer muren;
	void Start ()
    {
        muren = Mychild.GetComponent<MeshRenderer>();
		if(mytaget.gameObject.layer==12)
        {
            muren.material = m1;
        }
        if (mytaget.gameObject.layer == 13)
        {
            muren.material = m2;
        }


    }
	
	// Update is called once per frame
	
}
