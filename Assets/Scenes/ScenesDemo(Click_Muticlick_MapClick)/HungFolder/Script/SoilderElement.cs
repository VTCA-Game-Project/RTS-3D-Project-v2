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
    private float TimeperUpdate = 0.5f;
    private int Count;

    public Text _cout;
   
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
                stateCowdown = false;
                CowDownComplete = true;


            }
        }
            if (CowDownComplete)
            {

                newcolo.a *= -1;
                CountDownIMG.color = newcolo;
                CowDownComplete = false;

                Count--;
                Debug.Log(this.gameObject.name +"_"+ Count);


            }

        

           

           


        

       

    }

    public void OnUnitClick()
    {
        Count++;
      
    }
}
