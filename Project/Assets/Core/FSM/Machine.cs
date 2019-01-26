using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Core.FSM
{
    /// <summary>
    /// TODO Handle events do not force everything to appear in the loop
    /// Make it so that some states respond to events
    /// </summary>

    [System.Serializable]
    public class Machine
    {
        //Must be set manually
        [SerializeField]
        private int contextSize = 1;
        public object[] Context = null;

        public void SetContext(object context, int idx)
        {
            if(Context == null) this.Context = new object[contextSize];
            Context[idx] = context;

        }

        public void Start()
        {
            stack = new Stack<State>();
            dictionary = new Dictionary<int, State>();

            State first = Populate();

            // sets the first in the array as active
            if (first != null)
            {
                SetState(first.Id);
            }
        }

        [SerializeField]
        public State Top
        { get { return stack == null || stack.Count==0 ? null : stack.Peek(); } }


        [SerializeField]
        public Resource[] states;
        private Stack<State> stack = null;
        private Dictionary<int, State> dictionary;

        private bool enabled = true;

        public void Disable()
        {
            enabled = false;
        }

        public void Enable()
        {
            enabled = true;
        }


        /// <summary>
        /// populates the dictionary and returns the first state
        /// </summary>
        /// <returns></returns>
        private State Populate()
        {
            State first = null;

            foreach (Resource res in states)
            {
                if (res != null)
                {
                    if (dictionary.ContainsKey(res.Id))
                        continue;

                    State state = CreateState(res);
                    dictionary.Add(res.Id, state);

                    if(first == null)
                        first = state;
                }

            }

            return first;

        }

        public virtual State CreateState(Resource resource)
        {
            return resource.Create(Context);
        }


        public string StateName = "";


        public void Tick()
        {
            if (!enabled)
                return;

            if (stack.Count == 0)
                return;

            if (Top != null)
            {
                int pos = Top.ToString().LastIndexOf(".") + 1;
                StateName = Top.ToString().Substring(pos, Top.ToString().Length - pos);

                Top.BeginTick();
                Top.EndTick();
            }
        }

        public void OnDrawGizmos()
        {
            if (!enabled)
                return;

            if (stack == null)
                return;

            if (stack.Count == 0)
                return;

            if (Top != null)
            {
                Top.OnDrawGizmos();
            }
        }

        public void SetState<T>(T state, params object[] args)
        {
            if (Top != null)
            {
                if (Top.Id == -1)
                    return;

                Top.Exit();
                stack.Clear();
            }
            
            stack.Push(dictionary[(int)(object)state]);
            dictionary[(int)(object)state].Enter(args);
        }

        public void SetState(int state, params object[] args)
        {
            if (Top != null)
            {
                if (Top.Id == (int)(object)state)
                    return;

                if (Top.Id == -1)
                {
                    return;
                }
            
                Top.Exit();
                stack.Clear();
            }

            stack.Push(dictionary[(int)(object)state]);
            dictionary[(int)(object)state].Enter(args);
        }

        public void PushState(int state, params object[] args)
        {
            stack.Push(dictionary[state]);
            dictionary[state].Enter(args);
        }

        public void PopState()
        {
            if (Top != null)
            {
                if (Top.Id == -1)
                    return;

                Top.Exit();
                stack.Pop();
            }
        }

    }

}
