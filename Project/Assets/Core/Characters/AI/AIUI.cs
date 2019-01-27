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

    bool broke = false;


    // Update is called once per frame
    void Update()
    {

        if (agent != null)
        {
            transform.position = agent.transform.position;

            if (broke)
                return;
            Image.fillAmount = agent.HP / agent.MAXHP;

            if (Mathf.Approximately(Image.fillAmount, 0) || Image.fillAmount < 0)
            {
                Image.fillAmount = 1;
                Image.sprite = Core.Library.Instance.spriteBroked;
                broke = true;
            }


        }
        else if(visit != null)
        {
            transform.position = visit.transform.position;

            if (broke)
                return;
            Image.fillAmount = visit.HP / visit.MAXHP;

            if (Mathf.Approximately(Image.fillAmount, 0) || Image.fillAmount < 0)
            {
                Image.fillAmount = 1;
                Image.sprite = Core.Library.Instance.spriteBroked;
                broke = true;
            }

        }
    }
}
