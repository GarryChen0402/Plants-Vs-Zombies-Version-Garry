using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellRow : MonoBehaviour
{
    public List<Cell> cells;
    public int row;

    private void Awake()
    {
        foreach(Cell cell in cells)
        {
            cell.row = row;
        }
    }
}
