using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDownUnitBar : MonoBehaviour
{
   
   
    [HideInInspector]
    public StateUnitButton State = new StateUnitButton();

    private ButtonDown parentbuttondow = new ButtonDown();

    private void Awake()
    {
        State = StateUnitButton.NONE;
        parentbuttondow = GetComponentInParent<ButtonDown>();
    }
   

    public void ChangeState(string State)
    {


        switch (State)
        {
            case "BuildButton":

                this.State = StateUnitButton.OBA;
                break;

            case "DefButton":
                this.State = StateUnitButton.ODA;
              
                break;
            case "SoilderButton":
                this.State = StateUnitButton.OSA;
              
                break;
            case "TankButton":
                this.State = StateUnitButton.OTA;
             
                break;
            case "":
                this.State = StateUnitButton.NONE;

                break;

        }

        parentbuttondow.UnitElementactive();
    }
}
