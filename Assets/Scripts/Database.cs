using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Data;
using Mono.Data.Sqlite;
using System;

public class Plants
{
    public string id;
    public string name;
    public string time;
    public string quantity;
    public string sell_price;
    public string buy_price;
    public string season = "0";
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

        GetPlants();
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

        // Crea un ocmando para ejecutar la query
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