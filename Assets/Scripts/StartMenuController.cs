//#define LOG_TRACE_INFO
//#define LOG_EXTRA_INFO

using UnityEngine;
using System.Collections;

//------------------------------------------------------------------------------
// class definition
//------------------------------------------------------------------------------
public class StartMenuController : MonoBehaviour
{
	private static StartMenuController menuController;

	//--------------------------------------------------------------------------
	// public static methods
	//--------------------------------------------------------------------------
	//--------------------------------------------------------------------------
	// protected mono methods
	//--------------------------------------------------------------------------
	protected void Awake()
	{
		menuController = this;
	}
	
	protected void OnDestroy()
	{
		if(menuController != null)
		{
			menuController = null;
		}
	}
	
	protected void OnDisable()
	{
	}
	
	protected void OnEnable()
	{
	}
	
	protected void Start()
	{
	}
	
	protected void Update()
	{
		
		if(UnityEngine.Input.GetButtonDown("Fire1") == true)
		{
			MainController.SwitchScene("Game Scene");
		}
	}

	//--------------------------------------------------------------------------
	// private methods
	//--------------------------------------------------------------------------
}