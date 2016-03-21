﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class NumberImageRenderer : MonoBehaviour
{
    [System.Serializable]
    public class TextNumRenderData
    {
        public TextNumRenderData(string fileName, Transform parentTrasform, Color color, float justification)
        {
            this.fileName = fileName;
            this.parentTrasform = parentTrasform;
            this.color = color;
            this.justification = justification;
        }

        public string fileName;
        public Transform parentTrasform;
        public Color color;

        /// <summary>
        /// 位置揃いする間隔
        /// </summary>
        public float justification;
    }

    class CreateData
    {
        public RectTransform rectTrans;
        public Image image;
    }

    int digit = 0;
    int oldDigit = 0;

    Sprite[] sprites = null;
    List<CreateData> createList = new List<CreateData>();


    [SerializeField]
    TextNumRenderData data;

    [SerializeField]
    bool isDecimalPointSmall = false;

    [SerializeField]
    float decimalPointJustification = 0;

    [SerializeField]
    bool rightJustified = false;

    Vector3 startPosition = Vector3.zero;

    void Start()
    {
        startPosition = data.parentTrasform.GetComponent<RectTransform>().anchoredPosition3D;
    }

    void OnValidate()
    {
        ChnageColor(data.color);
    }

    CreateData Create(Sprite sprite)
    {
        var instance = new GameObject();
        instance.name = data.fileName + "_" + sprite.name;
        instance.transform.SetParent(data.parentTrasform);
        var rectTrans = instance.AddComponent<RectTransform>();
        rectTrans.localScale = Vector3.one;
        rectTrans.sizeDelta = data.parentTrasform.GetComponent<RectTransform>().sizeDelta;
        rectTrans.localRotation = Quaternion.identity;

        var image = instance.AddComponent<Image>();
        image.sprite = sprite;
        image.color = data.color;

        var createData = new CreateData() { rectTrans = rectTrans, image = image };

        return createData;
    }

    void ResourcesLoad()
    {
        if (sprites == null)
        {
            createList.Clear();
            sprites = Resources.LoadAll<Sprite>(data.fileName);
        }
    }

    /// <summary>
    /// 描画処理
    /// </summary>
    /// <param name="text"></param>
    void Rendering(string text)
    {
        if (oldDigit != text.Length)
        {
            foreach (Transform create in data.parentTrasform)
            {
                Destroy(create.gameObject);
            }
            createList.Clear();
            oldDigit = digit;
        }

        bool isNext = false;

        for (int i = 0; i < text.Length; i++)
        {
            var sprite = Array.Find(sprites, s => s.name == text[i].ToString());

            digit++;
            if (digit > oldDigit)
            {
                oldDigit = digit;
                var createData = Create(sprite);

                if (rightJustified)
                {
                    var size = !isNext ? 0 : createData.rectTrans.sizeDelta.x - data.justification;
                    var x = i * size;

                    var rectTrans = data.parentTrasform as RectTransform;
                    rectTrans.anchoredPosition =
                        new Vector2(startPosition.x - x, startPosition.y);

                    isNext = true;
                }

                createData.rectTrans.anchoredPosition3D = new Vector3(i * (createData.rectTrans.sizeDelta.x - data.justification), 0, 0);

                createList.Add(createData);
            }
            else
            {
                var createData = createList[i];
                createData.image.sprite = sprite;
                createData.rectTrans.name = data.fileName + "_" + sprite.name;
            }

        }

        DecimalPointSmall(text);

        digit = 0;

    }

    /// <summary>
    /// 小数点以下を小さくする処理
    /// </summary>
    void DecimalPointSmall(string text)
    {
        if (!isDecimalPointSmall) return;

        bool isDecimalPoint = false;
        bool isNext = false;
        var parentTrans = data.parentTrasform.GetComponent<RectTransform>();

        for (int i = 0; i < text.Length; i++)
        {
            var createData = createList[i];

            if (isDecimalPoint)
            {
                createData.rectTrans.sizeDelta = parentTrans.sizeDelta / 2;

                var space = isNext ? decimalPointJustification : 0;

                createData.rectTrans.anchoredPosition3D =
                    new Vector3(i * (createData.rectTrans.sizeDelta.x * 2 - data.justification - space), -createData.rectTrans.sizeDelta.y / 2, 0);

                isNext = true;
            }
            else
            {
                createData.rectTrans.sizeDelta = parentTrans.sizeDelta;
                createData.rectTrans.anchoredPosition3D =
                    new Vector3(i * (createData.rectTrans.sizeDelta.x - data.justification), 0, 0);
            }

            if (text[i] == '.')
            {
                isDecimalPoint = true;
            }

        }

    }

    /// <summary>
    /// 数字を画像で描画する関数
    /// </summary>
    public void Rendering(double score)
    {
        if (score <= -1) return;

        ResourcesLoad();

        Rendering(score.ToString("f2"));
    }

    /// <summary>
    /// 数字を画像で描画する関数
    /// </summary>
    public void Rendering(int score)
    {
        if (score <= -1) return;

        ResourcesLoad();

        Rendering(score.ToString());
    }

    /// <summary>
    /// 色をすべて変更する。
    /// </summary>
    /// <param name="color"></param>
    public void ChnageColor(Color color)
    {
        data.color = color;
        foreach (var list in createList)
        {
            list.image.color = data.color;
        }
    }

}