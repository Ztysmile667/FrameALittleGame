using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MT;
using UnityEngine;
using UnityEngine.EventSystems;


public class EnterExit : KinectPointer.IWatcher
{
    public void Process(PointerEventData pointerData, List<RaycastResult> raycastResults, KinectMouse device)
    {
        if (pointerData.pointerEnter == null)
        {
            foreach (RaycastResult raycast in raycastResults)
            {
                GameObject targetGO = ExecuteEvents.ExecuteHierarchy(raycast.gameObject, pointerData, ExecuteEvents.pointerEnterHandler);
                if (targetGO != null)
                {
                    pointerData.pointerCurrentRaycast = raycast;
                    pointerData.pointerEnter = targetGO;
                    device.count = true;
                    break;
                }
            }
        }
        else
        {
            // Needs to be first result on the list, a new object can come on front and thats the one which is overed now (it happens in the dropdown)
            if (raycastResults.Count == 0 || !raycastResults[0].gameObject.transform.BelongsToHierarchy(pointerData.pointerEnter.transform))
            {
                ExecuteEvents.ExecuteHierarchy(pointerData.pointerEnter, pointerData, ExecuteEvents.pointerExitHandler);
                pointerData.pointerEnter = null;
                device.count = false;
            }
        }
    }
}

