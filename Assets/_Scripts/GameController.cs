/*TwinStick Assignment
 * Roald Russel T. Palaya
 * 300714999
 * Date last Modified: 10/3/2016
 */


using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour {

    private int _lives;
    private int _score;

    [Header("UI objects")]
    public Text LivesLbl;
    public Text ScoreLbl;
    public Text GameOverLbl;
    public Text FinalScoreLbl;
    public Button RestartButton;

    [Header("Game Objects")]
    public GameObject meteor1;
    public GameObject meteor2;
    public GameObject meteor3;
    public GameObject ship;
    public GameObject star;


    public int Lives
    {
        get {
            return this._lives;
        }
        set {
            this._lives = value;
            if (this._lives <= 0)
            {
               this._endGame();

            } else
            {
                this._lives = value;
                this.LivesLbl.text = "Lives: "+this._lives;
            }
        }
    }
    public int Score
    {
        get
        {
            return this._score;
        }
        set
        {
            this._score = value;
            this.ScoreLbl.text = "Score: " + this._score;
        }
    }



    // Use this for initialization
    void Start() {
       
        this._lives = 3;
        this._score = 0;
        Debug.Log("you start with: " + this._lives);
        this.GameOverLbl.gameObject.SetActive(false);
        this.FinalScoreLbl.gameObject.SetActive(false);
        this.RestartButton.gameObject.SetActive(false);
    }
    
	
	// Update is called once per frame
	void Update () {
	
	}
    //happens after player loses
    private void _endGame()
    {
        this.FinalScoreLbl.gameObject.SetActive(true);
        this.FinalScoreLbl.text = "Final Score: " + this.Score;
        this.GameOverLbl.gameObject.SetActive(true);
        this.RestartButton.gameObject.SetActive(true);
        this.ScoreLbl.gameObject.SetActive(false);
        this.LivesLbl.gameObject.SetActive(false);
        this.ship.gameObject.SetActive(false);
        this.star.gameObject.SetActive(false);
        this.meteor1.gameObject.SetActive(false);
        this.meteor2.gameObject.SetActive(false);

        this.meteor3.gameObject.SetActive(false);

    }
    //Enables to play the game again
    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
