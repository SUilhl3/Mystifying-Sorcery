using UnityEngine;
using UnityEngine.UI;

public class InstructionPanelController : MonoBehaviour
{
    public GameObject instructionPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePanel()
    {
        instructionPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
