using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Prefabliste
{
    private static Prefabliste prefablist;

    public static Prefabliste Instance()
    {

        if (prefablist == null)
        {
            prefablist = new Prefabliste();
        }

        return prefablist;
    }

    private Prefabliste()
    {

    }

    public enum PrefabName { Axt, Hacke, Spitzhacke, Blatt }

    public GameObject GetGameObject(string name)
    {
        GameObject value = Resources.Load<GameObject>("AllPrefabs/" + name);      
        return value;
    }

    public GameObject GetGameObject(PrefabName name)
    {
        return Resources.Load<GameObject>("AllPrefabs/" + name);
    }





}





