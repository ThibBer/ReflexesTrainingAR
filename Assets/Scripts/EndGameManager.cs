using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [SerializeField]
    private Text _score;

    // Start is called before the first frame update
    void Start()
    {
        _score.text = _score.text + GameManager.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
