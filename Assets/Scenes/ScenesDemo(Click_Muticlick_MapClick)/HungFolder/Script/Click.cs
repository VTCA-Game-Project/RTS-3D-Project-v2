using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

    [SerializeField]
    private LayerMask Clicklayer;
    [SerializeField]
    private Camera _camraycast;
    [SerializeField]
    private GameObject ClickPoint;
    private List<GameObject> SelectObject;
    [HideInInspector]
    public List<GameObject> SelectableObjects;

    private Vector3 mousepos1;
    private Vector3 mousepos2;
    // Update is called once per frame
    void Awake()
    {
        SelectObject = new List<GameObject>();
        SelectableObjects = new List<GameObject>();
      
    }
    void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        { ClearSelection(); }
            if (Input.GetMouseButtonDown(0))
        {
           // ClearSelection();
            mousepos1 = _camraycast.ScreenToViewportPoint(Input.mousePosition);
            RaycastHit rayHit;
            
           
            var ray = _camraycast.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray ,out rayHit,Mathf.Infinity, Clicklayer))
            {
               ClickOn _clickon= rayHit.collider.GetComponent<ClickOn>();
                Debug.Log(rayHit.collider.name);

                ClearSelection();
                SelectObject.Add(rayHit.collider.gameObject);
                _clickon.currentselect = true;
                _clickon.ClickMe();
            }
          
        }
            if(Input.GetMouseButtonUp(0))
        {
            mousepos2 = _camraycast.ScreenToViewportPoint(Input.mousePosition);
            if (mousepos1 != mousepos2)
                SelectObs();
        }
	}
    void SelectObs()
    {
        List<GameObject> remObject = new List<GameObject>();
        Rect selectrect = new Rect(mousepos1.x, mousepos1.y, mousepos2.x - mousepos1.x, mousepos2.y - mousepos1.y);
        foreach (GameObject selectobj in SelectableObjects)

        {
            if (selectobj != null)
            {
                if (selectrect.Contains(_camraycast.WorldToViewportPoint(selectobj.transform.position), true))
                {
                    SelectObject.Add(selectobj);
                    selectobj.GetComponent<ClickOn>().currentselect = true;
                    selectobj.GetComponent<ClickOn>().ClickMe();
                }

            }
            else
            {
                remObject.Add(selectobj);
            }
        }
        if(remObject.Count>0)
        {
            foreach(GameObject rem in remObject)
            {
                SelectableObjects.Remove(rem);
            }
            remObject.Clear();
        }
    }
    void ClearSelection()
    {

        if (SelectObject.Count > 0)
        {
            foreach (GameObject obj in SelectObject)
            {
                obj.GetComponent<ClickOn>().currentselect = false;
                obj.GetComponent<ClickOn>().ClickMe();
            }
            SelectObject.Clear();
        }
    }
}

