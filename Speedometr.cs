using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    /// <summary>
    ///  140 - 100
    /// </summary>
    void Update()
    {
        float speed = player.GetComponent<Rigidbody>().velocity.magnitude;

        GetComponent<Text>().text = "Speed = " + (speed * 6).ToString("#.##");
         
    }
}
