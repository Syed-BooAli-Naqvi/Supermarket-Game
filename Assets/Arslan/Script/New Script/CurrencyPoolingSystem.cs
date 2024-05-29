using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CurrencyPoolingSystem : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefeb;
        public int size;
    }
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;

    //public bool Istrue;
    //public float cooldown, saveValue;

    public List<string> tags;
    public string spawntag;
    public static CurrencyPoolingSystem instance;
    public List<GameObject> SpawningOBject;
    public List<Transform> NoteMovingPosList, CoinMovingPosList;
    public GameObject Currentobj;

    public Transform currencymoveOnCounterPos, currencymoveBackPos, SpawnPosition;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //StartCoroutine("Generator");
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefeb, this.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
        //saveValue = cooldown;
    }
    public GameObject SpownFromPool(string tag,Vector3 position, Quaternion rotation)
    {


        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag   " + tag + "  Does not exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(false);

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        //objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);


        return objectToSpawn;

    }

    public void GenerateSelectCurrency(int num)
    {
        //Istrue = true;

        spawntag = tags[num];
        //SpownFromPool(spawntag, SpawnPosition.position, Quaternion.identity);
        SpawningOBject.Add(SpownFromPool(spawntag, SpawnPosition.position, Quaternion.identity));
    }
    public void CurrencyBackMoveAfterBilling()
    {
        StartCoroutine(CurrencyBackMoveByWait());
    }
    IEnumerator CurrencyBackMoveByWait()
    {
        for (int i = 0; i < SpawningOBject.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);
            SpawningOBject[i].transform.DOMove(currencymoveBackPos.position, 0.3f).SetEase(Ease.Linear);
        }
        for (int i = 0; i < SpawningOBject.Count; i++)
        {
            SpawningOBject[i].SetActive(false);
        }
    }
    
    public void ResetAllCurrency()
    {

        for (int i = 0; i < SpawningOBject.Count; i++)
        {
            SpawningOBject[i].SetActive(false);
        }
        SpawningOBject.Clear();
    }


}
