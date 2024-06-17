using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] private List<MenuID> _menus = new();
    private int _currentMenuIndex = 0;

    private void Start()
    {
        foreach (MenuID menu in _menus)
        {
            menu.Menu.Setup();
            menu.Menu.OnMenuChange += HandleMenuChange;
            menu.Menu.gameObject.SetActive(false);
        }

        if (_menus.Count > 0)
        {
            _menus[_currentMenuIndex].Menu.gameObject.SetActive(true);
        }
    }

    private void HandleMenuChange(string id)
    {
        for (int i = 0; i < _menus.Count; i++)
        {
            var menu = _menus[i];
            if (menu.ID == id)
            {
                _menus[_currentMenuIndex].Menu.gameObject.SetActive(false);
                menu.Menu.gameObject.SetActive(true);
                _currentMenuIndex = i;
                break;
            }
        }
    }
}

[Serializable]
public struct MenuID
{
    [field: SerializeField] public string ID { get; set; }
    [field: SerializeField] public Menu Menu { get; set; }
}
