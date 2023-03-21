using System;

namespace EffectFlags
{
    // 삭제나 제거는 지양
    // 다른 능력치 증가로 상쇄
    
    [Flags]
    public enum BattlePositiveEffectFlags
    {
        // Buff - 30
        AttackUp,                   // 공업
        DefenseUp,                  // 방업
        SpeedUp,                    // 속업
        CriticalUp,                 // 크리업
        DodgeUp,                    // 회피업
        AccuracyUp,                 // 명중업
        Regeneration,               // 재생, 방어 상승
        Barrier,                    // 방어막
        Invincibility,              // 무적 
        Reflect,                    // 반사
        Stealth,                    // 은신 : 공격시 2배 대미지
        Camouflage,                 // 위장 : 회피 증가,
        Endurance,                  // 인내 : 이번 턴에 한해서, 일정 확률로 행동불능 면역
        Immune,                     // 면역 : 나의 행동불능 지속 시간 감소
        Aura,                       // 오라 : 기력 증가, 속도 증가
        Cured,                      // 치유 : 회복효율 증가
        Revive,                     // 부활 : 죽은 상태에서 부황
        Support,                    // 지원 : 지속 효과 피해 감소
        Connection,                 // 연대 : 연대 효과를 지닌 아군 수 만큼, 피해 분할  
        Shield,                     // 방패 : 방어 대폭증가, 공격 감소, 일정 확률로 받는 피해 무시
        Sacrify,                    // 희생 : 일정 데미지 상한선까지 피해 대신 입음
        Enlightenment,              // 각성 : 상대 처치시, 능력치 상승
        Absorb,                     // 흡수 : 피격시 능력치 상승
        Encouragement,              // 격려 : 모든 속도 증가
        Concentration,              // 집중 : 크리 , 명중 증가
        Insights,                   // 통찰 : 획득 경험치 증가
        Blessing,                   // 기원 : 모든 능력 전반 증가
        Berserk,                    // 광폭 : 공격력 대폭 증가, 방어력 감소, 치명 피해를 입었다면, 잠시 뒤 사망
        Justification,              // 정의 : 누적 인장 마다 공격력 증가
        Focus,                      // 경계 : 크리티컬 피해 감소
    }
    [Flags]
    public enum BattleNegativeEffectFlags
    {
        // Debuff - 30
        AttackDown,                 // 공 하락
        DefenseDown,                // 방 하락
        SpeedDown,                  // 속 하락
        CriticalDown,               // 크리 하락
        DodgeDown,                  // 회피 하락
        AccuracyDown,               // 명중 하락
        Disease,                    // 질병 : 버프 효과 1개 제거
        Cursed,                     // 저주 : 능력 전반 하락, 스킬 방어 중심, 일반 공격 불가
        Poison,                     // 중독 : 재생 하락, 중독으로 인한 피해 증가
        Frozen,                     // 빙결 : 행동 불가, 크리 방어 하락
        Paralyzed,                  // 마비 : 행동 불가, 일반 방어 하락
        Confused,                   // 혼란 : 일정 확률로 행동 불가, 공격 하락
        Sleep,                      // 수면 : 행동 불가, 스킬 방어 하락 
        Blind,                      // 실명 : 명중 하락, 크리 확률 하락
        Stun,                       // 기절 : 행동 불가
        Silence,                    // 침묵 : 스킬 사용 불가
        Fear,                       // 공포 : 일정 확률로 행동 불가, 속도 하락
        Charm,                      // 매혹 : 일시적으로 같은 편을 공격 ( n초 혹은 n턴 ) 
        Bleeding,                   // 출혈 : 공격 하락, 피해
        Burn,                       // 화염 : 모든 방어 하락, 화염으로 인한 피해 증가 
        Vulnerable,                 // 취약 : 스킬 방어 하락
        Weakened,                   // 약화 : 일반 방어 하락
        Exhausted,                  // 탈진 : 크리확률 하락, 공격 하락, 속도 하락
        MindControlled,             // 회유 : 상태 해제 전까지 같은 편을 공격
        Drunk,                      // 취함 : 능력 전반 하락, 일반 방어 중심, 스킬 사용 불가
        Overwhelmed,                // 압도 : 이동 속도 하락, 기력 하락, 
        Disarmed,                   // 무장 해제 : 아이템 능력치 반영 안됨
        Petrified,                  // 석화 : 행동 불가, 방어 증가
        Judgement,                  // 심판 : 일정 시간 후, 처형 또는 피해
        Imprint,                    // 인장 : 인장효과가 있는 상대방은, 아군 처치시마다 인장 피해를 받음
    }
    [Flags]
    public enum NonBattleEffectFlags
    {
        // 생산, 소비 , 유통에 관한 효과
    }
    [Flags]
    public enum HybridEffectFlags
    {
        // 글로벌 효과, 패시브 ,이벤트 효과
    }
}