using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MT
{
    public static class TransformExtensions
    {
        public static bool BelongsToHierarchy(this Transform subject, Transform hierarchy)
        {
            if (subject == null)
                return false;
            else if (subject == hierarchy)
                return true;
            else
                return BelongsToHierarchy(subject.transform.parent, hierarchy);
        }
    }
}

[RequireComponent(typeof(EventSystem))]
public class KinectPointer : MonoBehaviour {

    public interface IWatcher
    {
        void Process(PointerEventData pointerData, List<RaycastResult> raycastResults, KinectMouse mouse);
    }
    IWatcher[] __watchers = null;
    IWatcher[] m_watchers
    {
        get
        {
            if (__watchers == null)
            {
                __watchers = new IWatcher[] { new EnterExit(), new Click(), new Drag(), new SelectDeselect(GetSelectedObject, NewObjectSelected), new Scroll(ShowWheel) };
            }
            return __watchers;
        }
    }
    EventSystem __eventSystem;
    EventSystem m_eventSystem { get { return __eventSystem ?? (__eventSystem = GetComponent<EventSystem>()); } }
    private GameObject GetSelectedObject()
    {
        return m_eventSystem.currentSelectedGameObject;
    }

    private void NewObjectSelected(GameObject obj)
    {
        m_eventSystem.SetSelectedGameObject(obj);
    }

    private void ShowWheel(bool obj)
    {
       
    }
    PointerEventData m_pointerData = null;

    public KinectMouse m_device;

    public void Process()
    {
        if (m_device == null)
        {
            this.enabled = false;
            return;
        }else
        {
            if(!m_device.gameObject.activeSelf||m_device.enabled==false)
            {
                this.enabled = false;
                return;
            }
        }
        this.enabled = true;

        //Valve.VR.VRControllerState_t s = m_device.GetState();
        //Debug.LogFormat("{0},{1} - {2},{3} - {4},{5} - {6},{7} - {8},{9}", s.rAxis0.x, s.rAxis0.y, s.rAxis1.x, s.rAxis1.y, s.rAxis2.x, s.rAxis2.y, s.rAxis3.x, s.rAxis3.y, s.rAxis4.x, s.rAxis4.y);
        //Debug.LogFormat("{0}", m_device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger));

        if (m_pointerData == null)
        {
            m_pointerData = new PointerEventData(m_eventSystem);
            m_pointerData.pointerId = m_device.pointerId;
        }

     
        List<RaycastResult> raycastResults = RaycastAll(ref m_pointerData);

    
        foreach (IWatcher i in m_watchers)
        {
            i.Process(m_pointerData, raycastResults, m_device);
        }

    }
    Transform m_trackedObjectTransform;

    List<RaycastResult> RaycastAll(ref PointerEventData pointerData)
    {
        pointerData.pointerCurrentRaycast = new RaycastResult()
        {
            worldPosition = m_trackedObjectTransform.position,
            worldNormal = Vector3.forward
        };

        List<RaycastResult> raycastResult = new List<RaycastResult>();
        m_eventSystem.RaycastAll(pointerData, raycastResult);
        return raycastResult;
    }
    public void Start()
    {
        m_trackedObjectTransform = m_device.transform;
        KinectInputMode.Current.SetPointer(this);
    }
    private void OnDestroy()
    {
        KinectInputMode.Current.RemovePointer(this);
    }

}
