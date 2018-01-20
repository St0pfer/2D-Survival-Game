using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;

public class SaveGameSettings : MonoBehaviour
{
    /*
    public Playerbars myPlayerbars;
    public GameObject Charakter;
    public Object Object;
    public float posx;
    public float posy;
    public float posz;
    public string name;
    public float posxC;
    public float posyC;
    public List<string> Names;
    public List<float> PosX;
    public List<float> PosY;
    public List<float> PosZ;
    public List<string> Nameparent;
    public List<string> Child;
    public bool loading;
    private bool destroyed;*/

    public MapGenerator myMapGenerator;
    private JsonData ObjectData;
    private GameObject Char;
    public GameObject MapGenerator;
    public GameObject Tilemap;
    private Playerbars myPlayerbars;
    private string jsonString;
    public int id;
    public int layer;
    public string name;
    public double posx;
    public double posy;
    public double posz;
    public NightDayCircel myNightDayCircel;
    public GameObject NightDayCircel;
    public List<Chest.ChestItems> Chestlist = new List<Chest.ChestItems>();
    public List<Map> MapObjects = new List<Map>();


    void Start()
    {
        myMapGenerator = MapGenerator.GetComponent<MapGenerator>();
        myNightDayCircel = NightDayCircel.GetComponent<NightDayCircel>();
        Char = GameObject.Find("Charakter");
        myPlayerbars = Char.GetComponent<Playerbars>();
        /*
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Game")
        {
            Charakter = GameObject.Find("Charakter");
            myPlayerbars = Charakter.GetComponent<Playerbars>();
        }

        List<string> Names = new List<string>();
        List<float> PosX = new List<float>();
        List<float> PosY = new List<float>();
        List<float> PosZ = new List<float>();
        List<string> Nameparent = new List<string>();
        List<string> Child = new List<string>();
        */
    }

