using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIUI : MonoBehaviour
{
    [SerializeField]
    public Image Image;

    [SerializeField]
    public AgentAI agent;

    // or

    [SerializeField]
    public VisitorAI visit;

    // Update is called once per frame
    void Update()
    {

        if (agent != null)
        {
            transform.position = agent.transform.position;
            Image.fillAmount = agent.HP / agent.MAXHP;
        }
        else if(visit != null)
        {
            transform.position = visit.transform.position;
            Image.fillAmount = visit.HP / visit.MAXHP; 
        }
    }
}
