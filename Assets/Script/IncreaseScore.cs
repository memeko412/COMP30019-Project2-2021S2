using UnityEngine;
using System.Collections;

public class IncreaseScore : MonoBehaviour {

    public int incrementAmount;
    public ScoreController scoreController;

    void Start ()
    {
        // Find score manager by tag, if not referenced already
        if (scoreController == null)
        {
            this.scoreController = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<ScoreController>();
        }
    }

    // Increment player score when destroyed
    void OnDestroy()
    {
        this.scoreController.score += this.incrementAmount;
        print("score is:"+scoreController.score);
    }
}