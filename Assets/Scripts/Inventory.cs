using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Grid grid;

    List<ArrayList> list;
    public List<Plants> plants;

    void Start()
    {
        grid = GetComponent<Grid>();

        list = Database.GetInventory();
        plants = new List<Plants>();

        for (int i = 0; i < list.Count; i++)
        {
            Plants pl = new Plants();

            pl.id = (int)list[i][0];
            pl.name = list[i][1].ToString();
            pl.time = (int)list[i][2];
            pl.quantity = (int)list[i][3];
            pl.sell_price = (float)list[i][4];
            pl.buy_price = (float)list[i][5];
            pl.season = (int)list[i][6];

            plants.Add(pl);

            GameObject cell = grid.GetCell(i);
        }
    }
}