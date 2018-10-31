using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DM01Sate : MonoBehaviour
{
	void Start()
	{
		//状态拥有者
		Context context = new Context();
		//设置默认状态
		context.SetState(new ConcreteStateA(context));

		context.Handle(5);
		context.Handle(20);
		context.Handle(30);
		context.Handle(5);
	}
}
public class Context
{
	private IState m_state;
	//可以在外部设置context的状态
	public void SetState(IState state)
	{
		m_state = state;
	}
	//可以执行IState状态时的Handle方法
	public void Handle(int arg)
	{
		m_state.Handle(arg);
	}
}

public interface IState
{
	void Handle(int args);
}

//两个状态实现的子类
public class ConcreteStateA : IState
{
	private Context m_Context;
	public ConcreteStateA(Context context)
	{
		m_Context = context;
	}

	public void Handle(int args)
	{
		Debug.Log("ConcreateStateA.Handle" + args);
		if (args > 10)
		{
			//切换状态
			m_Context.SetState(new ConcreteStateB(m_Context));
		}
	}
}

public class ConcreteStateB : IState
{
	private Context m_Context;
	public ConcreteStateB(Context context)
	{
		m_Context = context;
	}

	public void Handle(int args)
	{
		Debug.Log("ConcreateStateB.Handle" + args);
		if (args <= 10)
		{
			//
			m_Context.SetState(new ConcreteStateA(m_Context));
		}
	}
}
