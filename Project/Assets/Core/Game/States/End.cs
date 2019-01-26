using UnityEngine;
using System.Collections;
using Core.FSM;

namespace Core.States
{
    public class End : Resource
    {
        public override int Id => (int)States.Id.End;

        public override FSM.State Create(object[] context)
        {
            return new State(context, this);
        }

        public class State : States.State
        {
            public State(object[] context, Core.FSM.Resource resource) : base(context, resource) { }
        }

    }
}