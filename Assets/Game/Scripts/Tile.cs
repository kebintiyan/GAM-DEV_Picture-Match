using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

	[SerializeField] Image tileImage;
	[SerializeField] Text questionText;
	[SerializeField] int tileType;
	private bool active;

	// Use this for initialization
	void Start () {
		active = true;
		tileImage.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick() {
		if (active) {
			tileImage.enabled = !tileImage.enabled;
			questionText.enabled = !questionText.enabled;
			active = false;

			Parameters parameters = new Parameters ();
			parameters.PutExtra (KeyNames.KEY_TILE_TYPE, this.tileType);

			EventBroadcaster.Instance.PostEvent (EventNames.ON_TILE_CLICKED);
		}
	}

	public void OnTilesChecked() {
		
	}
}
