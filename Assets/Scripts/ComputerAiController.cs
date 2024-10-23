using System.Collections.Generic;

public class ComputerAiController
{
    const int MIN_VAL = -100000;
    const int MAX_VAL = 100000;
    TableState currentTableState;
    public int maxDepth = 5;
    int nextStepX, nextStepY;
    public void CalculateNextStep(List<List<CellController>> list, int winCondition) {
        //currentTableState = new TableState(list, winCondition);
        StateNode rootNode = new StateNode(list, maxDepth, winCondition);
        CalculateAlphaBeta(rootNode, true, MIN_VAL, MAX_VAL);

        list[nextStepY][nextStepX].ChangeCellState(CellState.Computer);

    }

    float CalculateAlphaBeta(StateNode node, bool isComputer, float alpha, float beta) {
        float res = 0;
        StateNode tempNode = node;
        if (node.childNode.Count > 0) {
            if (isComputer) {
                res = MIN_VAL;
                foreach (StateNode n in node.childNode) {
                    float tempNum = CalculateAlphaBeta(n, !isComputer, res, beta);
                    // Debug.Log(n.resX + " " + n.resY + " " + tempNum);
                    if (tempNum > res) {
                        res = tempNum;
                        tempNode = n;
                        
                    }
                    if (tempNum >= beta) {
                        break;
                    }
                }
            } else {
                res = MAX_VAL;
                foreach (StateNode n in node.childNode) {
                    float tempNum = CalculateAlphaBeta(n, !isComputer, alpha, res);
                    //Debug.Log(n.resX + " " + n.resY + " " + tempNum);
                    if (tempNum < res) {
                        res = tempNum;
                        
                    }
                    if (tempNum <= alpha) {
                        break;
                    }
                }
            }
        } else {
            res = node.tableState.GetTableValue();
        }

        if (node.depth == 1 && tempNode.resX != -1 && tempNode.resY != -1) {
            nextStepX = tempNode.resX;
            nextStepY = tempNode.resY;
        }

        return res;
    }
}

