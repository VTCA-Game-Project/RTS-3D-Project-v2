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
    public void Showon(string name)
    {

        if(!downfunction.returnactivebuton()|| downfunction.CheckActiveButton(name))
        {
            Selected = !Selected;
            ButtonElement butel = this.GetComponent<ButtonElement>();
            downfunction.ChangeStatusButton(butel);

        }
        else
        {

        }
        if (gameObject.tag == "Active")
        {

            if (OnCownDown == false)
                Selected = !Selected;

            if (Selected == true)
                OnCownDown = true;
        }
    }



   
    public void ChangeButtonColor(ButtonElement butel)
    {
        _color = _image.color;
        if (butel.Selected == true)
        {
            _color.a =90;
        }
        else
        {
            _color.a = 0;
        }

        Debug.Log(_color.a);
        _image.color = _color;
    }
}
