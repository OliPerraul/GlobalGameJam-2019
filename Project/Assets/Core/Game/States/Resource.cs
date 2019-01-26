using System.Collections;
using System.Collections.Generic;
using Core.FSM;
using UnityEngine;

namespace Core.States
{
    public enum Id
    {
        Main,
        Start,
        End
    }

    public abstract class Resource : FSM.Resource
    {

    }
}