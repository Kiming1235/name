# 자원 채집 MVP 연결 가이드

## 1. 추가된 스크립트
- `Assets/Scripts/Objects/ResourceNode.cs`
- `Assets/Scripts/NPC/NpcInventory.cs`
- `Assets/Scripts/NPC/NpcResourceCollector.cs`

## 2. 목적
이 구성은 스피키 NPC가 가장 가까운 자원 노드를 찾아 이동하고, 인접하면 자원을 1개씩 획득하는 가장 단순한 채집 MVP다.

## 3. 자원 오브젝트 세팅
### 나무 오브젝트
- `TreeObject`
- `ResourceNode`
  - resourceKind = Wood
  - currentAmount = 10
  - harvestPerAction = 1

### 바위 오브젝트
- `RockObject`
- `ResourceNode`
  - resourceKind = Stone
  - currentAmount = 10
  - harvestPerAction = 1

### 풀 오브젝트
- `GrassObject`
- `ResourceNode`
  - resourceKind = Food
  - currentAmount = 5
  - harvestPerAction = 1

## 4. NPC 프리팹 세팅
NpcBasePrefab에 추가:
- `NpcInventory`
- `NpcResourceCollector`

기존 포함 권장:
- `NpcController`
- `NpcBrain`
- `NpcGridPositionSync`
- `QuarterViewSorter`
- `SpriteRenderer`
- `Collider2D`

## 5. 이동 스크립트 주의
현재 이동 관련 스크립트는 2개다.
- `NpcSimpleMovementMotor`
- `NpcResourceCollector`

`NpcResourceCollector`는 목표 자원 쪽으로 직접 한 칸 이동한다.
따라서 자원 채집 테스트 중에는 **NpcSimpleMovementMotor를 잠시 빼거나 비활성화하는 것을 권장**한다.

이유:
- 둘 다 `GridPosition`을 바꿀 수 있어서
- 같은 프레임 또는 짧은 간격에서 중복 이동처럼 보일 수 있다.

## 6. 이벤트 로그 연결
`NpcResourceCollector`의 `eventLogManager`에 씬의 `EventLogManager`를 연결하면
- "스피키 A 가 Wood 자원 1 획득"
같은 로그가 출력된다.

## 7. 현재 동작 방식
1. NPC가 가장 가까운 `ResourceNode`를 찾음
2. 거리가 1 초과면 한 칸 이동
3. 거리가 1 이하이면 1회 채집
4. 인벤토리에 자원 추가
5. 상태를 Gathering / Chopping / Mining 으로 변경
6. 로그 기록

## 8. 확인 체크리스트
- 스피키가 자원 쪽으로 이동하는가
- 인접하면 자원이 줄어드는가
- NPC 인벤토리 수치가 증가하는가
- 로그가 출력되는가

## 9. 다음 확장 포인트
- 저장고에 입고
- 자원 종류별 우선순위
- 배고픔이 높을 때 Food 우선 채집
- 자원이 고갈되면 다른 노드로 전환
- 벌목/채굴 시간 차등 적용