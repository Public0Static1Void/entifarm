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

        StartCoroutine(WaitToGrow()); /// Comprobará cada segundo que plantas tienen que crecer
    }

    private void RemovePlant(int pos)
    {
        if (plants.Count <= 0)
            return;

        plants.RemoveAt(pos); /// Quita la planta

        next--;

        for (int i = 0; i < next; i++) /// Dibuja las plantas en su nueva posición
        {
            fl_grid.ChangeCellImage(i, plants[i].sprites[plants[i].curr_sprite]); /// Método de Grid.cs para cambiar el sprite
        }
        fl_grid.ChangeCellImage(next, fl_grid.plants_spr);
    }

    public void AddPlant(Plants pl)
    {
        if (next == (fl_grid.width * fl_grid.height) - 1)
            return;

        pl.LoadSprites();

        pl.grow_tick = pl.time;

        plants.Add(pl);

        fl_grid.ChangeCellImage(next, pl.sprites[0]);

        next++;
    }

    /// <summary>
    /// Comprueba cada segundo que plantas tienen que crecer
    /// </summary>
    IEnumerator WaitToGrow()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < next && plants.Count > 0; i++)
        {
            if (plants[i].grow_tick <= 0) /// Una vez el tiempo de la planta llegue a 0 esta crece
            {
                if (!plants[i].Grow(1))
                {
                    RemovePlant(i);
                    break;
                }
                plants[i].grow_tick = plants[i].time; /// Hace que la planta espere otro ciclo para crecer
                fl_grid.ChangeCellImage(i, plants[i].sprites[plants[i].curr_sprite]); /// Actualiza el sprite de la celda al siguiente
            }
            else
                plants[i].grow_tick--; /// Resta a los segundos que faltan para que crezca
        }
        StartCoroutine(WaitToGrow()); /// Repite el bucle
    }
}