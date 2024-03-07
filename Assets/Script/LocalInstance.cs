using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LocalInstance : MonoBehaviour
{
    [SerializeField] private GameObject prafab;
    [SerializeField] Transform testObject;
    
    GameObject prafabInstance;

    bool isLoaded = false;

    public void LateUpdate()
    {
        testObject.Rotate(0, 70f * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.I))
        {
            isLoaded = true;
            testObject.localScale = new Vector3(2, 2, 2);
            
            prafabInstance = Instantiate(prafab);
        }
        
        if(isLoaded)
        {
            if (prafabInstance != null)
            {
                testObject.localScale = new Vector3(3, 3, 3);
            }
            else
            {
                testObject.localScale = new Vector3(0, 0, 0);
            }
        }
    }
}