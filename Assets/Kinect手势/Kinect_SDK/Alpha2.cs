using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(EventSystem))]
public class Alpha2 : BaseInputModule {

    PointerEventData pointerData;
    EventSystem __eventSystem;
    EventSystem m_eventSystem { get { return __eventSystem ?? (__eventSystem = GetComponent<EventSystem>()); } }
    public override void Process()
    {
       if(pointerData==null)
        {
            pointerData = new PointerEventData(m_eventSystem);
            pointerData.pointerId = this.transform.GetSiblingIndex() + 255;

        }
        Debug.Log(pointerData.pointerId);
    }

    GameObject GetCurrentGameObject()
    {
        if (pointerData != null && pointerData.enterEventCamera != null)
        {
            return pointerData.pointerCurrentRaycast.gameObject;
        }

        return null;
    }
}
