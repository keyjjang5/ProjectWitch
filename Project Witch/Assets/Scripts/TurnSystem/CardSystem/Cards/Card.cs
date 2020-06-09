﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Card : MonoBehaviour
{
    /*
     * name : 이 카드의 이름
     * cost : 이 카드를 사용하는 것에 소모되는 cost의 양
     */
    protected new string name;
    protected uint cost;
    public uint Cost { get { return cost; } }
    private Vector3 scale;
    protected int maxRange;
    protected int minRange;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        //MouseEnter();
        if (!CardSystem.instance.ReadyFlag)
            Highlight();
    }

    private void OnMouseExit()
    {
        //MouseExit();
        if (!CardSystem.instance.ReadyFlag)
            DeHighlight();
    }

    private void OnMouseDown()
    {
        Select();
    }

    private void OnMouseOver()
    {
        
    }

    // 카드를 강조 표시 할 때 사용한다.
    public void Highlight()
    {
        scale = transform.localScale;

        //transform.Translate(0, 1, -1);
        transform.localScale = transform.localScale * 1.4f;
    }

    // 카드의 강조 표시를 취소 할 때 사용한다.
    public void DeHighlight()
    {
        //transform.Translate(0, -1, 1);
        transform.localScale = scale;
    }

    // 카드를 선택 했을 때 사용한다.
    public void Select()
    {
        if (CardSystem.instance.ReadyFlag)
            return;

        CardSystem.instance.Select(gameObject);
    }

    // 카드의 선택을 취소 했을 때 사용한다.
    public void Cancel()
    {
        DeHighlight();
    }

    // 사용했을 때 사용되는 효과를 나타낸다.
    virtual public bool Use(GameObject target, int depth)
    { 
        Debug.Log("card");

        return true;
    }

    // 카드의 사정거리를 확인한다.
    virtual public bool CheckRange(int depth)
    {
        if (minRange > depth || depth > maxRange)
        {
            Debug.Log("당신은 사정거리가 맞지 않습니다.");
            return false;
        }

        return true;
    }

    // UI 업데이트
    virtual public void UIUpdate()
    {
        HPSystem.instance.UpdateHp();
    }
}
