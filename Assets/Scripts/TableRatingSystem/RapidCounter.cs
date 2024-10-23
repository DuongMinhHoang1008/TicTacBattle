using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RapidCount {
    Row,
    Column,
    Cross,
    RevCross
}
//Count the rapid of row, column, cross, rev cross
public class RapidCounter {
    private int rapidRowCount = 1;
    private int rapidColumnCount = 1;
    private int rapidCrossCount = 1;
    private int rapidRevCrossCount = 1;

    public int GetRapidCount(RapidCount rapidCount) {
        int res = 1;
        switch (rapidCount) {
            case RapidCount.Row:
                res = rapidRowCount;
                break;
            case RapidCount.Column:
                res = rapidColumnCount;
                break;
            case RapidCount.Cross:
                res = rapidCrossCount;
                break;
            case RapidCount.RevCross:
                res = rapidRevCrossCount;
                break;
        }
        return res;
    }

    public void SetRapidCount(RapidCount rapidCount, int val) {
        switch (rapidCount) {
            case RapidCount.Row:
                rapidRowCount = val;
                break;
            case RapidCount.Column:
                rapidColumnCount = val;
                break;
            case RapidCount.Cross:
                rapidCrossCount = val;
                break;
            case RapidCount.RevCross:
                rapidRevCrossCount = val;
                break;
        }
    }
}