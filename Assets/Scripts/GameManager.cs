using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SelectTarget selectTarget;
    public TileScript tileScript;
    public bool useRandomFleet = true;
    public int randomFleetSeed = 0;

    private void Start()
    {
        TryAutoWireReferences();
        StartMatch();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartMatch();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void StartMatch()
    {
        if (tileScript == null)
        {
            Debug.LogWarning("TileScript reference missing on GameManager; the board will stay empty.");
            return;
        }

        var layout = useRandomFleet
            ? BoardLayouts.GenerateRandomFleet(randomFleetSeed == 0 ? System.Environment.TickCount : randomFleetSeed)
            : BoardLayouts.BasicFleet;

        tileScript.SetupStartBoard(layout);

        if (selectTarget != null)
        {
            selectTarget.BeginMatch();
        }
        else
        {
            Debug.LogWarning("SelectTarget reference missing on GameManager.");
        }
    }

    private void TryAutoWireReferences()
    {
        if (tileScript == null)
        {
            tileScript = FindObjectOfType<TileScript>();
        }

        if (selectTarget == null)
        {
            selectTarget = FindObjectOfType<SelectTarget>();
        }

        if (selectTarget != null && selectTarget.tileScript == null)
        {
            selectTarget.tileScript = tileScript;
        }
    }
}
