# 월드 및 데이터 모델 설계

## 1. 좌표 구조
- 내부 좌표: `Vector2Int GridPosition`
- 화면 좌표: 쿼터뷰 변환 좌표
- 로직은 전부 Grid 기준으로 판단한다.

## 2. 월드 오브젝트 공통 구조
```csharp
public abstract class WorldObject
{
    public string Id;
    public string DisplayName;
    public Vector2Int GridPosition;
    public bool IsBlocking;
    public bool IsInteractable;
    public bool IsDestroyed;
    public int HitPoints;
    public string OwnerNpcId;
}
```

## 3. 오브젝트 분류
### 3.1 자연물
- TreeObject
- GrassObject
- RockObject
- BushObject

### 3.2 건물
- HouseObject
- StorageObject
- FarmObject
- WorkshopObject
- ShrineObject

### 3.3 이벤트성 오브젝트
- CorpseObject
- FireObject
- DroppedItemObject

## 4. 예시 세부 구조
```csharp
public class TreeObject : WorldObject
{
    public int WoodAmount;
    public float Growth;
    public bool CanBeChopped;
    public bool IsFruitTree;
}

public class RockObject : WorldObject
{
    public int StoneAmount;
    public bool CanBeMined;
}

public class HouseObject : WorldObject
{
    public int Capacity;
    public List<string> ResidentNpcIds;
    public Vector2Int DoorPosition;
}
```

## 5. NPC 데이터 구조
```csharp
public class NpcData
{
    public string Id;
    public string DisplayName;
    public int Age;
    public string Role;

    public PersonalityProfile Personality;
    public ValueProfile Values;
    public NeedProfile Needs;
    public EmotionProfile Emotion;
    public MemoryProfile Memory;
    public RelationshipProfile Relationships;

    public Vector2Int GridPosition;
    public NpcState CurrentState;
    public string CurrentTargetId;
}
```

## 6. 스피키 생성 규칙
- 표시 이름은 `스피키 A`, `스피키 B`, `스피키 C`, `스피키 D`
- 내부 ID는 `spiky_a`, `spiky_b`, `spiky_c`, `spiky_d`
- 성격 프리셋은 시작 시 랜덤 선택
- 다른 NPC와 완전히 동일한 규칙 사용

## 7. 성격 프리셋 방향
완전 무작위 태그 조합보다 프리셋 풀에서 랜덤 선택하는 방식으로 시작한다.

예시 프리셋:
- 신중함, 겁 많음, 공감적
- 공격적, 충동적, 경쟁적
- 호기심 많음, 사교적, 수다스러움
- 계산적, 이기적, 생존 우선

## 8. 관계 데이터
NPC는 서로에 대해 다음 값을 가질 수 있다.
- 친밀도
- 신뢰도
- 공포
- 증오
- 빚/은혜

## 9. 기억 데이터
- 단기 기억: 최근 본 대상, 최근 들은 말, 현재 위협
- 장기 기억: 도움, 배신, 공격, 사망 사건
- 서사 기억: 인생 전환점, 큰 상실, 승진, 전투 생존

## 10. 인벤토리 및 자원
NPC 및 저장고는 자원을 보유한다.
- 식량
- 목재
- 돌
- 도구
- 기타 제작 재료