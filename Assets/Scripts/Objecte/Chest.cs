using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;


public class Chest : MonoBehaviour, ISerializable
{

    public SaveGameSettings mySaveGameSettings;
    public GameObject SaveGameManager;
    public GameObject SoundManager;
    public GameObject ChestSlotpanel;
    public Animator animator;
    public GameObject UI;
    public GameObject Chestcap;
    public GameObject Chestblack;
    private Color tmp;
    public AudioSource AudioSource;
    public AudioClip closechest;
    public AudioClip openchest;
    public string[] allSlots;
    public string[] allItems;
    public int[] amountItems;
    public ChestItems[] packet;
    public Dictionary<GameObject, int> ChestID;
    public List<string> Slotlist = new List<string>();
    public List<string> Itemlist = new List<string>();
    public List<int> amountItemlist = new List<int>();



    // Use this for initialization
    void Start()
    {
        SoundManager = GameObject.Find("SoundManager");
        SaveGameManager = GameObject.Find("SaveGameManager");
        mySaveGameSettings = SaveGameManager.GetComponent<SaveGameSettings>();
        animator = GetComponent<Animator>();
        tmp = this.GetComponent<SpriteRenderer>().color;
        allSlots = new string[9];
        allItems = new string[9];
        amountItems = new int[20];

        ChestID = new Dictionary<GameObject, int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UI.activeSelf)
        {
            animator.SetBool("chestopen", true);
        }
        else
        {
            animator.SetBool("chestopen", false);
        }
        GetContent();
    }

    public void OpenChestSound()
    {
        SoundManager.SendMessage("PlaySound", "openchest");
    }

    public void CloseChestSound()
    {
        SoundManager.SendMessage("PlaySound", "closechest");
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CloseChestSound();
        }
        this.GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.b, tmp.g, tmp.a - 0.5f);
        Chestblack.GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.b, tmp.g, tmp.a - 0.5f);
        Chestcap.GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.b, tmp.g, tmp.a - 0.5f);
    }
    private void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.b, tmp.g, tmp.a);
        Chestblack.GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.b, tmp.g, tmp.a);
        Chestcap.GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.b, tmp.g, tmp.a);
    }

    public void SaveContent()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = "Game_Data/Savegame/";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        FileStream file = File.Create("Game_Data/Savegame/" + "/savedChests.gd");

        bf.Serialize(file, allSlots);
        bf.Serialize(file, allItems);
        bf.Serialize(file, amountItems);
    }

    public void LoadContent()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = "Game_Data/Savegame/";
        if (!Directory.Exists(path))
        {
            FileStream file = File.Open("Game_Data/Savegame/savedChests.gd", FileMode.Open);
            allSlots = (string[])bf.Deserialize(file);

        }
    }
    // bisschen Abstrakt xD
    public void Serialize(Stream stream)
    {
        using (var writer = new StreamWriter(stream))
        {
            writer.WriteLine(GetType().FullName);
            writer.WriteLine(allItems.Length);
            for (int i = 0; i < allItems.Length; i++)
            {
                writer.WriteLine(allSlots[i]);
                writer.WriteLine(allItems[i]);
                writer.WriteLine(amountItems[i]);
            }
        }
    }

    public void DeSerialize(StreamReader reader)
    {
        // using (var reader = new StreamReader(stream))
        //  {
        string temp = reader.ReadLine();
        int amountSlots = int.Parse(temp); //Anzahl der Slots

        for (int i = 0; i < amountSlots; i++)
        {
            string Slotname = reader.ReadLine(); // Slotname
            string name = reader.ReadLine(); // Itemname 
            int amountitem = int.Parse(reader.ReadLine()); // Itemanzahl

            allSlots[i] = Slotname;
            allItems[i] = name;
            amountItems[i] = amountitem;

            if (!string.IsNullOrEmpty(name)) // oder string.empty oder != ""
            {
                for (int j = 0; j < amountitem; j++)
                {
                    ClearSlots();
                    GameObject New = InstantiateItem(name);
                    GameObject Parent = SearchSlot(allSlots[i], ChestSlotpanel.transform); //GetSlot(allSlots[i], null);
                    New.transform.SetParent(Parent.transform);
                    New.transform.localPosition = new Vector3(0, 0, 0);
                    New.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }

    public void GetContent()   // ChestSlot > ChestSlotItem
    {
        foreach (Transform Slot in ChestSlotpanel.transform)
        {
            // Slotnummer ermitteln
            var output = Regex.Match(Slot.name, @"\d+").Value;
            int slotnumber = Int32.Parse(output);
            //    allSlots[slotnumber - 1] = Slot.name;

            int childcounterS = Slot.childCount;
            if (childcounterS > 0)
            {
                Transform SlotItem = Slot.transform.GetChild(0);
                allSlots[slotnumber - 1] = SlotItem.name;
                int childcounterSI = SlotItem.childCount;
                if (childcounterSI > 1)
                {
                    amountItems[slotnumber - 1] = childcounterSI - 1;
                    for (int i = 0; i < childcounterSI; i++)
                    {
                        Transform Item = SlotItem.transform.GetChild(i);
                        allItems[slotnumber - 1] = Item.name;
                    }
                }
            }
        }
    }


    private GameObject InstantiateItem(string name)
    {
        UnityEngine.Object Prefab = Resources.Load("AllPrefabs/" + name);
        GameObject New = Instantiate(Prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        New.name = Prefab.name;
        return New;
    }

    private GameObject SearchSlot(string name, Transform ChestSlotpanel)
    {
        Transform Placeholder = null;
        foreach (Transform Slot in ChestSlotpanel)
        {
            int childcounterS = Slot.childCount;
            if (childcounterS > 0)
            {
                Transform SlotItem = Slot.transform.GetChild(0);
                if (SlotItem.name == name)
                {
                    Placeholder = SlotItem;
                    return Placeholder.gameObject;
                }
            }
        }
        return Placeholder.gameObject;
    }

    private void ClearSlots()
    {
        foreach (Transform Slot in ChestSlotpanel.transform)
        {
            int childcounterS = Slot.childCount;
            if (childcounterS > 0)
            {
                Transform SlotItem = Slot.transform.GetChild(0);
                int childcounterI = SlotItem.childCount;
                if (childcounterI > 1)
                {
                    Transform Item = SlotItem.transform.GetChild(1);
                    Destroy(Item.gameObject);
                }
            }
        }
    }


    public class ChestItems
    {
        public double posx;
        public double posy;
        public double posz;
        public List<string> Slot;
        public List<string> Item;
        public List<int> amountItem;

        public ChestItems(double posx, double posy, double posz, List<string> Slot,
                          List<string> Item, List<int> amountItem)
        {
            this.posx = posx;
            this.posy = posy;
            this.posz = posz;
            this.Slot = Slot;
            this.Item = Item;
            this.amountItem = amountItem;
        }
    }

    public void SaveChest()
    {
        Slotlist.Clear();
        Itemlist.Clear();
        amountItemlist.Clear();

        double posx = this.transform.position.x;
        double posy = this.transform.position.y;
        double posz = this.transform.position.z;

        // Kistenslots mit Items und deren Anzahl ausgelesen
        foreach (Transform Slot in ChestSlotpanel.transform)
        {
            int counterSlot = Slot.childCount;
            if (counterSlot > 0)
            {
                Transform SlotItem = Slot.transform.GetChild(0);

                int counterSI = SlotItem.childCount;
                if (counterSI > 1)
                {
                    Transform Item = SlotItem.transform.GetChild(1);
                    Slotlist.Add(SlotItem.name);
                    Itemlist.Add(Item.name);
                    amountItemlist.Add(counterSI - 1);
                }
            }
        }

        ChestItems items = new ChestItems(posx, posy, posz, Slotlist, Itemlist, amountItemlist);
        mySaveGameSettings.Chestlist.Add(items);
    }

    public void LoadChest()
    {
        // Alle Kisten zerstören
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject Obj in allObjects)
        {
            if (Obj.name == "Chest")
                Destroy(Obj.gameObject);
        }

        string stringposx;
        string stringposy;
        string stringposz;
        string Slotname;
        string Itemname;
        string stringamountItem;

        string jsonString = File.ReadAllText("Game_Data/Savegame/Chestsave.json");
        JsonData ObjectData = JsonMapper.ToObject(jsonString);

        int countChest = ObjectData.Count;

        // Kistendurchlauf
        for (int k = 0; k < countChest; k++)
        {
            // Kiste mit Position instanzieren
            stringposx = ObjectData[k][0].ToString();
            stringposy = ObjectData[k][1].ToString();
            stringposz = ObjectData[k][2].ToString();
            float posx = float.Parse(stringposx);
            float posy = float.Parse(stringposy);
            float posz = float.Parse(stringposz);
            GameObject Prefab = Resources.Load("AllPrefabs/Chest") as GameObject;
            GameObject Chest = Instantiate(Prefab, new Vector3(posx, posy, posz), Quaternion.identity);
            Chest.name = "Chest";

            // Slots suchen 
            string stringcountSlot = ObjectData[k][3].Count.ToString();
            int countSlot = int.Parse(stringcountSlot);
            for (int s = 0; s < countSlot; s++)
            {
                Slotname = ObjectData[k][3][s].ToString();
                Chest ChestScript = Chest.GetComponent<Chest>();
                GameObject Slot = ChestScript.SearchSlot(Slotname, ChestScript.ChestSlotpanel.transform);

                Itemname = ObjectData[k][4][s].ToString();
                stringamountItem = ObjectData[k][5][s].ToString();
                int amountItem = int.Parse(stringamountItem);
                for (int i = 0; i < amountItem; i++)
                {
                    GameObject ItemPrefab = Resources.Load("AllPrefabs/"+Itemname) as GameObject;
                    GameObject Item = Instantiate(ItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    Item.transform.SetParent(Slot.transform);
                    Item.transform.position = new Vector3(0, 0, 0);
                    Item.name = Itemname;
                }
            }
        }
    }
}


