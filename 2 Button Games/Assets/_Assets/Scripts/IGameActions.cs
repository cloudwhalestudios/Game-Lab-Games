using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameActions
{
    void StartGame();
    void QuitGame();
    void StartMainMenu();
    void StartPlayerSelect();
    void Play();
    void ShowResults();
    void Undo();
    void PausePlay();
}