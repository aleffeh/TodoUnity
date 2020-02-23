using UnityEngine;
using UnityEngine.UI;

public class CrudManager : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _prefabDialog;
    [SerializeField] private Button _addButton;
    
    
    private DialogController _dialogController;
    
    void Start()
    {
        _dialogController = DialogController.NewInputDialog(_canvas, _prefabDialog,"dksds", (dialog) => { Debug.Log(dialog.Text); }, (() => { }));
        _addButton.onClick.AddListener(() => _dialogController.Show());
    }
    
    
}