    void Update()
    {
        /*
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Game")
        {
            Charakter = GameObject.Find("Charakter");
            if (Charakter != null)
                myPlayerbars = Charakter.GetComponent<Playerbars>();
            posxC = Charakter.transform.position.x;
            posyC = Charakter.transform.position.y;
        }
        */
    }
    /*

        //it's static so we can call it from anywhere
        public void Save()
        {
            Nameparent.Clear();
            Child.Clear();
            Names.Clear();
            PosX.Clear();
            PosY.Clear();
            PosZ.Clear();
            Delete();
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

            BinaryFormatter bf = new BinaryFormatter();
            string path = "Game_Data/Savegame/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            FileStream file = File.Create("Game_Data/Savegame/" + "/savedGames.gd");
            foreach (GameObject Obj in allObjects)
            {
                if (Obj.layer == 10)
                {
                    int childvalue;
                    if (Obj.name == "Rightarm" || Obj.name == "Leftarm")
                    { childvalue = 0; }
                    else { childvalue = 1; }

                    int childcounter = Obj.transform.childCount;
                    if (childcounter > childvalue)
                    {
                        for (int i = childvalue; i < childcounter; i++)
                        {
                            Transform child = Obj.transform.GetChild(childvalue);
                            Nameparent.Add(Obj.name);
                            Child.Add(child.name);
                        }
                    }

                }

                if (Obj.transform.parent == null && Obj.layer != 5 && Obj.layer != 9)
                {
                    name = Obj.name;
                    posx = Obj.transform.position.x;
                    posy = Obj.transform.position.y;
                    posz = Obj.transform.position.z;
                    Names.Add(name);
                    PosX.Add(posx);
                    PosY.Add(posy);
                    PosZ.Add(posz);
                }
            }
            bf.Serialize(file, Nameparent);
            bf.Serialize(file, Child);
            bf.Serialize(file, Names);
            bf.Serialize(file, PosX);
            bf.Serialize(file, PosY);
            bf.Serialize(file, PosZ);
            bf.Serialize(file, posxC);
            bf.Serialize(file, posyC);
            bf.Serialize(file, myPlayerbars.currentHealth);
            bf.Serialize(file, myPlayerbars.currentHunger);
            bf.Serialize(file, myPlayerbars.currentThirst);
            bf.Serialize(file, myPlayerbars.currentEXP);
            bf.Serialize(file, myPlayerbars.LvLcounter);
            file.Close();
        }

        public void Load()
        {
            loading = true;
            // (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
            if (File.Exists("Game_Data/Savegame/" + "/savedGames.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open("Game_Data/Savegame/savedGames.gd", FileMode.Open);
                Nameparent = (List<string>)bf.Deserialize(file);
                Child = (List<string>)bf.Deserialize(file);
                Names = (List<string>)bf.Deserialize(file);
                PosX = (List<float>)bf.Deserialize(file);
                PosY = (List<float>)bf.Deserialize(file);
                PosZ = (List<float>)bf.Deserialize(file);
                // Charakter Stats laden
                posxC = (float)bf.Deserialize(file);
                posyC = (float)bf.Deserialize(file);
                myPlayerbars.currentHealth = (float)bf.Deserialize(file);
                myPlayerbars.currentHunger = (float)bf.Deserialize(file);
                myPlayerbars.currentThirst = (float)bf.Deserialize(file);
                myPlayerbars.currentEXP = (float)bf.Deserialize(file);
                myPlayerbars.LvLcounter = (int)bf.Deserialize(file);
                file.Close();



                GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                // Alle Gegenstände der Scene außer Player und UI zerstören
                foreach (GameObject Obj in allObjects)
                {
                    if (Obj.transform.parent != null && Obj.layer != 5 && Obj.layer != 9 && Obj.layer != 10 && Obj.transform.parent.gameObject.layer !=10)
                    {
                        Destroy(Obj);
                    }

                    if (Obj.transform.parent == null && Obj.layer != 5 && Obj.layer != 9 && Obj.layer != 10)
                    {
                        Destroy(Obj);
                    }
                }
                destroyed = true;
                // Alle Objecte außer Player und UI neu generieren
                if (destroyed)
                {
                    for (int i = 0; i < Names.Count; i++)
                    {
                        Object Prefab = Resources.Load("AllPrefabs/" + Names[i]);
                        if (Prefab != null)
                        {
                            Object New = Instantiate(Prefab, new Vector3(PosX[i], PosY[i], PosZ[i]), Quaternion.identity);
                            New.name = Prefab.name;
                        }
                    }
                }
                // Alle Inventar Objecte neu generieren
                if (destroyed)
                {
                    for (int i = 0; i < Nameparent.Count; i++)
                    {
                        GameObject Parent = GameObject.Find(Nameparent[i]);
                        if(Parent == null){ throw new System.NullReferenceException("parent not found"); }
                        if (Parent.activeSelf == true)
                        {
                            Object Prefab = Resources.Load("AllPrefabs/" + Child[i]);
                            GameObject New = Instantiate(Prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            New.name = Prefab.name;
                            New.transform.SetParent(Parent.transform);
                        }
                    }
                }

            }
            Charakter.transform.position = new Vector2(posxC, posyC);
            loading = false;
            destroyed = false;
        }
        public void Delete()
        {
            if (File.Exists("Game_Data/Savegame/" + "/savedGames.gd"))
            {
                File.Delete("Game_Data/Savegame/" + "/savedGames.gd");
            }
        }

        public void LoadDelay()
        {
            Invoke("Load", 1f);
        }

        public void Serialize()
        {
            string path = "Game_Data/Savegame/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            FileStream file = File.Create(path +"/savedChests.gd");

            Chest[] allObjects = UnityEngine.Object.FindObjectsOfType<Chest>();
            foreach (ISerializable Obj in allObjects)
            {
                Obj.Serialize(file);
            }
            file.Close();
        }

        public void DeSerialize()
        {
            string path = "Game_Data/Savegame/";
            if (File.Exists("Game_Data/Savegame/" + "/savedChests.gd"))
            {
                FileStream file = File.OpenRead(path + "/savedChests.gd");

                using (var reader = new StreamReader(file))
                {
                    string temp = reader.ReadLine();
                    ISerializable Chest = (ISerializable) FindObjectOfType(System.Type.GetType(temp));
                    var Temp = (ISerializable) Chest;
                    Chest.DeSerialize(reader);
                }
                file.Close();
            }
        }

        */

