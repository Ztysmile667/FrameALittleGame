using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using MT;

public class Click : KinectPointer.IWatcher
{
    private bool sended = false;
    public void Process(PointerEventData pointerData, List<RaycastResult> raycastResults, KinectMouse device)
    {
        pointerData.eligibleForClick = device.processValue>=device.invidTime;
        if(pointerData.eligibleForClick==false)
        {
            sended = false;
        }
        pointerData.rawPointerPress = raycastResults.Count == 0 ? null : raycastResults[0].gameObject;

        if (pointerData.eligibleForClick)
        {

            foreach (RaycastResult raycast in raycastResults)
            {
                if(!sended)
                {
                    if (ExecuteEvents.Execute(raycast.gameObject, pointerData, ExecuteEvents.pointerClickHandler))
                    {
                        pointerData.pointerPressRaycast = raycast;
                        pointerData.pointerPress = raycast.gameObject;
                        pointerData.pressPosition = pointerData.position;
                        sended = true;
                        break;

                    }
                }
            }
        }
    }

}

