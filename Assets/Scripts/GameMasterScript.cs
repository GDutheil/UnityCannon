using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Composites;

public class GameMasterScript : MonoBehaviour
{
    public Target targetPrefab;
    public GameObject uiCanvas;
    public GameObject scoreText;
    public GameObject mainCamera;
    public float uiHorizontalDistance, uiHeight;
    // public UnityEvent scored;
    int time, score;

    void Start()
    {
        Target target = Instantiate(targetPrefab);
        target.scored.AddListener(scoreChanged);
    }
    
    void Update()
    {
        Vector3 forwardDirection = mainCamera.transform.forward;
        forwardDirection.y = 0;
        uiCanvas.transform.position = mainCamera.transform.position + forwardDirection.normalized * uiHorizontalDistance;
        uiCanvas.transform.Translate(0, uiHeight, 0);
        uiCanvas.transform.rotation = mainCamera.transform.rotation;
    }

    void scoreChanged()
    {
        score++;
        print("super ! score = " + score);
        scoreText.GetComponent<TextMeshProUGUI>().SetText("Current score : " + score);
    }
}
