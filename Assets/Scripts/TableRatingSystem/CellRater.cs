using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rating the cell by rapid counter
public class CellRater {
    public CellState state { get; private set; }
    public Dictionary<RapidCount, bool> headBlock = new Dictionary<RapidCount, bool>()
    {
        { RapidCount.Row, false },
        { RapidCount.Column, false },
        { RapidCount.Cross, false },
        { RapidCount.RevCross, false }
    };
    RapidCounter rapidCounter = new RapidCounter();
    public CellRater(CellState cellState) {
        state = cellState;
    }

    public int GetRapidObjCount(RapidCount rapidCount) {
        return rapidCounter.GetRapidCount(rapidCount);
    }

    public void SetRapidObjCount(RapidCount rapidCount, int val) {
        rapidCounter.SetRapidCount(rapidCount, val);
    }

    public void SetCellState(CellState cs) {
        state = cs;
    }
}
