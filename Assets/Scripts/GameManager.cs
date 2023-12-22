using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm { get; private set; }

    [SerializeField] private Animator shop_anim;
    private bool set_shop = false;

    public float coins = 100;
    [SerializeField] private UnityEngine.UI.Text coins_text;

    public List<Plants> plants;

    public string user = "";
    public int id_user = -1;
    
    [SerializeField] private TMP_InputField registerField_name;
    [SerializeField] private TMP_InputField registerField_psw;
    [SerializeField] private TMP_InputField loginField_name;
    [SerializeField] private TMP_InputField loginField_psw;

    void Start()
    {
        if (gm != null)
            Destroy(this);
        else
            gm = this;

        DontDestroyOnLoad(this);

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

        // Añade los listeners
        if (registerField_name != null)
        {
            registerField_name.onEndEdit.AddListener(delegate { OnEnterUserData(registerField_name, registerField_psw, false); });
            registerField_psw.onEndEdit.AddListener(delegate { OnEnterUserData(registerField_name, registerField_psw, false); });
            loginField_name.onEndEdit.AddListener(delegate { OnEnterUserData(loginField_name, loginField_psw, true); });
            loginField_psw.onEndEdit.AddListener(delegate { OnEnterUserData(loginField_name, loginField_psw, true); });
        }

        // Suscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        if (coins_text != null)
            coins_text.text = "Coins: " + n.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            set_shop = !set_shop;
            if (shop_anim != null)
                shop_anim.SetBool("scroll", set_shop);
        }
    }

    public string GetMD5Hash(string text)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = md5.ComputeHash(textBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2")); /// El x2 hace que se codifique de forma hexadecimal
            }

            return sb.ToString();
        }
    }

    public bool LoginUser(string us, string psw)
    {
        List<ArrayList> list = Database.GetUser(us, psw);

        if (list == null)
        {
            Debug.Log(string.Format("Usuario: {0} no encontrado en la base de datos", us));
            return false;
        }

        id_user = int.Parse(list[0][0].ToString());
        user = list[0][1].ToString();

        ChangeScene("Main");

        return true;
    }

    public bool RegisterUser(string user, string psw)
    {
        if (user.Length <= 3 || psw.Length <= 3)
        {
            Debug.Log("Contraseña o usuario incorrectos");
            return false;
        }

        Database.CreateUser(user, psw);

        ChangeScene("Main");

        return true;
    }

    private void OnEnterUserData(TMP_InputField name_in, TMP_InputField psw_in, bool login)
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && name_in.text != "" && psw_in.text != "")
        {
            string name = "";
            string psw = "";

            name = name_in.text;
            psw = psw_in.text;

            if (login)
                LoginUser(name, psw);
            else
                RegisterUser(name, psw);
        }
    }

    private void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            shop_anim = GameObject.Find("Shop items").GetComponent<Animator>();
            coins_text = GameObject.Find("Coins text").GetComponent<UnityEngine.UI.Text>();
        }
    }
}