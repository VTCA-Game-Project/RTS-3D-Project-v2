using Common.Entity;
using InterfaceCollection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{
    private ISelectable selectableObject;
    private void Awake()
    {
        selectableObject = GetComponent<AIAgent>();
    }
    void Start()
    {
        Click.Instance.Add(this);
        UnSelect();
    }

    public void Select()
    {
        selectableObject.Select();           
    }
    public void UnSelect()
    {
        selectableObject.UnSelect();
    }
    public void Action()
    {
        selectableObject.Action();
    }
}
