using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieceProperties : MonoBehaviour {

	public GameObject currTile;
	public bool isWhite;
	void OnTriggerEnter(Collider col){
		currTile = col.gameObject;

		if (isWhite) {
			Creator.whiteBoardStatus [col.gameObject.name [0] - 48, col.gameObject.name [1] - 48] = true;
		} else {
			Creator.blackBoardStatus [col.gameObject.name [0] - 48, col.gameObject.name [1] - 48] = true;
		}
 	}

	public bool hasMoved = false;

	void OnTriggerExit(Collider col){
		if (isWhite) {
			Creator.whiteBoardStatus [col.gameObject.name [0] - 48, col.gameObject.name [1] - 48] = false;
		} else {
			Creator.blackBoardStatus [col.gameObject.name [0] - 48, col.gameObject.name [1] - 48] = false;
		}
	}

 }
