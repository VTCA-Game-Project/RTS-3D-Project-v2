using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoilderElement : MonoBehaviour {

    // Use this for initialization
    [HideInInspector]
    public Image CurrentImage;
   
    public Sprite NewImage;
    public GameObject Mouse;
    public Image CountDownIMG;
    private Pointer ClickEvent;
    private float totaltime = 0f;
    private float delaytime = 5f;
    private float TimeperUpdate = 1f;
    private int Count;
   
    Color newcolo;
    private bool stateCowdown;
    private bool CowDownComplete;
    void Start()
    {
        Count = 0;
        ClickEvent = Mouse.GetComponent<Pointer>();

        CurrentImage = GetComponent<Image>();

        CurrentImage.sprite = NewImage;
        newcolo = CountDownIMG.color;
        newcolo.a = 0;
        CountDownIMG.color = newcolo;
    }

    // Update is called once per frame
    void Update () {


        if(Count>0)
        {
            newcolo.a = 180;
            CountDownIMG.color = newcolo;
        }

       while(Count>0)
        {
            totaltime += TimeperUpdate * Time.deltaTime;

            CountDownIMG.fillAmount -= (TimeperUpdate / delaytime) * Time.deltaTime;
            if (totaltime > delaytime)
            {
                CountDownIMG.fillAmount = 1;
                totaltime = 0f;
                stateCowdown = false;
                CowDownComplete = true;


            }

            if (CowDownComplete)
            {

                newcolo.a *= -1;
                CountDownIMG.color = newcolo;
                CowDownComplete = false;

                Count--;


            }

        }

           

           


        

       

    }

    public void OnUnitClick()
    {
        Count++;
      
    }
}
