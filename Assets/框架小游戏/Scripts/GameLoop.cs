using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {

	private SceneStateController controller = null;

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);//在每一个场景中都有，切换场景时不删除
	}
	// Use this for initialization
	void Start () {
		controller = new SceneStateController();
		controller.SetState(new StartState(controller),false);
	}
	
	// Update is called once per frame
	void Update () {
		controller.StateUpdate();
	}
}
