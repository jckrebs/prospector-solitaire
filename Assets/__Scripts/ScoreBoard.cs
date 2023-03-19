using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// The ScoreBoard class manages showing the score to the player
[RequireComponent(typeof(TMP_Text))]
public class ScoreBoard : MonoBehaviour
{
    private static ScoreBoard S; // The singleton for ScoreBoard

    [Header("Dynamic")]
    [SerializeField]
    private int _score = 0;
    // The Score property also sets the text of the TMP_Text
    public int score
    {
        get => _score;
        set
        {
            _score = value;
            textMP.text = _score.ToString("#,##0");
        }
    }

    private TMP_Text textMP;

    void Awake()
    {
        if (S != null) Debug.LogError("ScoreBoard.S is already set!");
        S = this;

        textMP = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// A static accessor for the score of the ScoreBoard Singleton
    /// </summary>
    /// <value></value>
    public static int SCORE
    {
        get => S.score;
        set => S.score = value;
    }

    // When called by SendMessage, this adds the fs.score to the S.score
    static public void FS_CALLBACK(FloatingScore fs)
    {
        SCORE += fs.score;
    }
}
