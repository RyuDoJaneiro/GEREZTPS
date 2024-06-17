using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject buttonsContainer;

    public event Action<string> OnMenuChange = delegate { };

    public void Setup()
    {
        foreach (Transform button in buttonsContainer.transform)
        {
            button.GetComponent<ButtonController>().Setup(HandleButtonClick);
        }
    }

    private void HandleButtonClick(string id)
    {
        OnMenuChange?.Invoke(id);
    }
}
