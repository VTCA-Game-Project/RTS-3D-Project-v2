using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    private Camera rtsCamera;

    private RaycastHit hitInfo;
    private Ray ray;

    #region Properties
    // using projection
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
        if (Input.GetMouseButtonUp(0))
        {
            ray = rtsCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray:            ray,
                                hitInfo:        out hitInfo,
                                maxDistance:    100.0f,
                                layerMask:      LayerMask.GetMask("Place")))
            {
                Position = hitInfo.point;
            }
        }

    }
}
