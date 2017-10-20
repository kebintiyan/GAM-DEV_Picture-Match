using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : View {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClickResume() {
		this.Hide ();
		EventBroadcaster.Instance.PostEvent (EventNames.ON_UNPAUSE);
	}

	public void OnClickMainMenu() {
		LoadManager.Instance.LoadScene (SceneNames.MAIN_MENU_SCENE);
	}
}
