using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KinectInputMode : BaseInputModule {

    public static KinectInputMode Current { get; set; }

    public override void Process()
    {
        foreach (var item in mController)
        {
            item.Process();
        }
    }
    protected override void Awake()
    {
        Current = this;
    }
    
    
    public override void UpdateModule()
    {
        base.UpdateModule();
    }
    private List<KinectPointer> mController = new List<KinectPointer>();
   
    public void SetPointer(KinectPointer point)
    {
        mController.Add(point);
    }

    public void RemovePointer(KinectPointer point)
    {
        if(mController.Contains(point))
        {
            mController.Remove(point);
        }
    }
}
