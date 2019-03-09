using UnityEngine;

public class BaseMenuController : BaseController, IGameActions
{
    // TODO Move to two button menu controller
    [Header("_______________", order = 0)]
    [Header("Base Menu Controller", order = 1)]
    [Header("--------------------", order = 2)]
    private string placeholder;

    public override void Awake()
    {
        StartGame();
    }
    public virtual void OnDestroy()
    {
        QuitGame();
    }

    public void StartGame()
    {
    }
    public void QuitGame()
    {
    }


    public void StartMainMenu()
    {
    }
    public void StartPlayerSelect()
    {
    }
    public void Play()
    {
    }
    public void ShowResults()
    {
    }


    public void PausePlay()
    {
    }
    public void Undo()
    {
    }
}
