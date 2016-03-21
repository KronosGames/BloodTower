using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EFFECT_ID
{
    NULL,   //< 未定義
    FLAME,  //< 炎
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
    public GameObject prefab = null;
}

public class EffectData
{
    public EFFECT_ID effectID = EFFECT_ID.NULL;
    public GameObject effectObject = null;
    public ParticleSystemData particleSystemData = new ParticleSystemData();
    public EffectMover effectMove = null;

    public bool isActive = false;
    public bool isAutoDestroy = false;
}

public class EffectManager : ManagerBase
{
    static readonly Vector3 LostPosition = new Vector3(5000, 0, 0);

    [SerializeField]
    int createNum = 5;

    [SerializeField]
    List<EffectDatabase> setEffectDBList = new List<EffectDatabase>();

    static List<EffectData> effectDataList = new List<EffectData>();

	void Start ()
    {
        effectDataList.Clear();

        InitManager(this, MANAGER_ID.EFFECT);

        for (int createIndex = 0; createIndex < setEffectDBList.Count; createIndex++)
        {
            var db = setEffectDBList[createIndex];
            for (int i = 0; i < createNum; i++)
            {
                EffectData data = new EffectData();

                data.effectID = db.effectID;
                GameObject obj = Instantiate(db.prefab) as GameObject;
                obj.transform.SetParent(transform);
                obj.name = db.prefab.name + i.ToString("00");
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                data.effectObject = obj;
                data.effectMove = obj.GetComponent<EffectMover>();

                ParticleSystemData particleData = data.particleSystemData;
                particleData.trans = obj.transform.GetChild(0);
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
            var data = effectDataList[i];
            if (!data.isAutoDestroy) continue;

            if (data.effectMove.IsHit())
            {
                Stop(ref data);
                return;
            }
        }
    }

    static public EffectData PlayEffect(EFFECT_ID id, Vector3 createPosition, Vector3 targetPosition, float power, bool isAutoDestroy = false)
    {
        for (int i = 0; i < effectDataList.Count; i++)
        {
            var data = effectDataList[i];
            
            if(data.effectID != id) continue;
            if(data.isActive) continue;

            data.isActive = true;

            data.effectObject.transform.position = createPosition;
            data.effectMove.SetTargetPosition(targetPosition, power);
            data.particleSystemData.particleSystem.Play();

            data.effectObject.SetActive(true);
            data.isAutoDestroy = isAutoDestroy;

            return data;
        }

        return null;

    }

    static public void Stop(ref EffectData playData)
    {
        playData.isActive = false;
        playData.particleSystemData.particleSystem.Stop();
        playData.effectObject.SetActive(false);
        playData.effectObject.transform.position = LostPosition;
    }

}
