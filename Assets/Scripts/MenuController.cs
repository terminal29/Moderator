using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{
    public CameraEffects cameraEffects;
    public GameObject intro1;
    public GameObject intro2;
    public GameObject mainMenu;
    public GameObject guide1;
    public GameObject guide2;
    public GameObject guide3;

    private List<GameObject> screens;

    public enum MenuScreen
    {
        Intro1,
        Intro2,
        MainMenu,
        Guide1,
        Guide2,
        Guide3
    };

    private MenuScreen currentScreen = MenuScreen.Intro1;

    public void EnableScreen(MenuScreen screen)
    {
        foreach (GameObject screenObject in screens)
        {
            screenObject.SetActive(false);
        }
        switch (screen)
        {
            case MenuScreen.Intro1:
                intro1.SetActive(true);
                break;
            case MenuScreen.Intro2:
                intro2.SetActive(true);
                break;
            case MenuScreen.MainMenu:
                mainMenu.SetActive(true);
                break;
            case MenuScreen.Guide1:
                guide1.SetActive(true);
                break;
            case MenuScreen.Guide2:
                guide2.SetActive(true);
                break;
            case MenuScreen.Guide3:
                guide3.SetActive(true);
                break;
        }
        currentScreen = screen;
    }

    // Start is called before the first frame update
    void Start()
    {
        screens = new List<GameObject> { intro1, intro2, mainMenu, guide1, guide2, guide3 };
        StartCoroutine(RunIntroAnimation());
    }

    private IEnumerator RunIntroAnimation()
    {
        EnableScreen(MenuScreen.Intro1);
        yield return new WaitForSeconds(3);
        EnableScreen(MenuScreen.Intro2);
        yield return new WaitForSeconds(3);
        EnableScreen(MenuScreen.MainMenu);
    }

    public void OnPlayPressed()
    {
        EnableScreen(MenuScreen.Guide1);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

    public void OnNextPressed()
    {
        if (currentScreen == MenuScreen.Guide1)
        {
            cameraEffects.Shake(0.1f, 0.1f);
            EnableScreen(MenuScreen.Guide2);
        }
        else if (currentScreen == MenuScreen.Guide2)
        {
            cameraEffects.Shake(0.1f, 0.1f);
            EnableScreen(MenuScreen.Guide3);
        }
        else if (currentScreen == MenuScreen.Guide3)
        {
            OnSkipPressed();
        }
    }

    public void OnSkipPressed()
    {
        cameraEffects.Shake(0.1f, 0.1f);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
