using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

	[SerializeField] Image tileImage;
	[SerializeField] Text questionText;
	[SerializeField] int tileType;
	private bool active;
	private bool matched;

	private bool removeInstance;

	// Use this for initialization
	void Start () {
		active = true;
		tileImage.enabled = false;
		removeInstance = false;
		matched = false;

		EventBroadcaster.Instance.AddObserver (EventNames.ON_TOGGLE_ACTIVE, this.OnToggleActive);
	}
	
	// Update is called once per frame
	void Update () {
		if (removeInstance) {
			removeInstance = false;
			StartCoroutine (RemoveObserver());
		}
	}

	public void OnClick() {
		if (active) {
			tileImage.enabled = true;
			questionText.enabled = false;
			active = false;

			Parameters parameters = new Parameters ();
			parameters.PutExtra (KeyNames.KEY_TILE_TYPE, this.tileType);

			EventBroadcaster.Instance.AddObserver (EventNames.ON_TILES_CHECKED, this.OnTilesChecked);
			EventBroadcaster.Instance.PostEvent (EventNames.ON_TILE_CLICKED, parameters);
		}
	}

	public void OnTilesChecked(Parameters args) {
		bool match = args.GetBoolExtra (KeyNames.KEY_IS_MATCH, false);

		if (!match) {
			this.active = true;
			this.tileImage.enabled = false;
			this.questionText.enabled = questionText.enabled = true;
		}
		else {
			this.active = false;
			matched = true;
			tileImage.enabled = true;
			questionText.enabled = false;
		}

		removeInstance = true;

	}

	public void OnToggleActive() {
		Debug.Log ("Toggled");
		if (!matched) {
			this.active = !this.active;
		}
	}

	IEnumerator RemoveObserver() {
		yield return new WaitForSeconds (0.1f);
		EventBroadcaster.Instance.RemoveActionAtObserver(EventNames.ON_TILES_CHECKED, this.OnTilesChecked);
	}
}
