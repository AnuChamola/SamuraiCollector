using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] PoolInfo[] objectsInfo;

    List<PoolInfo> refObjInfo = new List<PoolInfo>();
    List<string> refKeys = new List<string>();

    private Dictionary<string, PoolInfo> objectsDictionary;

    public static PoolManager instance;
    
    private void OnEnable()
    {
        InitializeDictionary();
        //JBXGameController.Instance.OnBackToMainMenu += ResetPool;
    }

    private void Awake()
    {
        instance = this;
        //Debug.LogError("Awake of PoolManager Hit >>>");
        objectsDictionary = new Dictionary<string, PoolInfo>();
        //Debug.LogError("Awake of PoolManager Hit : refObjInfo : " + (refObjInfo == null));
        refObjInfo.AddRange(objectsInfo);

        foreach (var info in refObjInfo)
        {
            if (info != null && string.IsNullOrEmpty(info.key))
            {
                if (info.prefab != null)
                    info.key = info.prefab.GetInstanceID().ToString();
            }
            //Debug.LogError("Awake of PoolManager Hit : refKeys : " + (refKeys == null));
            refKeys.Add(info.key);
        }
    }

    public void ResetPool()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        objectsDictionary.Clear();
    }

    /*  private void Update()
      {
          if(Input.GetKeyDown(KeyCode.Y))
          {
              ObjectsManager.Instance.SetPoolInfo(objectsInfo);
          }
      }
  */
    void InitializeDictionary()
    {
        //objectsDictionary = new Dictionary<string, PoolInfo>();
        /*   for(int i = 0; i < objectsInfo.Length; i++)
           {
               PoolInfo info = objectsInfo[i];
               if (string.IsNullOrEmpty(info.key)) info.key = info.prefab.GetInstanceID().ToString();
               Transform container = new GameObject(info.key + "_Pool").transform;
               container.parent = transform;
               info.objectsContainer = container;

               info.prefabsList = new List<GameObject>();
               for (int j = 0; j < info.poolSize; j++)
               {
                   GameObject go = Instantiate(info.prefab, info.objectsContainer);
                   go.SetActive(false);
                   info.prefabsList.Add(go);
               }
               objectsDictionary.Add(info.key, info);
           }*/



    }

    /// <summary>
    /// Checks if the object with this key can be spawned without creating a pool manually (Note: creating pool manually is needed for objects not added from inspector)
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool ContainsKey(string key)
    {
        return refKeys.Contains(key) || objectsDictionary.ContainsKey(key);
    }

    public void CreatePool(GameObject prefab, string key, int poolSize)
    {

        Debug.Log("### creating obj for ** " + key);
        string newKey = key;
        if (string.IsNullOrEmpty(newKey)) newKey = prefab.GetInstanceID().ToString();

        if (objectsDictionary.ContainsKey(newKey))
            return;

        PoolInfo info = new PoolInfo(newKey, prefab, poolSize);
        Transform container = new GameObject(info.key + "_Pool").transform;
        container.parent = transform;
        info.objectsContainer = container;

        info.prefabsList = new List<GameObject>();
        for (int j = 0; j < info.poolSize; j++)
        {
            GameObject go = Instantiate(info.prefab, info.objectsContainer);
            go.SetActive(false);
            info.prefabsList.Add(go);
        }
        objectsDictionary.Add(info.key, info);
    }

    public void CreatePool(string key)
    {

        PoolInfo objectInfo = GetObjectInfo(key);

        GameObject prefab = objectInfo.prefab;
        int poolSize = objectInfo.poolSize;

        Debug.Log("### creating obj for ** " + key);
        string newKey = key;
        if (string.IsNullOrEmpty(newKey)) newKey = prefab.GetInstanceID().ToString();

        if (objectsDictionary.ContainsKey(newKey))
            return;

        PoolInfo info = new PoolInfo(newKey, prefab, poolSize);
        Transform container = new GameObject(info.key + "_Pool").transform;
        container.parent = transform;
        info.objectsContainer = container;

        info.prefabsList = new List<GameObject>();
        for (int j = 0; j < info.poolSize; j++)
        {
            GameObject go = Instantiate(info.prefab, info.objectsContainer);
            go.SetActive(false);
            info.prefabsList.Add(go);
        }
        objectsDictionary.Add(info.key, info);
    }


    public GameObject GetObject(string _key)
    {
        //Debug.Log("### Get obj for ** " + _key);
        if (!objectsDictionary.ContainsKey(_key))
        {
            PoolInfo objInfo = GetObjectInfo(_key);

            if (objInfo == null)
            {

                int index = refKeys.IndexOf(_key);
                Debug.Log("### Get obj for index ** " + index);

                //objInfo = objectsInfo[index];

                if (index >= 0)
                    objInfo = objectsInfo[index];
                else
                {
                    index = refObjInfo.FindIndex(x => x.key == _key);

                    if (index >= 0)
                        objInfo = objectsInfo[index];
                    else
                        Debug.Log("### index is negative for ** " + _key);

                }

                if (objInfo != null)
                {
                    Debug.Log("### adding obj for first time >> " + objInfo.key + " :: " + objInfo.prefab.name);
                }

            }
            if (objInfo != null)
                CreatePool(objInfo.prefab, _key, objInfo.poolSize);
            else
                Debug.Log("### index is negative for ** " + _key);

        }


        List<GameObject> list = objectsDictionary[_key].prefabsList;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null && !list[i].activeInHierarchy)
            {
                list[i].SetActive(true);
                return list[i];
            }
        }

        GameObject go = Instantiate(objectsDictionary[_key].prefab, objectsDictionary[_key].objectsContainer);
        list.Add(go);
        return go;
    }
    public void ReturnToPool(string _key)
    {
        //Debug.Log("### Get obj for ** " + _key);
        if (!objectsDictionary.ContainsKey(_key))
        {
            PoolInfo objInfo = GetObjectInfo(_key);

            if (objInfo == null)
            {

                int index = refKeys.IndexOf(_key);
                Debug.Log("### Get obj for index ** " + index);

                //objInfo = objectsInfo[index];

                if (index >= 0)
                    objInfo = objectsInfo[index];
                else
                {
                    index = refObjInfo.FindIndex(x => x.key == _key);

                    if (index >= 0)
                        objInfo = objectsInfo[index];
                    else
                        Debug.Log("### index is negative for ** " + _key);

                }

                if (objInfo != null)
                {
                    Debug.Log("### adding obj for first time >> " + objInfo.key + " :: " + objInfo.prefab.name);
                }

            }
            if (objInfo != null)
                CreatePool(objInfo.prefab, _key, objInfo.poolSize);
            else
                Debug.Log("### index is negative for ** " + _key);

        }


        List<GameObject> list = objectsDictionary[_key].prefabsList;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null && list[i].activeInHierarchy)
            {
                list[i].SetActive(false);
                
            }
        }
    }
    PoolInfo GetObjectInfo(string key)
    {
        
        int index = objectsInfo.ToList().FindIndex(x => x.key == key);
        

        if (index >= 0)
            return objectsInfo[index];

        Debug.Log("### Getting key for >> " + key + " :: Returning null !! ");

        return null;
    }

    [System.Serializable]
    public class PoolInfo
    {
        public string key;
        public GameObject prefab;
        public int poolSize;

        [HideInInspector]
        public Transform objectsContainer;
        [HideInInspector]
        public List<GameObject> prefabsList;

        public PoolInfo(string key, GameObject prefab, int poolSize)
        {
            this.key = key;
            this.prefab = prefab;
            this.poolSize = poolSize;
        }
    }
}
