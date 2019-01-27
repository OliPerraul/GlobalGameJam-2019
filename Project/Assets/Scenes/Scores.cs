using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Scores : MonoBehaviour
{
    //public Scores Instance;//;


    [SerializeField]
    public Text hs;
    [SerializeField]
    public Text s;


    [SerializeField]
    private float lostScreenTimeLim = 5f;
    private float time = 0f;



    // Start is called before the first frame update
    void Start()
    {
        //Instance = this;

        if (Core.Game.Instance.Score == Core.Game.Instance.Score)
        {
            hs.text = "New Highscore: " + Core.Game.Instance.Highscore;
            s.text = "";

        }
        else
        {
            s.text = "Score: " + Core.Game.Instance.Score;
            hs.text = "Highscore: " + Core.Game.Instance.Highscore;
 

        }
    }


    public void Update()
    {
        if (time >= lostScreenTimeLim)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("StartScreen");
            }
        }
        else
        {
            time += Time.deltaTime;
        }
    }



}
