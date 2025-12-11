using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellList : MonoBehaviour
{
    public List<CellRow> cellRows;

    public int rows;
    public int columns;

    private void Awake()
    {
        rows = cellRows.Count();
        columns = cellRows[0].cells.Count();
    }


}
