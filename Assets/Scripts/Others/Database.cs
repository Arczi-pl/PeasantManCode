using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class Database : MonoBehaviour
{
	private string _dbName;
	private IDbConnection _dbConnection;

    void Start()
    {
		_dbName = "URI=file:" + Application.persistentDataPath + "/" + "Database.db";
		_dbConnection = new SqliteConnection(_dbName);
		// create table and insert all levels locked if not exists
        CreateDB();
		InsertLevels();
    }

    public void CreateDB() {
		_dbConnection.Open();
		IDbCommand dbCommand = _dbConnection.CreateCommand();

		dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS levels (level_id INTEGER PRIMARY KEY, is_unlocked INTEGER, UNIQUE(level_id))";
		dbCommand.ExecuteNonQuery();
        
		_dbConnection.Close();
    }

	public void InsertLevels() {
		_dbConnection.Open();
		IDbCommand dbCommand = _dbConnection.CreateCommand();
			
		for (int i = 1; i <= 10; i++)
		{	
			try 
			{
				int status = 0;
				if (i == 1) 
					status = 1;

				dbCommand = _dbConnection.CreateCommand();
				dbCommand.CommandText = "INSERT INTO levels (level_id, is_unlocked) VALUES (" + i.ToString() + ", " + status + ")";
				dbCommand.ExecuteNonQuery();
			}
			catch (SqliteException)
			{
				continue;
			}
		}
		
		_dbConnection.Close();
	}

	public string[] GetUnlockedLevels() {
		_dbConnection.Open();
		IDbCommand dbCommand = _dbConnection.CreateCommand();

		dbCommand = _dbConnection.CreateCommand();
		dbCommand.CommandText = "SELECT level_id FROM levels WHERE is_unlocked=1";
		IDataReader reader = dbCommand.ExecuteReader();
		string unlockedLevels = "";
		while(reader.Read())
		{
			unlockedLevels += reader[0].ToString() + ";";
		}
		
		_dbConnection.Close();
		return unlockedLevels.Remove(unlockedLevels.Length - 1, 1).Split(';');
	}

	public void UnlockLevel(int level)
	{
		_dbConnection.Open();
		IDbCommand dbCommand = _dbConnection.CreateCommand();

		dbCommand = _dbConnection.CreateCommand();
		dbCommand.CommandText = "UPDATE levels SET is_unlocked=1 WHERE level_id=" + level;
		dbCommand.ExecuteNonQuery();

		_dbConnection.Close();
	}

	public void ResetProgress()
	{
		_dbConnection.Open();
		IDbCommand dbCommand = _dbConnection.CreateCommand();

		for (int i = 2; i <= 10; i++)
		{	
			dbCommand = _dbConnection.CreateCommand();
			dbCommand.CommandText = "UPDATE levels SET is_unlocked=0 WHERE level_id=" + i;
			dbCommand.ExecuteNonQuery();
		}

		_dbConnection.Close();
	}
}
