using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

// 入力を操作管理する
//  - Button
//  - Drag And Drop
//  - Slider
//  - Scroll

public class PointerData
{
    public GameObject m_cGameObject = null;
    public Vector2 m_vPosition = Vector2.zero;
}

public class UIInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    enum EDRAG_STATE
    { 
        eNONE,
        eBEGIN,
        eDRAG,
        eEND
    }

    static EDRAG_STATE m_eState = EDRAG_STATE.eEND;
    static PointerEventData m_cPointerData = null;

    // ドラッグし始めたかどうか
    static public bool IsBeginDrag() { return m_eState == EDRAG_STATE.eBEGIN; }

    // ドラッグ中かどうか
    static public bool IsDrag() { return m_eState == EDRAG_STATE.eDRAG; }
    
    // ドラッグ終了したか
    static public bool IsEndDrag() { return m_eState == EDRAG_STATE.eEND; }

    // ポイントデータ
    static public PointerEventData GetPointerData() { return m_cPointerData; }


    public void OnBeginDrag(PointerEventData cPointerEventData)
    {
        m_eState = EDRAG_STATE.eBEGIN;
        m_cPointerData = cPointerEventData;
    }

    public void OnDrag(PointerEventData cPointerEventData)
    {
        m_eState = EDRAG_STATE.eDRAG;
        m_cPointerData = cPointerEventData;
    }

    public void OnEndDrag(PointerEventData cPointerEventData)
    {
        m_eState = EDRAG_STATE.eEND;
        m_cPointerData = cPointerEventData;

        StartCoroutine("WaitEndDrag");
    }


    IEnumerator WaitEndDrag()
    {
        yield return new WaitForEndOfFrame();

        m_eState = EDRAG_STATE.eNONE;

    }
}
