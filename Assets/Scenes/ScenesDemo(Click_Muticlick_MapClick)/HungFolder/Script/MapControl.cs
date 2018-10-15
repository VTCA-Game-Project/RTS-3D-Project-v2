using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour {
    [HideInInspector]
    public Map battlefield;

    private List<CubeManager> ListGO = new List<CubeManager>();
    public GameObject modelGO;


    public Vector2 MapCellSize;
	// Use this for initialization
	void Start () {
        battlefield = new Map();

        for(int i=0;i<battlefield.width;i++)
        {
            for(int j=0;j<battlefield.height;j++)
            {
                var newGO = Instantiate(modelGO, new Vector3(i * MapCellSize.x, 0, j * MapCellSize.y), Quaternion.identity);
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
