using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitElement : MonoBehaviour {

    private ButtonDownUnitBar UnitBarState = new ButtonDownUnitBar();
    private BuildElement Build = new BuildElement();
    private DefElement Def = new DefElement();
    private SoilderElement Soild = new SoilderElement();
    private TankElement Tank = new TankElement();

    public void Start()
    {
        UnitBarState=GetComponentInParent<ButtonDownUnitBar>();
       
        Build.enabled = false; Def.enabled = false; Soild.enabled = false; Tank.enabled = false;
    }

    public void DoSomeThing()
    {

    }
    public void ActiveComponent()
    {
        Build = GetComponent<BuildElement>();
        Def = GetComponent<DefElement>();
        Soild = GetComponent<SoilderElement>();
        Tank = GetComponent<TankElement>();

        if (UnitBarState.State == StateUnitButton.OBA)
        { Build.enabled = true; Def.enabled = false; Soild.enabled = false; Tank.enabled = false; }
        if (UnitBarState.State == StateUnitButton.ODA)
        { Build.enabled = false; Def.enabled = true; Soild.enabled = false; Tank.enabled = false; }
        if (UnitBarState.State == StateUnitButton.OSA)
        { Build.enabled = false; Def.enabled = false; Soild.enabled = true; Tank.enabled = false; }
        if (UnitBarState.State == StateUnitButton.OTA)
        { Build.enabled = false; Def.enabled = false; Soild.enabled = false; Tank.enabled = true; }
        if (UnitBarState.State == StateUnitButton.NONE)
        { Build.enabled = false; Def.enabled = false; Soild.enabled = false; Tank.enabled = false; }
    }
    private void Update()
    {
        ActiveComponent();
    }
}
