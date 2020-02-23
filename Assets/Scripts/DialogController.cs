using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private GameObject _canvas;
    
    [SerializeField] private GameObject _dialog;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Button _bntSim;
    [SerializeField] private Button _btnNao;

    [SerializeField] private Action<DialogController> _onSim;
    [SerializeField] private Action _onNao;
    [SerializeField] private DialogStateMachine _stateMachine;
    
    public string Text
    {
        get => _inputField.text;
        set => _inputField.text = value;
    }
    
    public static DialogController NewInputDialog(GameObject canvas, GameObject prefab, string title,
        Action<DialogController> onSim,
        Action onNao)
    {
        var go = Instantiate(prefab, canvas.transform).GetComponent<DialogController>();
        go._onSim = onSim;
        go._onNao = onNao;
        go._title.text = title;
        go._canvas = canvas;
        go.enabled = false;
        go._bntSim.onClick.AddListener(call: () =>
        {
            go._onSim.Invoke(go);
            go.Hide();
            
        });
        go._btnNao.onClick.AddListener(() =>
        {
            go._onNao.Invoke();
            go.Hide();
        });
        return go;
    }

    public void Hide() => _stateMachine.ChangeState(true);

    public void Show() => _stateMachine.ChangeState(false);
}