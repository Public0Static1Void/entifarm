using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ArrayList> list;
    public List<int> plants_id;

    private Grid inv_grid;
    private Grid pl_grid;
    private Floor floor;

    private int next_plant = 0;

    void Start()
    {
        inv_grid = GetComponent<Grid>();
        GameObject fl = GameObject.Find("Floor grid");
        pl_grid = fl.GetComponent<Grid>();
        floor = fl.GetComponent<Floor>();

        UpdateInventory();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AddPlant();
        }
    }

    public void UpdateInventory()
    {
        list = Database.GetInventory();
        plants_id = new List<int>();

        int count = list.Count;

        if (count > inv_grid.height) // Si hay más plantas que celdas, se añaden las celdas necesarias
        {
            inv_grid.AddXY(0, 2);
            inv_grid.CreateGrid();
        }

        for (int i = 0; i < count; i++)
        {
            plants_id.Add(int.Parse(list[i][0].ToString()));
        }

        for (int i = 0; i < inv_grid.height; i++)
        {
            inv_grid.ChangeCellImage(i, inv_grid.plants_spr); // cambia todas las celdas al sprite base
        }

        for (int i = 0; i < count; i++)
        {
            inv_grid.ChangeCellImage(i, GameManager.gm.plants[plants_id[i] - 1].sprites[0]);
        }        
    }

    private void AddPlant()
    {
        if (next_plant > plants_id.Count || plants_id.Count <= 0)
            return;
        if (next_plant == plants_id.Count && next_plant > 0)
            next_plant--;

        floor.AddPlant(GameManager.gm.GetPlantId(plants_id[next_plant])); // planta la planta

        Database.RemoveFromInventory(plants_id[next_plant]); // quita la planta del inventario

        UpdateInventory();

        if (next_plant < plants_id.Count)
            next_plant++;
        else
            next_plant = 0;
    }
}