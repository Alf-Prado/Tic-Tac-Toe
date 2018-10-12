using UnityEngine;
using System.Collections;

public class BoardPosition : MonoBehaviour {
    /// <summary>
    /// Variable del tablero, establecida desde el Inspector
    /// </summary>
    public Board TheBoard;
    /// <summary>
    /// Indicamos si esta posición ya esta ocupado o no
    /// </summary>
    public bool Used;

	public int Type;
    int pos;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnMouseDown() {

        TheBoard.PlaceToken(this);
        TheBoard.CheckWinner();
    }
}
