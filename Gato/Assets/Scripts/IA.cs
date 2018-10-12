using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesAndScores
{
    public int score;
    public int move;

    public MovesAndScores(int score, int move)
    {
        this.score = score;
        this.move = move;
    }

}

public class IA : MonoBehaviour {
    //Circulo = computadora = 1
    string[] empty;
    int[] IAlugar;
    int computadora = 1;
    int player = 0;
    int opponent;
    int puntaje;
    List<MovesAndScores> childrenScores;

    void Start()
    {
        empty = new string[9];
    }

    void Update()
    {
        
    }

    public List<int> AvailablePlaces()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (empty[i] == "Empty")
            {
                result.Add(i);
            }
        }
        return result;
    }

    public void GetMinimax(int[] IAlugar, int turno, int state)
    {
        childrenScores = new List<MovesAndScores>();
        Minimax(IAlugar, turno, state);
    }

    public int Minimax(int[] IAlugar, int turno, int state)
    {
        puntaje = CheckIAWinner(IAlugar, turno);
        if (puntaje != 0)
        {
            return puntaje;
        }

        CheckEmpty(IAlugar);

        List<int> scores = new List<int>();
        List<int> posMoves = AvailablePlaces();

        if (posMoves.Capacity == 0)
        {
            return 0;
        }

        for (int i = 0; i < posMoves.Count; i++)
        {
            int move = posMoves[i];

            if (turno == computadora)
            {
                IAlugar[move] = turno;
                int newScore = Minimax(IAlugar, player, state + 1);
                scores.Add(newScore);

                if (state == 0)
                {
                    MovesAndScores mAndS = new MovesAndScores(newScore, move);
                    mAndS.score = newScore;
                    mAndS.move = move;
                    childrenScores.Add(mAndS);
                }
            }
            else if (turno == player)
            {
                IAlugar[move] = turno;
                int newScore = Minimax(IAlugar, computadora, state + 1);
                scores.Add(newScore);
            }

            IAlugar[move] = 2;
        }

        return turno == computadora ? CheckMaxScores(scores) : CheckMinScores(scores);
        
        

    }

    public int CheckMaxScores(List<int> scores)
    {
        int bestMove = -1;
        int max = -1000;
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] > max)
            {
                max = scores[i];
                bestMove = i;
            }
        }
        
        return scores[bestMove];
        
    }

    public int CheckMinScores(List<int> scores)
    {
        int bestMove = -1;
        int min = 1000;
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] < min)
            {
                min = scores[i];
                bestMove = i;
            }
        }

        return scores[bestMove];
    }

    public int BestMove()
    {
        int max = -1000;
        int bestMove = -1;

        for (int i = 0; i < childrenScores.Count; i++)
        {
            if (max < childrenScores[i].score)
            {
                max = childrenScores[i].score;
                bestMove = i;
            }
        }

        if (bestMove > -1)
        {
            return childrenScores[bestMove].move;
        }

        int noMove = 0;
        return noMove;
    }

    public void CheckEmpty(int[] IAlugar)
    {
        for (int i = 0; i < 9; i++)
        {
            if (IAlugar[i]==2)
            {
                empty[i] = "Empty";

            }
            else
            {
                empty[i] = "Used";
            }
        }
        
    }

    public int CheckIAWinner(int[] IAlugar, int jugador)
    {
        for (int i = 0; i < 3; i++)
        {
            int j = i * 3;
            //Vertical
            if (IAlugar[i] == jugador && IAlugar[i + 3] == jugador && IAlugar[i + 6] == jugador)
            {
                if (jugador == 0)
                {
                    return -10;
                }
                else
                if(jugador == 1){
                    return 10;
                }
            }

            //Horizontal
            if (IAlugar[j] == jugador && IAlugar[j + 1] == jugador && IAlugar[j + 2] == jugador)
            {
                if (jugador == 0)
                {
                    return -10;
                }
                else
                if (jugador == 1)
                {
                    return 10;
                }
            }

        }

        //Diagonal 1
        if (IAlugar[0] == jugador && IAlugar[4] == jugador && IAlugar[8] == jugador)
        {
            if (jugador == 0)
            {
                return -10;
            }
            else
                if (jugador == 1)
            {
                return 10;
            }
        }

        //Diagonal 2
        if (IAlugar[2] == jugador && IAlugar[4] == jugador && IAlugar[6] == jugador)
        {
            if (jugador == 0)
            {
                return -10;
            }
            else
                if (jugador == 1)
            {
                return 10;
            }
        }

        return 0;
    }


}
