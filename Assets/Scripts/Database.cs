using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Data;
using Mono.Data.Sqlite;
using System;

public class Plants
{
    public int id;
    public string name;
    public float time;
    public int quantity;
    public float sell_price;
    public float buy_price;
    public int season = 0;
}
public class Database : MonoBehaviour
{
    public static IDbConnection conn;

    string db_name = "entifarm.db";

    // Start is called before the first frame update
    void Awake()
    {
        conn = new SqliteConnection(string.Format("URI=file:{0}", db_name));

        conn.Open();

        Plants a = new Plants();

        GetPlants();
    }

    public static List<ArrayList> SendQuery(string query)
    {
        List<ArrayList> res = new List<ArrayList>();

        using (IDbCommand cmd = conn.CreateCommand())
        {
            cmd.CommandText = query;

            using (IDataReader reader = cmd.ExecuteReader())
            {
                int aux = 0;

                while (reader.Read())
                {
                    res.Add(new ArrayList());
                    res[aux] = new ArrayList();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        res[aux].Add(reader[i].ToString());
                        Debug.Log(reader[i].ToString());
                    }

                    aux++;
                }
            }
        }

        return res;
    }

    public static List<ArrayList> GetInventory()
    {
        List<ArrayList> res = new List<ArrayList>();

        using (IDbCommand cmd = conn.CreateCommand())
        {
            cmd.CommandText = "SELECT * FROM plants_users";

            using (IDataReader reader = cmd.ExecuteReader())
            {
                int aux = 0;

                while (reader.Read())
                {
                    res.Add(new ArrayList());
                    res[aux] = new ArrayList();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        res[aux].Add(reader[i].ToString());
                        Debug.Log(reader[i].ToString());
                    }

                    aux++;
                }
            }
        }

        return res;
    }

    public static List<ArrayList> GetPlants()
    {
        List<ArrayList> res = new List<ArrayList>();

        string query = "SELECT * FROM plants";

        /*
        IDbCommand comm = conn.CreateCommand();

        comm.CommandText = query;

        
        

        IDataReader reader = comm.ExecuteReader();
        
        while(reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                res.Add(reader.GetValue(i));
                Debug.Log(reader.GetValue(i));
            }
        }
        */

        // Crea un comando para ejecutar la query
        using (IDbCommand comm = conn.CreateCommand())
        {
            comm.CommandText = query;


            // Ejecuta la query
            using (IDataReader reader = comm.ExecuteReader())
            {
                int aux = 0;
                // Lee los datos
                while (reader.Read())
                {                
                    res.Add(new ArrayList());
                    res[aux] = new ArrayList();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        res[aux].Add(reader[i].ToString());
                        Debug.Log(reader[i].ToString());
                    }

                    aux++;
                }
            }

            return res;
        }
    }
}