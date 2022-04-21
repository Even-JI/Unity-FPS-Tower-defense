using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScoreScript : MonoBehaviour
{
    public Text counterText;
    public int count;

    void Start()
    {
        count = 0;
    }

    void Update()
    {
        counterText.text = count.ToString();
    }
}
