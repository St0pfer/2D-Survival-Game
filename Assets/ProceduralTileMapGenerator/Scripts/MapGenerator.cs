using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * FileName: MapGenerator.cs
 * FileType: Visual C# Source file
 * Author : Frazer05
 * Extended by: Kaelthas0
 * Date : 12/12/2017
 * Description : Class for creating the Map
 */

public class MapGenerator : MonoBehaviour
{
    public bool mapgeneratoractive = true;
    public int mapWidth = 100;    //groese der Map X
    public int mapHeight = 100;   //groese der Map Y
    public int VoronoiPointCount = 100;
    public float spriteGroesse = 1;  //Unity-Groesse des Sprites

    public TileType[] TileTypes; //enthält alle möglichen Map-Teile
    public Sprite BorderSprite;
    //public int BorderSize = 5;
    public float ItemSpawnChance = 0.5f;
    public ItemSpawn[] ItemSpawns;
    private Lottery<GameObject> itemSpawnLottery;

    public GameObject tile; //das Prefab mit einem SpriteRenderer drauf

    public Transform parent;    //Leeres GameObject, das alle Sprites enthält
    private Transform cameraTransform;   //Transform der Kamera

    public bool ShowWholeMap = false; //Sollte nur zum testen aktiviert werden
    private int screenWidth; //anzahl der Teile auf X-Koordinate
    private int screenHeight;    //anzahl der Teile auf Y-Koordinate

    private byte[,] map;    //Array das die Werte der Teile enthält
    private SpriteRenderer[,] spriteRenderer;   //Array das alle SpriteRenderer enthält

    private Sprite[] sprites;

    void Start()
    {
        if (mapgeneratoractive)
        {

            cameraTransform = Camera.main.transform;

            Vector3 cameraP1 = Camera.main.ScreenToWorldPoint(Vector3.zero);
            Vector3 cameraP2 = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
            screenWidth = (int)(Mathf.Abs(cameraP2.x - cameraP1.x) / spriteGroesse) + 5;
            screenHeight = (int)(Mathf.Abs(cameraP2.y - cameraP1.y) / spriteGroesse) + 5;

            //Erstelle eine gerade anzahl an SpriteRenderer auf der map
            screenWidth += screenWidth % 2 == 0 ? 0 : 1;
            screenHeight += screenHeight % 2 == 0 ? 0 : 1;

            if (ShowWholeMap)
            {
                screenWidth = mapWidth + 20;
                screenHeight = mapHeight + 20;
            }

            spriteRenderer = new SpriteRenderer[screenWidth, screenHeight]; //Speicher für SpriteRenderer

            int halfScreenWidth = screenWidth / 2;  //erstellen der Sprites
            int halfScreenHeight = screenHeight / 2;
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    GameObject NewTile = GameObject.Instantiate(tile, new Vector3((spriteGroesse * x - halfScreenWidth * spriteGroesse), (spriteGroesse * y - halfScreenHeight * spriteGroesse), 0), Quaternion.identity, parent);
                    spriteRenderer[x, y] = NewTile
                        .GetComponent<SpriteRenderer>();    //Instantiert ein neues Objekt mit SpriteRender, und speichert diesen im SpriteRendererArray
                }
            }

            sprites = new Sprite[TileTypes.Length + 1];
            for (int i = 1; i < TileTypes.Length + 1; i++)
            {
                sprites[i] = TileTypes[i - 1].TypeSprite;
            }
            sprites[0] = BorderSprite;

            VoronoiMap voronoiMap = new VoronoiMap(TileTypes);
            map = voronoiMap.GenerateMap(mapWidth, mapHeight, VoronoiPointCount);

            //GenerateBorder();

            GenerateItemSpawnLotteries();
            SpawnItems();

