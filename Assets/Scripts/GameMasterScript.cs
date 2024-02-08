using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Composites;

public class GameMasterScript : MonoBehaviour
{
    [Header("Game Parameters")]
    public int timePerTarget = 60;
    public double timeLimit = -1;
    public float targetMinDistance = 10f;
    public float targetMaxDistance = 15f;

    [Header("UI")]
    public float uiHorizontalDistance = 0.8f;
    public float uiHeight = 0.6f;
    public float viewUiAngle = 0.1f;

    [Header("Scene objects")]
    public Target targetPrefab;
    public GameObject uiCanvas;
    public GameObject scoreText;
    public GameObject mainCamera;
    public GameObject cannon;

    int score;
    float totalElapsedTime, currentTargetTime;
    bool isGameRunning;
    Target target;

    void Start()
    {
        target = Instantiate(targetPrefab);
        target.scored.AddListener(scoreChanged);
        isGameRunning = true;
    }
    
    void Update()
    {
        if (!isGameRunning) {
            if (!target.IsDestroyed()) {
                Destroy(target.gameObject);
                print("target turned off");
            }
            else {
                print("target supposedly destroyed");
            }
            scoreText.GetComponent<TextMeshProUGUI>().SetText("Final score : " + score);

        }
        else if (timeLimit != -1 && Time.realtimeSinceStartup > timeLimit) {
            isGameRunning = false;
        }
        else {
            currentTargetTime += Time.deltaTime;
            if (currentTargetTime > timePerTarget) {
                Destroy(target.gameObject);
                print("out of time!");
                createTarget();
            }
        }

        Vector3 forwardDirection = mainCamera.transform.forward;
        if (forwardDirection.y > viewUiAngle) {
            forwardDirection.y = 0;
            uiCanvas.transform.position = mainCamera.transform.position + forwardDirection.normalized * uiHorizontalDistance;
            uiCanvas.transform.Translate(0, uiHeight, 0);
            uiCanvas.transform.rotation = mainCamera.transform.rotation;
            uiCanvas.SetActive(true);
        }
        else {
            uiCanvas.SetActive(false);
        }
    }

    void scoreChanged()
    {
        score++;
        print("super ! score = " + score);
        scoreText.GetComponent<TextMeshProUGUI>().SetText("Current score : " + score);
        createTarget();
    }

    void createTarget()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f)).normalized;
        float distance = Random.Range(targetMinDistance, targetMaxDistance);
        print("dir " + randomDirection + ", dist " + distance);
        Vector3 newTargetPosition = cannon.transform.position + randomDirection * distance;
        target = Instantiate(targetPrefab, newTargetPosition, Quaternion.identity);
        target.scored.AddListener(scoreChanged);
        target.transform.LookAt(cannon.transform);
        currentTargetTime = 0f;
    }
}
