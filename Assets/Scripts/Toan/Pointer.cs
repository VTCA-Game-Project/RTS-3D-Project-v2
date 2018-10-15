using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public static Pointer Instance;
  
    public Camera rtsCamera;
    private Vector2 LatPoint;
    private RaycastHit hitInfo;
    private Ray ray;
   
    private List<CubeManager> ListCubeSelected = new List<CubeManager>();
    private List<CubeManager> ListInSelect = new List<CubeManager>();

    private List<CubeManager> ListBuildCube = new List<CubeManager>();


    public bool OnselectTaget = false;

    private bool OnholdTaget = false;


    [HideInInspector]

    public Vector2 BuildSize = new Vector2();
    #region Properties
 
    public GameObject Map;
    private MapControl ControlMap = new MapControl();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if( Instance != null) Destroy(Instance.gameObject);
    }

    public void Start()
    {
        if (Map != null)
        {
            ControlMap = Map.GetComponent<MapControl>();
        }
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


       


        if (Input.GetMouseButtonUp(0))
        {
            ray = rtsCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray:            ray,
                                hitInfo:        out hitInfo,
                                maxDistance:    Mathf.Infinity,
                                layerMask:      LayerMask.GetMask("Place")))
            {
                Position = hitInfo.point;
               
            }
        }



        if (OnselectTaget == true)
        {
            foreach (CubeManager cube in ListInSelect)
            {

                Debug.Log(CheckBuildedCude(cube));
                if (CheckBuildedCude(cube))
                {

                    foreach (CubeManager _cube in ListInSelect)
                    {
                        _cube.SetState("Not");
                        _cube.OnraycastIn();
                    }
                    break;
                }
                else
                {
                    cube.SetState("Can");
                    cube.OnraycastIn();


                    foreach (CubeManager _cube in ListBuildCube)
                    {
                        _cube.SetState("Was");
                        _cube.OnraycastIn();
                    }
                    
                }

            }

            ray = rtsCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray: ray,
                                hitInfo: out hitInfo,
                                maxDistance: Mathf.Infinity,
                                layerMask: LayerMask.GetMask("Place")))
            {

                CubeManager cube = hitInfo.collider.gameObject.GetComponent<CubeManager>();


                if ( cube != null)
                {

                    setBuildSize(BuildSize, cube);

                }
                foreach (CubeManager _cube in ListCubeSelected)
                {

                    if (_cube.CodeLocal != cube.CodeLocal && !checkCubetaget(_cube) && !CheckBuildedCude(_cube))

                    {
                        _cube.SetState("None");
                        _cube.OnraycastIn();

                    }


                }
                if (ListCubeSelected.Count >= 100)
                {

                    for (int k = 0; k < ListInSelect.Count; k++)
                    {
                        ListInSelect[k].SetState("None");
                        ListInSelect[k].OnraycastIn();
                    }
                    ListCubeSelected = new List<CubeManager>();
                    ListInSelect = new List<CubeManager>();
                }





            }


            if (OnholdTaget)
            {

                if (Input.GetMouseButtonDown(0) && select() == false)
                {
                    return;
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        for (int k = 0; k < ListInSelect.Count; k++)
                        {
                          
                            ListInSelect[k].CanBuild = false;
                          
                            ListBuildCube.Add(ListInSelect[k]);
                        }
                        ListCubeSelected = new List<CubeManager>();
                        ListInSelect = new List<CubeManager>();
                        ResetTaget();
                    }
                }

                      
                    
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnholdTaget = true;
            }



        }



    }




    private void setBuildSize(Vector2 size, CubeManager currentpoint)
    {

        if (LatPoint != currentpoint.CodeLocal)
        {

            ListInSelect = new List<CubeManager>();

            if (!CheckBuildedCude(currentpoint))
            {
                LatPoint = currentpoint.CodeLocal;               
                ListInSelect.Add(currentpoint);
            }

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {

                    CubeManager sub = ControlMap.GetCubeBylocal(currentpoint.CodeLocal + new Vector2(i, j));

                        sub.SetState("Can");
                        ListCubeSelected.Add(sub);
                        ListInSelect.Add(sub);
                 


                }
            }
        }
    }

   

    private bool select()
    {
        for (int i = 0; i < ListInSelect.Count; i++)
        {
            if (ListInSelect[i].CanBuild==false)
            {
                Debug.Log(ListInSelect[i].name);
                return false;
            }
        }
        return true;
    }

    private bool checkCubetaget(CubeManager _cube)
    {

        for (int i = 0; i < ListInSelect.Count; i++)
        {
            if (_cube.CodeLocal == ListInSelect[i].CodeLocal)
            {
                return true;
            }
        }


        return false;
    }
    private bool CheckBuildedCude(CubeManager _cube)
    {
       
      foreach(CubeManager cube in ListBuildCube)
        {
            if (_cube.CodeLocal==cube.CodeLocal)
            {
                return true;
            }
        }

        return false;
    }

    private void SetBack()
    {
        for (int i = 0; i < ListBuildCube.Count; i++)
        {
            ListBuildCube[i].SetState("Was");
            ListBuildCube[i].OnraycastIn();
        }
    }


    public void ResetTaget()
    {
        OnselectTaget = false;
        OnholdTaget = false;
        BuildSize = new Vector2();

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
