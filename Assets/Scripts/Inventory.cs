using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ArrayList> list;
    public List<Plants> plants;

    private Grid inv_grid;
    private Grid pl_grid;
    private Floor floor;

    private int next_plant;

    void Start()
    {
        inv_grid = GetComponent<Grid>();
        GameObject fl = GameObject.Find("Floor grid");
        pl_grid = fl.GetComponent<Grid>();
        floor = fl.GetComponent<Floor>();

        list = Database.GetPlants();
        plants = new List<Plants>();

        for (int i = 0; i < list.Count; i++)
        {
            Plants pl = new Plants();

            pl.id = int.Parse("" + list[i][0]);
            pl.name = list[i][1].ToString();
            pl.time = int.Parse("" +list[i][2]);
            pl.quantity = int.Parse("" + list[i][3]);
            pl.sell_price = float.Parse("" + list[i][4]);
            pl.buy_price = float.Parse("" + list[i][5]);
            int.TryParse("" + list[i][6], out pl.season);

            pl.LoadSprites();

            plants.Add(pl);

            //Pone el texto en la celda
            inv_grid.ChangeCellImage(inv_grid.GetCell(i), plants[i].sprites[0]);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AddPlant();
        }
    }

    private void AddPlant()
    {
        if (next_plant >= plants.Count)
            return;

        floor.AddPlant(plants[next_plant]);

        if (next_plant < plants.Count)
            next_plant++;
        else
            next_plant = 0;

    }
}