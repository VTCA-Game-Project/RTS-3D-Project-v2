using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDown : MonoBehaviour, IPointerDownHandler
{
    
    public GameObject[] ButtonList;

    private ButtonElement element;
    delegate void tranfrom();
    tranfrom Buttonhandler;
    private void Awake()
    {
       
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        string even = eventData.pointerEnter.name.ToString();
        switch (even)
        {
            case "BuildButton":
                element = ButtonList[0].GetComponent<ButtonElement>();
                Buttonhandler = element.Showon;
                break;
            case "DefButton":
                element = ButtonList[1].GetComponent<ButtonElement>();
                Buttonhandler = element.Showon;
                break;
            case "SoilderButton":
                element = ButtonList[2].GetComponent<ButtonElement>();
                Buttonhandler = element.Showon;
                break;
            case "TankButton":
                element = ButtonList[3].GetComponent<ButtonElement>();
                Buttonhandler = element.Showon;
                break;
            case "Number5":
                element = ButtonList[4].GetComponent<ButtonElement>();
                Buttonhandler = element.Showon;
                break;
            case "Number6":
                element = ButtonList[5].GetComponent<ButtonElement>();
                Buttonhandler = element.Showon;
                break;
            case "Number7":
                element = ButtonList[6].GetComponent<ButtonElement>();
                Buttonhandler = element.Showon;
                break;
            case "Number8":
                element = ButtonList[7].GetComponent<ButtonElement>();
                Buttonhandler = element.Showon;
                break;

        }
        Buttonhandler();
    }
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeStatusButton(ButtonElement butel)
    {

        for (int j = 0; j < ButtonList.Length; j++)
        {

            ButtonElement _butel = ButtonList[j].GetComponent<ButtonElement>();
            if (_butel.name != butel.name)
                _butel.Selected = false;

        }

    }

}
