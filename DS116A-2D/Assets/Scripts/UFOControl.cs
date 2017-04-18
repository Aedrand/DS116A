using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UFOControl : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public Text timeText;
    public GameObject pickupPrefab;

    private Rigidbody2D ufoBody;
    private int timeCount;
    private int count;

	// Use this for initialization
	void Start () {
        ufoBody = GetComponent<Rigidbody2D>();
        count = 0;
        winText.text = "";
        setCountText();
	}

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if(count < 12 && winText.text == "")
        {
            timeText.text = Time.time.ToString("0.00");
        }
        
    }

    void FixedUpdate () {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHoriz, moveVert);
        ufoBody.AddForce(movement * speed);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUp"))
        {
            collision.gameObject.SetActive(false);

            count++;
            setCountText();
        }
    }
    
    void setCountText()
    {
        countText.text = "Collected: " + count + "/12";

        if(count >= 12)
        {
            winText.text = "You Win!- " + timeText.text + " seconds!";
            GameObject[] objs = GameObject.FindGameObjectsWithTag("PickUp");
            foreach(GameObject target in objs)
            {
                GameObject.Destroy(target);
            }

        }
    }
}
