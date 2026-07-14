using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string current_scene;
    string map_scene;
    [SerializeField] PlayerShareData playerShareData;

    void Start()
    {
        GameDebug();
    }

    public void GameStart()
    {
        // シーンを追加でロードします。
        SceneManager.LoadScene("Map01", LoadSceneMode.Additive);

        current_scene = "Map01";
    }

    public void GameDebug()
    {
        SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
    }

    public void ChangeScene(string newScene)
    {
        StartCoroutine(CorChangeScene(newScene));
    }

    public IEnumerator CorChangeScene(string newScene)
    {
        if (current_scene == "Battle")
        {
            newScene = map_scene;
        }

        Debug.Log(current_scene + newScene);

        foreach (var item in FindObjectsByType<PlayerShareData>(FindObjectsSortMode.None))
        {
            if (item.playerShareDataType == PlayerShareDataType.HOST) continue;

            playerShareData.SetData(item.GetShareData(), item.GetStatusData());
        }

        var op = SceneManager.UnloadSceneAsync(current_scene);
        yield return op;

        SceneManager.LoadScene(newScene, LoadSceneMode.Additive);

        if (newScene == "Battle")
        {
            map_scene = current_scene;
        }

        current_scene = newScene;
    }
}
