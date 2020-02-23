using System;
using Models;
using Repository;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class TodoItemController : MonoBehaviour
    {
        [SerializeField] private Button removeButton;
        [SerializeField] private TextMeshProUGUI textTitle;

        public static void CreateItem(GameObject itemPrefab,TodoItem item, Transform parent,TodoRepository repository)
        {
            var go = Instantiate(itemPrefab, parent).GetComponent<TodoItemController>();
            go.removeButton.onClick.AddListener(() =>
            {
                repository.RemoveItem(item.Id);
                Destroy(go.gameObject);
            });
            go.textTitle.text = item.Text;
        }

    }
}