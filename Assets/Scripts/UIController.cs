using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI spaceBarText;
    public RawImage spaceBarButton;
    public TextMeshProUGUI leftText;
    public RawImage leftButton;
    public TextMeshProUGUI rightText;
    public RawImage rightButton;


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
            spaceBarButton.color = Color.white;
        }
        else
        {
            spaceBarText.color = Color.gray;
            spaceBarButton.color = Color.gray;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            leftText.color = Color.white;
            leftButton.color = Color.white;
        }
        else
        {
            leftText.color = Color.gray;
            leftButton.color = Color.gray;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rightText.color = Color.white;
            rightButton.color = Color.white;
        }
        else
        {
            rightText.color = Color.gray;
            rightButton.color = Color.gray;
        }
    }
}
