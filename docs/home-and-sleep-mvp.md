# 귀가 및 수면 MVP 가이드

## 1. 추가된 스크립트
- `Assets/Scripts/Objects/HouseResidence.cs`
- `Assets/Scripts/NPC/NpcHomeBinding.cs`
- `Assets/Scripts/NPC/NpcSleepController.cs`

## 2. 목적
이 구성은 NPC가 피로가 높아지면 자기 집으로 돌아가고, 집에 도착하면 수면으로 피로를 회복하도록 만든다.

## 3. 집 오브젝트 세팅
집 프리팹 또는 오브젝트에 아래를 부착한다.
- `HouseObject`
- `HouseResidence`
- 필요 시 `Collider2D`
- 필요 시 `QuarterViewSorter`

주의:
- `HouseObject`의 `GridPosition` 또는 `HouseResidence`의 `fallbackGridPosition`이 정확해야 한다.
- 집 위치가 잘못되면 NPC가 엉뚱한 좌표로 이동할 수 있다.

## 4. NPC 프리팹 세팅
NpcBasePrefab에 추가:
- `NpcHomeBinding`
- `NpcSleepController`

기존 권장 구성:
- `NpcController`
- `NpcBrain`
- `NpcGridPositionSync`
- `QuarterViewSorter`
- `SpriteRenderer`
- `Collider2D`

## 5. 동작 방식
1. 게임 시작 시 `NpcHomeBinding`이 가까운 집을 찾아 할당
2. 피로가 `fatigueSleepThreshold` 이상이면 귀가 시도
3. 집까지 한 칸씩 이동
4. 집 도착 시 `Sleeping` 상태 진입
5. `fatigueRecoveryPerTick` 만큼 피로 감소
6. 수면 중에는 `hungerIncreaseWhileSleeping` 만큼 허기 증가

## 6. 주의사항
### 6.1 이동 충돌 가능성
현재 이동을 바꾸는 컴포넌트는 여러 개가 있을 수 있다.
- `NpcSimpleMovementMotor`
- `NpcResourceCollector`
- `NpcPriorityResourceCollector`
- `NpcSleepController`

여러 개를 동시에 쓰면 `GridPosition`을 서로 덮어쓸 수 있다.

### 6.2 권장 테스트 조합
#### 귀가/수면만 테스트할 때
- `NpcSleepController` 활성화
- `NpcSimpleMovementMotor` 비활성화
- `NpcResourceCollector` 비활성화
- `NpcPriorityResourceCollector` 비활성화

#### 채집 + 수면을 같이 테스트할 때
- `NpcPriorityResourceCollector` 활성화
- `NpcSleepController` 활성화
- `NpcSimpleMovementMotor` 비활성화
- `NpcResourceCollector` 비활성화

이 조합이 현재 구조상 가장 안전하다.

## 7. 이벤트 로그
`NpcSleepController`의 `eventLogManager`를 연결하면
- "스피키 A 가 집에서 수면 중"
같은 로그가 반복 출력될 수 있다.

필요하면 나중에 상태 전환 시 1회만 출력하도록 개선 가능하다.

## 8. 확인 체크리스트
- NPC가 가까운 집을 배정받는가
- 피로가 높아지면 집으로 이동하는가
- 집에 도착하면 `Sleeping` 상태가 되는가
- 피로 수치가 감소하는가
- 허기가 조금씩 증가하는가

## 9. 다음 추천 단계
- 낮/밤 시간대 기반 귀가
- 수면 중 행동 잠금
- 집 수용 인원 초과 처리
- 집과 저장고 우선순위 충돌 정리
- 행동 오케스트레이터 도입