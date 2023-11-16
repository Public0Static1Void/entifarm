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

    [Header("Tama�o de la grid")]
    [SerializeField] private int width;
    [SerializeField] private int height;

    [Header("Otros datos de dies�o")]
    [SerializeField] private float size = 0;
    [SerializeField] private float offset = 0;

    void Awake()
    {
        plants_data = new List<Plants>();

        // A�ade todos los datos de las plantas a plants_data
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
                /// Inicializa el bot�n
                GameObject panel = new GameObject();

                /// Crea el objeto en la escena y pone el poseedor del script como parent
                Instantiate(panel);

                /// Setea la posici�n y el tama�o
                panel.AddComponent<RectTransform>();
                RectTransform rt = panel.GetComponent<RectTransform>();
                rt.position = new Vector2(transform.position.x + j * offset,
                                          transform.position.y + i * offset);
                rt.localScale = new Vector2(size, size);

                /// A�ade al bot�n su acci�n
                panel.AddComponent<UnityEngine.UI.Button>();
                panel.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => PrintPosition(panel));

                /// Le pone el sprite al bot�n
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
                // Establece la posici�n de los sprites que tendr�n los paneles
                Vector2 pos = new Vector2(transform.position.x + j * offset,
                                          transform.position.y + i * offset);
                Gizmos.DrawWireCube(pos, panel_size);
            }
        }
    }
}