using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

	[SerializeField] Image tileImage;
	[SerializeField] Text questionText;
	[SerializeField] int tileType;
	private bool active;

	private bool removeInstance;

	// Use this for initialization
	void Start () {
		active = true;
		tileImage.enabled = false;
		removeInstance = false;
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
			tileImage.enabled = !tileImage.enabled;
			questionText.enabled = !questionText.enabled;
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
			this.tileImage.enabled = !tileImage.enabled;
			this.questionText.enabled = !questionText.enabled;
		}

		removeInstance = true;

	}

	public void OnToggleActive() {
		
	}

	IEnumerator RemoveObserver() {
		yield return new WaitForSeconds (0.1f);
		EventBroadcaster.Instance.RemoveActionAtObserver(EventNames.ON_TILES_CHECKED, this.OnTilesChecked);
	}
}
