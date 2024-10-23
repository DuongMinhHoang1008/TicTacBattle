using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The state of the table
public class TableState {
    public List<List<CellRater>> cellRaters {get; private set;}
    float tableValue = 0;
    TableRater tableRater;
    int winConditionNum;
    public TableState(List<List<CellController>> list, int winCondition) {
        tableRater  = new TableRater(winCondition);
        cellRaters = new List<List<CellRater>>();
        for (int i = 0; i < list.Count; i++) {
            List<CellRater> cells = new List<CellRater>();
            for (int j = 0; j < list[i].Count; j++) {
                cells.Add(new CellRater(list[i][j].cellState));
            }
            cellRaters.Add(cells);
        }
        winConditionNum = winCondition;
    }

    public TableState(List<List<CellRater>> list, int winCondition) {
        tableRater  = new TableRater(winCondition);
        cellRaters = new List<List<CellRater>>();
        for (int i = 0; i < list.Count; i++) {
            List<CellRater> cells = new List<CellRater>();
            for (int j = 0; j < list[i].Count; j++) {
                cells.Add(new CellRater(list[i][j].state));
            }
            cellRaters.Add(cells);
        }
        winConditionNum = winCondition;
    }

    void CalculateCellRater(CellRater setCell, CellRater getCell, RapidCount rapidCount) {
        if (getCell.state == setCell.state && setCell.state != CellState.None) {
            setCell.headBlock[rapidCount] = getCell.headBlock[rapidCount];
            setCell.SetRapidObjCount(
                rapidCount,
                1 + getCell.GetRapidObjCount(rapidCount)
            );
        }
    }

    public void CalculateTableState() {
        for (int y = 0; y < cellRaters.Count; y++) {
            for (int x = 0; x < cellRaters[y].Count; x++) {
                //Calculate rapid row
                if (x == 0 || CompareStateAndNotNone(cellRaters[y][x - 1], cellRaters[y][x]) == 0) {
                    cellRaters[y][x].headBlock[RapidCount.Row] = true;
                }
                if (x > 0) {
                    CalculateCellRater(cellRaters[y][x], cellRaters[y][x - 1], RapidCount.Row);
                    if (x == cellRaters[y].Count - 1 || cellRaters[y][x + 1].state != cellRaters[y][x].state) {
                        bool tailBlock = false;
                        if (x == cellRaters[y].Count - 1 || CompareStateAndNotNone(cellRaters[y][x + 1], cellRaters[y][x]) == 0) tailBlock = true;
                        tableRater.PlusRapid(cellRaters[y][x], RapidCount.Row, cellRaters[y][x].headBlock[RapidCount.Row], tailBlock);
                    }
                }

                //Calculate rapid column
                if (y == 0 || CompareStateAndNotNone(cellRaters[y - 1][x], cellRaters[y][x]) == 0) {
                    cellRaters[y][x].headBlock[RapidCount.Column] = true;
                }
                if (y > 0) {
                    CalculateCellRater(cellRaters[y][x], cellRaters[y - 1][x], RapidCount.Column);
                    if (y == cellRaters.Count - 1 || cellRaters[y + 1][x].state != cellRaters[y][x].state) {
                        bool tailBlock = false;
                        if (y == cellRaters.Count - 1 || CompareStateAndNotNone(cellRaters[y + 1][x], cellRaters[y][x]) == 0) tailBlock = true;
                        tableRater.PlusRapid(cellRaters[y][x], RapidCount.Column, cellRaters[y][x].headBlock[RapidCount.Column], tailBlock);
                    }
                }

                //Calculate rapid cross
                if ((x == 0 && y == 0) 
                    || (x != 0 && y != 0
                        && CompareStateAndNotNone(cellRaters[y - 1][x - 1], cellRaters[y][x]) == 0)
                    ) {
                    cellRaters[y][x].headBlock[RapidCount.Cross] = true;
                }
                if (x > 0 && y > 0) {
                    CalculateCellRater(cellRaters[y][x], cellRaters[y - 1][x - 1], RapidCount.Cross);
                    if (y == cellRaters.Count - 1 || x == cellRaters[y].Count - 1 || cellRaters[y + 1][x + 1].state != cellRaters[y][x].state) {
                        bool tailBlock = false;
                        if (y == cellRaters.Count - 1 
                            || x == cellRaters[y].Count - 1 
                            || CompareStateAndNotNone(cellRaters[y + 1][x + 1], cellRaters[y][x]) == 0) tailBlock = true;
                        tableRater.PlusRapid(cellRaters[y][x], RapidCount.Cross, cellRaters[y][x].headBlock[RapidCount.Cross], tailBlock);
                    }
                }

                //Calculate rapid reverse cross
                if ((x == cellRaters[y].Count - 1 && y == 0) 
                    || (x != cellRaters[y].Count - 1 && y != 0
                        && CompareStateAndNotNone(cellRaters[y - 1][x + 1], cellRaters[y][x]) == 0)
                    ) {
                    cellRaters[y][x].headBlock[RapidCount.RevCross] = true;
                }
                if (x < cellRaters[y].Count - 1 && y > 0) {
                    CalculateCellRater(cellRaters[y][x], cellRaters[y - 1][x + 1], RapidCount.RevCross);
                    if (y == cellRaters.Count - 1 || x == 0 || cellRaters[y + 1][x - 1].state != cellRaters[y][x].state) {
                        bool tailBlock = false;
                        if (y == cellRaters.Count - 1 
                            || x == 0
                            || CompareStateAndNotNone(cellRaters[y + 1][x - 1], cellRaters[y][x]) == 0) tailBlock = true;
                        tableRater.PlusRapid(cellRaters[y][x], RapidCount.RevCross, cellRaters[y][x].headBlock[RapidCount.RevCross], tailBlock);
                    }
                }
            }
        }
    }

    /// <summary>
    /// return: 1 if both are the same and not none; 0 if different and not none; -1 if any is none
    /// </summary>

    int CompareStateAndNotNone(CellRater c1, CellRater c2) {
        CellState s1 = c1.state;
        CellState s2 = c2.state;
        if (s1 == s2) {
            if (s1 == CellState.None) return -1;
            else return 1;
        } else {
            if (s1 == CellState.None || s2 == CellState.None) return -1;
            else return 0;
        }
    }

    public float GetTableValue() {
        CalculateTableState();
        // int rapidVal = 100;
        for (int i = 1; i <= winConditionNum; i++) {
            float rapidMultiply = (float) Math.Pow(10, i - 1);
            tableValue += rapidMultiply * tableRater.GetRapidCountByRapidNum(CellState.Computer, i);
            tableValue -= rapidMultiply * tableRater.GetRapidCountByRapidNum(CellState.Player, i);
        }
        return tableValue;
    }
}
