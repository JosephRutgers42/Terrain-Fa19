using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanePilot : MonoBehaviour
{
    public Text countText;
    public float speed = 90.0f;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("plane pilot script added to: " + gameObject.name);
        count = 0;
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveCamTo = transform.position - transform.forward * 0.5f + Vector3.up * 0.5f;
        float bias = 0.96f;
        Camera.main.transform.position = Camera.main.transform.position * bias + moveCamTo * (1.0f - bias);
        Camera.main.transform.LookAt(transform.position + transform.forward * 10.0f);
        transform.position += transform.forward * Time.deltaTime * speed;
        speed -= transform.forward.y * Time.deltaTime * 10.0f;

        if (speed < 35.0f) {
            speed = 35.0f;
        }
        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
        float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);

        if (terrainHeightWhereWeAre > transform.position.y) {

            transform.position = new Vector3(transform.position.x, terrainHeightWhereWeAre, transform.position.z);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        print("i hit it");
        if (other.gameObject.CompareTag("Pick Up"))

        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            
        } 
    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
    }

}
