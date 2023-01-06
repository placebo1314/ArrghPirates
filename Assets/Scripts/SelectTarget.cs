using System.Collections;
using System.Collections.Generic;
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

    public float duration = 1;

    void Start()
    {
        // DebugConsole:
        Debug.Log("Start SelectTargetScript");
    }

    public IEnumerator Shoot()
    {
        int shoots = 5;
        PositiveXBtn.gameObject.SetActive(true);
        PositiveXBtn.onClick.AddListener(() => MoveTarget(1, 0));
        NegativeXBtn.gameObject.SetActive(true);
        NegativeXBtn.onClick.AddListener(() => MoveTarget(-1, 0));
        PositiveXYtn.gameObject.SetActive(true);
        PositiveXYtn.onClick.AddListener(() => MoveTarget(0, 1));
        NegativeYBtn.gameObject.SetActive(true);
        NegativeYBtn.onClick.AddListener(() => MoveTarget(0, -1));
        ShootBtn.onClick.AddListener(() => shoots = ShootTarget(shoots));
        while (shoots > 0)
        {
            yield return null;
        }
        PositiveXBtn.gameObject.SetActive(false);
        NegativeXBtn.gameObject.SetActive(false);
        PositiveXYtn.gameObject.SetActive(false);
        NegativeYBtn.gameObject.SetActive(false);
        // DebugConsole:
        Debug.Log("ShootEnd ! ");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void MoveTarget(int x, int y)
    {
        //getrendered tile element from tilescript
        int lastX = XPos;
        int lastY = YPos;
        if (x != 0)
            XPos += x;
        else
            YPos += y;
        int pos = CalculatePos(XPos, YPos);
        if (pos != -1)
        {
            //Renderer NewTarget = tiles[pos].GetComponent<Renderer>();
            tileScript.SetTargetWater(tiles[pos].GetComponent<Renderer>());
            int lastPos = CalculatePos(lastX, lastY);
            if(!tileScript.Board.ContainsKey(lastPos.ToString()) || tileScript.Board[lastPos.ToString()] == "0")
                tileScript.SetOriginalWater(tiles[lastPos].GetComponent<Renderer>());
            else if (tileScript.Board[lastPos.ToString()][0] == 'H')
                tileScript.SetBlackWater(tiles[lastPos].GetComponent<Renderer>());
        }
        else
        {
            XPos = lastX;
            YPos = lastY;
        }
    }

    private int CalculatePos(int x, int y)
    {
        if(x < 20 && x >= 0 && y >= 0 && y < 12)
            return (y*20) + x;
        StartCoroutine(textScript.ChangeTextWithTime("Can't move out ! AARRGH!", 3));
        return -1;
    }

    private int ShootTarget(int shoots)
    {
        string pos = CalculatePos(XPos, YPos).ToString();
        // DebugConsole:
        Debug.Log("Shoot: " + XPos + " " + YPos);
        if(tileScript.Board.ContainsKey(pos) && tileScript.Board[pos][0] == 'H')
            StartCoroutine(textScript.ChangeTextWithTime("BlackWater!", 3));
        StartCoroutine(DropBullet());
        tileScript.Board[pos] = "H2";
        return --shoots;
    }

    IEnumerator DropBullet()
    {
        Bullet.SetActive(true);
        // DebugConsole:
        Debug.Log("BulletActivate");
        float time = 0;
        while (time <= duration)
        {
            time += Time.deltaTime;
            Bullet.transform.position = Vector3.Lerp(aPos.position, tiles[CalculatePos(XPos, YPos)].transform.position, time / duration);
            yield return null;
        }
        if (time > duration)
        {
            Bullet.SetActive(false);
            // DebugConsole:
            Debug.Log("BulletDeActivate");

            //tileScript.SetBlackWater(tiles[CalculatePos(XPos, YPos)].GetComponent<Renderer>());
        }
    }
}
