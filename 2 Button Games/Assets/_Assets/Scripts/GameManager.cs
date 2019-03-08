using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    #region Singleton Instance
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance;  } }
    #endregion
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
        [SerializeField, ReadOnly] private GameState state;
        [SerializeField, ReadOnly] private GameState lastState;

        public bool allowRevert;

        public GameState State
        {
            get => state;
            private set
            {
                print("Game State set to " + value.ToString());
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

    public enum PlayState
    {
        NotPlaying,
        Waiting,
        Start,
        Angle,
        Distance,
        Finish,
        Switch
    }

    [Serializable]
    public struct PlayStateController
    {
        [SerializeField, ReadOnly] private PlayState state;
        [SerializeField, ReadOnly] private PlayState lastState;
        [SerializeField, ReadOnly] private int currentPlayerIndex;
        [SerializeField, ReadOnly] private static int nextPlayerId = -1;
        [SerializeField, ReadOnly] private List<int> playerIds;

        public bool allowRevert;
        
        public PlayState State
        {
            get => state;
            private set
            {
                print("Play State set to " + value.ToString());
                LastState = State;
                state = value;
            }
        }
        public PlayState LastState { get => lastState; private set => lastState = value; }

        public PlayStateController(PlayState state) : this()
        {
            State = state;
        }

        public bool AdvanceState(PlayState desiredState = PlayState.NotPlaying)
        {
            if (state == desiredState)
            {
                print("Desired play state is already active");
                return true;
            }
            else if (desiredState != PlayState.NotPlaying)
            {
                switch (State)
                {
                    case PlayState.NotPlaying:
                        State = PlayState.Waiting;
                        break;
                    case PlayState.Waiting:
                        State = PlayState.Start;
                        break;
                    case PlayState.Start:
                        State = PlayState.Angle;
                        break;
                    case PlayState.Angle:
                        State = PlayState.Distance;
                        break;
                    case PlayState.Distance:
                        State = PlayState.Finish;
                        break;
                    case PlayState.Finish:
                        State = PlayState.Switch;
                        break;
                    case PlayState.Switch:
                        State = PlayState.Waiting;
                        break;
                    default:
                        break;
                }
                
                if (state == desiredState)
                {
                    print("Desired play state is now active.");
                    return true;
                }

                print("Trying to advance to play state " + desiredState.ToString() + ".");
            }
            else
            {
                Debug.LogError("Cannot advance to play state " + desiredState.ToString() + "!");
            }


            return false;
        }

        public void RevertStateChange()
        {
            if (allowRevert && LastState != PlayState.NotPlaying)
                State = LastState;
        }

        public void DetermineStartingPlayer(int overridePlayerId = -1)
        {
            var playerIdIndex = playerIds.Find(x => x == overridePlayerId);
            if (playerIdIndex == -1) {
                playerIdIndex = UnityEngine.Random.Range(0, playerIds.Count);
            }

            currentPlayerIndex = playerIdIndex;
        }

        public bool IsCurrentPlayer(int playerId)
        {
            return playerId == playerIds[currentPlayerIndex];
        }

        public int AddPlayer()
        {
            var id = nextPlayerId++;
            playerIds.Add(id);
            return id;
        }

        public bool RemovePlayer(int playerId)
        {
            return playerIds.Remove(playerId);
        }
            
    }
    #endregion

    public enum InputMode
    {
        OneButton,
        TwoButtons,
        ThreeButtons,
        Full,
    }
    public InputMode inputMode = InputMode.TwoButtons;

    [Header("State controls")]
    public GameStateController gameStateController;
    public PlayStateController playStateController;

    public float autoAdvanceTimer = 5f;

    [SerializeField, ReadOnly] private float elapsedAdvanceTime = 0;
    [SerializeField, ReadOnly] private bool takeInfluence = false;

    public void Awake()
    {
        #region Singleton Instance
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        gameStateController = new GameStateController(GameState.Boot);
        playStateController = new PlayStateController(PlayState.NotPlaying);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
