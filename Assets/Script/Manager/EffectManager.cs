using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EFFECT_ID
{
    NULL,           //< 未定義
    FLAME,          //< 炎
    BIG_SPLASH,     //< 大きい血しぶき
    SMALL_SPLASH,   //< 小さい血しぶき
    BIG_SMOKE,      //< 大きい煙
    SMALL_SMOKE,    //< 小さい煙
    WEAPON_HIT,     //< 武器オブジェクトヒット
    WATER_SPILL,    //< 水こぼれたとき
    WATER_SUPPLY,   //< 聖水補給
}

public class ParticleSystemData
{
    public Transform trans = null;
    public ParticleSystem particleSystem = null;
}

[System.Serializable]
public class EffectDatabase
{
    public EFFECT_ID effectID = EFFECT_ID.NULL;
    public int createCount = 3;
    public GameObject prefab = null;
}

public class EffectData
{
    public EFFECT_ID effectID = EFFECT_ID.NULL;
    public GameObject effectObject = null;
    public ParticleSystemData particleSystemData = new ParticleSystemData();

    public int playIndex = -1;
    public bool isActive = false;
    public bool isAutoDestroy = false;
}

public class EffectManager : ManagerBase
{
    static readonly Vector3 LostPosition = new Vector3(50000, 0, 0);

    [SerializeField]
    List<EffectDatabase> setEffectDBList = new List<EffectDatabase>();

    static List<EffectData> effectDataList = new List<EffectData>();
    static int playIndexCount = -1;

    void Start ()
    {
        playIndexCount = -1;
        effectDataList.Clear();

        InitManager(this, MANAGER_ID.EFFECT);

        for (int createIndex = 0; createIndex < setEffectDBList.Count; createIndex++)
        {
            var db = setEffectDBList[createIndex];
            for (int i = 0; i < db.createCount; i++)
            {
                EffectData data = new EffectData();

                data.effectID = db.effectID;
                GameObject obj = Instantiate(db.prefab) as GameObject;
                obj.transform.SetParent(transform);
                obj.name = db.prefab.name + i.ToString("00");
                obj.transform.localPosition = LostPosition;
                data.effectObject = obj;

                ParticleSystemData particleData = data.particleSystemData;
                particleData.trans = obj.transform;
                particleData.particleSystem = particleData.trans.GetComponent<ParticleSystem>();
                particleData.particleSystem.Stop();

                data.effectObject.SetActive(false);
                data.isActive = false;

                effectDataList.Add(data);
            }
        }
	}

    void Update()
    {
        for (int i = 0; i < effectDataList.Count; i++)
        {
            EffectData data = effectDataList[i];
            if (!data.isActive) continue;
            if (!data.isAutoDestroy) continue;

            if (!data.particleSystemData.particleSystem.isPlaying)
            {
                Stop(data.playIndex);
                return;
            }
        }
    }

    static public int PlayEffect(EFFECT_ID id, Vector3 createPosition, bool isAutoDestroy = false)
    {
        for (int i = 0; i < effectDataList.Count; i++)
        {
            EffectData data = effectDataList[i];
            
            if(data.effectID != id) continue;
            if(data.isActive) continue;

            playIndexCount++;

            data.isActive = true;
            data.effectObject.SetActive(true);

            data.effectObject.transform.position = createPosition;
            data.particleSystemData.particleSystem.Play();

            data.isAutoDestroy = isAutoDestroy;
            data.playIndex = playIndexCount;

            return data.playIndex;
        }

        return -1;
    }

    static public void Stop(int playIndex)
    {
        for (int i = 0; i < effectDataList.Count; i++)
        {
            EffectData data = effectDataList[i];
            if (data.playIndex != playIndex) continue;

            data.playIndex = -1;
            data.isActive = false;
            data.particleSystemData.particleSystem.Stop();
            data.effectObject.SetActive(false);
            data.effectObject.transform.position = LostPosition;
        }
    }

}
