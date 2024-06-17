using System;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour
{
    [SerializeField] private string _ID;
    private Button _button;
    public event Action<string> OnClick = delegate { };

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleButtonClick);
    }

    public void Setup(Action<string> onClick)
    {
        OnClick = onClick;
    }

    private void HandleButtonClick()
    {
        OnClick?.Invoke(_ID);
    }
}
