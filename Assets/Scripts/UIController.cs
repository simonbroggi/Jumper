using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI spaceBarText;
    public TextMeshProUGUI modelText;

    private JumperBehaviour jumperBehaviour; 
    
    // Start is called before the first frame update
    void Start()
    {
        jumperBehaviour = FindObjectOfType<JumperBehaviour>();
        modelText.text = jumperBehaviour.transform.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            spaceBarText.color = Color.white;
        }
        else
        {
            spaceBarText.color = Color.gray;
        }
    }
}
