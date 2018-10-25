using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnMap : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private Camera raycam;

    public GameObject effect;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
            }
            else
            {
                ClickMap(GetObjectPosition());
            }
        }
    }
    public void ClickMap(Vector3 taget)
    {
       
            var minieffect = Instantiate(effect, taget, Quaternion.identity);


            Destroy(minieffect, 0.3f);
        
    }
    private Vector3 GetObjectPosition()
    {
        Vector2 pos = Input.mousePosition;
        Ray ray = raycam.ScreenPointToRay(new Vector3(pos.x, pos.y, 0.0f));
        RaycastHit hit;
        Physics.Raycast(ray, out hit,Mathf.Infinity,LayerMask.GetMask("Place","Floor"));
        return hit.point;
    }
}
