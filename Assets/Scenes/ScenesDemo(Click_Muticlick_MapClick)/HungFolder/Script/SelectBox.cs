using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBox : MonoBehaviour {
    [SerializeField]
    private RectTransform selectSquareImages;
    [SerializeField]
    private Camera _camraycast;


    Vector3 startPos;
    Vector3 endPos;
    void Start () {

        selectSquareImages.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
	if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(_camraycast.ScreenPointToRay(Input.mousePosition),out hit,Mathf.Infinity))
            {
                startPos = hit.point;
            }
        }
    if(Input.GetMouseButtonUp(0))
        {
            selectSquareImages.gameObject.SetActive(false);
        }
    if(Input.GetMouseButton(0))
        {
            if(!selectSquareImages.gameObject.activeInHierarchy)
            {
                selectSquareImages.gameObject.SetActive(true);
            }
            endPos = Input.mousePosition;

            Vector3 squareStar = _camraycast.WorldToScreenPoint(startPos);
            squareStar.z = 0f;
            Vector3 centre = (squareStar + endPos) / 2f;
            selectSquareImages.position = centre;
            float sizex = Mathf.Abs(squareStar.x - endPos.x);
            float sizey = Mathf.Abs(squareStar.y - endPos.y);
            selectSquareImages.sizeDelta = new Vector2(sizex, sizey);
        }
	}
}
