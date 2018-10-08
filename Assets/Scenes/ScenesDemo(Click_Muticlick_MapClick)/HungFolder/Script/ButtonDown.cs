using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDown : MonoBehaviour, IPointerDownHandler
{
    
    public GameObject[] ButtonList;
    public GameObject[] UnitList;
    private ButtonElement element;
    private UnitElement UnitElement;
    delegate void tranfrom();
    tranfrom Buttonhandler;
    public GameObject UnitBar;
    private ButtonDownUnitBar UnitBarControl;
    private void Awake()
    {
        UnitBar.SetActive ( false);
        UnitBarControl= UnitBar.GetComponent<ButtonDownUnitBar>();
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
            case "Unit1":
                UnitElement = UnitList[0].GetComponent<UnitElement>();
                Buttonhandler = UnitElement.DoSomeThing;
                break;
            case "Unit2":
                UnitElement = UnitList[1].GetComponent<UnitElement>();
                Buttonhandler = UnitElement.DoSomeThing;
                break;
            case "Unit3":
                UnitElement = UnitList[2].GetComponent<UnitElement>();
                Buttonhandler = UnitElement.DoSomeThing;
                break;
            case "Unit4":
                UnitElement = UnitList[3].GetComponent<UnitElement>();
                Buttonhandler = UnitElement.DoSomeThing;
                break;
            case "Unit5":
                UnitElement = UnitList[4].GetComponent<UnitElement>();
                Buttonhandler = UnitElement.DoSomeThing;
                break;
            case "Unit6":
                UnitElement = UnitList[5].GetComponent<UnitElement>();
                Buttonhandler = UnitElement.DoSomeThing;
                break;
            case "Unit7":
                UnitElement = UnitList[6].GetComponent<UnitElement>();
                Buttonhandler = UnitElement.DoSomeThing;
                break;
            case "Unit8":
                UnitElement = UnitList[7].GetComponent<UnitElement>();
                Buttonhandler = UnitElement.DoSomeThing;
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

        CheckButtonlistSate();
    }
    public void ChangeStatusButton(ButtonElement butel)
    {

        for (int j = 0; j < ButtonList.Length; j++)
        {

            ButtonElement _butel = ButtonList[j].GetComponent<ButtonElement>();
            if (_butel.name != butel.name)
                _butel.Selected = false;


            if(_butel.Selected==true)
            {
                _butel.ChangeButtonColor(_butel);
                UnitBarControl.ChangeState(_butel.name);
               


                EnableUnitBar();
                
            }
            else
            {
                _butel.ChangeButtonColor(_butel);
                
            }

        }

    }
    public void EnableUnitBar()
    {
        UnitBar.SetActive (true);
    }
    public void DisableUnitBar()
    {
        UnitBar.SetActive(false);
    }
    private void CheckButtonlistSate()
    {


        if (Input.GetMouseButtonDown(0))
        {
            int index = 0;
            for (int j = 0; j < ButtonList.Length; j++)
            {
                ButtonElement _butel = ButtonList[j].GetComponent<ButtonElement>();

                if (_butel.Selected == false)
                    index++;
            }
            if (index > 3)
            {
                DisableUnitBar();
              
            }
        }
    }
    public void UnitElementactive()
    {
        for(int i =0;i<UnitList.Length;i++)
        {

            UnitElement = UnitList[i].GetComponentInChildren<UnitElement>();
            UnitElement.ActiveComponent();
        }
          

    }

  

}
