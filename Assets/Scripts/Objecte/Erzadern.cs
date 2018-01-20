using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erzadern : MonoBehaviour
{

    public Ctrl myCtrl;
    public GameObject Charakter;
    public GameObject SoundManager;
    public int currentDurability;
    public Dictionary<string, int> AderDurability;
    public Dictionary<string, int> AderDamage;
    Animator animator;

    // Use this for initialization
    void Start()
    {
        SoundManager = GameObject.Find("SoundManager");
        animator = GetComponent<Animator>();
        Charakter = GameObject.Find("Charakter");
        myCtrl = Charakter.GetComponent<Ctrl>();

        // Haltbarkeit
        AderDurability = new Dictionary<string, int>();
        AderDurability.Add("Stonevein", 25);
        AderDurability.Add("Tinvein", 50);
        AderDurability.Add("Coppervein", 100);
        AderDurability.Add("Ironvein", 150);
        AderDurability.Add("Goldvein", 200);


        // Schaden
        AderDamage = new Dictionary<string, int>();
        AderDamage.Add("Stonevein", 5);
        AderDamage.Add("Tinvein", 10);
        AderDamage.Add("Coppervein", 20);
        AderDamage.Add("Ironvein", 25);
        AderDamage.Add("Goldvein", 30);

        // Eigene Haltbarkeit festlegen
        currentDurability = AderDurability[this.name];
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDurability <= 0)
            DropItem();
        if (myCtrl.workrdy == false)
            animator.SetBool("hit", false);

    }

    void OnTriggerEnter2D(Collider2D Tool)
    {
        if (Tool.name.Contains("Pickaxe") && myCtrl.workrdy == true)
        {
            // Referenz herstellen
            Items myItems = Tool.gameObject.GetComponent<Items>();
            // Von Item Schaden holen
            int dmgvalue = myItems.ItemDamage[Tool.name];
            // Eigenen Damage holen
            int owndmgvalue = AderDamage[this.name];
    
            // Haltbarkeit Tool / Ader gegenseitig abziehen
            Tool.SendMessage("TakeDamage", owndmgvalue);
            currentDurability -= dmgvalue;

            SoundManager.SendMessage("PlaySound", "pickaxe");
            animator.SetBool("hit", true);
        }
    }

    private void DropItem()
    {
        for (int i = 0; i < 3; i++)
        {
            string Name = this.name.Substring(0, this.name.Length - 4);
            float placeX = Random.Range(-0.5f, 0.5f);
            float placeY = Random.Range(-0.5f, 0.5f);
            if (!this.name.Contains("Stone"))
            {
                // Erzadern
                GameObject Spawn = Instantiate(Prefabliste.Instance().GetGameObject(Name + "ore"), new Vector3(0, 0, 0), Quaternion.identity);
                Spawn.transform.position = this.transform.position + new Vector3(placeX, placeY, 0);
                Spawn.name = Name + "ore";
                var spritetake = Spawn.GetComponent<SpriteRenderer>();
                spritetake.sortingOrder = 1;
            }
            else
            {
                // Stein
                GameObject Spawn = Instantiate(Prefabliste.Instance().GetGameObject(Name), new Vector3(0, 0, 0), Quaternion.identity);
                Spawn.transform.position = this.transform.position + new Vector3(placeX, placeY, 0);
                Spawn.name = Name;
                var spritetake = Spawn.GetComponent<SpriteRenderer>();
                spritetake.sortingOrder = 1;

            }
        }

        float placeVX = Random.Range(-1f, 1f);
        float placeVY = Random.Range(-1f, 1f);
        // Vein
        GameObject Vein = Instantiate(Prefabliste.Instance().GetGameObject("Vein"), new Vector3(0, 0, 0), Quaternion.identity);
        Vein.transform.position = this.transform.position + new Vector3(placeVX, placeVY, 0);
        Vein.name = "Vein";
        Vein myVein = Vein.GetComponent<Vein>();
        myVein.vein = this.name;
        var spritetakeV = Vein.GetComponent<SpriteRenderer>();
        spritetakeV.sortingOrder = 1;

        SoundManager.SendMessage("PlaySound", "stonebreak");
        Destroy(this.gameObject);
        Charakter.SendMessage("EXP", 10);
    }
}
