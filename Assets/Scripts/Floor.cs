using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private Grid fl_grid;
    private Inventory inv;

    private List<Plants> plants;

    public int next = 0;
    void Start()
    {
        fl_grid = GetComponent<Grid>();

        plants = new List<Plants>();
    }

    void Update()
    {
        for (int i = 0; i < next && plants.Count > 0; i++)
        {
            if (plants[i].grow_tick > Timer.timer.tick)
            {
                if (!plants[i].Grow(1))
                    RemovePlant(i);
            }
        }
    }

    private void RemovePlant(int pos)
    {
        if (plants.Count <= 0)
            return;

        // Asigna la nueva posición de las plantas
        for (int i = 0; i < next; i++)
        {
            fl_grid.ChangeCellImage(i, plants[i].sprites[plants[i].curr_sprite]);
        }
        fl_grid.ChangeCellImage(next, fl_grid.plants_spr); /// Cambia el último sprite al suelo

        plants.RemoveAt(pos); // Quita la planta

        next--;
    }

    public void AddPlant(Plants pl)
    {
        if (next == (fl_grid.width * fl_grid.height) - 1)
            return;

        pl.LoadSprites();

        pl.grow_tick = Timer.timer.tick + pl.time;

        plants.Add(pl);

        fl_grid.ChangeCellImage(next, pl.sprites[0]);

        next++;
    }
}