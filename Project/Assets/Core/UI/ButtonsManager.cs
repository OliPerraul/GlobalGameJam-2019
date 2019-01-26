using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public void OnStart()
    {
        Core.Game.Instance.Machine.SetState(Core.States.Id.Start);
    }

}
