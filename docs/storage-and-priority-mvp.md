# 저장고 입고 및 우선순위 채집 MVP 가이드

## 1. 추가된 스크립트
- `Assets/Scripts/Objects/StorageObject.cs`
- `Assets/Scripts/NPC/NpcInventoryAccess.cs`
- `Assets/Scripts/NPC/NpcPriorityResourceCollector.cs`

## 2. 목적
이 구성은 아래 2가지를 해결한다.
- 인벤토리가 어느 정도 차면 저장고로 자원을 옮긴다.
- 배고픔이 높을 때 Food 자원을 우선 찾는다.

## 3. 저장고 오브젝트 세팅
저장고 프리팹 또는 오브젝트에 아래를 부착한다.
- `StorageObject`
- 필요 시 `Collider2D`
- 필요 시 `QuarterViewSorter`

주의:
- `StorageObject`는 `WorldObject`를 상속하므로 `GridPosition` 값이 있어야 한다.
- 저장고 위치가 (0,0)으로 남아 있으면 NPC가 잘못된 위치로 이동할 수 있다.

## 4. NPC 프리팹 세팅
기존 `NpcResourceCollector` 대신 아래 구성을 권장한다.
- `NpcInventory`
- `NpcPriorityResourceCollector`

이미 `NpcResourceCollector`가 붙어 있다면, 테스트 중에는 **둘 중 하나만 활성화**해야 한다.

이유:
- 두 수집기가 동시에 같은 NPC의 `GridPosition`과 상태를 바꾸면 충돌할 수 있다.

## 5. 동작 방식
### 5.1 채집 우선순위
- Hunger >= hungerFoodPriorityThreshold 이면 Food 노드를 먼저 찾음
- Food 노드가 없으면 가장 가까운 아무 자원이나 찾음

### 5.2 입고 우선순위
- 인벤토리 총량 >= depositThreshold 이면 저장고를 먼저 찾음
- 저장고 근처에 도착하면 Food / Wood / Stone 순서로 입고

## 6. 필드 설명
### NpcPriorityResourceCollector
- `actionInterval`: 행동 갱신 간격
- `depositThreshold`: 이 수치 이상 들고 있으면 입고 시도
- `hungerFoodPriorityThreshold`: 이 수치 이상 배고프면 Food 우선 탐색

## 7. 이벤트 로그
다음 로그가 발생할 수 있다.
- "스피키 A 가 Food 자원 1 획득"
- "스피키 B 가 저장고에 Wood 자원 3 입고"

## 8. 확인 체크리스트
- Hunger가 높을 때 풀(Food)로 먼저 가는가
- 인벤토리가 차면 저장고로 이동하는가
- 저장고 수치가 증가하는가
- 인벤토리 수치가 줄어드는가
- 두 수집기를 동시에 붙이지 않았는가

## 9. 현재 구조상 주의사항
- `NpcInventory`는 private 필드를 사용하므로, 감소 처리는 `NpcInventoryAccess`가 reflection으로 수행한다.
- 이 방식은 현재 구조를 적게 건드리는 대신, 나중에는 `NpcInventory`에 공식 `Remove` 메서드를 추가하는 쪽이 더 좋다.

## 10. 다음 추천 단계
- 집 귀가 / 수면 루프
- 맵 범위 밖 이동 제한
- 자원별 채집 시간 차등
- 저장고 용량 제한
- 직업별 선호 자원 분화