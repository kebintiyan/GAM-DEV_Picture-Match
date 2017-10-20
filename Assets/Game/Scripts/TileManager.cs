using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager {

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

	public TileManager() {
		clicked = 0;
		EventBroadcaster.Instance.AddObserver (EventNames.ON_TILE_CLICKED, this.OnTileClicked);
	}

	public int[] RequestTiles(int tileNum) {


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
		Debug.Log ("Tile Clicked");
		if (clicked == 0) {
			int clicked1 = parameters.GetIntExtra (KeyNames.KEY_TILE_TYPE, 1);
			clicked++;
		}
		else {
			int clicked2 = parameters.GetIntExtra (KeyNames.KEY_TILE_TYPE, 1);
			clicked = 0;

			Parameters checkParams = new Parameters ();
			checkParams.PutExtra (KeyNames.KEY_IS_MATCH, clicked1 == clicked2);
			EventBroadcaster.Instance.PostEvent (EventNames.ON_TILES_CHECKED, checkParams);
		}
	}
}
