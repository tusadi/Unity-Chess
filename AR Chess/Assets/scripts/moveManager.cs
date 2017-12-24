using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveManager : MonoBehaviour {

	int posX,posY; 		//current pos;

	public static bool[,] possibilities;

	public GameObject marker;
	public GameObject currentMarker;

	public selector sel;
	public Creator cre;

	public List<GameObject> placedMarkers;
	public static List<List<GameObject>> paths;
 
	public static List<GameObject> allowedPaths;

	bool isKillTrue = false;
	void Start(){
		sel = GetComponent<selector> ();
		cre = GetComponent<Creator> ();	
		placedMarkers = new List<GameObject> ();
		paths = new List<List<GameObject>> ();
		allowedPaths = new List<GameObject> ();
		possibilities = new bool[8, 8];
	}

 	public void listMoves(){
 

		if (sel.selected != null) {

			string pos = sel.selected.GetComponent<pieceProperties> ().currTile.name;
			posX = pos [0] - 48;
			posY = pos [1] - 48;

			markCurrent (posX, posY);

//			Debug.Log (pos + "__" + posX + " " + posY);
			string currentPiece = sel.selected.tag;

			if (currentPiece.Equals ("bPawn")) {		//fixed
				int i = posX - 1;

				if (!sel.selected.GetComponent<pieceProperties> ().hasMoved && !Creator.blackBoardStatus[i,posY] && !Creator.whiteBoardStatus[i,posY]) {
					Debug.Log ("move 2!");
					mark (i - 1, posY);
					paths.Add (new List<GameObject> (){cre.board [i - 1, posY] });
				}

				if (i >-1) {

					if (!Creator.whiteBoardStatus [i, posY]) {
						mark (i, posY);
						paths.Add (new List<GameObject> (){ cre.board [i, posY] });

					}

					if (posY + 1 < 8 && Creator.whiteBoardStatus [i, posY + 1]) {
						mark (i, posY + 1);
						paths.Add (new List<GameObject> (){ cre.board [i, posY + 1] });
					}
					if (posY - 1 > -1 && Creator.whiteBoardStatus [i, posY - 1]) {
						mark (i, posY - 1);
						paths.Add (new List<GameObject> (){ cre.board [i, posY - 1] });
					}
 				} 
 
			} else if (currentPiece.Equals ("wPawn")) {		//fixed
				
				int i = posX + 1;

				if (!sel.selected.GetComponent<pieceProperties> ().hasMoved && !Creator.blackBoardStatus[i,posY] && !Creator.whiteBoardStatus[i,posY]) {
					Debug.Log ("move 2!");
					mark (i + 1, posY);
					paths.Add (new List<GameObject> (){ cre.board [i + 1, posY] });
				}

				if (i < 8) {
					if (!Creator.blackBoardStatus [i, posY]) {
						mark (i, posY);
						paths.Add (new List<GameObject> (){ cre.board [i, posY] });

					}

					if (posY + 1 < 8 && Creator.blackBoardStatus [i, posY + 1]) {
						mark (i, posY + 1);
						paths.Add (new List<GameObject> (){ cre.board [i, posY + 1] });
					}
					if (posY -1 > -1 && Creator.blackBoardStatus [i, posY - 1]) {
						mark (i, posY - 1);
						paths.Add (new List<GameObject> (){ cre.board [i, posY - 1] });
					}
 				}

			} else if (currentPiece.Equals ("king")) {		//fixed

				if (posX < 7) {
					mark (posX + 1, posY);
 					paths.Add (new List<GameObject> (){ cre.board [posX + 1, posY] });

					if (posY < 7) {
						mark (posX + 1, posY + 1);
						paths.Add (new List<GameObject> (){ cre.board [posX + 1, posY + 1] });
 					}
					if (posY > 0) {
						mark (posX + 1, posY - 1);
						paths.Add (new List<GameObject> (){ cre.board [posX + 1, posY - 1] });
 					}
				}

				if (posX > 0) {
					mark (posX - 1, posY);
					paths.Add (new List<GameObject> (){ cre.board [posX - 1, posY] });
 
					if (posY < 7) {
						mark (posX - 1, posY + 1);
						paths.Add (new List<GameObject> (){ cre.board [posX - 1, posY + 1] });
 					}
					if (posY > 0) {
						mark (posX - 1, posY - 1);
						paths.Add (new List<GameObject> (){ cre.board [posX - 1, posY - 1] });
 					}
				}

				if (posY > 0) {
					mark (posX, posY - 1);
					paths.Add (new List<GameObject> (){ cre.board [posX, posY - 1] });
 				}
				if (posY < 7) {
					mark (posX, posY + 1);
					paths.Add (new List<GameObject> (){ cre.board [posX, posY + 1] });
 				}


			} else if (currentPiece.Equals ("queen")) {
				
				int tempX = posX;
				int tempY = posY;
 				List<GameObject> tempList = new List<GameObject> ();

				//rook's
				int i = tempX - 1;
				while (i > -1) {
					if (mark (i, posY)) {
						tempList.Add (cre.board [i, posY]);
						i--;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();

				i = tempX + 1;
				while (i < 8) {
					if (mark (i, posY)) {
						tempList.Add (cre.board [i, posY]);
						i++;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();

				int j = tempY - 1;
				while (j > -1) {
					if (mark (posX, j)) {
						tempList.Add (cre.board [posX, j]);
						j--;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();

				j = tempY + 1;
				while (j < 8) {
					if (mark (posX, j)) {
						tempList.Add (cre.board [posX, j]);
						j++;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();

 				//bishop's
				i = posX + 1; j = posY + 1;
				while (i < 8 && j < 8) {
					if (mark (i, j)) {
						tempList.Add (cre.board [i, j]);
						i++;
						j++;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();

				i = posX + 1; j = posY - 1;
				while (i < 8 && j > -1) {
					if (mark (i, j)) {
						tempList.Add (cre.board [i, j]);
						i++;
						j--;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();

				i = posX - 1; j = posY + 1;
				while (i > -1 && j < 8) {
					if (mark (i, j)) {
						tempList.Add (cre.board [i, j]);
						i--;
						j++;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();
 
				i = posX - 1; j = posY - 1;
				while (i > -1 && j > -1) {
					if (mark (i, j)) {
						tempList.Add (cre.board [i, j]);
						i--;
						j--;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();
  
			} else if (currentPiece.Equals ("rook")) {		//fixed

				int tempX = posX;
				int tempY = posY;
 				List<GameObject> tempList = new List<GameObject> ();

				//rook's
				int i = tempX - 1;
				while (i > -1) {
					if (mark (i, posY)) {
						tempList.Add (cre.board [i, posY]);
						i--;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();

				i = tempX + 1;
				while (i < 8) {
					if (mark (i, posY)) {
						tempList.Add (cre.board [i, posY]);
						i++;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();

				int j = tempY - 1;
				while (j > -1) {
					if (mark (posX, j)) {
						tempList.Add (cre.board [posX, j]);
						j--;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();

				j = tempY + 1;
				while (j < 8) {
					if (mark (posX, j)) {
						tempList.Add (cre.board [posX, j]);
						j++;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();


			} else if (currentPiece.Equals ("knight")) {		//fixed



				int i = posX + 1;
 				if (posX + 1 < 8) {
					if (posY + 2 < 8) {
						mark (i, posY + 2);
						paths.Add (new List<GameObject> (){ cre.board [i, posY + 2] });
					}
					if (posY - 2 > -1) {
						mark (i, posY - 2);
						paths.Add (new List<GameObject> (){ cre.board [i, posY - 2] });
					}
				}

				i = posX + 2;
				if (posX + 2 < 8) {
					if (posY + 1 < 8) {
						mark (i, posY + 1);
						paths.Add (new List<GameObject> (){ cre.board [i, posY + 1] });
					}
					if (posY - 1 > -1) {
						mark (i, posY - 1);
						paths.Add (new List<GameObject> (){ cre.board [i, posY - 1] });
					}
				}

				i = posX - 1;
				if (posX - 1 > -1) {
					if (posY + 2 < 8) {
						mark (i, posY + 2);
						paths.Add (new List<GameObject> (){ cre.board [i, posY + 2] });
					}
					if (posY - 2 > -1) {
						mark (i, posY - 2);
						paths.Add (new List<GameObject> (){ cre.board [i, posY - 2] });
					}
				}

				i = posX - 2;
				if (posX - 2 > -1) {
					if (posY + 1 < 8) {
						mark (i, posY + 1);
						paths.Add (new List<GameObject> (){ cre.board [i, posY + 1] });
					}
					if (posY - 1 > -1) {
						mark (i, posY - 1);
						paths.Add (new List<GameObject> (){ cre.board [i, posY - 1] });
					}
				}

			} else if (currentPiece.Equals ("bishop")) {		//fixed

			
				List<GameObject> tempList = new List<GameObject> ();

				int i = posX + 1, j = posY + 1;
				while (i < 8 && j < 8) {
					if (mark (i, j)) {
						tempList.Add (cre.board [i, j]);  
						i++;
						j++; 
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> (); 

				i = posX + 1; j = posY - 1;
				while (i < 8 && j > -1) {
					if (mark (i, j)) {
						tempList.Add (cre.board [i, j]);
						i++;
						j--;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();

				i = posX - 1; j = posY + 1;
				while (i > -1 && j < 8) {
					if (mark (i, j)) {
						tempList.Add (cre.board [i, j]);
						i--;
						j++;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();

 				i = posX - 1; j = posY - 1;
				while (i > -1 && j > -1) {
					if (mark (i, j)) {
						tempList.Add (cre.board [i, j]);
						i--;
						j--;
						if (isKillTrue) {
							isKillTrue = false;
							break;
						}
					} else {
						break;
					}
				}
				paths.Add (tempList);
				tempList = new List<GameObject> ();
			}
 		}
	}

	void markCurrent(int i, int j){

		GameObject temp = GameObject.Instantiate (currentMarker, cre.board [i, j].transform.position , Quaternion.identity);
		placedMarkers.Add (temp);

	}

	bool mark(int i,int j){

		bool isWhite = sel.selected.GetComponent<pieceProperties>().isWhite;

		if (!Creator.whiteBoardStatus [i, j] && !Creator.blackBoardStatus [i, j]) {
			GameObject temp = GameObject.Instantiate (marker, cre.board [i, j].transform.position, Quaternion.identity);
			placedMarkers.Add (temp);
			possibilities [i, j] = true;
			//allowedPaths.Add (tile);
			return true;
		} else if (isWhite && Creator.whiteBoardStatus [i, j]) {
			possibilities [i, j] = false;
			return false;
		} else if (!isWhite && Creator.blackBoardStatus [i, j]) {
			possibilities [i, j] = false;
			return false;
		} else {
			GameObject temp = GameObject.Instantiate (currentMarker, cre.board [i, j].transform.position, Quaternion.identity);
			placedMarkers.Add (temp);
			possibilities [i, j] = true;
			isKillTrue = true;
			//allowedPaths.Add (tile);
			return true;
		}

	}

	public void unmark(){
		foreach (GameObject g in placedMarkers) {
			Destroy (g.gameObject);
		}
		placedMarkers = new List<GameObject> ();
		paths = new List<List<GameObject>> ();
		//Debug.Log ("unmarked");
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				possibilities [i, j] = false;
			}
		}
	}

 }