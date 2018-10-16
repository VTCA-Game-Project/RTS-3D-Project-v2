using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tree_Control : MonoBehaviour {
    [HideInInspector]
    public Map Battlefile;
    private List<Vector2> LocalTreeList = new List<Vector2>();

    public GameObject OakTree;
    public GameObject FirTree;
    int lensx ; int lengy ;
    // Use this for initialization
    void Awake()
    {
        Battlefile = new Map();

        for (int i = 0; i < 200; i++)
        {
            int a = Random.Range(0, 99);
            int b = Random.Range(0, 99);

            if (a > 20 && a < 79 && b > 20 && b < 79)
            {
                var newGO = Instantiate(FirTree, new Vector3(a, 0f, b), Quaternion.identity);
                LocalTreeList.Add(new Vector2(a, b));

            }
        }


        for (int i = 0; i < Battlefile.width; i++)
        {
            for (int j = 0; j < Battlefile.height; j++)
            {

                if (i == 0 || j == 0 || j == Battlefile.height - 1 || i == Battlefile.width - 1)
                {
                    var newGO = Instantiate(FirTree, new Vector3(i, 0f, j), Quaternion.identity);
                    LocalTreeList.Add(new Vector2(i, j));
                }

             

            }
        }
    }

    public List<Vector2> GetTreeList()
    {
        return LocalTreeList;
    }
}