            refreshScreen();    //SpriteRenderer erneuern
        }
    }

    void Update()
    {
        if (mapgeneratoractive)
        {
            if (ShowWholeMap)
                return;

            if (Mathf.Abs(cameraTransform.position.x - parent.position.x) > spriteGroesse || Mathf.Abs(cameraTransform.position.y - parent.position.y) > spriteGroesse)   //Parent zu weit von Kamera entfernt?
            {
                parent.position = new Vector3((int)(cameraTransform.position.x / spriteGroesse) * spriteGroesse, (int)(cameraTransform.position.y / spriteGroesse) * spriteGroesse, 0); //Parent neue Position setzten
                refreshScreen();    //SpriteRenderer erneuern
            }
        }
    }



    //private void GenerateBorder()
    //{
    //    mapWidth += BorderSize * 2;
    //    mapHeight += BorderSize * 2;

    //    byte[,] mapWithBorder = new byte[mapWidth, mapHeight];

    //    for(int x = BorderSize; x < mapWidth-BorderSize; x++)
    //    {
    //        for(int y = BorderSize; y < mapHeight-BorderSize; y++)
    //        {
    //            mapWithBorder[x, y] = map[x - BorderSize, y - BorderSize];
    //        }
    //    }

    //    map = mapWithBorder;
    //}

    private void GenerateItemSpawnLotteries()
    {
        itemSpawnLottery = new Lottery<GameObject>();
        foreach (ItemSpawn item in ItemSpawns)
        {
            itemSpawnLottery.Add(item.ItemPrefab, item.Weight);
        }

        foreach (TileType type in TileTypes)
        {
            foreach (ItemSpawn item in type.UniqueItemForTileType)
            {
                type.ItemSpawnLottery.Add(item.ItemPrefab, item.Weight);
            }
        }
    }

    private void SpawnItems()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                TileType type = TileTypes[map[x, y] - 1];

                bool spawnItem = Random.Range(0, 100) <= type.ItemSpawnChance && type.UniqueItemForTileType.Length != 0;
                if (spawnItem)
                {
                    SpawnItemAtLocation(x, y, type.ItemSpawnLottery.Draw());
                }
                else
                {
                    bool generalSpawnItem = Random.Range(0, 100) <= ItemSpawnChance && ItemSpawns.Length != 0;
                    if (generalSpawnItem)
                    {
                        SpawnItemAtLocation(x, y, itemSpawnLottery.Draw());
                    }
                }
            }
        }
    }

    private void SpawnItemAtLocation(int x, int y, GameObject item)
    {
        string name = item.name;
        GameObject Item = Instantiate(item, new Vector3(x * spriteGroesse - mapWidth / 2 * spriteGroesse, y * spriteGroesse - mapHeight / 2 * spriteGroesse), Quaternion.identity);
        Item.name = name;
    }

    /// <summary>
    /// Setzt alle SpriteRenderer auf den aktuellen Wert der Map
    /// </summary>
    private void refreshScreen()
    {
        int posX = ((int)Mathf.Round(parent.position.x / spriteGroesse) + mapWidth / 2 - screenWidth / 2);    //transformiert koordinaten von Unity-KoordinatenSystem
        int posY = ((int)Mathf.Round(parent.position.y / spriteGroesse) + mapHeight / 2 - screenHeight / 2);  //zu Map-KoordinatenSystem zu Screen-Koordinaten

        for (int x = 0; x < screenWidth; x++)
        {
            for (int y = 0; y < screenHeight; y++)
            {

                try
                {
                    spriteRenderer[x, y].sprite = sprites[map[x + posX, y + posY]];  //setzt jedes SpriteRenderer auf den aktuellen Wert der Map }
                    if(spriteRenderer[x, y].sprite.name == "water")
                    {
                        spriteRenderer[x, y].GetComponent<BoxCollider2D>().enabled = true;
                        spriteRenderer[x, y].gameObject.AddComponent<Fishable>();
                    }
                    else { spriteRenderer[x, y].GetComponent<BoxCollider2D>().enabled = false; }
  
                }
                catch (System.Exception ex)
                {
                    spriteRenderer[x, y].sprite = BorderSprite;
                }
            }
        }
    }

    /// <summary>
    /// Aendert ein Teil der Map
    /// </summary>
    /// <param name="posX">x-Koordinate in Unity-KoordinatenSystem</param>
    /// <param name="posY">y-Koordinate in Unity-KoordinatenSystem</param>
    /// <param name="value">Wert des Teils</param>
    public void changeTile(int posX, int posY, byte value)
    {
        map[posX + mapWidth / 2, posY + mapHeight / 2] = value; //setzt den Wert der Map, transformiert die Koordinaten vorher in Map-Koordinaten

        posX -= (int)(parent.position.x / spriteGroesse) - screenWidth / 2;  //transformation in Screen-Koordinaten
        posY -= (int)(parent.position.y / spriteGroesse) - screenHeight / 2; //für das Aktualisieren des SpriteRenderers

        if (posX >= 0 && posY >= 0 && posX < screenWidth && posY < screenHeight)    //im Bereich der Kamera?
        {
            spriteRenderer[posX, posY].sprite = sprites[value];  //Wert setzten
        }
    }
}

[System.Serializable]
public class TileType
{
    public Sprite TypeSprite;
    public float Weight = 1;
    public float ItemSpawnChance = 0.5f;
    public ItemSpawn[] UniqueItemForTileType;
    public Lottery<GameObject> ItemSpawnLottery = new Lottery<GameObject>();
}

[System.Serializable]
public class ItemSpawn
{
    public GameObject ItemPrefab;

    public float Weight = 1;
}