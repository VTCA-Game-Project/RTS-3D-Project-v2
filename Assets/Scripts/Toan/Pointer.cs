using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public static Pointer Instance;
  
    public Camera rtsCamera;
 
    private RaycastHit hitInfo;
    private Ray ray;
   
   
   


    #region Properties
 
   

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if( Instance != null) Destroy(Instance.gameObject);
    }

   
    public Vector3 Position
    {
        get
        {
            return Vector3.ProjectOnPlane(transform.position, Vector3.up);
        }
        set
        {
            transform.position = value;
        }
    }
    #endregion
    private void Update()
    {



    }




   

    public void PutPointer()
    {
        ray = rtsCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray: ray,
                            hitInfo: out hitInfo,
                            maxDistance: Mathf.Infinity,
                            layerMask: LayerMask.GetMask("Place")))
        {
            Position = hitInfo.point;
        }
        else
        {
            Debug.Log("Hit false");
        }
    }
}