    public void Save()
    {

        SaveCharakter();
        SaveObjects();
        SaveStoredItems();
        DeleteChests();
        GetChestContent();
        SaveChests();
        myMapGenerator.ShowWholeMap = true;
        SaveMap();
        myMapGenerator.ShowWholeMap = false;
    }

    public void Load()
    {
        myMapGenerator.mapgeneratoractive = false;
        LoadCharakter();
        LoadObjects();
        LoadStoredItems();
        LoadChests();
        LoadMap();
    }

    public class Charakter
    {
        public double currentHealth;
        public double currentHunger;
        public double currentThirst;
        public double currentEXP;
        public int LvLcounter;
        public double posx;
        public double posy;
        public double posz;
        public int day;
        public int hour;
        public double minute;

        public Charakter(double currentHealth, double currentHunger, double currentThirst,
                         double currentEXP, int LvLcounter, double posx, double posy, double posz, int day, int hour, double minute)
        {
            this.currentHealth = currentHealth;
            this.currentHunger = currentHunger;
            this.currentThirst = currentThirst;
            this.currentEXP = currentEXP;
            this.LvLcounter = LvLcounter;
            this.posx = posx;
            this.posy = posy;
            this.posz = posz;
            this.day = day;
            this.hour = hour;
            this.minute = minute;
        }
    }

    public void SaveCharakter()
    {

        Charakter Player = new Charakter(myPlayerbars.currentHealth, myPlayerbars.currentHunger,
                                       myPlayerbars.currentThirst, myPlayerbars.currentEXP,
                                       myPlayerbars.LvLcounter, Char.transform.position.x,
                                       Char.transform.position.y, Char.transform.position.z,
                                       myNightDayCircel.day, myNightDayCircel.hour, myNightDayCircel.minute);

        ObjectData = JsonMapper.ToJson(Player);
        File.WriteAllText("Game_Data/Savegame/Charsave.json", ObjectData.ToString());
    }

    public void LoadCharakter()
    {
        jsonString = File.ReadAllText("Game_Data/Savegame/Charsave.json");
        ObjectData = JsonMapper.ToObject(jsonString);

        string stringHealth = ObjectData[0].ToString();
        string stringHunger = ObjectData[1].ToString();
        string stringThirst = ObjectData[2].ToString();
        string stringEXP = ObjectData[3].ToString();
        string stringLvL = ObjectData[4].ToString();
        string stringposx = ObjectData[5].ToString();
        string stringposy = ObjectData[6].ToString();
        string stringposz = ObjectData[7].ToString();
        string stringday = ObjectData[8].ToString();
        string stringhour = ObjectData[9].ToString();
        string stringminute = ObjectData[10].ToString();

        float Health = float.Parse(stringHealth);
        float Hunger = float.Parse(stringHunger);
        float Thirst = float.Parse(stringThirst);
        float EXP = float.Parse(stringEXP);
        int LvL = int.Parse(stringLvL);
        float posx = float.Parse(stringposx);
        float posy = float.Parse(stringposy);
        float posz = float.Parse(stringposz);
        int day = int.Parse(stringday);
        int hour = int.Parse(stringhour);
        float minute = float.Parse(stringminute);

        // Zuweisung
        myPlayerbars.currentHealth = Health;
        myPlayerbars.currentHunger = Hunger;
        myPlayerbars.currentThirst = Thirst;
        myPlayerbars.currentEXP = EXP;
        myPlayerbars.LvLcounter = LvL;
        Char.transform.position = new Vector3(posx, posy, posz);
        myNightDayCircel.day = day;
        myNightDayCircel.hour = hour;
        myNightDayCircel.minute = minute;
    }

