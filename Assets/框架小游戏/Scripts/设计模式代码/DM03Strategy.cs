using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 策略模式
/// </summary>
public class DM03Strategy:MonoBehaviour
{
	private void Start()
	{
		StrategyContext context = new StrategyContext();
		context.strategy = new ConcreteStrategyA();//只改这里就好了
		context.Cal();
	}
	
	

}
/// <summary>
/// 拥有者
/// </summary>
public class StrategyContext
{
	public IStrategy strategy ;
	public void Cal()
	{
		strategy.Cal();
	}
}
public interface IStrategy
{
	void Cal();
}

public class ConcreteStrategyA : IStrategy
{
	public void Cal()
	{
		Debug.Log("使用A策略计算");
	}
}
public class ConcreteStrategyB : IStrategy
{
	public void Cal()
	{
		Debug.Log("使用B策略计算");
	}
}


