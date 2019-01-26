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

        public int MainId = 0;

        public OnVisit OnVisitHandler;
        public OnBuyStatusChanged OnBuyStatusChangedHandler;

        public float time = 0f;
        public float NextVisitTime = 0f;
        public float BuyStatus = 0f;
        public ConfigValues Values;

        public StateEnum State;

        public Collectibles.Resource Collectible;


        public void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
            SceneManager.LoadScene("StartScreen");
            //OnVisitHandler.ON

        }


        public void StartGame()
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


                    GameObject reAgent =
                        Instantiate(Library.Instance.REAgent,
                        Level.Instance.SpawnPoint.transform.position,
                        Quaternion.identity);

                    AgentAI ai = reAgent.GetComponent<AgentAI>();
                    ai.RegisterSnapPoints(Level.Instance.DownSnapPointsArray, Level.Instance.UpperSnapPointsArray);

                    if (OnVisitHandler != null) OnVisitHandler.Invoke();
                    break;

                case StateEnum.StartScreen:
                    EndGame();

                    break;
            }
        }


        public void Update()
        {
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