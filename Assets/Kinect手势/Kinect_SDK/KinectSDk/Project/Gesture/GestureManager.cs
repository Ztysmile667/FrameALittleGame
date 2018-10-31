using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GestureManager
{

    private GestureManager()
    {
        Resgister();
    }
    private static GestureManager instance;
    public static GestureManager Instance
    {
        get
        {
            if (instance == null) instance = new GestureManager();
            return instance;
        }
    }

    private Dictionary<KinectGestures.Gestures, GestureBase> gesturesType = new Dictionary<KinectGestures.Gestures, GestureBase>();

    public void Resgister()
    {
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var item in types)
        {
            if (typeof(GestureBase).IsAssignableFrom(item) && item.Name != typeof(GestureBase).Name)
            {
                GestureBase g = Activator.CreateInstance(item) as GestureBase;
                gesturesType.Add(g.gesture, g);
            }
        }
    }

   public GestureBase GetGesture(KinectGestures.Gestures gesture)
    {
        if(gesturesType.ContainsKey(gesture))
        {
            return gesturesType[gesture];
        }
        return null;
    }
}
