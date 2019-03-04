using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Menu GameObjects")]
    public GameObject MainMenuScreen;
    public GameObject GamesScreen;
    public GameObject OptionsScreen;
    public GameObject CreditsScreen;

    [Header("Button Sounds")]
    public bool AlternateSounds;

    [Header("Button Sounds")]
    public AudioSource launch;

    [Header("Button Sounds")]
    public AudioSource forward;
    public AudioSource backward;
    public AudioSource select;
    public AudioSource gameSelect;

    [Header("Alternative Button Sounds")]
    public AudioSource forwardAlt;
    public AudioSource backwardAlt;
    public AudioSource selectAlt;
    public AudioSource gameSelectAlt;

    private bool goingBack;
    private bool goingForward;
    private bool goingGame;

    /*
    void Start()
    {

    }

    void Update()
    {
        
    }
    */

    public void MouseHover()
    {
        select.Play(0);
    }

    private void DeactivateAllScreens()
    {
        MainMenuScreen.SetActive(false);
        GamesScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }
    public void GoToCredits()
    {
        DeactivateAllScreens();
        CreditsScreen.SetActive(true);
        forward.Play(0);
    }
    public void GoToOptions()
    {
        DeactivateAllScreens();
        OptionsScreen.SetActive(true);
        forward.Play(0);
    }
    public void GoToGames()
    {
        DeactivateAllScreens();
        GamesScreen.SetActive(true);
        forward.Play(0);
    }
    public void GoToMainMenu()
    {
        DeactivateAllScreens();
        MainMenuScreen.SetActive(true);
        backward.Play(0);
    }

    public void SelectedGame1()
    {
        DeactivateAllScreens();
        gameSelect.Play(0);
    }
    public void SelectedGame2()
    {
        DeactivateAllScreens();
        gameSelect.Play(0);
    }
    public void SelectedGame3()
    {
        DeactivateAllScreens();
        gameSelect.Play(0);
    }
}
