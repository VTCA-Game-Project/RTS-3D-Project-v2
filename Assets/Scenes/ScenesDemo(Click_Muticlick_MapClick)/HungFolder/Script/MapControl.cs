using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour {
    [HideInInspector]
    public Map battlefield;

    private List<CubeManager> ListGO = new List<CubeManager>();
    public GameObject modelGO;
	// Use this for initialization
	void Start () {
        battlefield = new Map();

        for(int i=0;i<battlefield.width;i++)
        {
            for(int j=0;j<battlefield.height;j++)
            {

                //GameObject CellGO = new GameObject("Cell_" + i + "_" + j);


                //Vector3 Scale = modelGO.transform.localScale;
                //CellGO.transform.localScale = Scale;
                //CellGO.transform.position= new Vector3(i * Scale.x, 0, j * Scale.z);
                //BoxCollider CellBox = CellGO.AddComponent<BoxCollider>();


                //Renderer CellRen = CellGO.AddComponent<Renderer>();
                //CellGO.layer = 9;
                var newGO = Instantiate(modelGO, new Vector3(i * 10, 0, j * 10), Quaternion.identity);
                CubeManager CM = newGO.GetComponent<CubeManager>();
                CM.CodeLocal = new Vector2(i, j);
                ListGO.Add(CM);
                
               
            }
        }
        Debug.Log(ListGO.Count);
	}
	
    public CubeManager GetCubeBylocal(Vector2 local)
    {
       
      for(int i=0;i<ListGO.Count;i++)
        {
           
            if((int)ListGO[i].CodeLocal.x==(int)local.x&& (int)ListGO[i].CodeLocal.y==(int)local.y)
            {
                return ListGO[i];
            }
        }

      
        return null;
    }
   
	// Update is called once per frame
	void Update () {
		
	}
}
