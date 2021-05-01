using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeCountText : MonoBehaviour
{
    [SerializeField]
    Text challengeCountText = null;
    void Reset()
    {
        challengeCountText = GetComponent<Text>();
    }
    void Start()
    {
        challengeCountText.text = FirstStageSceneManager.ChallengeCount.ToString();
    }
}
