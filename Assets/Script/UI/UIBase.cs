using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


// UIの基礎となるクラス
public class UIBase : MonoBehaviour
{
    UI_TYPE_ID id = UI_TYPE_ID.NONE;
    List<Button> buttonList = new List<Button>();
    List<UIInput> inputList = new List<UIInput>();

    // 毎フレーム処理させる
    protected virtual void UpdateUI() { }

    // ゲームオブジェクトが削除するときに呼ばれる。
    protected virtual void DestroyUI() { }

    // UIを初期化する。
    protected void InitUI(UIBase behaviour, UI_TYPE_ID id,UI_SCREEN_TYPE screenType = UI_SCREEN_TYPE.NONE)
    {
        Button[] buttons = behaviour.GetComponentsInChildren<Button>();
        UIInput[] uiInputs = behaviour.GetComponentsInChildren<UIInput>();

        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];
            buttons[i].onClick.AddListener(() => { behaviour.OnButtonClickProcess(button); });
            buttonList.Add(buttons[i]);
        }

        for (int i = 0; i < uiInputs.Length; i++) { inputList.Add(uiInputs[i]); }

        this.id = id;

        UIManager.Register(behaviour, screenType);
        UIAnimation.Register(behaviour);
        
    }

    void OnDestroy()
    {
        DestroyUI();
        UIManager.Remove(this);
        UIAnimation.Remove(this);
    }

    // ボタンが押されたらこの処理が実行させる。
    protected virtual void ButtonClickProccess(Button clickButton) { }
    protected virtual void ButtonClickSelect(Button clickButton, int count) { }

    // ボタンが押されたらこの処理が実行させる。
    void OnButtonClickProcess(Button clickButton)
    {
        int length = clickButton.name.Length;
        string strCount = clickButton.name.Substring(length - 2);
        int count = 0;
        bool isParse = int.TryParse(strCount, out count);
        if (isParse)
        {
            ButtonClickSelect(clickButton, count);
        }
        else
        {
            ButtonClickProccess(clickButton);
        }

    }

    protected void ResetButton(UIBase root)
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].onClick.RemoveAllListeners();
        }

        Button[] buttons = root.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];
            buttons[i].onClick.AddListener(() => { OnButtonClickProcess(button); });
            buttonList.Add(buttons[i]);
        }
    }

    protected void AttachUIInput(GameObject cObj)
    {
        for(int i = 0;i<inputList.Count;i++)
        {
            if (inputList[i].gameObject == cObj)
            {
                Debug.Log("すでに" + cObj .name + "に UIInput があります。");
                return;
            }
        }

        inputList.Add(cObj.AddComponent<UIInput>());
    }



    // ボタンロックを設定する。
    // 引数 : buttonRootを設定すると、そのBaseのボタンは有効化にならない。
    protected void SetButtonLock(bool isLock, UIBase buttonRoot = null)
    {
        UIBase[] list = UIManager.GetUIBaseList();
        for (int i = 0; i < list.Length; i++)
        {
            if (buttonRoot != null)
            {
                if (list[i] == buttonRoot) continue;
            }

            int buttonCount = list[i].buttonList.Count;

            for (int ii = 0; ii < buttonCount; ii++)
            {
                list[i].buttonList[ii].enabled = !isLock;
            }
        }
    }

    // ---------------------------------------------------------
    // 公開用関数
    // ---------------------------------------------------------

    // IDを取得
    public UI_TYPE_ID GetID()
    {
        return id;
    }

    // UIベースのアップデート処理
    // UIManagerで呼ばれています。
    public void UpdateUIBase()
    {
        UpdateUI();
    }

    // UIのセットアップ
    public virtual void SetupUI() { }

    // UIを開く処理
    public virtual void Open() { }

    // UIを閉じる処理
    public virtual void Close() { }
}
