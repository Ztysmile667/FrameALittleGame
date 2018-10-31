using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KinectWWControll : MonoBehaviour
{
    public Canvas canvas;
    public Image RightImage;
    public Image btn;
    private bool isAgent;

    public RawImage UserTextue;
	// Use this for initialization
	void Awake ()
	{
        //判断设备是否准备好
	    isAgent = KinectManager.Instance.IsInitialized();

	}
	
	// Update is called once per frame
	void Update () {
	    if (isAgent )
	    {
	        if (UserTextue.texture == null)
	        {
	            //获取彩色数据
	            // Texture2D texture = KinectManager.Instance.GetUsersClrTex();
	            //获取深度数据
	            Texture2D texture = KinectManager.Instance.GetUsersLblTex();
	            UserTextue.texture = texture;
            }
	     
           //判断是否检测到玩家
	        if (KinectManager.Instance.IsUserDetected())
	        {
	            //获取玩家的ID
	            long userId = KinectManager.Instance.GetPrimaryUserID();
                //获取右手
	            int jointType = (int) KinectInterop.JointType.HandRight;
                //是否追踪到关节点
	            if (KinectManager.Instance.IsJointTracked(userId,jointType))
	            {
                    //获取右手位置
	                Vector3 rightHandpos = KinectManager.Instance.GetJointKinectPosition(userId, jointType);
                    //把右手的位置转化到屏幕坐标上
	                Vector3 RightSceenpos = Camera.main.WorldToScreenPoint(rightHandpos);
                    Vector2 rightui = new Vector2(RightSceenpos.x,RightSceenpos.y);
                    Vector2 righthanduipos;
                    ////判断是否在ui上
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) canvas.transform,
	                    rightui, null, out righthanduipos))
	                {
                        //将手的位置复制给ui
                        RectTransform  rightrect  = RightImage.transform as RectTransform;
	                    rightrect.anchoredPosition = righthanduipos;

	                }
                    //判断手的ui是否在目标ui上
	                if (RectTransformUtility.RectangleContainsScreenPoint(btn.rectTransform,rightui,null))
	                {
                        //获取右手手势
	                    KinectInterop.HandState rightHanstate = KinectManager.Instance.GetRightHandState(userId);
	                    if (rightHanstate == KinectInterop.HandState.Closed)
	                    {
	                      
	                    }

	                }
	                else
	                {

	                }
                }
	            
	        }
	    }
	}
}
