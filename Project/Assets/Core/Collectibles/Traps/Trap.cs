using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Collectibles.Traps
{
    public class Trap : MonoBehaviour
    {
        [SerializeField]
        public float strength;

        //if(Vector3)

        public void OnTriggerEnter(Collider other)
        {
            var th = other.GetComponent<AITriggerHelper>();
            if (th != null)
            {

                var AgentAI = th.visitor.GetComponent<AgentAI>();
                if (AgentAI != null)
                {
                    Game.Instance.BuyStatus -= strength;
                    if (Game.Instance.OnBuyStatusChangedHandler != null) Game.Instance.OnBuyStatusChangedHandler.Invoke();

                    AgentAI.HP -= strength;
                    if (AgentAI.HP <= 0)
                    {
                        AgentAI.SetState(AgentAI.State.ExitHouse);
                    }

                    Destroy(gameObject);
                    Game.Instance.AICount--;
                    if (Game.Instance.AICount <= 0)
                    {
                        Game.Instance.AICount = 0;
                        Game.Instance.OnWaveCleared();
                    }

                }

                var VisitorAI = th.visitor.GetComponent<VisitorAI>();
                if (VisitorAI != null)
                {
                    Game.Instance.BuyStatus -= strength;
                    if (Game.Instance.OnBuyStatusChangedHandler != null) Game.Instance.OnBuyStatusChangedHandler.Invoke();

                    VisitorAI.HP -= strength;
                    if (VisitorAI.HP <= 0)
                    {
                        //VisitorAI.
                        VisitorAI.SetState(VisitorAI.State.ExitHouse);
                    }


                    Destroy(gameObject);
                    Game.Instance.AICount--;
                    //Game.Instance.AICount--;
                    if (Game.Instance.AICount <= 0)
                    {
                        Game.Instance.AICount = 0;
                        Game.Instance.OnWaveCleared();
                    }

                }
            }
        }

    }

}
