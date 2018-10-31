using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KinectMouse : MonoBehaviour
{
    public float processValue;
    [Tooltip("跟踪的关节点")]
    [Header("跟踪的关节点")]
    public KinectInterop.JointType jointType;
    private static int numid = 0;
    private int id = -1;
    public int pointerId
    {
        get
        {
            if (id == -1)
            {
                if (numid > int.MaxValue) numid = 0;
                id = numid++;
            }

            return id;
        }
    }
    private float mouseX, mouseY;
    public float invidTime = 3f;
    private RectTransform rectTransform;

    public bool count { get; set; }

    public Player player { get; set; }

    public float scale = 1.382f;
    public Image fill;
    public KinectPointer pointer;
    private static UnityEngine.Object source;

    public static KinectMouse SetMouse(Player _player, KinectInterop.JointType type, int w = 128, int h = 128)
    {
        if (source == null) source = Resources.Load("KMouse");
        if (source != null)
        {
            KinectMouse k = (Instantiate(source) as GameObject).GetComponent<KinectMouse>();
            k.player = _player;
            k.jointType = type;
            k.pointer = EventSystem.current.gameObject.AddComponent<KinectPointer>();
            k.pointer.m_device = k;
            k.player.DisposeEvent += k.Player_DisposeEvent;
            return k;
        }
        return null;
    }


    private void Awake()
    {
        mouseX = transform.localPosition.x;
        mouseY = transform.localPosition.y;
        rectTransform = transform as RectTransform;
    }

    public void Player_DisposeEvent()
    {
        DestroyImmediate(this.gameObject);
        Destroy(pointer);
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.gameObject.activeSelf)
        {
            count = false;
            processValue = 0f;
            return;
        }
        if (count)
        {
            if (processValue < invidTime)
            {
                processValue += Time.deltaTime;
            }
            else 
            {
            }
        }
        else
        {
            processValue = 0f;
        }
        
        if (player == null)
        {
            return;
        }
        if (KinectManager.Instance.IsJointTracked(player.userId, (int)jointType))
        {
            Vector3 posJoint = KinectManager.Instance.GetJointPosColorOverlay2D(player.userId, (int)jointType);
            posJoint.x *= scale;
            posJoint.y = posJoint.y * Screen.height / (float)Screen.width * scale;
            mouseX = (int)Mathf.SmoothStep(mouseX, posJoint.x, 0.618f);
            mouseY = (int)Mathf.SmoothStep(mouseY, posJoint.y, 0.618f);

            rectTransform.anchoredPosition3D = new Vector3(mouseX - Screen.width*0.5f, Screen.height * 0.5f - mouseY);
        }
        transform.SetAsLastSibling();
        if (fill)
        {
            fill.fillAmount = processValue/invidTime;
        }
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
}
