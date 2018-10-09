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

    public void DoSomeThing()
    {

        if (this.gameObject.name == "Unit1" && UnitBarState.State == StateUnitButton.OBA)
        {
          
            Build.OnUnit1Click();
        }


    }
    public void ActiveComponent()
    {


        if (UnitBarState.State == StateUnitButton.OBA)
        { Build.enabled = true; Def.enabled = false; Soild.enabled = false; Tank.enabled = false; Build.CurrentImage.sprite = Build.NewImage; }
        if (UnitBarState.State == StateUnitButton.ODA)
        { Build.enabled = false; Def.enabled = true; Soild.enabled = false; Tank.enabled = false; Def.CurrentImage.sprite = Def.NewImage; }
        if (UnitBarState.State == StateUnitButton.OSA)
        { Build.enabled = false; Def.enabled = false; Soild.enabled = true; Tank.enabled = false; Soild.CurrentImage.sprite = Soild.NewImage; }
        if (UnitBarState.State == StateUnitButton.OTA)
        { Build.enabled = false; Def.enabled = false; Soild.enabled = false; Tank.enabled = true; Tank.CurrentImage.sprite = Tank.NewImage; }
        if (UnitBarState.State == StateUnitButton.NONE)
        {
            Build.enabled = false; Def.enabled = false; Soild.enabled = false; Tank.enabled = false;

            CurrentImage.sprite = null;
        }
    }
    private void Update()
    {
        //ActiveComponent();
    }
}
