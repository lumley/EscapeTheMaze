//#define LOG_TRACE_INFO
//#define LOG_EXTRA_INFO

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//------------------------------------------------------------------------------
// class definition
//------------------------------------------------------------------------------
public class StartMenuController : MonoBehaviour
{
	public Canvas quitMenu;
	public Button startButton;
	public Button exitButton;
	
	private static StartMenuController menuController;

	//--------------------------------------------------------------------------
	// public static methods
	//--------------------------------------------------------------------------
	public void ShowQuitMenu(){
		quitMenu.enabled=true;
		startButton.enabled=false;
		exitButton.enabled=false;
	}
	
	public void LeaveQuitMenu(){
		quitMenu.enabled=false;
		startButton.enabled=true;
		exitButton.enabled=true;
	}
	
	public void StartPlaying(){
		MainController.SwitchScene("Game Scene");
	}
	
	public void QuitGame(){
		Application.Quit();
	}
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
		quitMenu.enabled=false;
	}
	
	protected void Update()
	{
		
	}

	//--------------------------------------------------------------------------
	// private methods
	//--------------------------------------------------------------------------
}