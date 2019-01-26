using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Core
{


    public class Game : MonoBehaviour
    {
        public static Game Instance;

        public Core.FSM.Machine Machine;

        public void Awake()
        {
            Instance = this;

            DontDestroyOnLoad(Instance.gameObject);
            SceneManager.LoadScene("StartScreen");
        }
    }
}