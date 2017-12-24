using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selector : MonoBehaviour {

	public GameObject selected = null;
	public Text bugger;
	Creator cre;
	public moveManager man;

	bool raised = false;
	public static bool isWhitesTurn = true;
	bool GameOver = false;

	public Text text;

	void Start(){
		cre = GetComponent<Creator> ();
		man = GetComponent<moveManager> ();
	}

	void Update () {

		if (!GameOver) {
			//android
			if (Application.platform == RuntimePlatform.Android && Input.GetTouch (0).phase == TouchPhase.Began) {
				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100)) {

					if (hit.collider.tag.Equals ("tile")) {

						if (selected != null) {
							int x = hit.collider.gameObject.name [0] - 48;
							int y = hit.collider.gameObject.name [1] - 48;

							if (moveManager.possibilities [x, y]) {
								selected.transform.position = cre.board [x, y].transform.position + Vector3.up * 0.1f;
								if (selected.gameObject.tag.Equals ("bPawn") || selected.gameObject.tag.Equals ("wPawn")) {
									selected.gameObject.GetComponent<pieceProperties> ().hasMoved = true;
								}
								isWhitesTurn = !isWhitesTurn;
								if (isWhitesTurn) {
									text.text = "White's turn";
								} else {
									text.text = "Black's turn";
								}
							}
							selected = null;
							man.unmark ();
						}
					} else {

						if (selected == null) {
							if (!(isWhitesTurn ^ hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite)) {
								selected = hit.collider.gameObject;
								man.unmark ();
								man.listMoves ();
							}
						}

						if (selected != null && hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite != selected.GetComponent<pieceProperties> ().isWhite) {//kill

							int x = hit.collider.gameObject.GetComponent<pieceProperties> ().currTile.name [0] - 48;
							int y = hit.collider.gameObject.GetComponent<pieceProperties> ().currTile.name [1] - 48;

							if (moveManager.possibilities [x, y]) {
								if (hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite) {
									Creator.whiteBoardStatus [x, y] = false;
								} else {
									Creator.blackBoardStatus [x, y] = false;
								}
								if (hit.collider.gameObject.tag.Equals ("king")) {
									if (hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite) {
										Creator.isWhiteKingDead = true;
									} else {
										Creator.isBlackKingDead = true;
									}
								}
								Destroy (hit.collider.gameObject);														//killing here
								if (isWhitesTurn) {
									Creator.blackStrength--;
								} else {
									Creator.whiteStrength--;
								}

								if (Creator.whiteStrength == 0 || Creator.blackStrength == 0) {			//TODO
									GameOver = true;
								}
								if (Creator.isBlackKingDead || Creator.isWhiteKingDead) {
									GameOver = true;
								}

								selected.transform.position = cre.board [x, y].transform.position + Vector3.up * 0.1f;


								if (selected.gameObject.tag.Equals ("bPawn") || selected.gameObject.tag.Equals ("wPawn")) {
									selected.gameObject.GetComponent<pieceProperties> ().hasMoved = true;
								}
								isWhitesTurn = !isWhitesTurn;
								if (isWhitesTurn) {
									text.text = "White's turn";
								} else {
									text.text = "Black's turn";
								}
								selected = null;
								man.unmark ();

								if (GameOver) {
									if (!isWhitesTurn) {
										text.text = "Game Over...\nWhite Wins";
									} else {
										text.text = "Game Over...\nBlack Wins";
									}
									return;
								}

							} else {
								if (!(isWhitesTurn ^ hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite)) {
									selected = hit.collider.gameObject;
									man.unmark ();
									man.listMoves ();
								}
							}

						} else {		//just change selected piece

							if (!(isWhitesTurn ^ hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite)) {
								selected = hit.collider.gameObject;
								man.unmark ();
								man.listMoves ();
							}
						}

					}

				}
			}

			//pc
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100)) {

					if (hit.collider.tag.Equals ("tile")) {

						if (selected != null) {
							int x = hit.collider.gameObject.name [0] - 48;
							int y = hit.collider.gameObject.name [1] - 48;

							if (moveManager.possibilities [x, y]) {
								selected.transform.position = cre.board [x, y].transform.position + Vector3.up * 0.1f;
								if (selected.gameObject.tag.Equals ("bPawn") || selected.gameObject.tag.Equals ("wPawn")) {
									selected.gameObject.GetComponent<pieceProperties> ().hasMoved = true;
								}
								isWhitesTurn = !isWhitesTurn;
								if (isWhitesTurn) {
									text.text = "White's turn";
								} else {
									text.text = "Black's turn";
								}
							}
							selected = null;
							man.unmark ();
						}
					} else {

						if (selected == null) {
							if (!(isWhitesTurn ^ hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite)) {
								selected = hit.collider.gameObject;
								man.unmark ();
								man.listMoves ();
							}
						}

						if (selected != null && hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite != selected.GetComponent<pieceProperties> ().isWhite) {//kill

							int x = hit.collider.gameObject.GetComponent<pieceProperties> ().currTile.name [0] - 48;
							int y = hit.collider.gameObject.GetComponent<pieceProperties> ().currTile.name [1] - 48;

							if (moveManager.possibilities [x, y]) {
								if (hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite) {
									Creator.whiteBoardStatus [x, y] = false;
								} else {
									Creator.blackBoardStatus [x, y] = false;
								}
								if (hit.collider.gameObject.tag.Equals ("king")) {
									if (hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite) {
										Creator.isWhiteKingDead = true;
									} else {
										Creator.isBlackKingDead = true;
									}
								}
								Destroy (hit.collider.gameObject);														//killing here
								if (isWhitesTurn) {
									Creator.blackStrength--;
								} else {
									Creator.whiteStrength--;
								}

								if (Creator.whiteStrength == 0 || Creator.blackStrength == 0) {			//TODO
									GameOver = true;
								}
								if (Creator.isBlackKingDead || Creator.isWhiteKingDead) {
									GameOver = true;
								}

								selected.transform.position = cre.board [x, y].transform.position + Vector3.up * 0.1f;


								if (selected.gameObject.tag.Equals ("bPawn") || selected.gameObject.tag.Equals ("wPawn")) {
									selected.gameObject.GetComponent<pieceProperties> ().hasMoved = true;
								}
								isWhitesTurn = !isWhitesTurn;
								if (isWhitesTurn) {
									text.text = "White's turn";
								} else {
									text.text = "Black's turn";
								}
								selected = null;
								man.unmark ();


								if (GameOver) {
									if (!isWhitesTurn) {
										text.text = "Game Over...\nWhite Wins";
									} else {
										text.text = "Game Over...\nBlack Wins";
									}
									return;
								}
							} else {
								if (!(isWhitesTurn ^ hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite)) {
									selected = hit.collider.gameObject;
									man.unmark ();
									man.listMoves ();

								}
							}

						} else {		//just change selected piece

							if (!(isWhitesTurn ^ hit.collider.gameObject.GetComponent<pieceProperties> ().isWhite)) {
								selected = hit.collider.gameObject;
								man.unmark ();
								man.listMoves ();
							}
						}

					}


				}
			}
		}
	}

	void raiseSelected(){
		if (selected != null && raised == false) {
			selected.transform.position = selected.transform.position + Vector3.up * 2;
			raised = true;
		}
	}

	void lowerSelected(){
		if (selected != null && raised==true) {
			selected.transform.position = selected.transform.position - Vector3.up * 2;
			raised = false;
		}
	}
}
