using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public static TableController instance { get; private set; }
    List<List<CellController>> cells;
    public ComputerAiController computerAi {get; private set;}
    [SerializeField] int cellNumber = 9;
    [SerializeField] int winCondition = 3;
    [SerializeField] GameObject cellPref;
    private void Awake() {
        if (instance == null) {
            cells = new List<List<CellController>>();
            computerAi = new ComputerAiController();
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        int num = (int) Mathf.Sqrt(cellNumber);
        for (int i = 0; i < num; i++) {
            List<CellController> tempCells = new List<CellController>();
            for (int j = 0; j < num; j++) {
                GameObject cell = Instantiate(cellPref, gameObject.transform);
                tempCells.Add(cell.GetComponent<CellController>());
            }
            cells.Add(tempCells);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ComputerMove() {
        computerAi.CalculateNextStep(cells, winCondition);
    }
}
