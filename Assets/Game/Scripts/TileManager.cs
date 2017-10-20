using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour {

	public const int TILE_TYPE_1 = 1;
	public const int TILE_TYPE_2 = 2;
	public const int TILE_TYPE_3 = 3;
	public const int TILE_TYPE_4 = 4;
	public const int TILE_TYPE_5 = 5;
	public const int TILE_TYPE_6 = 6;
	public const int TILE_TYPE_7 = 7;
	public const int TILE_TYPE_8 = 8;
	public const int TILE_TYPE_9 = 9;

	int clicked;
	int clicked1;
	int clicked2;
	int tileNum;

	bool notMatch = false;

	void Start() {
		clicked = 0;
		EventBroadcaster.Instance.AddObserver (EventNames.ON_TILE_CLICKED, this.OnTileClicked);
	}

	void Update() {
		Debug.Log ("Not Match: " + notMatch);
		if (notMatch) {
			notMatch = false;
			StartCoroutine (NotMatch ());
		}
	}

	public int[] RequestTiles(int tileNum) {
		this.tileNum = tileNum;

		int numToGenerate = tileNum / 2;

		int[] tiles = new int[tileNum];

		List<int> arrayLocations = new List<int> ();

		for (int i = 0; i < tileNum; i++) {
			arrayLocations.Add(i);
		}

		for (int i = 0; i < numToGenerate; i++) {
			int tileToGenerate = Random.Range (1, 10);


			// Repeat two times to generate pair tiles
			int index = Random.Range(0, arrayLocations.Count);
			tiles [arrayLocations[index]] = tileToGenerate;
			arrayLocations.RemoveAt(index);

			index = Random.Range(0, arrayLocations.Count);
			tiles [arrayLocations[index]] = tileToGenerate;
			arrayLocations.RemoveAt(index);
		}
			
		return tiles;
	}

	public void OnTileClicked(Parameters parameters) {
		if (clicked == 0) {
			clicked1 = parameters.GetIntExtra (KeyNames.KEY_TILE_TYPE, 1);
			clicked++;
		}
		else if (clicked == 1){
			clicked2 = parameters.GetIntExtra (KeyNames.KEY_TILE_TYPE, 1);
			clicked = 0;

			Debug.Log ("Clicked1: " + clicked1 + " Clicked2: " + clicked2);

			if (clicked1 != clicked2) {
				notMatch = true;
			}
			else {
				// MATCHED
				Parameters checkParams = new Parameters ();
				checkParams.PutExtra (KeyNames.KEY_IS_MATCH, true);
				EventBroadcaster.Instance.PostEvent (EventNames.ON_TILES_CHECKED, checkParams);
				EventBroadcaster.Instance.PostEvent (EventNames.ON_UPDATE_SCORE);

				tileNum -= 2;
				if (tileNum == 0) {
					EventBroadcaster.Instance.PostEvent (EventNames.ON_FINISH_LEVEL);
				}
			}
		}
	}

	IEnumerator NotMatch() {
		Parameters checkParams = new Parameters ();
		checkParams.PutExtra (KeyNames.KEY_IS_MATCH, false);
		yield return new WaitForSeconds(0.5f);
		EventBroadcaster.Instance.PostEvent (EventNames.ON_TILES_CHECKED, checkParams);
	}
}
