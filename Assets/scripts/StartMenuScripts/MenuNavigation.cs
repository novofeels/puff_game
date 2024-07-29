using UnityEngine;
using TMPro;

public class MenuNavigation : MonoBehaviour
{
    public TextMeshProUGUI[] menuOptions;
    public RectTransform cursor;
    private int currentIndex = 0;
    [SerializeField] int cursorOffset1 = 50;
    [SerializeField] int cursorOffset2 = 50;
    LevelManager levelManager;

    void Start()
    {
        UpdateCursor();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + menuOptions.Length) % menuOptions.Length;
            UpdateCursor();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % menuOptions.Length;
            UpdateCursor();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectOption();
        }
    }

 void UpdateCursor()
{
    float cursorXPosition = cursor.position.x; // Default position

    // Adjust x position based on the current index
    switch (currentIndex)
    {
        case 0:
            cursorXPosition = menuOptions[0].transform.position.x + cursorOffset1; // Adjust -50 based on your preference
            break;
        case 1:
            cursorXPosition = menuOptions[1].transform.position.x + cursorOffset2; // Adjust -30 based on your preference
            break;
        // Add more cases if you have more menu options
    }

    // Update the cursor position
    cursor.position = new Vector3(cursorXPosition, menuOptions[currentIndex].transform.position.y, cursor.position.z);
}


    void SelectOption()
    {
        // Add functionality for selecting the option
        Debug.Log("Selected: " + menuOptions[currentIndex].text);
        if (menuOptions[currentIndex].text == "START GAME" || menuOptions[currentIndex].text == "PLAY AGAIN?")
        {
            levelManager.LoadGame();
        } else if (menuOptions[currentIndex].text == "Main Menu")
        {
            levelManager.LoadStartMenu();
        } else if (menuOptions[currentIndex].text == "OPTIONS")
        {
            Debug.Log("Options menu not implemented yet");
        }

        
     

    
}
}
