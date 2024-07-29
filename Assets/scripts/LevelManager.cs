using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern System.IntPtr GetTokenFromLocalStorage();

    private string token;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        #if UNITY_WEBGL && !UNITY_EDITOR
            // Call the JavaScript function to get the token from local storage
            Debug.Log("Calling GetTokenFromLocalStorage from Unity WebGL build...");
            System.IntPtr tokenPtr = GetTokenFromLocalStorage();
            token = Marshal.PtrToStringUTF8(tokenPtr);
            Debug.Log("Token received from JavaScript: " + token);
        #else
            // For other platforms, display a warning message
            Debug.LogWarning("Local storage access is only available in WebGL builds.");
        #endif
    }

    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        StartCoroutine(WaitAndLoadScene(1, 2f));
    }

    public void LoadStartMenu()
    {
        StartCoroutine(WaitAndLoadScene(0, 1f));
    }

    public void LoadDeathMenu()
    {
        StartCoroutine(SubmitScoreAndLoadScene(2, 2f));
    }

    IEnumerator WaitAndLoadScene(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator SubmitScoreAndLoadScene(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Create a new UnityWebRequest for POST
        UnityWebRequest request = new UnityWebRequest("http://localhost:8000/scores/submit-score", "POST");

        // Set the authorization header with the received token
        if (!string.IsNullOrEmpty(token))
        {
            request.SetRequestHeader("Authorization", "Token " + token);
        }
        else
        {
            Debug.LogError("Token is null or empty.");
        }

        // Set the content type header
        request.SetRequestHeader("Content-Type", "application/json");

        // Create the JSON body
        int score = scoreKeeper.GetScore();
        string jsonBody = $"{{\"score\":{score}}}";

        // Convert the JSON body to a byte array
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending score: " + request.error);
        }
        else
        {
            Debug.Log("Score submitted successfully: " + request.downloadHandler.text);
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
