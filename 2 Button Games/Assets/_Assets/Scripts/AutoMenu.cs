using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoMenu : MonoBehaviour
{
    public enum MenuType
    {
        StartMenu,
        SelectionMenu,
        PathSelectMenu,
        PowerSelectMenu
    }

    public MenuType currentMenu = MenuType.StartMenu;

    [Serializable]
    public struct Menu
    {
        public string name;
        public GameObject UIElement;
        public MenuType type;

        public Menu(string name, GameObject UIElement, MenuType type)
        {
            this.name = name;
            this.UIElement = UIElement;
            this.type = type;
        }
    }

    public List<Menu> menus = new List<Menu>();

    public float waitTime = 5f;
    public bool gameIsDone = false;

    float elapsedTime = 0;

    void Start()
    {
        SwitchActiveMenu(currentMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (menus != null && menus.Count > 0)
        {
            if (elapsedTime >= waitTime)
            {
                elapsedTime = 0;

                switch (currentMenu)
                {
                    case MenuType.StartMenu:
                        SwitchActiveMenu(MenuType.SelectionMenu);
                        break;
                    case MenuType.SelectionMenu:
                        SwitchActiveMenu(MenuType.PathSelectMenu);
                        break;
                    case MenuType.PathSelectMenu:
                        SwitchActiveMenu(MenuType.PowerSelectMenu);
                        break;
                    case MenuType.PowerSelectMenu:
                        if (gameIsDone)
                        {
                            SwitchActiveMenu(MenuType.StartMenu);
                        }
                        else
                        {
                            SwitchActiveMenu(MenuType.PathSelectMenu);
                        }
                        break;
                    default:
                        break;
                }
            }

            elapsedTime += Time.deltaTime;
        }
    }

    void SwitchActiveMenu(MenuType desiredMenu)
    {
        menus.Find(menuItem => menuItem.type == currentMenu).UIElement.SetActive(false);
        menus.Find(menuItem => menuItem.type == desiredMenu).UIElement.SetActive(true);
        currentMenu = desiredMenu;
    }
}
