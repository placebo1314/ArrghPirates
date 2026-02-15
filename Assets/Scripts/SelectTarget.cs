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

    public bool IsGameActive => gameActive;

    private void OnDestroy()
    {
        UnwireButtonListeners();
    }

    private void Update()
    {
        if (!gameActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveTarget(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveTarget(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            MoveTarget(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            MoveTarget(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            ShootTarget();
        }
    }

    public void BeginMatch()
    {
        PrepareNewGame();
    }

    public IEnumerator Shoot()
    {
        BeginMatch();

        while (gameActive)
        {
            yield return null;
        }

        ToggleControls(false);
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
        textScript?.ShowTemporaryMessage("Irányítás: WASD/nyilak + Space/Enter", 2f);

        if (!gameActive)
        {
            EndGame(totalShips == 0);
        }
    }

    private void ToggleControls(bool value)
    {
        if (PositiveXBtn != null) PositiveXBtn.gameObject.SetActive(value);
        if (NegativeXBtn != null) NegativeXBtn.gameObject.SetActive(value);
        if (PositiveXYtn != null) PositiveXYtn.gameObject.SetActive(value);
        if (NegativeYBtn != null) NegativeYBtn.gameObject.SetActive(value);
        if (ShootBtn != null) ShootBtn.gameObject.SetActive(value);
    }

    private void WireButtonListeners()
    {
        if (listenersRegistered)
        {
            return;
        }

        if (PositiveXBtn != null) PositiveXBtn.onClick.AddListener(() => MoveTarget(1, 0));
        if (NegativeXBtn != null) NegativeXBtn.onClick.AddListener(() => MoveTarget(-1, 0));
        if (PositiveXYtn != null) PositiveXYtn.onClick.AddListener(() => MoveTarget(0, 1));
        if (NegativeYBtn != null) NegativeYBtn.onClick.AddListener(() => MoveTarget(0, -1));
        if (ShootBtn != null) ShootBtn.onClick.AddListener(ShootTarget);
        listenersRegistered = true;
    }

    private void UnwireButtonListeners()
    {
        if (!listenersRegistered)
        {
            return;
        }

        if (PositiveXBtn != null) PositiveXBtn.onClick.RemoveAllListeners();
        if (NegativeXBtn != null) NegativeXBtn.onClick.RemoveAllListeners();
        if (PositiveXYtn != null) PositiveXYtn.onClick.RemoveAllListeners();
        if (NegativeYBtn != null) NegativeYBtn.onClick.RemoveAllListeners();
        if (ShootBtn != null) ShootBtn.onClick.RemoveAllListeners();
        listenersRegistered = false;
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
            ? "Győzelem! Az ellenséges flottát a mélybe küldtük. Nyomj R-t az új csatához."
            : "Elfogyott a lőszer, vissza kell vonulnunk! Nyomj R-t az új csatához.";

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
