using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Grid : MonoBehaviour
{
    [Header("Sprite del panel")]
    public Sprite plants_spr;

    [Header("Tamaño de la grid")]
    public int width;
    public int height;

    [Header("Otros datos de diseño")]
    [SerializeField] private float x_size = 0;
    [SerializeField] private float y_size = 0;
    [SerializeField] private float offset = 0;

    [Space(10)]
    [SerializeField] private bool setParent = false;

    [Space(10)]
    [SerializeField] private bool createOnLaunch = false;


    private List<GameObject> cells;
    private List<UnityEngine.UI.Image> cells_im;

    [SerializeField] private bool showGizmos = true;

    void Awake()
    {
        cells = new List<GameObject>();
        cells_im = new List<UnityEngine.UI.Image>();

        /*
        plants_data = new List<Plants>();

        // Añade todos los datos de las plantas a plants_data (el cast da problemas)
        List<ArrayList> plants = Database.GetPlants();
        for (int i = 0; i < plants.Count; i++)
        {
            Plants pl = new Plants();

            pl.id = (int)plants[i][0];
            pl.name = plants[i][1].ToString();
            pl.time = (int)plants[i][2];
            pl.quantity = (int)plants[i][3];
            pl.sell_price = (float)plants[i][4];
            pl.buy_price = (float)plants[i][5];
            pl.season = (int)plants[i][6];

            plants_data.Add(pl);
        }
        */

        if (createOnLaunch)
            CreateGrid();
    }

    /// <summary>
    /// Crea el campo de celdas
    /// </summary>
    public void CreateGrid()
    {
        // Crear los paneles
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                /// Crea el botón en la escena
                GameObject panel = new GameObject();

                /// Pone la posición y el tamaño
                panel.AddComponent<RectTransform>();
                RectTransform rt = panel.GetComponent<RectTransform>();
                rt.position = new Vector2(transform.position.x + j * offset,
                                          transform.position.y - i * offset);
                rt.localScale = new Vector2(x_size, y_size);

                /// Añade al botón su acción
                panel.AddComponent<UnityEngine.UI.Button>();
                //panel.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PrintPosition(panel));

                /// Le pone el sprite al botón
                panel.AddComponent<UnityEngine.UI.Image>();
                UnityEngine.UI.Image im = panel.GetComponent<UnityEngine.UI.Image>();
                im.sprite = plants_spr;

                /// Cambia el nombre del objeto
                panel.name = string.Format("{0} {1}", transform.name, i + j);

                /// Añade un texto al panel
                ///panel.AddComponent<UnityEngine.UI.Text>();
                ///Text text = panel.GetComponent<Text>();
                ///text.text = string.Format("{0} {1}", transform.name, i + j);
                ///text.color = Color.white;

                /// Pone al poseedor del script como padre
                if (setParent)
                    panel.transform.SetParent(transform, true);
                else
                    panel.transform.SetParent(transform.parent, true);

                cells.Add(panel);
                cells_im.Add(im);
            }
        }
    }

    /// <summary>
    /// Devuelve el GameObject de la celda indicada en i
    /// </summary>
    public GameObject GetCell(int i)
    {
        return cells[i];
    }

    /// <summary>
    /// Cambia el sprite de la celda indicada en n
    /// </summary>
    public void ChangeCellImage(int n, Sprite spr)
    {
        if (n > cells_im.Count)
            return;

        cells_im[n].sprite = spr;
    }

    /// <summary>
    /// Pone las filas y/o columnas indicadas
    /// </summary>
    public void SetXY(int x, int y)
    {
        width = x;
        height = y;
    }

    /// <summary>
    /// Añade las filas y/o columnas indicadas
    /// </summary>
    public void AddXY(int x, int y)
    {
        width += x;
        height += y;
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos)
            return;

        Vector3 panel_size = new Vector3 (x_size * 100, y_size * 100, 0);

        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                // Establece la posición de los sprites que tendrán los paneles
                Vector2 pos = new Vector2(transform.position.x + j * offset,
                                          transform.position.y - i * offset);
                Gizmos.DrawWireCube(pos, panel_size);
            }
        }
    }
}