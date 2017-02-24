using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public GameObject cell;
    private int length;
    private int width;
    public int mineCount;
    public Transform map;

    private int flags;
    public static Generator Instance
    {
        get
        {
            return instance;
        }
    }
    private static Generator instance;
    public CellBehaviour[,] field; //Небезопасно. Извне можно изменить ссылку на поле.
 
    void Start() {
        length = GameConstants.GetLength();
        width = GameConstants.GetWidth();
        instance = this;
        flags = 0;
        if (mineCount == 0)
            mineCount = length * width / 15;
        field = new CellBehaviour[length,width];
        float scale = cell.transform.localScale.x;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                field[i,j] = (Instantiate(cell, new Vector3(i * scale, 0, j*scale), Quaternion.identity) as GameObject).GetComponent<CellBehaviour>();
                field[i, j].SetIndex(i,j);
                field[i, j].GetComponent<Transform>().SetParent(map, true);
            }
        }
        SetField();
    }
    public static int FlagsLeft()
    {
        Generator g = Generator.instance;
        return g.mineCount - g.flags;
    }
    public void SetField()
    {
        for (int k = 0; k < mineCount; k++)
        {
            int x = Random.Range(0, length), y = Random.Range(0, width);
            while(field[x,y].IsMine())
            {
                x = Random.Range(0, length);
                y = Random.Range(0, width);
            }
            field[x, y].SetMine();
            MinesCountUpdateNormal(x, y);
        }
    }
    void MinesCountUpdate(int x, int y)
    {
        //Проходим в квадратике вокруг мины и увеличиваем показатель количества мин вокруг
        int i_temp = 0;
        int j_temp = 0;
        for (int i = -1; i < 2; i++)
        {
            i_temp = (x + i < 0) ? (length - 1) : ((x + i > length - 1) ? 0 : (x + i));
            for (int j = -1; j < 2; j++)
            {
                j_temp = (y + j < 0) ? (width - 1) : ((y + j > width - 1) ? 0 : (y + j));
                CellBehaviour cell = field[i_temp, j_temp];
                if (!cell.IsMine())
                {
                    cell.status++;
                }
            }
        }
    }
    void MinesCountUpdateNormal(int x, int y)
    {
        //Проходим в квадратике вокруг мины и увеличиваем показатель количества мин 
        for (int i = -1; i < 2; i++)
        {
            if (x + i < 0 || x + i > length - 1)
                continue;
            for (int j = -1; j < 2; j++)
            {
                if (y + j < 0 || y + j > width - 1)
                    continue;
                CellBehaviour cell = field[x + i, y + j];
                if (!cell.IsMine())
                {
                    cell.status++;
                }

            }
        }
    }
    public static void IncreaseFlags()
    {
        Generator.instance.flags++;
        UIManager.UpdateText();
    }
    public static void DecreaseFlags()
    {
        Generator.instance.flags--;
        UIManager.UpdateText();
    }
    void Update () {
		
	}
}