    public class SavingGameObjects
    {
        public int id;
        public int layer;
        public string name;
        public double posx;
        public double posy;
        public double posz;

        public SavingGameObjects(int id, int layer, string name,
                                  double posx, double posy, double posz)
        {
            this.id = id;
            this.layer = layer;
            this.name = name;
            this.posx = posx;
            this.posy = posy;
            this.posz = posz;
        }

    }


    public void SaveObjects()
    {
        int idcounter = 0;

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        int arraylength = allObjects.Length;

        SavingGameObjects[] SavedObjects = new SavingGameObjects[arraylength];
        List<SavingGameObjects> ObjectList = new List<SavingGameObjects>();

        for (int i = 0; i < arraylength; i++)
        {
            GameObject Obj = allObjects[i];

            if (Obj.layer != 10 && Obj.layer != 5 && Obj.layer != 9 && Obj.transform.parent == null)
            {
                id = idcounter;
                layer = Obj.layer;
                name = Obj.name;
                posx = Obj.transform.position.x;
                posy = Obj.transform.position.y;
                posz = Obj.transform.position.z;

                SavingGameObjects Object = new SavingGameObjects(id, layer, name, posx, posy, posz);
                ObjectList.Add(Object);
                idcounter++;
            }
        }
        SavedObjects = ObjectList.ToArray();
        ObjectData = JsonMapper.ToJson(SavedObjects);
        File.WriteAllText("Game_Data/Savegame/Objsave.json", ObjectData.ToString());
    }

    public void LoadObjects()
    {
        // Alle Objecte löschen----------------------------------------------------------

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        int arraylength = allObjects.Length;

        for (int i = 0; i < arraylength; i++)
        {
            GameObject Obj = allObjects[i];
            if (Obj.layer != 10 && Obj.layer != 5 && Obj.layer != 9 && Obj.transform.parent == null)
            {
                Destroy(Obj.gameObject);
            }
        }

        jsonString = File.ReadAllText("Game_Data/Savegame/Objsave.json");
        ObjectData = JsonMapper.ToObject(jsonString);
        int count = ObjectData.Count;
        for (int i = 0; i < count; i++)
        {
            name = ObjectData[i][2].ToString();
            string stringposx = ObjectData[i][3].ToString();
            string stringposy = ObjectData[i][4].ToString();
            string stringposz = ObjectData[i][5].ToString();
            posx = float.Parse(stringposx);
            posy = float.Parse(stringposy);
            posz = float.Parse(stringposz);
            Object Prefab = Resources.Load("AllPrefabs/" + name);
            if (Prefab != null)
            {
                Object New = Instantiate(Prefab, new Vector3((float)posx, (float)posy, (float)posz), Quaternion.identity);
                New.name = name;
            }
        }
    }

    public class StoredItems
    {
        public string name;
        public string parentname;

        public StoredItems(string name, string parentname)
        {
            this.name = name;
            this.parentname = parentname;
        }
    }


