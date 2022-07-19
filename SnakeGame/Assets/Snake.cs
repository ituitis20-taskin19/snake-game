using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private enum State{
        Alive,
        Dead
    }

    private State state;

    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    private SpriteRenderer sr;
    //public Rigidbody2D rigidbody;
    private List<Color> colorList = new List<Color>();
    private int count;
    private int ind = 0;

    private float colorChangeRate_snake = 0.1f;
    private float timer_snake;

    private void Start(){
        //_segments = new List<Transform>();
        state = State.Alive;
        _segments.Add(this.transform);
        //Grow();
    }

    private void Update()
    {
        switch (state) {
            case State.Alive:
                HandleInput();
                break;
            case State.Dead:
                if(Input.GetKeyDown(KeyCode.R))
                {
                    ResetGame();
                }
                timer_snake += Time.deltaTime;
                if(timer_snake > colorChangeRate_snake)
                {
                    //Debug.Log("here");
                    sr.color = colorList[ind];
                    ind = (ind+1)%count;
                    timer_snake = 0;
                }
                break;
        }

    }

    private void HandleInput(){
                if(Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down || Input.GetKeyDown(KeyCode.UpArrow) && _direction != Vector2.down){
            _direction = Vector2.up;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && _direction != Vector2.up || Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up){
            _direction = Vector2.down;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && _direction != Vector2.right|| Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right){
            _direction = Vector2.left;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && _direction != Vector2.left || Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left){
            _direction = Vector2.right;
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        /*another solution:
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Vector2.down;
            }
        }
        */
    }


    private void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevel);

    }

    private void PauseGame()
    {
        if(Time.timeScale == 1f){Time.timeScale = 0f;}
        else{Time.timeScale = 1f;}
    }

    private void FixedUpdate(){

        switch (state) {
            case State.Alive:
                for(int i = _segments.Count - 1; i > 0; i--){
                    _segments[i].position = _segments[i-1].position;
                }

                this.transform.position = new Vector3(
                    Mathf.Round(this.transform.position.x) + _direction.x,//snake moves in a grid so we need whole numbers
                    Mathf.Round(this.transform.position.y) + _direction.y,
                    0.0f //z we dont need to update z for this game
                );
                break;
            case State.Dead:
                break;
        }

    }

    public void Grow(){
        
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    public void Die(){
        Debug.Log("Die");
        ChangeColor();
        //rigidbody.velocity = Vector3.zero;
        //ResetGame();
        //Time.timeScale = 0f;
    }
    
    private void ChangeColor(){
        //Debug.Log("color");
        sr = GetComponent<SpriteRenderer>();
        colorList.Add(Color.red);
        colorList.Add(Color.black);
        count = colorList.Count;

        /*timer_snake += Time.deltaTime;
        if(timer_snake > colorChangeRate_snake)
        {
            Debug.Log("here");
            sr.color = colorList[ind];
            ind = (ind+1)%count;
            timer_snake = 0;
        }*/
    }

     private void OnTriggerEnter2D(Collider2D other){
        
        if(other.tag == "Obstacle")
        {
            Debug.Log("Collided with " + other.tag);
            state = State.Dead;
            //Debug.Log("Die");
            //CMDebug.TextPopup("DEAD!",transform.position);
            Die();
        }
        else if(other.tag == "Food"){
            Grow();
        }
    }

    

}
