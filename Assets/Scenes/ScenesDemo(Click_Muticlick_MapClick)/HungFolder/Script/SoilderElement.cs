﻿using DelegateCollection;
using EnumCollection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pattern;

public class SoilderElement : MonoBehaviour {

    // Use this for initialization
    [HideInInspector]
    public Image CurrentImage;
     string PlayerClass=Singleton.classname;
    public Sprite NewImage;
    public GameObject Mouse;
    public Image CountDownIMG;
   
    private float totaltime = 0f;
    private float delaytime = 5f;
    private float TimeperUpdate = 0.5f;
    private int Count;
    Soldier UnitType;
    public Text _cout;
   
    Color newcolo;
   
    private bool CowDownComplete;
    void Start()
    {
        Count = 0;
     
        switch(this.gameObject.name)
        {
            case "UnitSoilder1":
                if (PlayerClass == "Orc")
               
                    UnitType = Soldier.Warrior;
               
                if (PlayerClass == "Human")
               
                    UnitType = Soldier.Warrior;
                    break;
                
            case "UnitSoilder2":
                UnitType = Soldier.Archer;
                break;
            case "UnitSoilder3":
                UnitType = Soldier.Magic;
                break;
            case "UnitSoilder4":
                UnitType = Soldier.WoodHorse;
                break;
        }
        CurrentImage = GetComponent<Image>();

        CurrentImage.sprite = NewImage;
        newcolo = CountDownIMG.color;
        newcolo.a = 0;
        CountDownIMG.color = newcolo;
    }

    // Update is called once per frame
    void Update () {

        _cout.text = Count.ToString();
        if (Count > 0)
        {

            newcolo.a = 180;
            CountDownIMG.color = newcolo;



            totaltime += TimeperUpdate * Time.deltaTime;

            CountDownIMG.fillAmount -= (TimeperUpdate / delaytime) * Time.deltaTime;
            if (totaltime > delaytime)
            {
                CountDownIMG.fillAmount = 1;
                totaltime = 0f;
             
                CowDownComplete = true;


            }
        }
            if (CowDownComplete)
            {

                newcolo.a *= -1;
                CountDownIMG.color = newcolo;
                CowDownComplete = false;

                Count--;
         
            if (createSoldier != null)
            {
                Debug.Log("start create");
                createSoldier(UnitType);
            }

            }

        

           

           


        

       

    }

    public void OnUnitClick(string input)
    {
        if(input== "LEFT")
        Count++;
        if (input == "RIGHT"&&Count>=0)
        {
            if (Count <= 0)
            {
                CountDownIMG.fillAmount = 1;
                totaltime = 0f;

                CowDownComplete = true;
                newcolo.a *= -1;
                CountDownIMG.color = newcolo;
                CowDownComplete = false;
            }
            if(Count >0)
            Count--;
        }



    }

    protected Create createSoldier;
    public void setsomething(Create method)
    {
        createSoldier = method;
    }
}
