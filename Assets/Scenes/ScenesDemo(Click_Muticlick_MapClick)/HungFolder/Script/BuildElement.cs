using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumCollection;
public class BuildElement : MonoBehaviour {

    // Use this for initialization
    ConstructId Id = ConstructId.Barrack;
    [HideInInspector]   
    public Image CurrentImage;
    public Sprite NewImage;
    public GameObject Mouse;
    public Image CountDownIMG;
    private Pointer ClickEvent;
    private float totaltime=0f;
    private float delaytime =5f;
    private float TimeperUpdate = 1f;
    private float totaltimeImage = 0f;
    private float delaytimeImag = 0.5f;
    Color newcolo;
    private bool stateCowdown;
    private bool CowDownComplete;
    void Start()
    {
        ClickEvent = Mouse.GetComponent<Pointer>();

        CurrentImage = GetComponent<Image>();

        CurrentImage.sprite = NewImage;
        newcolo = CountDownIMG.color;
        newcolo.a = 0;
        CountDownIMG.color = newcolo;
    }

    // Update is called once per frame
    void Update ()
    {
        if(stateCowdown)
        {
            totaltime += TimeperUpdate * Time.deltaTime;

            CountDownIMG.fillAmount -=(TimeperUpdate/delaytime) * Time.deltaTime;

        }

        if(totaltime>delaytime)
        {
            CountDownIMG.fillAmount = 1;
             totaltime = 0f;
            stateCowdown = false;
            CowDownComplete = true;
          
           
        }

        if(CowDownComplete)
        {
            if(totaltimeImage> delaytimeImag)

            {
              
                newcolo.a *= -1;
                CountDownIMG.color = newcolo;

                totaltimeImage = 0;
            }
            else
            {
               
                totaltimeImage += 0.5f * Time.deltaTime;
            }

        }

        
		
	}

    public void OnUnitClick(string inputvalues, Vector2 buidSize)
    {
        if (!stateCowdown&&!CowDownComplete)
        {
            
            stateCowdown = true;

           

            newcolo.a =180;
            CountDownIMG.color= newcolo;
        }
        if (CowDownComplete)
        {
            if (inputvalues == "LEFT")
            {
              
                ClickEvent.ResetTaget();
                ClickEvent.BuildSize = buidSize;
                ClickEvent.OnselectTaget = true;
                CowDownComplete = false;

                newcolo.a = 0;
                CountDownIMG.color = newcolo;
            }
            if (inputvalues == "RIGHT")
            {
                CowDownComplete = false;

                newcolo.a = 0;
                CountDownIMG.color = newcolo;
            }

        }

    }




}
