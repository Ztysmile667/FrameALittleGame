using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MT;
using UnityEngine;
using UnityEngine.EventSystems;


public class Scroll : KinectPointer.IWatcher
{
    Action<bool> m_showWheel;
    public Scroll(Action<bool> showWheel)
    {
        m_showWheel = showWheel;
    }
    public void Process(PointerEventData pointerData, List<RaycastResult> raycastResults, KinectMouse device)
    {
        bool showWheel = false;
        foreach (RaycastResult raycast in raycastResults)
        {
            if (ExecuteEvents.CanHandleEvent<IScrollHandler>(raycast.gameObject))
            {
                showWheel = true;
            }
            if (pointerData.scrollDelta != Vector2.zero)
            {
                GameObject targetGO = ExecuteEvents.ExecuteHierarchy(raycast.gameObject, pointerData, ExecuteEvents.scrollHandler);
                if (targetGO != null)
                {
                    showWheel = true;
                }
            }
        }
        m_showWheel(showWheel);
    }
}
