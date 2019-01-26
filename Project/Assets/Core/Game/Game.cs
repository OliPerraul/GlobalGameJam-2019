using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Core
{
    public delegate void OnVisit();
    public delegate void OnBuyStatusChanged();

    public class Game : MonoBehaviour
    {
        [SerializeField]
        public int MainId = 0;


        public enum StateEnum
        {
            Start,
            AwaitingVisitor,
            Visit,
            End,
            StartScreen,
        }

        public static Game Instance;

        //public int MainId = 0;

        int numPlayers = 0;


        public OnVisit OnVisitHandler;
        public OnBuyStatusChanged OnBuyStatusChangedHandler;

        public float time = 0f;
        public float NextVisitTime = 0f;
        public float BuyStatus = 0f;
        public ConfigValues Values;


        public float TrapSpawnTime = 0f;
        public float TrapSpawnTimeLimit = 0f;


        public GameObject player1;
        public GameObject player2;


        public StateEnum State = StateEnum.StartScreen;

        public Collectibles.Resource Collectible;

        public int AICount = 0;


        public void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
            SceneManager.LoadScene("StartScreen");
            //OnVisitHandler.ON

        }


        public void StartGame(int i = 0)
        {
            switch (MainId)
            {
                case 0:

                    SceneManager.LoadScene("Main");
                    break;

                case 1:
                    SceneManager.LoadScene("Main1");
                    break;

                case 2:
                    SceneManager.LoadScene("Main2");
                    break;

            }


            SetState(StateEnum.Start);

            Values = Config.Instance.StartValues; // copy
            NextVisitTime = Random.Range(Config.Instance.StartValues.VisitTimeMin, Config.Instance.StartValues.VisitTimeMax);

            numPlayers = i;
            Invoke("StartUP", 1f);
           

        }


        public void StartUP()
        {
            int i = numPlayers;
            if (i == 0)
            {
                player1 =
                Instantiate(Library.Instance.Player1,
                Level.Instance.SpawnPointP1.transform.position,
                Quaternion.identity);

                Level.Instance.player = player1.GetComponentInChildren<Player>();

                CompleteProject.CameraFollow.Instance.target = Level.Instance.player.transform;
                //Level.Instance.player = player1.GetComponentInChildren<Player>();

            }
            else if (i == 1)
            {
                player2 =
                Instantiate(Library.Instance.Player2,
                Level.Instance.SpawnPointP2.transform.position,
                Quaternion.identity);

                Level.Instance.player = player2.GetComponentInChildren<Player>();
                //Level.Instance.player = player2.GetComponentInChildren<Player>();

                CompleteProject.CameraFollow.Instance.target = Level.Instance.player.transform;
                CompleteProject.CameraFollow.Instance.DoStart();



            }
            else if (i == 2)
            {
                player1 =
                Instantiate(Library.Instance.Player1,
                Level.Instance.SpawnPointP1.transform.position,
                Quaternion.identity);

                player2 =
                Instantiate(Library.Instance.Player2,
                Level.Instance.SpawnPointP2.transform.position,
                Quaternion.identity);

                Level.Instance.player = player1.GetComponentInChildren<Player>();
                var p2t = player2.GetComponentInChildren<Player>();
                Level.Instance.player2 = p2t;

                CompleteProject.CameraFollow.Instance.target = Level.Instance.player.transform;
                CompleteProject.CameraFollow.Instance.target2 = p2t.transform;
                CompleteProject.CameraFollow.Instance.DoStart();

            }
        }



        public void EndGame()
        {
            SceneManager.LoadScene("StartScreen");
            //SetState(StateEnum.StartScreen);
        }



        public void SetState(StateEnum s)
        {
            State = s;

            switch (State)
            {
                case StateEnum.Start:
                    time += 0;
                    break;

                case StateEnum.AwaitingVisitor:
                    time = 0;
                    BuyStatus = 0;
                    if (OnBuyStatusChangedHandler != null) OnBuyStatusChangedHandler.Invoke();
                    break;

                case StateEnum.Visit:
                    time = 0;
                    NextVisitTime = -1; // TODO
                    BuyStatus = Values.StartBuyStatus;
                    State = StateEnum.Visit;


                    //INST AGENT (VISITOR AND AGENTS ARE THE SAME NOW)
                    GameObject reAgent =
                        Instantiate(Library.Instance.REAgent,
                        Level.Instance.SpawnPoint.transform.position,
                        Quaternion.identity);

                    AgentAI ai = reAgent.GetComponentInChildren<AgentAI>();
                    ai.RegisterSnapPoints(Level.Instance.DownSnapPointsArray, Level.Instance.UpperSnapPointsArray);

                    ai.HP = Random.Range(Values.MinHP, Values.MaxHP)*2;
                    ai.MAXHP = ai.HP;

                    // INSTANTIATE VISIT
                    AICount = 1;
                    for (int i = 0; i < Random.Range(Values.NumVisitorMin, Values.NumVisitorMax); i++)
                    {
                        AICount++;
                        GameObject visitor =
                        Instantiate(Library.Instance.Visitor,
                        Level.Instance.SpawnVisitors[i].transform.position,
                        Quaternion.identity);

                        VisitorAI vai = visitor.GetComponentInChildren<VisitorAI>();


                        vai.agentTransform = ai.transform;

                        // Register on agent arrived event
                        ai.OnArrivedToDestinationHandler += vai.OnREAgentArrivedToDest;
                        ai.OnLeaderWantsExitHandler += vai.OnLeaderWantsQuit;

                        vai.HP = Random.Range(Values.MinHP, Values.MaxHP);
                        vai.MAXHP = vai.HP;

                    }

  
                    if (OnVisitHandler != null) OnVisitHandler.Invoke();
                    break;

                case StateEnum.StartScreen:
                    EndGame();

                    break;
            }
        }


        public void SpawnTrap()
        {
            GameObject[] choices =
                { Library.Instance.Collect1,
                Library.Instance.Collect2 ,
                Library.Instance.Collect3 ,
                Library.Instance.Collect4 };

            int choiceIdx = 0;
            do
            {
                choiceIdx = Random.Range(0, choices.Length);


            }
            while (choices[choiceIdx] == null);


            int iidx = Random.Range(0, Level.Instance.CollectibleSnapsArray.Length);

            Vector3 spwnPt = Level.Instance.CollectibleSnapsArray[iidx];
            Instantiate(choices[choiceIdx], spwnPt, Quaternion.identity);
        }


        public void OnWaveCleared()
        {
            Instantiate(Library.Instance.TextWaveClear, UI.HUD.Instance.transform);
            SetState(StateEnum.Start);

            Instance.Values.BuyStatusIncrement += Difficulty.Instance.BuyStatusIncrementIncr;
            Instance.Values.BuyStatusComplete += Difficulty.Instance.BuyStatusCompleteIncr;
            Instance.Values.StartBuyStatus += Difficulty.Instance.StartBuyStatusIncr;
            Instance.Values.MinHP += Difficulty.Instance.MinHPIncr;
            Instance.Values.MaxHP += Difficulty.Instance.MaxHPIncr;

        }
        

        public void Update()
        {
            switch (State)
            {
                case StateEnum.AwaitingVisitor:
                case StateEnum.Visit:
                    TrapSpawnTime += Time.deltaTime;

                    if (TrapSpawnTime >= TrapSpawnTimeLimit)
                    {
                        SpawnTrap();
                        TrapSpawnTime = 0;
                        TrapSpawnTimeLimit = Random.Range(Values.TrapSpawnFreqMin, Values.TrapSpawnFreqMax);
                    }

                break;

            }

            switch(State)
            {
                case StateEnum.Start:
                    time += Time.deltaTime;
                    // START VISIT
                    if (time >= Values.StartupTime) // Do visit
                    {
                        SetState(StateEnum.AwaitingVisitor);

                    }
                    break;

                case StateEnum.AwaitingVisitor:
                    time += Time.deltaTime;

                    // START VISIT
                    if (time >= NextVisitTime) // Do visit
                    {
                        SetState(StateEnum.Visit);

                    }

                    break;

                case StateEnum.Visit:
                    // UPDATE VISIT
                    if (Values.BuyStatusIncrement < 0)
                        Values.BuyStatusIncrement = 1;
                    BuyStatus += Values.BuyStatusIncrement;

                    if (OnBuyStatusChangedHandler != null) OnBuyStatusChangedHandler.Invoke();

                    if (BuyStatus >= Values.BuyStatusComplete)
                    {
                        SetState(StateEnum.End);
                    }

                    break;


                case StateEnum.End:
                    time += Time.deltaTime;
                    if (time >= Values.EndTime) // Do visit
                    {
                        SetState(StateEnum.StartScreen);

                    }

                    break;
            }
        }







    }
}