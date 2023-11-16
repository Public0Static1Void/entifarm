using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlantsGrid : MonoBehaviour
{

    private List<Plants> plants_data;

    [Header("Sprite del panel")]
    private UnityEngine.UI.Button plants_bt;
    [SerializeField] private Sprite plants_spr;

    [Header("Tamaño de la grid")]
    [SerializeField] private int width;
    [SerializeField] private int height;

    [Header("Otros datos de diesño")]
    [SerializeField] private float size = 0;
    [SerializeField] private float offset = 0;

    void Awake()
    {
        plants_data = new List<Plants>();

        // Añade todos los datos de las plantas a plants_data
        List<ArrayList> plants = Database.GetPlants();
        for (int i = 0; i < plants.Count; i++)
        {
            Plants pl = new Plants();

            pl.id = plants[i][0].ToString();
            pl.name = plants[i][1].ToString();
            pl.time = plants[i][2].ToString();
            pl.quantity = plants[i][3].ToString();
            pl.sell_price = plants[i][4].ToString();
            pl.buy_price = plants[i][5].ToString();
            pl.season = plants[i][6].ToString();

            plants_data.Add(pl);
        }
    }

    void Start()
    {
        // Crear los paneles
        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                /// Inicializa el botón
                GameObject panel = new GameObject();

                /// Crea el objeto en la escena y pone el poseedor del script como parent
                Instantiate(panel);

                /// Setea la posición y el tamaño
                panel.AddComponent<RectTransform>();
                RectTransform rt = panel.GetComponent<RectTransform>();
                rt.position = new Vector2(transform.position.x + j * offset,
                                          transform.position.y + i * offset);
                rt.localScale = new Vector2(size, size);

                /// Añade al botón su acción
                panel.AddComponent<UnityEngine.UI.Button>();
                panel.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PrintPosition(panel));

                /// Le pone el sprite al botón
                panel.AddComponent<UnityEngine.UI.Image>();
                panel.GetComponent<UnityEngine.UI.Image>().sprite = plants_spr;

                panel.transform.SetParent(transform, true);
            }
        }
    }

    public void PrintPosition(GameObject ob)
    {
        Debug.Log(string.Format("X: {0}, Y: {1}", ob.transform.position.x, ob.transform.position.y));
    }

    private void OnDrawGizmos()
    {
        Vector3 panel_size = new Vector3 (size, size, 0);

        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                // Establece la posición de los sprites que tendrán los paneles
                Vector2 pos = new Vector2(transform.position.x + j * offset,
                                          transform.position.y + i * offset);
                Gizmos.DrawWireCube(pos, panel_size);
            }
        }
    }
}