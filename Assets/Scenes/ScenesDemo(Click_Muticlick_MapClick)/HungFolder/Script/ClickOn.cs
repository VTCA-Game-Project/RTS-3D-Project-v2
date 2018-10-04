using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour {


    [SerializeField]
    private Material red;
    [SerializeField]
    private Material Green;
    [HideInInspector]
    public bool currentselect = false;
    private MeshRenderer myrend;

    
	void Start () {
        myrend = GetComponent<MeshRenderer>();
        Camera.main.gameObject.GetComponent<Click>().SelectableObjects.Add(this.gameObject);

        ClickMe();
	}
	
	// Update is called once per frame
	
    public void ClickMe()
    {
        if (currentselect == true)
            myrend.material = Green;
        else
            myrend.material = red;
    }
    public void addAbleObject(ref List<GameObject> rep)
    {
        rep.Add(this.gameObject);
    }
}
