using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppCode : MonoBehaviour
{
    // Start is called before the first frame update


    private SerialController serialController;
    void Start()
    {
        serialController = GameObject.Find("Serial").GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {





        //Send Data

        if (Input.GetKeyDown(KeyCode.A) == true){

        serialController.SendSerialMessage("A");

        }



        string message = serialController.ReadSerialMessage();

        if (message == null) return;
        Debug.Log("Data from arduino: " + message);
    }
}
