using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MT;
using UnityEngine;
using UnityEngine.EventSystems;


public class SelectDeselect : KinectPointer.IWatcher
{
    Func<GameObject> m_onGetSelectObject;
    Action<GameObject> m_onSetSelectObject;
    public SelectDeselect(Func<GameObject> onGetSelectObject, Action<GameObject> onSetSelectObject)
    {
        m_onGetSelectObject = onGetSelectObject;
        m_onSetSelectObject = onSetSelectObject;
    }

    public const float minPressValue = 0.1f;
    public void Process(PointerEventData pointerData, List<RaycastResult> raycastResults, KinectMouse device)
    {
        float triggerValue = device.processValue;
        if (triggerValue >= minPressValue)
        {
            GameObject targetGO = null;
            foreach (RaycastResult raycast in raycastResults)
            {

                targetGO = ExecuteEvents.ExecuteHierarchy(raycast.gameObject, pointerData, ExecuteEvents.selectHandler);
                if (targetGO != null)
                {
                    break;
                }
            }

            if (targetGO == null)
            {
                GameObject currentSelected = m_onGetSelectObject();
                if (currentSelected != null)
                {
                    ExecuteEvents.ExecuteHierarchy(currentSelected, pointerData, ExecuteEvents.deselectHandler);
                }
                device.processValue = 0f;
            }
            m_onSetSelectObject(targetGO);
        }
    }
}

