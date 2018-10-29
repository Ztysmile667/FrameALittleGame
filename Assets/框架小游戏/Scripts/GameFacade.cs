using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 外观和单例模式还是中介者
/// </summary>
public class GameFacade {
	private static GameFacade _instance = new GameFacade();

	private bool m_IsGameOver = false;//判断游戏是否结束

	public static GameFacade Instance { get { return _instance; } }

	private GameFacade() { }//把类的构造方法私有化，不让外部调用构造方法实例化

	public bool IsGameOver{ get { return m_IsGameOver; } }

	private ArchievementSystem m_ArchievementSystem;
	private CampSystem m_CampSystem;
	private CharacterSystem m_CharacterSystem;
	private EnergySystem m_EnergySystem;
	private GameEventSystem m_GameEventSystem;

	private CampInfoUI m_CampInfoUI;
	private GamePauseUI m_GamePauseUI;
	private GameStateInfoUI m_GameStateInfoUI;
	private SoldierInfoUI m_SoldierInfoUI;
	/// <summary>
	/// 初始化
	/// </summary>
	public void Init()
	{
		m_ArchievementSystem = new ArchievementSystem();
		m_CampSystem = new CampSystem();
		m_CharacterSystem = new CharacterSystem();
		m_EnergySystem = new EnergySystem();
		m_GameEventSystem = new GameEventSystem();

		m_CampInfoUI = new CampInfoUI();
		m_GamePauseUI = new GamePauseUI();
		m_GameStateInfoUI = new GameStateInfoUI();
		m_SoldierInfoUI = new SoldierInfoUI();

		m_ArchievementSystem.Init();
		m_CampSystem.Init();
		m_CharacterSystem.Init();
		m_EnergySystem.Init();
		m_GameEventSystem.Init();

		m_CampInfoUI.Init();
		m_GamePauseUI.Init();
		m_GameStateInfoUI.Init();
		m_SoldierInfoUI.Init();
	}
	/// <summary>
	/// 每帧进行调用，负责游戏的更新
	/// </summary>
	public void Update()
	{
		m_ArchievementSystem.Update();
		m_CampSystem.Update();
		m_CharacterSystem.Update();
		m_EnergySystem.Update();
		m_GameEventSystem.Update();

		m_CampInfoUI.Update();
		m_GamePauseUI.Update();
		m_GameStateInfoUI.Update();
		m_SoldierInfoUI.Update();
	}
	/// <summary>
	/// 切换到别的场景释放资源
	/// </summary>
	public void Release()
	{
		m_ArchievementSystem.Release();
		m_CampSystem.Release();
		m_CharacterSystem.Release();
		m_EnergySystem.Release();
		m_GameEventSystem.Release();

		m_CampInfoUI.Release();
		m_GamePauseUI.Release();
		m_GameStateInfoUI.Release();
		m_SoldierInfoUI.Release ();
	}
}
