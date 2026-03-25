# Unity 씬 및 프리팹 구성안

## 1. 기본 씬 구조
### Scene: VillageSimulation
- Main Camera
- GridRoot
- GroundTilemap
- WorldObjectsRoot
- NpcRoot
- EventRoot
- UIRoot
- Managers

## 2. Managers 구성
- GameManager
- TimeManager
- SimulationManager
- GridManager
- WorldManager
- NpcManager
- ObjectManager
- EventLogManager
- UIManager

## 3. 프리팹 분류
### 자연물 프리팹
- TreePrefab
- GrassPrefab
- RockPrefab
- BushPrefab

### 건물 프리팹
- HousePrefab
- StoragePrefab
- FarmPrefab
- WorkshopPrefab

### NPC 프리팹
- NpcBasePrefab
- AnimalPrefab

## 4. 프리팹 공통 원칙
- 모든 주요 오브젝트는 개별 프리팹으로 존재
- 공통 스크립트 + 타입별 스크립트 구조 사용
- 시각 Sprite와 충돌 영역은 분리 가능하게 설계

## 5. Sprite 및 정렬
- 쿼터뷰 2.5D 느낌의 스프라이트 사용
- Y축 기반 Sorting Order 계산
- 큰 오브젝트는 시각 영역과 실제 충돌 영역을 분리

## 6. UI 구성
- 상단: 시간, 배속, 자원 총량
- 좌측: 사건 로그
- 우측: 선택 대상 상세 패널
- 하단: 선택된 NPC의 현재 행동, 감정, 목표 요약

## 7. NPC 상세 패널 표시 항목
- 이름
- 역할
- 성격 태그
- 현재 상태
- 현재 목표
- 배고픔/피로
- 최근 기억
- 관계 목록

## 8. 오브젝트 상세 패널 표시 항목
- 이름
- 위치
- 내구도
- 자원량
- 상호작용 가능 여부
- 소유/거주 정보

## 9. 폴더 구조 예시
```text
Assets/
  Art/
    Sprites/
      Environment/
      Buildings/
      NPC/
  Prefabs/
    Objects/
    Buildings/
    NPC/
    UI/
  Scenes/
    VillageSimulation.unity
  Scripts/
    Core/
    NPC/
    Objects/
    Buildings/
    AI/
    UI/
    Data/
```

## 10. 초기 배치 권장안
- 집 1채
- 나무 8~12개
- 바위 4~6개
- 풀 10~20개
- 스피키 A/B/C/D 4명
- 저장고 1개

이 구성이면 초기 자원 루프와 생존 루프를 검증하기 좋다.