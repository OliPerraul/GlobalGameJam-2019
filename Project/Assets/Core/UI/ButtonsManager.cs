using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.UI
{
    
    public class ButtonsManager : MonoBehaviour
    {
        public void OnStart()
        {
            Core.Game.Instance.Machine.SetState(Core.States.Id.Start);
        }

    }

}