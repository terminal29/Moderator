using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
[RequireComponent(typeof(AudioSource))]
public class MenuController : MonoBehaviour
{
    public CameraEffects cameraEffects;
    public GameObject intro1;
    public GameObject intro2;
    public GameObject mainMenu;
    public GameObject guide1;
    public GameObject guide2;
    public GameObject guide3;

    private AudioSource source;
    public AudioClip buttonPress;
    public AudioClip startupWhine;
    public AudioClip menuButtonPress;
    public AudioSource whinePlayer;

    private List<GameObject> screens;

    public enum MenuScreen
    {
        None,
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
        source = GetComponent<AudioSource>();
        StartCoroutine(RunIntroAnimation());
    }

    private IEnumerator RunIntroAnimation()
    {
        EnableScreen(MenuScreen.None);
        source.PlayOneShot(buttonPress);
        yield return new WaitForSeconds(0.5f);
        source.PlayOneShot(startupWhine, 0.2f);
        yield return new WaitForSeconds(0.5f);
        EnableScreen(MenuScreen.Intro1);
        for (int i = 0; i < 30; i++)
        {
            whinePlayer.volume = (i / 30f) * 0.07f;
            yield return new WaitForSeconds(0.1f);
        }
        EnableScreen(MenuScreen.Intro2);
        yield return new WaitForSeconds(3);
        EnableScreen(MenuScreen.MainMenu);
    }

    public void OnPlayPressed()
    {
        source.PlayOneShot(menuButtonPress);
        EnableScreen(MenuScreen.Guide1);
    }

    public void OnQuitPressed()
    {
        source.PlayOneShot(menuButtonPress);
        Application.Quit();
    }

    public void OnNextPressed()
    {
        cameraEffects.Shake(0.1f, 0.1f);
        source.PlayOneShot(menuButtonPress);
        if (currentScreen == MenuScreen.Guide1)
        {
            EnableScreen(MenuScreen.Guide2);
        }
        else if (currentScreen == MenuScreen.Guide2)
        {
            EnableScreen(MenuScreen.Guide3);
        }
        else if (currentScreen == MenuScreen.Guide3)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnSkipPressed()
    {
        cameraEffects.Shake(0.1f, 0.1f);
        source.PlayOneShot(menuButtonPress);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
