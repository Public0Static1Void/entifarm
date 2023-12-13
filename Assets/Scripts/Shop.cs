using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private List<ArrayList> list;
    private List<Plants> plants;

    private Grid sh_grid;
    private Inventory inv;

    [SerializeField] private UnityEngine.UI.Text coins_text;


    private void Start()
    {
        list = Database.GetPlants();
        plants = new List<Plants>();

        inv = GameObject.Find("Inventory").GetComponent<Inventory>();

        sh_grid = GetComponent<Grid>();
        sh_grid.height = list.Count;

        for (int i = 0; i < list.Count; i++)
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

            GameObject ob = sh_grid.GetCell(i);

            ob.GetComponent<UnityEngine.UI.Image>().sprite = pl.sprites[0];

            ob.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => BuyPlant(pl)); // Cambia el listener por defecto
        }
    }

    

    public void BuyPlant(Plants pl)
    {
        if (GameManager.gm.coins < pl.buy_price && !GameManager.gm.mouse0Clicked)
            return;

        GameManager.gm.coins -= pl.buy_price;
        GameManager.gm.UpdateCoinsText(GameManager.gm.coins);

        Database.InsertOnInventory(pl.id);
        inv.UpdateInventory();
    }
}