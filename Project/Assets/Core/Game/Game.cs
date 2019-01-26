using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Core
{


    public class Game : MonoBehaviour
    {
        public static Game Instance;

        public FSM.Machine Machine;

        public void Awake()
        {
            Instance = this;

            Machine.Start();
            Machine.SetContext(Instance, 0); //

            DontDestroyOnLoad(Instance.gameObject);
            SceneManager.LoadScene("StartScreen");
        }


        public void Update()
        {
            Machine.Tick();
        }
    }
}