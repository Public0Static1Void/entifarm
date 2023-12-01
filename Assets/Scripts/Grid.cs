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

    private List<Plants> plants_data;

    [Header("Sprite del panel")]
    public Sprite plants_spr;

    [Header("Tama�o de la grid")]
    public int width;
    public int height;

    [Header("Otros datos de dise�o")]
    [SerializeField] private float x_size = 0;
    [SerializeField] private float y_size = 0;
    [SerializeField] private float offset = 0;

    [Space(10)]
    [SerializeField] private bool scroll = false;
    [SerializeField] private float scroll_speed = 0;
    [SerializeField] private float scroll_max = 0;
    [SerializeField] private float scroll_min = 0;
    [SerializeField] private bool setParent = false;

    [Space(10)]
    [SerializeField] private bool createOnLaunch = false;


    private List<GameObject> cells;
    private List<UnityEngine.UI.Image> cells_im;

    void Awake()
    {
        cells = new List<GameObject>();
        cells_im = new List<UnityEngine.UI.Image>();

        /*
        plants_data = new List<Plants>();

        // A�ade todos los datos de las plantas a plants_data (el cast da problemas)
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

    public void CreateGrid()
    {
        // Crear los paneles
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                /// Crea el bot�n en la escena
                GameObject panel = new GameObject();

                /// Pone la posici�n y el tama�o
                panel.AddComponent<RectTransform>();
                RectTransform rt = panel.GetComponent<RectTransform>();
                rt.position = new Vector2(transform.position.x + j * offset,
                                          transform.position.y - i * offset);
                rt.localScale = new Vector2(x_size, y_size);

                /// A�ade al bot�n su acci�n
                panel.AddComponent<UnityEngine.UI.Button>();
                //panel.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PrintPosition(panel));

                /// Le pone el sprite al bot�n
                panel.AddComponent<UnityEngine.UI.Image>();
                UnityEngine.UI.Image im = panel.GetComponent<UnityEngine.UI.Image>();
                im.sprite = plants_spr;

                /// Cambia el nombre del objeto
                panel.name = string.Format("{0} {1}", transform.name, i + j);

                /// A�ade un texto al panel
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


    public void PrintPosition(GameObject ob)
    {
        Debug.Log(string.Format("X: {0}, Y: {1}", ob.transform.position.x, ob.transform.position.y));
    }

    public GameObject GetCell(int i)
    {
        return cells[i];
    }

    public void ChangeCellText(GameObject cell, string text)
    {
        cell.GetComponent<UnityEngine.UI.Text>().text = text;
    }

    public void ChangeCellImage(int n, Sprite spr)
    {
        if (n > cells_im.Count)
            return;

        cells_im[n].sprite = spr;
    }
    private void OnDrawGizmos()
    {
        Vector3 panel_size = new Vector3 (x_size * 100, y_size * 100, 0);

        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                // Establece la posici�n de los sprites que tendr�n los paneles
                Vector2 pos = new Vector2(transform.position.x + j * offset,
                                          transform.position.y - i * offset);
                Gizmos.DrawWireCube(pos, panel_size);
            }
        }
    }
}