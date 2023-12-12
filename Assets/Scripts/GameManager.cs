using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm { get; private set; }

    [SerializeField] private Animator shop_anim;
    private bool set_shop = false;

    public float coins = 100;
    [SerializeField] private UnityEngine.UI.Text coins_text;

    public List<Plants> plants;

    void Start()
    {
        if (gm != null)
            Destroy(this);
        else
            gm = this;

        plants = new List<Plants>();
        List<ArrayList> list = Database.GetPlants(); // Consigue todas las plantas de la db

        for (int i = 0; i < list.Count; i++) /// Procesa la lista recibida y añade los datos a plants
        {
            Plants pl = new Plants();

            pl.id = int.Parse("" + list[i][0]);
            pl.name = list[i][1].ToString();
            pl.time = int.Parse("" + list[i][2]);
            pl.quantity = int.Parse("" + list[i][3]);
            pl.sell_price = float.Parse("" + list[i][4]);
            pl.buy_price = float.Parse("" + list[i][5]);
            int.TryParse("" + list[i][6], out pl.season);

            pl.LoadSprites();

            plants.Add(pl);
        }

        UpdateCoinsText(coins);
    }

    public Plants GetPlantId(int id)
    {
        for (int i = 0; i < plants.Count; i++)
        {
            if (plants[i].id == id)
                return plants[i];
        }

        Debug.Log("GM: No se ha encontrado la planta con id " + id);
        return plants[0];
    }

    public void UpdateCoinsText(float n)
    {
        coins_text.text = "Coins: " + n.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            set_shop = !set_shop;
            shop_anim.SetBool("scroll", set_shop);
        }
    }
}