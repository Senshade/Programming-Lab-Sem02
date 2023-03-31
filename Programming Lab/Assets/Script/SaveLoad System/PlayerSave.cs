using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    private int score = 0;
    private SaveManager saveManager;

    void Start() {
        saveManager = FindObjectOfType<SaveManager>();
        score = saveManager.LoadGame();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
            score++;
            print("Score added!");
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            saveManager.SaveGame(score);
            print("Score saved!");
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            score = saveManager.LoadGame();
            print($"Score is: {score}");
        }
    }
}