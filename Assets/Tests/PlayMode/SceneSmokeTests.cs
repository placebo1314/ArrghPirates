using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SceneSmokeTests
{
    private const string MenuScene = "MenuScene";
    private const string BoardScene = "SinglePlayBoard";
    private const string InventoryScene = "InventoryScene";

    [UnityTest]
    public IEnumerator MenuScene_Loads_WithoutErrors()
    {
        LogAssert.NoUnexpectedReceived();

        yield return LoadScene(MenuScene);

        Assert.AreEqual(MenuScene, SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator BoardScene_Loads_WithoutErrors()
    {
        LogAssert.NoUnexpectedReceived();

        yield return LoadScene(BoardScene);

        Assert.AreEqual(BoardScene, SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator InventoryScene_Loads_WithoutErrors_WhenSaveIsMissing()
    {
        LogAssert.NoUnexpectedReceived();
        DataSaver.deleteData("stats");

        yield return LoadScene(InventoryScene);

        Assert.AreEqual(InventoryScene, SceneManager.GetActiveScene().name);
    }

    private static IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!operation.isDone)
        {
            yield return null;
        }

        yield return null;
    }
}
