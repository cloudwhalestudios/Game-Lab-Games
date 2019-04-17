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
    public bool alternateSounds;

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

    private bool pressStart;

    public GameObject pressSpace;

    void Update()
    {
        if (!pressStart)
        {
            if (Input.GetKeyDown("space"))
            {
                pressStart = true;
                pressSpace.SetActive(false);
                GoToScreen(MainMenuScreen, forward, forwardAlt);
            }
        }
    }

    public void MouseHover()
    {
    	if (!alternateSounds) {
    		select.Play(0);
    	} else if (alternateSounds) {
    		selectAlt.Play(0);
    	}
    }

    public void PlayBackSound()
    {
        forward.Play(0);
    }

    private void DeactivateAllScreens()
    {
        MainMenuScreen.SetActive(false);
        GamesScreen.SetActive(false);
        OptionsScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }
    private void GoToScreen(GameObject targetScreen, AudioSource sfx, AudioSource altSfx = null) {
    	DeactivateAllScreens();
    	if (targetScreen != null) {
    		targetScreen.SetActive(true);
    	}
    	if (sfx != null && !alternateSounds) {
    		sfx.Play(0);
    	} else if (altSfx != null && alternateSounds) {
    		altSfx.Play(0);
    	}
    }
    public void GoToCredits()
    {
        GoToScreen(CreditsScreen, forward, forwardAlt);
    }
    public void GoToOptions()
    {
        GoToScreen(OptionsScreen, forward, forwardAlt);
    }
    public void GoToGames()
    {
        GoToScreen(GamesScreen, forward, forwardAlt);
    }
    public void GoToMainMenu()
    {
        GoToScreen(MainMenuScreen, backward, backwardAlt);
    }

    public void SelectedGame1()
    {
        GoToScreen(null, gameSelect, gameSelectAlt);
        pressStart = false;
        pressSpace.SetActive(true);
    }
    public void SelectedGame2()
    {
        GoToScreen(null, gameSelect, gameSelectAlt);
        pressStart = false;
        pressSpace.SetActive(true);
    }
    public void SelectedGame3()
    {
        GoToScreen(null, gameSelect, gameSelectAlt);
        pressStart = false;
        pressSpace.SetActive(true);
    }    
}
