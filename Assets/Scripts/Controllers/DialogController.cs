using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controllers
{
    [RequireComponent(typeof(DialogStateMachine))]
    public class DialogController : MonoBehaviour
    {
        private GameObject _canvas;

        [FormerlySerializedAs("_dialog")] [SerializeField] private GameObject dialog;
        [FormerlySerializedAs("_inputField")] [SerializeField] private TMP_InputField inputField;
        [FormerlySerializedAs("_title")] [SerializeField] private TextMeshProUGUI title;
        [FormerlySerializedAs("_bntSim")] [SerializeField] private Button bntSim;
        [FormerlySerializedAs("_btnNao")] [SerializeField] private Button btnNao;

        private Action<DialogController> _onSim;
        private Action _onNao;
        private DialogStateMachine _stateMachine;

        public string Text
        {
            get => inputField.text;
            set => inputField.text = value;
        }

        public static DialogController NewInputDialog(GameObject canvas, GameObject prefab, string title,
            Action<DialogController> onSim,
            Action onNao)
        {
            var go = Instantiate(prefab, canvas.transform).GetComponent<DialogController>();
            go._onSim = onSim;
            go._onNao = onNao;
            go.title.text = title;
            go._canvas = canvas;
            go.enabled = false;
            go._stateMachine = go.GetComponent<DialogStateMachine>();
            go.bntSim.onClick.AddListener(call: () =>
            {
                go._onSim.Invoke(go);
                go.Hide();
            });
            go.btnNao.onClick.AddListener(() =>
            {
                go._onNao.Invoke();
                go.Hide();
            });
            return go;
        }

        public void Hide() => _stateMachine.ChangeState(true);

        public void Show() => _stateMachine.ChangeState(false);
    }
}