using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnMap : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private Camera raycam;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ClickMap(GetObjectPosition());
        }
    }
    public void ClickMap(Vector3 taget )
    {
        transform.position = taget;
    }
    private Vector3 GetObjectPosition()
    {
        Vector2 pos = Input.mousePosition;
        Ray ray = raycam.ScreenPointToRay(new Vector3(pos.x, pos.y, 0.0f));
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.point;
    }
}
