using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.UI
{
    
    public class ButtonsManager : MonoBehaviour
    {
        public void OnStart1()
        {
            Game.Instance.StartGame(0);
        }

        public void OnStart2()
        {
            Game.Instance.StartGame(1);
        }

        public void OnStart3()
        {
            Game.Instance.StartGame(2);
        }

    }

}