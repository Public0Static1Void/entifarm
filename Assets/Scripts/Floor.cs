using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private Grid fl_grid;

    private List<Plants> plants;

    public int next = 0;
    void Start()
    {
        fl_grid = GetComponent<Grid>();

        plants = new List<Plants>();
    }

    void Update()
    {
        for (int i = 0; i < next; i++)
        {
            if (plants[i].grow_tick > Timer.timer.tick)
            {
                if (!plants[i].Grow(1))
                    RemovePlant(plants[i], i);
            }
        }
    }

    private void RemovePlant(Plants pl, int pos)
    {
        plants.RemoveAt(pos);

        // Asigna la nueva posición de las plantas
        for (int i = 0; i < next - 1; i++)
        {
            fl_grid.ChangeCellImage(fl_grid.GetCell(i), plants[i].sprites[plants[i].curr_sprite]);
        }
        fl_grid.ChangeCellImage(fl_grid.GetCell(next), fl_grid.plants_spr); /// Cambia el último sprite al suelo

        next--;
    }

    public void AddPlant(Plants pl)
    {
        if (next == (fl_grid.width * fl_grid.height) - 1)
            return;

        pl.LoadSprites();

        pl.grow_tick = Timer.timer.tick + pl.time;

        plants.Add(pl);

        fl_grid.ChangeCellImage(fl_grid.GetCell(next), pl.sprites[0]);

        next++;
    }
}