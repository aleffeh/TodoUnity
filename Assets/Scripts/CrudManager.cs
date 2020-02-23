using Controllers;
using Models;
using Repository;
using SQLite4Unity3d;
using UnityEngine;
using UnityEngine.UI;

public class CrudManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject prefabDialog;
    [SerializeField] private Button addButton;
    [SerializeField] private Transform contentView;
    [SerializeField] private GameObject itemPrefab;
    private TodoRepository _repository;


    private DialogController _dialogController;

    void Start()
    {
        Screen.fullScreen = false;
        _dialogController = DialogController.NewInputDialog(canvas, prefabDialog, "NOVO REGISTRO", AddData,
            () => { });
        addButton.onClick.AddListener(() => _dialogController.Show());

        _repository = new TodoRepository();
        LoadData();
    }

    private void LoadData()
    {
        var items = _repository.GetItems();
        foreach (var todoItem in items)
            CreateItem(todoItem);
    }

    private void AddData(DialogController dialogController)
    {
        var text = dialogController.Text;
        if (string.IsNullOrEmpty(text))
            return;

        var item = new TodoItem {Text = text};
        _repository.AddItem(item);
        CreateItem(item);
    }
    
    private void CreateItem(TodoItem item) => TodoItemController.CreateItem(itemPrefab,item,contentView,_repository);
}