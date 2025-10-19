using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectTarget : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject Bullet;

    public Button PositiveXBtn;
    public Button NegativeXBtn;
    public Button PositiveXYtn;
    public Button NegativeYBtn;
    public Button ShootBtn;
    public int XPos = 0;
    public int YPos = 0;

    public Transform aPos;
    public TileScript tileScript;
    public TopTextScript textScript;

    public float duration = 1f;
    public int startingShots = 15;

    private int shotsRemaining;
    private int hits;
    private int totalShips;
    private bool gameActive;
    private bool listenersRegistered;

    void Start()
    {
        Debug.Log("Start SelectTargetScript");
    }

    public IEnumerator Shoot()
    {
        PrepareNewGame();

        while (gameActive)
        {
            yield return null;
        }

        ToggleControls(false);
        Debug.Log("ShootEnd ! ");
    }

    private void PrepareNewGame()
    {
        shotsRemaining = startingShots;
        hits = 0;
        totalShips = tileScript != null ? tileScript.CountRemainingShips() : 0;
        XPos = Mathf.Clamp(XPos, 0, TileScript.Width - 1);
        YPos = Mathf.Clamp(YPos, 0, TileScript.Height - 1);
        gameActive = shotsRemaining > 0 && totalShips > 0;

        ToggleControls(true);
        WireButtonListeners();
        ResetTileVisuals();
        UpdateStatusText();

        if (!gameActive)
        {
            EndGame(totalShips == 0);
        }
    }

    private void ToggleControls(bool value)
    {
        PositiveXBtn.gameObject.SetActive(value);
        NegativeXBtn.gameObject.SetActive(value);
        PositiveXYtn.gameObject.SetActive(value);
        NegativeYBtn.gameObject.SetActive(value);
        ShootBtn.gameObject.SetActive(value);
    }

    private void WireButtonListeners()
    {
        if (listenersRegistered)
        {
            return;
        }

        PositiveXBtn.onClick.AddListener(() => MoveTarget(1, 0));
        NegativeXBtn.onClick.AddListener(() => MoveTarget(-1, 0));
        PositiveXYtn.onClick.AddListener(() => MoveTarget(0, 1));
        NegativeYBtn.onClick.AddListener(() => MoveTarget(0, -1));
        ShootBtn.onClick.AddListener(ShootTarget);
        listenersRegistered = true;
    }

    private void MoveTarget(int x, int y)
    {
        if (!gameActive)
        {
            return;
        }

        int lastX = XPos;
        int lastY = YPos;

        if (x != 0)
        {
            XPos += x;
        }
        else
        {
            YPos += y;
        }

        int pos = CalculatePos(XPos, YPos);
        if (pos != -1)
        {
            HighlightTile(pos);
            int lastPos = CalculatePos(lastX, lastY);
            if (lastPos != -1)
            {
                RestoreTile(lastPos);
            }
        }
        else
        {
            XPos = lastX;
            YPos = lastY;
        }
    }

    private void HighlightTile(int position)
    {
        if (tileScript == null || tiles == null || position < 0 || position >= tiles.Length)
        {
            return;
        }

        tileScript.SetTargetWater(tiles[position].GetComponent<Renderer>());
    }

    private void RestoreTile(int position)
    {
        if (tileScript == null || tiles == null || position < 0 || position >= tiles.Length)
        {
            return;
        }

        if (tileScript.TryGetTile(position, out TileScript.BoardTile lastTile) && lastTile.ShotResult == TileScript.TileShotResult.Hit)
        {
            tileScript.SetBlackWater(tiles[position].GetComponent<Renderer>());
        }
        else
        {
            tileScript.SetOriginalWater(tiles[position].GetComponent<Renderer>());
        }
    }

    private int CalculatePos(int x, int y)
    {
        if (x < TileScript.Width && x >= 0 && y >= 0 && y < TileScript.Height)
        {
            return (y * TileScript.Width) + x;
        }

        textScript?.ShowTemporaryMessage("Nem sodródhatunk ki a térképről!", 2f);
        return -1;
    }

    private void ShootTarget()
    {
        if (!gameActive)
        {
            return;
        }

        int pos = CalculatePos(XPos, YPos);
        if (pos == -1)
        {
            return;
        }

        if (tileScript == null || tiles == null || pos < 0 || pos >= tiles.Length)
        {
            return;
        }

        if (!tileScript.TryGetTile(pos, out TileScript.BoardTile tile))
        {
            Debug.LogWarning($"No tile data for position {pos}.");
            return;
        }

        if (tile.WasShot)
        {
            textScript?.ShowTemporaryMessage("Ide már céloztál, kapitány!", 2f);
            return;
        }

        shotsRemaining--;
        StartCoroutine(DropBullet(pos));

        if (tile.HasShip)
        {
            tileScript.RegisterHit(pos);
            hits++;
            tileScript.SetBlackWater(tiles[pos].GetComponent<Renderer>());
            textScript?.ShowTemporaryMessage("Találat! Remegjen a fedélzet!", 2.5f);
        }
        else
        {
            tileScript.RegisterMiss(pos);
            tileScript.SetOriginalWater(tiles[pos].GetComponent<Renderer>());
            textScript?.ShowTemporaryMessage("Csak a tenger loccsant.", 2f);
        }

        UpdateStatusText();

        if (tileScript.CountRemainingShips() == 0)
        {
            EndGame(true);
            return;
        }

        if (shotsRemaining <= 0)
        {
            EndGame(false);
            return;
        }

        HighlightTile(pos);
    }

    private IEnumerator DropBullet(int targetIndex)
    {
        if (Bullet == null || aPos == null || tiles == null || targetIndex < 0 || targetIndex >= tiles.Length)
        {
            yield break;
        }

        Bullet.SetActive(true);
        Debug.Log("BulletActivate");
        float time = 0f;
        Vector3 start = aPos.position;
        Vector3 end = tiles[targetIndex].transform.position;
        Bullet.transform.position = start;

        while (time <= duration)
        {
            time += Time.deltaTime;
            Bullet.transform.position = Vector3.Lerp(start, end, time / duration);
            yield return null;
        }

        Bullet.SetActive(false);
        Debug.Log("BulletDeActivate");
    }

    private void UpdateStatusText()
    {
        if (textScript == null)
        {
            return;
        }

        string status = $"Lövések: {shotsRemaining}/{startingShots} | Találatok: {hits}/{totalShips}";
        textScript.ChangeText(status);
    }

    private void EndGame(bool victory)
    {
        gameActive = false;
        string message = victory
            ? "Győzelem! Az ellenséges flottát a mélybe küldtük."
            : "Elfogyott a lőszer, vissza kell vonulnunk!";

        textScript?.ChangeText(message);
        ToggleControls(false);
    }

    private void ResetTileVisuals()
    {
        if (tileScript == null || tiles == null)
        {
            return;
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tileScript.TryGetTile(i, out TileScript.BoardTile tile) && tile.ShotResult == TileScript.TileShotResult.Hit)
            {
                tileScript.SetBlackWater(tiles[i].GetComponent<Renderer>());
            }
            else
            {
                tileScript.SetOriginalWater(tiles[i].GetComponent<Renderer>());
            }
        }

        int pos = CalculatePos(XPos, YPos);
        if (pos != -1)
        {
            HighlightTile(pos);
        }
    }
}
