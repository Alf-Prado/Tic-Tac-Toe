using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    /// <summary>
    /// Almacena el jugador que va a realizar el siguiente
    /// movimiento, se utiliza para saber que ficha
    /// colocar. Cuando el valor es 0 entonces se tira el 
    /// TACHE mientras que si el valor es 1 entonces se
    /// coloca CÍRCULO
    /// </summary>
    private int _playerGame;
    /// <summary>
    /// Prefab del Tache
    /// </summary>
    public GameObject Cross; //Jugador
    /// <summary>
    /// Prefab del Círculo
    /// </summary>
    public GameObject Circle; //Computadora

	public BoardPosition[] Positions;
    public int[] lugar;
    public IA computadora;
    int valor;
    public int canMove;

    void OnGUI () {
        string NextPlayer = _playerGame == 0 ? "TACHE" : "CÍRCULO";
        GUILayout.Label("Es el turno del jugador: " + NextPlayer);
    }

	// Use this for initialization
	void Start () {
        canMove = 0;
	    _playerGame = 0;
        lugar = new int[9];

        for (int i = 0; i < 9; i++)
        {
            lugar[i] = 2;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    /// <summary>
    /// Coloca la pieza de juego en la posición dada
    /// </summary>
    /// <param name="bp">Objeto BoardPosition que representa la posición
    /// en el tablero</param>
    public void PlaceToken (BoardPosition bp) {
        // Si la posición no está ocupada entonces colocamos
        // la ficha, de lo contrario, no hacemos nada
        if (!bp.Used) {
            
            // Guardamos la posición a utilizar
            Vector3 pos = bp.gameObject.transform.position;
            // Colocamos la ficha según el tipo de _playerGame que tenemos,
            // recuerda si _playerGame es 0 entonces colocamos un tache
            // de lo contrario colocamos un círculo
            
            GameObject obj = Instantiate(Cross,
                                                      pos,
                                                      Quaternion.identity) as GameObject;
            
            // Marcamos la casilla como ocupada
            bp.Used = true;
            
            
            //Fila 1
            if (pos.x < -2f && pos.y > 2f)
            {
                valor = 0;
            }
            

            if (pos.x > -2f && pos.x < 2f && pos.y > 2f)
            {
                valor = 1;
            }
            

            if (pos.x > 2f && pos.y > 2f)
            {
                valor = 2;
            }

            //Fila 2
            if (pos.x < -2f && pos.y < 2f && pos.y > -2f)
            {
                valor = 3;
            }
            

            if (pos.x > -2f && pos.x < 2f && pos.y < 2f && pos.y > -2f)
            {
                valor = 4; 
            }
           

            if (pos.x > 2f && pos.y < 2f && pos.y > -2f)
            {
                valor = 5;
            }

            //Fila 3
            if (pos.x < -2f && pos.y < -2f)
            {
                valor = 6;
            }

            if (pos.x > -2f && pos.x < 2f && pos.y < -2f)
            {
                valor = 7;
            }

            if (pos.x > 2f && pos.y < -2f)
            {
                valor = 8;
            }

            

            // Cambiamos la ficha para el siguiente juego
            _playerGame = 1;
            canMove++;
            SavePosition(valor, 0);

            //Turno de la computadora
            computadora.GetMinimax(lugar, 1, 0);
            int best = computadora.BestMove();
            if (best == 0)
            {
                pos.x = -2.481309f;
                pos.y = 2.488531f;
                pos.z = 0f;
            }
            if (best == 1)
            {
                pos.x = 0.007499248f;
                pos.y = 2.488531f;
                pos.z = 0f;
            }
            if (best == 2)
            {
                pos.x = 2.48025f;
                pos.y = 2.488531f;
                pos.z = 0f;
            }
            if (best == 3)
            {
                pos.x = -2.481309f;
                pos.y = -0.0002770126f;
                pos.z = 0f;
            }
            if (best == 4)
            {
                pos.x = 0.007499248f;
                pos.y = -0.0002770126f;
                pos.z = 0f;
            }
            if (best == 5)
            {
                pos.x = 2.48025f;
                pos.y = -0.0002770126f;
                pos.z = 0f;
            }
            if (best == 6)
            {
                pos.x = -2.481309f;
                pos.y = -2.424857f;
                pos.z = 0f;
            }
            if (best == 7)
            {
                pos.x = 0.007499248f;
                pos.y = -2.424857f;
                pos.z = 0f;
            }
            if (best == 8)
            {
                pos.x = 2.48025f;
                pos.y = -2.424857f;
                pos.z = 0f;
            }

            GameObject obj2 = Instantiate(Circle,
                                                      pos,
                                                      Quaternion.identity) as GameObject;

            valor = best;

            // Cambiamos la ficha para el siguiente juego
            _playerGame = 0;
            SavePosition(valor, 1);

        }

    }

    

    public void SavePosition(int posicion, int turno)
    {
        //Fila 1
        for (int i = 0; i < 9; i++)
        {
            if (posicion == i)
            {
                lugar[i] = turno;
            }
        }
        
    }
    

    public void CheckWinner()
    {
        for (int i = 0; i < 3; i++)
        {
            int j = i * 3;
            //Vertical
            if (lugar[i] == 0 && lugar[i + 3] == 0 && lugar[i + 6] == 0)
            {
                Debug.Log("Gano Cruz");
            }
            if (lugar[i] == 1 && lugar[i + 3] == 1 && lugar[i + 6] == 1)
            {
                Debug.Log("Gano Circulo");
            }
            //Debug.Log(lugar[i] + " " + lugar[i + 3] + " " + lugar[i + 6]);

            //Horizontal
            if (lugar[j] == 0 && lugar[j + 1] == 0 && lugar[j + 2] == 0)
            {
                Debug.Log("Gano Cruz");
            }
            if (lugar[j] == 1 && lugar[j + 1] == 1 && lugar[j + 2] == 1)
            {
                Debug.Log("Gano Circulo");
            }

        }

        //Diagonal 1
        if (lugar[0] == 0 && lugar[4] == 0 && lugar[8] == 0)
        {
            Debug.Log("Gano Cruz");
        }
        if (lugar[0] == 1 && lugar[4] == 1 && lugar[8] == 1)
        {
            Debug.Log("Gano Circulo");
        }

        //Diagonal 2
        if (lugar[2] == 0 && lugar[4] == 0 && lugar[6] == 0)
        {
            Debug.Log("Gano Cruz");
        }
        if (lugar[2] == 1 && lugar[4] == 1 && lugar[6] == 1)
        {
            Debug.Log("Gano Circulo");
        }
    }

       
}
