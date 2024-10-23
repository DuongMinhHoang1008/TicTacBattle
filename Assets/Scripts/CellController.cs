using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CellState {
    None = 0,
    Player = 1,
    Computer = 2
}

public class CellController : MonoBehaviour
{
    public CellState cellState { get; private set; } = CellState.None;
    [SerializeField] TextMeshProUGUI textPiece;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Change the state of the cell.
    /// Player -> cell will show X. 
    /// Computer -> cell will show O. 
    /// None -> cell will show nothing
    /// </summary>
    public void ChangeCellState(CellState state) {
        cellState = state;
        switch (state) {
            case CellState.Player:
                textPiece.text = "X";
                break;
            case CellState.Computer:
                textPiece.text = "O";
                break;
            default:
                textPiece.text = "";
                break;
        }
    }

    public void PlayerClickCell() {
        if (cellState == CellState.None) {
            ChangeCellState(CellState.Player);
            TableController.instance.ComputerMove();
        }
    }
}
