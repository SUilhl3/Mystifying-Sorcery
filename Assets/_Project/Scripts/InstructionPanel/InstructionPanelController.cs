using UnityEngine;
using UnityEngine.UI;

public class InstructionPanelController : MonoBehaviour
{
    public GameObject instructionPanel;
    [SerializeField] GameObject healthbar;
    [SerializeField] GameObject keyCounter;
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
        healthbar.SetActive(true);
        keyCounter.SetActive(true);
        Time.timeScale = 1f;
    }
}
