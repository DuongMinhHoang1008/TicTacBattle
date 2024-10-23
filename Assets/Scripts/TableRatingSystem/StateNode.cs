using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNode {
    public List<StateNode> childNode {get; private set;} = new List<StateNode>();
    public int resX {get; private set;} = -1;
    public int resY {get; private set;} = -1;
    public int depth {get; private set;} = 1;
    public TableState tableState;
    public StateNode(List<List<CellController>> list, int maxDepth, int winCondition) {
        tableState = new TableState(list, winCondition);
        for (int y = 0; y < tableState.cellRaters.Count; y++) {
            for (int x = 0; x < tableState.cellRaters[y].Count; x++) {
                if (tableState.cellRaters[y][x].state == CellState.None) {
                    childNode.Add(new StateNode(tableState.cellRaters, y, x, depth, maxDepth, winCondition));
                }
            }
        }
        //tableState.CalculateTableValue();
    }

    public StateNode(List<List<CellRater>> list, int tempY, int tempX, int parentDepth, int maxDepth, int winCondition) {
        tableState = new TableState(list, winCondition);
        resX = tempX;
        resY = tempY;
        depth = parentDepth + 1;
        //Debug.Log(tempX + " " + tempY + " " + tableState.cellRaters[tempY][tempX].state);
        if (depth % 2 == 0) {
            tableState.cellRaters[tempY][tempX].SetCellState(CellState.Computer);
        } else {
            tableState.cellRaters[tempY][tempX].SetCellState(CellState.Player);
        }
        if (depth < maxDepth) {

            for (int y = 0; y < tableState.cellRaters.Count; y++) {
                for (int x = 0; x < tableState.cellRaters[y].Count; x++) {
                    if (tableState.cellRaters[y][x].state == CellState.None) {
                        childNode.Add(new StateNode(tableState.cellRaters, y, x, depth, maxDepth, winCondition));
                    }
                }
            }
        }
        //  else {
        //     //Debug.Log(tempX + " " + tempY + " " + tableState.cellRaters[tempY][tempX].state);
        //     //Debug.Log("yes");
        //     tableState.CalculateTableValue();
        // }
    }
}
