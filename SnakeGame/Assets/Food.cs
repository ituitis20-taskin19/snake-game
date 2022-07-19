using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public bool isRainbow;

    private SpriteRenderer sr;

    private List<Color> colorList = new List<Color>();
    private int count;
    private int ind = 0;

    private float colorChangeRate = 0.1f;
    private float timer;

    private void Start(){
        sr = GetComponent<SpriteRenderer>();
        RandomizePosition();
        colorList.Add(Color.blue);
        colorList.Add(Color.red);
        colorList.Add(Color.yellow);
        colorList.Add(Color.green);
        //colorList.Add(new Color(0.34f, 0.1f, 0.53f));


        count = colorList.Count;

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > colorChangeRate /*&& Player.state==Alive*/)
        {
            sr.color = colorList[ind];
            ind = (ind+1)%count;
            timer = 0;
        }
        
        /*else if(Player.state==Dead){
            sr.color=
        }*/
        
    }
    
    private void RandomizePosition(){
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x+1, bounds.max.x-1);
        float y = Random.Range(bounds.min.y+1,bounds.max.y-1);

         this.transform.position = new Vector3(Mathf.Round(x),Mathf.Round(y),0.0f);

    }

    private void OnTriggerEnter2D(Collider2D other){
        //if(other.tag == "Player"){
            RandomizePosition();
        //}
    }
}
