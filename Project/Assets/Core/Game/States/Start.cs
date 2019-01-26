﻿using UnityEngine;
using System.Collections;
using Core.FSM;

namespace Core.States
{
    public class Start : Resource
    {
        public override int Id => (int)States.Id.Start;

        public override FSM.State Create(object[] context)
        {
            return new State(context, this);
        }

        public class State : States.State
        {
            public State(object[] context, Core.FSM.Resource resource) : base(context, resource) { }

            public override void Enter(params object[] args)
            {
                base.Enter(args);
            }
        }

    }
}