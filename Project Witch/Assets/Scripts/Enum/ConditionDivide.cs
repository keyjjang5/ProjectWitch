using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상태이상의 대분류를 정해 놓은 Enum 이 구분에 따른 구조 개편이 필요함
enum ConditionDivide
{
    Condition,
    CountCondition,
    CountdownCondition,
    AttackedCondition,
    HitedCondition
}

