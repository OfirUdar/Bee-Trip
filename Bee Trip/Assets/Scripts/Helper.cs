using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper 
{
   public static GameObject FindChildByTag(Transform parent,string tag)
    {
        foreach(Transform child in parent)
        {
            if (child.CompareTag(tag))
                return child.gameObject;
        }
        return null;
    }

    public static Transform[] FindChildsByTag(Transform parent, string tag)
    {
        List<Transform> l=new List<Transform>();
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
                l.Add(child);
        }   
        return l.ToArray();
    }


    public static GameObject FindChildByType<T>(Transform parent)
    {
        foreach (Transform child in parent)
        {   
            if(child.GetComponent<T>()!=null)
                return child.gameObject;
               
        }
        return null;
    }

    public static List<T> FindChildsByType<T>(Transform parent)
    {
        List<T> l = new List<T>();
        foreach (Transform child in parent)
        {
            T component = child.GetComponent<T>();
            if (component != null)
                l.Add(component);
        }
        return l;
    }
}
