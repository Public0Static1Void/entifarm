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
    IDbConnection conn;

    string db_name = "entifarm.db";

    // Start is called before the first frame update
    void Start()
    {
        conn = new SqliteConnection(string.Format("URI=file:{0}", db_name));

        conn.Open();

        Plants a = new Plants();
        ArrayList b = GetPlants();

        a.id = b[0].ToString();
        Debug.Log(GetPlants()[1].ToString());
    }

    public ArrayList GetPlants()
    {
        ArrayList res = new ArrayList();

        string query = "SELECT * FROM plants;";
        IDbCommand comm = conn.CreateCommand();

        comm.CommandText = query;

        IDataReader reader = comm.ExecuteReader();

        for (int i = 0; i < reader.FieldCount; i++)
        {
            res.Add(reader[i].ToString());
        }

        conn.Close();

        return res;
    }
}