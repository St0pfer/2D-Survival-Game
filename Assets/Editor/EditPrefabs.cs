using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class EditPrefabs : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
       // Invoke("CreatePrefabs",10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreatePrefabs()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject Obj in allObjects)
        {
            if (Obj.transform.parent == null && Obj.layer != 5)
            {
                Object prefab = UnityEditor.PrefabUtility.CreateEmptyPrefab("Assets/Resources/AllPrefabs/" + Obj.name + ".prefab");
               // PrefabUtility.ReplacePrefab(Obj, prefab);
            }
        }
    }
}
