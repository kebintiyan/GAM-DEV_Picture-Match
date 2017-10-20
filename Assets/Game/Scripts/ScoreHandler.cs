using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {

	[SerializeField] Text scoreText;
	private int currentScore;

	// Use this for initialization
	void Start () {
		scoreText.text = currentScore.ToString();
		EventBroadcaster.Instance.AddObserver (EventNames.ON_UPDATE_SCORE, this.OnUpdateScore);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnUpdateScore() {
		currentScore++;
		scoreText.text = currentScore.ToString();
	}
}
