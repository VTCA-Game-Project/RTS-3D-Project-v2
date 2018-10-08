using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonElement : MonoBehaviour {

    private CellManager cell;
    private bool OnCownDown=false ;
   [HideInInspector]
    public bool Selected = false;
    [HideInInspector]
    public string name;
    private Color _color;
    private ButtonDown downfunction;
    
    private Image _image;
	void Start ()
    {
        name = this.gameObject.name;
        cell = GameObject.FindObjectOfType<CellManager>();
        _image = GetComponent<Image>();
        downfunction = GetComponentInParent<ButtonDown>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        


    }
    public void Showon()
    {

        if(gameObject.tag=="ControlButton")
        {
            Selected = !Selected;
            ControlButtonUpdate();

        }
        if (gameObject.tag == "Active")
        {

            if (OnCownDown == false)
                Selected = !Selected;

            if (Selected == true)
                OnCownDown = true;
        }
    }



    private void ControlButtonUpdate()
    {
        
        ButtonElement butel = this.GetComponent<ButtonElement>();
        downfunction.ChangeStatusButton(butel);
       
      
       
    }
    public void ChangeButtonColor(ButtonElement butel)
    {
        _color = _image.color;
        if (butel.Selected == true)
        {
            _color.a += 1;
        }
        else
        {
            _color.a = 0;
        }


        _image.color = _color;
    }
}
