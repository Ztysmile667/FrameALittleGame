using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alpha3 : MonoBehaviour
{
    public Button btn;
    // Use this for initialization
    void Start()
    {
        btn.onClick.AddListener(()=>
        {
            OnClick(btn);
        });
        PlayerManager.Instance.OnNewPlayerAddEvent += Instance_OnSetPrimaryPlayerEvent;
    }

    private void Instance_OnSetPrimaryPlayerEvent(Player player)
    {
        KinectMouse k = KinectMouse.SetMouse(player, KinectInterop.JointType.HandRight);
        k.transform.SetParent(transform);
        k = KinectMouse.SetMouse(player, KinectInterop.JointType.HandLeft);
        k.transform.SetParent(transform);
    //    Invoke("hide", 10);
    }

    private void OnClick(Button button)
    {
        Debug.Log(this+"   "+ button.name);
    }

    // Update is called once per frame
    void Update()
    {

    }
    KinectMouse[] k;
    public void hide()
    {
        k = MonoBehaviour.FindObjectsOfType<KinectMouse>();
        if(k!=null)
        {
            foreach (var item in k)
            {
                item.Hide();
            }
        }
        Invoke("show", 10);
    }

    public void show()
    {
        if (k != null)
        {
            foreach (var item in k)
            {
                item.Show();
            }
        }
    }
}
