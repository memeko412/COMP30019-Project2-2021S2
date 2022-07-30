using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOnController : MonoBehaviour
{
    public Text scoreBoard;
    public Text remainBulletText;
	public Text totalBulletText;
    private Gun gun;
    GunController gunController;
    public ScoreController scoreController;
    public Player player;
    
    public Text grenadeNum;
    public Text scoreNum;
    
    

    public static bool lastGameWon;
    public static int lastGameScore;

    void Start() {
        gunController = player.GetComponent<GunController>();
        gun = gunController.GetTheHoldingGun();
        this.totalBulletText.text = "/"+gun.bulletNumber.ToString();
        this.scoreController.score = 0;
    }

    void Update() {
        if (player!=null) {
            gun = gunController.GetTheHoldingGun();
            this.totalBulletText.text = "/"+gun.bulletNumber.ToString();
            this.scoreBoard.text = "Score:";
            this.scoreNum.text = (this.scoreController.score).ToString();
            this.remainBulletText.text = gun.GetRemainBullet().ToString();
           
         
        }
    }

    public void GameOver()
    {
        GameOnController.lastGameScore = this.scoreController.score;
        GameOnController.lastGameWon = false;
        SceneManager.LoadScene("GameOver");
    }

    public void PlayerWon()
    {
        GameOnController.lastGameScore = this.scoreController.score;
        GameOnController.lastGameWon = true;
        SceneManager.LoadScene("GameOver");
    }
}
