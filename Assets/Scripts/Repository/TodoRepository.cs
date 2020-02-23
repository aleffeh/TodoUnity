using System.Collections.Generic;
using Models;
using SQLite4Unity3d;
using UnityEngine;

#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif

namespace Repository
{
    public class TodoRepository
    {
        private SQLiteConnection _connection;

        public TodoRepository()
        {
#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", "todolist.db");
#else
     // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, "todolist.db");

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
        var loadDb =
        new WWW("jar:file://" + Application.dataPath + "!/assets/" + "todolist.db");  // this is the path to your StreamingAssets in android
        while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
        // then save to Application.persistentDataPath
        File.WriteAllBytes(filepath, loadDb.bytes);
#else
	        var loadDb =
     Application.dataPath + "/StreamingAssets/" + "todolist.db";  // this is the path to your StreamingAssets in iOS
	        // then save to Application.persistentDataPath
	        File.Copy(loadDb, filepath);

#endif
            }
            var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            _connection.CreateTable<TodoItem>();
            _connection.CreateTable<TodoItem>();
        }

        public IEnumerable<TodoItem> GetItems() => _connection.Table<TodoItem>();

        public void RemoveItem(int id) => _connection.Delete<TodoItem>(id);

        public void AddItem(TodoItem item) => _connection.Insert(item);
    }
}