using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDown : MonoBehaviour, IPointerDownHandler
{
    
    public GameObject[] ButtonList;
    public GameObject[] UnitBuildList;
    public GameObject[] UnitDefList;
    public GameObject[] UnitSoilderList;
    public GameObject[] UnitTankList;
    public GameObject[] UnitManagerList;
    private ButtonElement element;
    private UnitElement UnitElement;
    delegate void tranfrom();
  
    
  
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
                element.Showon("BuildButton");
                break;
            case "DefButton":
                element = ButtonList[1].GetComponent<ButtonElement>();
                element.Showon("DefButton");
                break;
            case "SoilderButton":
                element = ButtonList[2].GetComponent<ButtonElement>();
                element.Showon("SoilderButton");
                break;
            case "TankButton":
                element = ButtonList[3].GetComponent<ButtonElement>();
                element.Showon("TankButton");
                break;
            case "UnitBuild1":
                UnitElement = UnitBuildList[0].GetComponent<UnitElement>();
                UnitElement.InBuldUnitClick(1, new Vector2(3, 3));
                break;
            case "UnitBuild2":
                UnitElement = UnitBuildList[1].GetComponent<UnitElement>();
                UnitElement.InBuldUnitClick(2, new Vector2(3, 3));
                break;
            case "UnitBuild3":
                UnitElement = UnitBuildList[2].GetComponent<UnitElement>();
                UnitElement.InBuldUnitClick(3,new Vector2 (3,3));
                break;
            case "UnitBuild4":
                UnitElement = UnitBuildList[3].GetComponent<UnitElement>();
                UnitElement.InBuldUnitClick(4, new Vector2(3, 3));
                break;
            case "UnitBuild5":
                UnitElement = UnitBuildList[4].GetComponent<UnitElement>();
                UnitElement.InBuldUnitClick(5, new Vector2(3, 3));
                break;
            case "UnitBuild6":
                UnitElement = UnitBuildList[5].GetComponent<UnitElement>();
                UnitElement.InBuldUnitClick(6, new Vector2(3, 3));
                break;
            case "UnitBuild7":
                UnitElement = UnitBuildList[6].GetComponent<UnitElement>();
                UnitElement.InBuldUnitClick(7, new Vector2(3, 3));
                break;
            case "UnitBuild8":
                UnitElement = UnitBuildList[7].GetComponent<UnitElement>();
                UnitElement.InBuldUnitClick(8, new Vector2(3, 3));
                break;


        }
       
    }
    

    // Use this for initialization
    void Start()
    {
        DisableUnitBar();
    }

    // Update is called once per frame
    void Update()
    {

        ClearUnactiveButton();
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
                switch(butel.name)
                {
                    case "BuildButton":
                        EnableUnitBar("UnitBuyBuild");
                        _butel.ChangeButtonColor(_butel);
                        break;
                    case "DefButton":
                        EnableUnitBar("UnitBuyDef");
                        _butel.ChangeButtonColor(_butel);
                        break;
                    case "SoilderButton":
                        EnableUnitBar("UnitBuySoilder");
                        _butel.ChangeButtonColor(_butel);
                        break;
                    case "TankButton":
                        EnableUnitBar("UnitBuyTank");
                        _butel.ChangeButtonColor(_butel);
                        break;
                }
               
               
              

              

              
                
            }
            else
            {
                _butel.ChangeButtonColor(_butel);
               

            }

        }

    }
    public void EnableUnitBar(string _name)
    {
        foreach (GameObject manager in UnitManagerList)
        {
            if (manager.name == _name)
                manager.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void DisableUnitBar()
    {
        foreach (GameObject manager in UnitManagerList)
        {
            manager.transform.eulerAngles = new Vector3(0, 90, 0);
        }
    }
    private void ClearUnactiveButton()
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
    public void UnitElementactive(string _name)
    {

          foreach(GameObject manager in UnitManagerList)
        {
            if (manager.name == _name)
                 manager.transform.eulerAngles= new Vector3(0,90,0);
            else
                manager.transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }



    public bool returnactivebuton()
    {
        foreach (GameObject GO in ButtonList)
        {
            ButtonElement _element = GO.GetComponent<ButtonElement>();
            if (_element.Selected == true)
            {
                return true;
            }
        }
        return false;
    }


    public bool CheckActiveButton(string name)
    {


        foreach (GameObject GO in ButtonList)
        {
            ButtonElement _element = GO.GetComponent<ButtonElement>();
            if (_element.Selected == true&&_element.name==name)
            {
                return true;
            }
        }
        return false;
    }

  

}
