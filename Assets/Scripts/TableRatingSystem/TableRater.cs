using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rating the whole table by all the cell by all the counter
public class TableRater {
    int[] playerRapid;
    int[] computerRapid;
    int winConditionNum;
    public TableRater(int winCondition) {
        playerRapid = new int[winCondition];
        computerRapid = new int[winCondition];
        winConditionNum = winCondition;
    }

    public int GetRapidCountByRapidNum(CellState state, int rapidNumber) {
        if (rapidNumber <= winConditionNum) {
            if (state == CellState.Player) {
                return playerRapid[rapidNumber - 1];
            } else if (state == CellState.Computer){
                return computerRapid[rapidNumber - 1];
            }
        }
        return -1;
    }

    public void SetRapidCountByRapidNum(CellState state, int rapidNumber, int val) {
        if (rapidNumber <= winConditionNum) {
            if (state == CellState.Player) {
                playerRapid[rapidNumber - 1] = val;
            } else if (state == CellState.Computer) {
                computerRapid[rapidNumber - 1] = val;
            }
        }
    }

    public void PlusRapid(CellRater cellRater, RapidCount rapidCount, bool headBlock, bool tailBlock) {
        int plusVal;
        if (headBlock && tailBlock) {
            plusVal = 0;
        } else {
            if (headBlock != tailBlock) plusVal = 1;
            else plusVal = 2;
        }

        if (cellRater.GetRapidObjCount(rapidCount) == winConditionNum) plusVal = 2;

        SetRapidCountByRapidNum(
            cellRater.state, 
            cellRater.GetRapidObjCount(rapidCount),
            plusVal + GetRapidCountByRapidNum(
                cellRater.state,
                cellRater.GetRapidObjCount(rapidCount)
            )
        );
    }
}
