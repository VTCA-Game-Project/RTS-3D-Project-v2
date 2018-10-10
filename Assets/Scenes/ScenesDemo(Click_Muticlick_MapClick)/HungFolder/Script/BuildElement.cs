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
    private Pointer ClickEvent;
    private float totaltime=0f;
    private float delaytime =5f;
    private bool stateCowdown;
    void Start()
    {
        ClickEvent = Mouse.GetComponent<Pointer>();

        CurrentImage = GetComponent<Image>();

        CurrentImage.sprite = NewImage;
    }

    // Update is called once per frame
    void Update ()
    {
        if(stateCowdown)
        {
            totaltime += 1 * Time.deltaTime;
        }

        if(totaltime>delaytime)
        {
            Vector2 buidSize = new Vector2(3, 3);
            ClickEvent.ResetTaget();
            ClickEvent.BuildSize = buidSize;
            ClickEvent.OnselectTaget = true;
            totaltime = 0f;
            stateCowdown = false;
        }
		
	}

    public void OnUnit1Click()
    {
        if (!stateCowdown)
        {
            Debug.Log("i'm here");
            stateCowdown = true;
            CurrentImage.color= Color.yellow;
        }
       
    }
}
