using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Data;
using Mono.Data.Sqlite;

public class Database : MonoBehaviour
{
    IDbConnection conn;

    string db_name = "entifarm.db";

    // Start is called before the first frame update
    void Start()
    {
        conn = new SqliteConnection(string.Format("URI=file:{0}", db_name));

        conn.Open();

        Debug.Log(GetPlants()[1].ToString());
    }

    public ArrayList GetPlants()
    {
        ArrayList res = new ArrayList();

        string query = "SELECT * FROM plants;";
        IDbCommand comm = conn.CreateCommand();

        comm.CommandText = query;

        IDataReader reader = comm.ExecuteReader();

        res.Add(reader[0].ToString());
        res.Add(reader[1].ToString());
        res.Add(reader[2].ToString());
        res.Add(reader[3].ToString());
        res.Add(reader[4].ToString());
        res.Add(reader[5].ToString());

        return res;
    }
}