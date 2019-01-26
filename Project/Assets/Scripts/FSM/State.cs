using UnityEngine;
using UnityEditor;

namespace Core.FSM
{
    public abstract class State
    {
        public Resource resource;
        public object[] context;


        public int Id { get { return resource.Id; } }

        public State(object[] context, Resource resource)
        {
            this.resource = resource;
            this.context = context;
        }

        abstract public void Enter(params object[] args);
        abstract public void Exit();
        abstract public void BeginTick();
        abstract public void EndTick();

        virtual public void DebugBeginTick()
        {
            if (resource.IsDebugMessageEnabled)
                Debug.Log("State BeginTick");
        }


        virtual public void DebugEndTick()
        {
            if (resource.IsDebugMessageEnabled)
                Debug.Log("State EndTick");
        }

        virtual public void DebugEnter()
        {
            if (resource.IsDebugMessageEnabled)
                Debug.Log("State Enter");
        }


        virtual public void DebugExit()
        {
            if (resource.IsDebugMessageEnabled)
                Debug.Log("State Exit");
        }

        virtual public void OnDrawGizmos()
        {

        }



    }

}