    // Inventar und UI gelagerte Items
    public void SaveStoredItems()
    {

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        int arraylength = allObjects.Length;
        StoredItems[] SavedItems = new StoredItems[arraylength];
        List<StoredItems> ItemList = new List<StoredItems>();

        foreach (GameObject Obj in allObjects)
        {
            if (Obj.transform.parent != null && Obj.transform.parent.gameObject.layer == 10 && Obj.name != "Text")
            {
                StoredItems Item = new StoredItems(Obj.name, Obj.transform.parent.name);
                ItemList.Add(Item);
            }

        }
        SavedItems = ItemList.ToArray();
        ObjectData = JsonMapper.ToJson(SavedItems);
        File.WriteAllText("Game_Data/Savegame/Itemsave.json", ObjectData.ToString());

    }
    public void LoadStoredItems()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject Obj in allObjects)
        {
            if (Obj.transform.parent != null && Obj.transform.parent.gameObject.layer == 10 && Obj.name != "Text")
            {
                Destroy(Obj.gameObject);
            }
        }

        jsonString = File.ReadAllText("Game_Data/Savegame/Itemsave.json");
        ObjectData = JsonMapper.ToObject(jsonString);
        int count = ObjectData.Count;
        for (int i = 0; i < count; i++)
        {
            string name = ObjectData[i][0].ToString();
            string parentname = ObjectData[i][1].ToString();

            GameObject Parent = GameObject.Find(parentname);
            GameObject Prefab = Resources.Load("AllPrefabs/" + name) as GameObject;
            GameObject New = Instantiate(Prefab, new Vector3(0, 0, 0), Quaternion.identity);
            New.name = name;
            New.transform.SetParent(Parent.transform);
            New.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void GetChestContent()
    {
        Object[] Objects = FindObjectsOfType<Chest>();
        foreach (Object Obj in Objects)
        {
            Chest linkToChestScript = (Chest)Obj;
            linkToChestScript.SaveChest();
        }
    }
    public void SaveChests()
    {
        string ObjectDataSlot = JsonMapper.ToJson(Chestlist);
        File.WriteAllText("Game_Data/Savegame/Chestsave.json", ObjectDataSlot);
    }
    public void LoadChests()
    {
        Object[] Objects = FindObjectsOfType<Chest>();
        foreach (Object Obj in Objects)
        {
            Chest linkToChestScript = (Chest)Obj;
            linkToChestScript.LoadChest();
        }
    }

    public void DeleteChests()
    {
        if (File.Exists("Game_Data/Savegame/Chestsave.json"))
            File.Delete("Game_Data/Savegame/Chestsave.json");
    }

    public class Map
    {
        public double posx;
        public double posy;
        public double posz;
        public string sprite;

        public Map(double posx, double posy, double posz, string sprite)
        {
            this.posx = posx;
            this.posy = posy;
            this.posz = posz;
            this.sprite = sprite;
        }

    }

    public void SaveMap()
    {
        MapObjects.Clear();
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject Obj in allObjects)
        {
            if (Obj.layer == 11)
            {
                Sprite sprite = Obj.GetComponent<SpriteRenderer>().sprite;
                Map NewMap = new Map(Obj.transform.position.x, Obj.transform.position.y,
                                     Obj.transform.position.z, sprite.name);
                MapObjects.Add(NewMap);
            }
        }
        int listcounter = MapObjects.Count;
        Map[] SavedTiles = new Map[listcounter];

        SavedTiles = MapObjects.ToArray();
        JsonData ObjectDataMap = JsonMapper.ToJson(SavedTiles);
        File.WriteAllText("Game_Data/Savegame/Mapsave.json", ObjectDataMap.ToString());
    }

    public void LoadMap()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject Obj in allObjects)
        {
            if (Obj.layer == 11)
            {
                Destroy(Obj);
            }
        }

        jsonString = File.ReadAllText("Game_Data/Savegame/Mapsave.json");
        JsonData ObjectDataMap = JsonMapper.ToObject(jsonString);

        int Tilecounter = ObjectDataMap.Count;
        for (int i = 0; i < Tilecounter; i++)
        {
            string stringposx = ObjectDataMap[i][0].ToString();
            string stringposy = ObjectDataMap[i][1].ToString();
            string stringposz = ObjectDataMap[i][2].ToString();
            string sprite = ObjectDataMap[i][3].ToString();
            posx = float.Parse(stringposx);
            posy = float.Parse(stringposy);
            posz = float.Parse(stringposz);

            GameObject PrefabTile = Resources.Load("AllPrefabs/Tile") as GameObject;
            GameObject NewTile = Instantiate(PrefabTile, new Vector3((float)0, (float)0, (float)0), Quaternion.identity);
            Sprite Sprite = Resources.Load("Tiles/"+sprite, typeof(Sprite)) as Sprite;
            NewTile.gameObject.GetComponent<SpriteRenderer>().sprite = Sprite;
            NewTile.name = "Tile";
            NewTile.transform.SetParent(Tilemap.transform);
            NewTile.transform.position = new Vector3((float)posx, (float)posy, (float)posz);
        }
    }
}


