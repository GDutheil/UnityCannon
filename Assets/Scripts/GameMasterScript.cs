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
    public float timeOutMalus = -0.1f;
    public double timeLimit = -1;
    public float targetMinDistance = 10f;
    public float targetMaxDistance = 15f;

    [Header("UI")]
    public float uiHorizontalDistance = 0.9f;
    public float uiHeight = 0.8f;
    public float viewUiAngle = 0.1f;

    [Header("Scene objects")]
    public Target targetPrefab;
    public GameObject uiCanvas;
    public TextMeshProUGUI uiText;
    public TextMeshProUGUI timeText;
    public GameObject mainCamera;
    public GameObject cannon;

    int nbMisses;
    float score, currentTargetTime, totalElapsedTime;
    bool isGameRunning, lastTargetTimedOut;
    Target target;

    void Start()
    {
        target = Instantiate(targetPrefab);
        target.scored.AddListener(scoreChanged);
        isGameRunning = true;
    }
    
    void Update()
    {
        totalElapsedTime += Time.deltaTime;
        if (!isGameRunning) {  // End of the game
            if (!target.IsDestroyed()) {
                Destroy(target.gameObject);
                print("target destroyed");
            }
            uiText.SetText("Final score : " + score);

        }  // Time reached the limit, ends the game
        else if (timeLimit != -1 && totalElapsedTime > timeLimit) {
            isGameRunning = false;
        }
        else {  // Game running = Main Loop
            currentTargetTime += Time.deltaTime;
            uiText.SetText("Total time : " + string.Format("{0:N1}", totalElapsedTime) 
                + "s \nCurrent score : " + string.Format("{0:N1}", score)
                + "\nMisses : " + nbMisses);
            if (currentTargetTime > timePerTarget) {
                Destroy(target.gameObject);
                print("Out of time! Target disappeared!");
                createTarget();
                lastTargetTimedOut = true;
                nbMisses++;
                score += timeOutMalus;
            }
            else if (lastTargetTimedOut && currentTargetTime < 3) {
                timeText.SetText("Too slow! Target disappeared!\nAnother appeared, don't waste time!");
            }
            else {
                if (timePerTarget - currentTargetTime < 5) {
                    timeText.color = new Color(1, 0, 0);
                }
                else {
                    timeText.color = new Color(1, 1, 1);
                }
                timeText.SetText(string.Format("{0:N1}", timePerTarget - currentTargetTime) + "s remaining before the target disappears!");

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
        print("Scored! score = " + score);
        createTarget();
        lastTargetTimedOut = false;
    }

    void createTarget()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f)).normalized;
        float distance = Random.Range(targetMinDistance, targetMaxDistance);
        Vector3 newTargetPosition = cannon.transform.position + randomDirection * distance;
        target = Instantiate(targetPrefab, newTargetPosition, Quaternion.identity);
        target.scored.AddListener(scoreChanged);
        target.transform.LookAt(cannon.transform);
        currentTargetTime = 0f;
    }
}
