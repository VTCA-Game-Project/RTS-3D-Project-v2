using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitElement : MonoBehaviour
{

    private ButtonDownUnitBar UnitBarState = new ButtonDownUnitBar();
    BuildElement Build;
    DefElement Def;
    SoilderElement Soild;
    TankElement Tank;
    private Image CurrentImage;
    public void Start()
    {

        CurrentImage = GetComponent<Image>();
        Build = GetComponent<BuildElement>();
        UnitBarState = GetComponentInParent<ButtonDownUnitBar>();
        Def = GetComponent<DefElement>();
        Soild = GetComponent<SoilderElement>();
        Tank = GetComponent<TankElement>();


    }
    public void InBuldUnitClick(int i, Vector2 Buildsize)
    {
        if (this.gameObject.name == "UnitBuild" + i )
        {
            string inputvalues = "";
            if (Input.GetMouseButtonDown(0))
                inputvalues = "LEFT";
            if (Input.GetMouseButtonDown(1))
                inputvalues = "RIGHT";
            Build.OnUnitClick(inputvalues, Buildsize);
            inputvalues = "";

        }
    }
    public void DoSomeThing(int i, Vector2 buildsize)
    {

       
        if (this.gameObject.name == "UnitSoilder" + i )
        {
          
           
            Soild.OnUnitClick();
           
        }




    }
   
    private void Update()
    {
        //ActiveComponent();
    }
}
