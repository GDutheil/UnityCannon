using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCannonRotation : MonoBehaviour
{

    public ScriptableFloat controlFloat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controlFloat.inputRotation < 25 && controlFloat.inputRotation > -90){
            transform.localEulerAngles = new Vector3(controlFloat.inputRotation, 0, 0);
        }
            
    }
}
