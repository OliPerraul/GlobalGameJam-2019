using System.Collections;
using System.Collections.Generic;
using Core.FSM;
using UnityEngine;

namespace Core.States
{
    public class State : FSM.State
    {
        public State(object[] context, Core.FSM.Resource resource) : base(context, resource) { }
        
        public override void BeginTick()
        {
            throw new System.NotImplementedException();
        }

        public override void EndTick()
        {
            throw new System.NotImplementedException();
        }

        public override void Enter(params object[] args)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }

}
