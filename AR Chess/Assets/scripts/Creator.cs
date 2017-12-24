using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {

	GameObject[] temp;
	public GameObject[,] board;
 
	public static bool[,] whiteBoardStatus, blackBoardStatus,validMoves;

	public static int whiteStrength = 16, blackStrength = 16;
	public static bool isWhiteKingDead = false, isBlackKingDead = false;
	// Use this for initialization
	void Start () {
		board = new GameObject[8, 8];
		whiteBoardStatus = new bool[8, 8];
		blackBoardStatus = new bool[8, 8];
		validMoves = new bool[8, 8];

		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				validMoves [i, j] = false;
			}
		}

		foreach(GameObject t in GameObject.FindGameObjectsWithTag ("tile")) {
			int i = (int)t.transform.position.x / 2;
			int j = (int)t.transform.position.z / 2;
			t.name = i + "" + j; 
			board [i, j] = t;
		}
  	}
}
