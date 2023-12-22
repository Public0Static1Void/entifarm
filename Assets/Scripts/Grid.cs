using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Grid : MonoBehaviour
{
    [Header("Sprite del panel")]
    public Sprite plants_spr;

    [Header("Tamaño de la grid")]
    public int width;
    public int height;

    [Header("Otros datos de diseño")]
    [SerializeField] public float x_size = 0;
    [SerializeField] public float y_size = 0;
    [SerializeField] public float offset = 0;

    [SerializeField] public bool hasText;
    [SerializeField] private int textNum = 1;
    [SerializeField] public Font textFont;

    [Space(10)]
    [SerializeField] private bool setParent = false;

    [Space(10)]
    [SerializeField] private bool createOnLaunch = false;

    private List<GameObject> cells;
    private List<UnityEngine.UI.Image> cells_im;

    [Space(10)]
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
                ///panel.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PrintPosition(panel));
                /// Esto hace que solo puedas pulsar el botón con click
                Navigation nav = new Navigation();
                nav.mode = Navigation.Mode.None;
                panel.GetComponent<UnityEngine.UI.Button>().navigation = nav;

                /// Le pone el sprite al botón
                UnityEngine.UI.Image im = panel.AddComponent<UnityEngine.UI.Image>();
                im.sprite = plants_spr;

                /// Cambia el nombre del objeto
                panel.name = string.Format("{0} {1}", transform.name, i + j);

                /// Añade un texto al panel
                if (hasText)
                {
                    GameObject t = new GameObject();
                    t.transform.SetParent(panel.transform);
                    t.name = "Text";
                    RectTransform rect = t.AddComponent<RectTransform>();
                    rect.anchoredPosition = new Vector2(50 * x_size, -7.5f * y_size);


                    UnityEngine.UI.Text text = t.AddComponent<UnityEngine.UI.Text>();
                    text.font = textFont;
                    text.fontSize = 32;
                    text.horizontalOverflow = UnityEngine.HorizontalWrapMode.Overflow;
                    text.text = "";

                    if (textNum > 1) /// Añade el segundo texto a la izquierda del panel
                    {
                        GameObject p = new GameObject();
                        p.transform.SetParent(panel.transform);
                        p.name = "Text2";
                        RectTransform rectt = p.AddComponent<RectTransform>();
                        rectt.anchoredPosition = new Vector2(-30 * x_size, -7.5f * y_size);


                        UnityEngine.UI.Text text2 = p.AddComponent<UnityEngine.UI.Text>();
                        text2.font = textFont;
                        text2.fontSize = text.fontSize;
                        text2.horizontalOverflow = text.horizontalOverflow;
                        text2.text = "";
                    }
                }

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
    /// Pone el sprite del argumento en la celda indicada en n
    /// </summary>
    public void ChangeCellImage(int n, Sprite spr)
    {
        if (n > cells_im.Count)
            return;

        cells_im[n].sprite = spr;
    }
    /// <summary>
    /// Cambia el texto de la celda indicada
    /// </summary>
    public void ChangeCellText(int n, string txt, int text_num)
    {
        if (n > cells.Count)
            return;

        switch (text_num)
        {
            case 1:
                transform.GetChild(n).GetComponentInChildren<UnityEngine.UI.Text>().text = txt;
                break;
            case 2:
                transform.GetChild(n).GetChild(1).GetComponent<UnityEngine.UI.Text>().text = txt;
                break;
        }

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
    /*
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
    */
}