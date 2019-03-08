using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region State Control structs
    public enum GameState
    {
        None,
        Boot,
        MainMenu,
        Shutdown,
        PlayerSelect,
        Path,
        Play,
        Pause,
        Results
    }

    [Serializable]
    public struct GameStateController
    {
        [SerializeField, ReadOnly] GameState state;
        [SerializeField, ReadOnly] GameState lastState;

        public bool allowRevert;

        public GameState State
        {
            get => state;
            private set
            {
                print("Game State switching to " + value.ToString());
                LastState = State;
                state = value;
            }
        }
        public GameState LastState { get; private set; }

        public GameStateController(GameState state) : this()
        {
            State = state;
        }

        /// <summary>
        /// Advances step to next step or towards desired step if possible
        /// </summary>
        /// <param name="desiredState"></param>
        /// <returns></returns>
        public bool AdvanceState(GameState desiredState = GameState.None)
        {
            if (state == desiredState)
            {
                print("Desired state is already active");
                return true;
            }
            else if (desiredState != GameState.None)
            {
                switch (State)
                {
                    case GameState.None:
                        State = GameState.Boot;
                        break;
                    case GameState.Boot:
                        State = GameState.MainMenu;
                        break;
                    case GameState.MainMenu:
                        if (desiredState == GameState.Shutdown)
                        {
                            State = GameState.Shutdown;
                            break;
                        }
                        State = GameState.PlayerSelect;
                        break;
                    case GameState.Shutdown:
                        // Exit game
                        break;
                    case GameState.PlayerSelect:
                        State = GameState.Path;
                        break;
                    case GameState.Path:
                        State = GameState.Play;
                        break;
                    case GameState.Play:
                        State = GameState.Results;
                        break;
                    case GameState.Pause:
                        if (desiredState == GameState.MainMenu)
                        {
                            State = GameState.MainMenu;
                            break;
                        }
                        RevertStateChange();
                        break;
                    case GameState.Results:
                        if (desiredState == GameState.MainMenu)
                        {
                            State = GameState.MainMenu;
                            break;
                        }
                        State = GameState.Play;
                        break;
                    default:
                        break;
                }

                if (state == desiredState)
                {
                    print("Desired state is now active.");
                    return true;
                }

                print("Trying to advance to state " + desiredState.ToString() + ".");
            }
            else
            {
                Debug.LogError("Cannot advance to " + desiredState.ToString() + "!");
            }

            
            return false;
        }

        public void RevertStateChange()
        {
            if (allowRevert && LastState != GameState.None)
                State = LastState;
        }
    }

    public enum TurnState
    {
        Start,
        Angle,
        Distance,
        Finish,
        Switch
    }

    [Serializable]
    public struct TurnStateController
    {

    }
    #endregion


    public GameStateController stateController;

    // Start is called before the first frame update
    void Start()
    {
        stateController = new GameStateController(GameState.Boot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
