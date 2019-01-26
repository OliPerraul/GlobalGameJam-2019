//using UnityEngine;
//using UnityEditor;
//using KinematicCharacterController;
//using System;
//using Core.FSM;
//using Core.FSM;


//// Character state

//namespace Core.World.Objects.Characters.FSM
//{
//    public enum Id
//    {
//        Action = 1 << 1,
//        Grounded = 1 << 2,
//        Airborne = 1 << 3,
//        Jump = 1 << 4,
//        Dead = 1 << 5,
//        Injured = 1 << 6,
//    }

//    // We don't care if we can't instantiate this SO, because it's abstract

//    public class Resource : Core.FSM.Resource
//    {
//        override public int Id { get { return -1; } }

//        public override Core.FSM.State Create(object[] context)
//        {
//            return new State();
//        }
//    }

//    public abstract class State : Core.FSM.State
//    {
//        public State(object[] context, Core.FSM.Resource resource) : base(context, resource) { }

//        public override void Enter(params object[] args) { }
//        public override void Exit() { }
//        public override void BeginTick() { }
//        public override void EndTick() { }

 



//    }


//}
