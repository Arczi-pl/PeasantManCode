using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class Database : MonoBehaviour
{
	string dbName;
	IDbConnection dbConnection;
    void Start()
    {
		dbName = "URI=file:" + Application.persistentDataPath + "/" + "Database.db";
		dbConnection = new SqliteConnection(dbName);
        createDB();
		insertLevels();
    }

    public void createDB() {
		dbConnection.Open();
		IDbCommand dbCommand = dbConnection.CreateCommand();

		dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS levels (level_id INTEGER PRIMARY KEY, is_unlocked INTEGER, UNIQUE(level_id))";
		dbCommand.ExecuteNonQuery();
        
		dbConnection.Close();
    }

	public void insertLevels() {
		dbConnection.Open();
		IDbCommand dbCommand = dbConnection.CreateCommand();
			
		for (int i = 1; i <= 10; i++)
		{	
			try 
			{
				int status = 0;
				if (i == 1) 
					status = 1;

				dbCommand = dbConnection.CreateCommand();
				dbCommand.CommandText = "INSERT INTO levels (level_id, is_unlocked) VALUES (" + i.ToString() + ", " + status + ")";
				dbCommand.ExecuteNonQuery();
			}
			catch (SqliteException)
			{
				continue;
			}
		}
		
		dbConnection.Close();
	}

	public string[] getUnlockedLevels() {
		dbConnection.Open();
		IDbCommand dbCommand = dbConnection.CreateCommand();

		dbCommand = dbConnection.CreateCommand();
		dbCommand.CommandText = "SELECT level_id FROM levels WHERE is_unlocked=1";
		IDataReader reader = dbCommand.ExecuteReader();
		string unlockedLevels = "";
		while(reader.Read())
		{
			unlockedLevels += reader[0].ToString() + ";";
		}
		
		dbConnection.Close();
		return unlockedLevels.Remove(unlockedLevels.Length - 1, 1).Split(';');
	}

	public void UnlockLevel(int level)
	{
		dbConnection.Open();
		IDbCommand dbCommand = dbConnection.CreateCommand();

		dbCommand = dbConnection.CreateCommand();
		dbCommand.CommandText = "UPDATE levels SET is_unlocked=1 WHERE level_id=" + level;
		dbCommand.ExecuteNonQuery();

		dbConnection.Close();
	}
}
