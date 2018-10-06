using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{


    [SerializeField]
    private Material red;
    [SerializeField]
    private Material Green;
    [HideInInspector]
    public bool currentselect = false;

    private AIAgent agent;

    private void Awake()
    {
        agent = GetComponent<AIAgent>();
    }
    void Start()
    {
        Camera.main.gameObject.GetComponent<Click>().SelectableObjects.Add(this.gameObject);
        Debug.Log(agent);
        ClickMe();
    }

    // Update is called once per frame

    public void ClickMe()
    {
        if (currentselect == true)
        {
            agent.Select();
        }
        else
        {
            agent.UnSelect();
        }
           
    }
}
