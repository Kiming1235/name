# Unity 연결 가이드

## 1. 목표
현재 저장소에 들어간 스크립트 골격을 Unity 씬에 연결하여 다음 상태까지 가는 것이 목표다.
- 스피키 A/B/C/D 생성
- 성격 프리셋 랜덤 배정
- Grid 좌표 기반 위치 관리
- 쿼터뷰 좌표 변환 적용
- NPC 클릭 선택 가능

## 2. Hierarchy 권장 구조
- Main Camera
- Managers
  - GameManager
  - TimeManager
  - SimulationManager
  - WorldManager
  - NpcManager
  - EventLogManager
  - GridManager
- WorldObjectsRoot
- NpcRoot
- UIRoot

## 3. 필수 스크립트 연결
### Managers 오브젝트
- GameManager
- TimeManager
- SimulationManager
- WorldManager
- NpcManager
- EventLogManager
- GridManager

### NpcRoot 하위
- NpcBasePrefab 생성
- NpcController 부착
- NpcBrain 부착
- NpcGridPositionSync 부착
- NpcSimpleMovementMotor 부착
- SpriteRenderer 부착
- Collider2D 부착
- QuarterViewSorter 부착

## 4. 스피키 프리팹 준비
NpcBasePrefab에 아래를 세팅한다.
- NpcController
- NpcBrain
- NpcGridPositionSync
- NpcSimpleMovementMotor
- QuarterViewSorter
- Collider2D
- SpriteRenderer

## 5. SpikyNpcSpawner 세팅
빈 오브젝트를 만들고 `SpikyNpcSpawner`를 부착한다.
- npcPrefab: NpcBasePrefab 연결
- personalityPresetDatabase: 있으면 연결
- npcRoot: NpcRoot 연결
- spawnPositions: 4개 좌표 입력
  - (0,0)
  - (2,0)
  - (0,2)
  - (2,2)

## 6. 성격 프리셋 데이터
`PersonalityPresetDatabase` ScriptableObject를 생성한다.
예시 프리셋 4개:
- 신중형: 신중함, 겁 많음, 공감적
- 공격형: 공격적, 충동적, 경쟁적
- 관찰형: 호기심 많음, 사교적, 수다스러움
- 생존형: 계산적, 이기적, 현실적

## 7. GridManager 세팅
- tileWidth = 1
- tileHeight = 0.5
- worldOrigin = (0,0,0)

## 8. UI 세팅
### UIRoot
- HudController
- NpcDetailPanel
- ObjectDetailPanel
- SelectionRaycaster

### SelectionRaycaster 연결
- mainCamera: Main Camera 연결
- npcDetailPanel: 패널 연결
- objectDetailPanel: 패널 연결

## 9. 현재 코드 기준 작동 흐름
1. 씬 시작
2. SpikyNpcSpawner가 스피키 A/B/C/D 생성
3. 각 NPC는 랜덤 성격 프리셋을 받음
4. NpcBrain이 상태를 결정
5. NpcSimpleMovementMotor가 Moving 상태일 때 GridPosition을 바꿈
6. NpcGridPositionSync가 GridPosition을 쿼터뷰 월드 좌표로 반영
7. SelectionRaycaster가 클릭 선택을 처리

## 10. 바로 확인할 체크포인트
- 스피키 A/B/C/D가 보이는가
- 각자 상세 패널에서 이름이 구분되는가
- 성격 태그가 표시되는가
- 클릭 선택이 되는가
- Moving 상태일 때 위치가 바뀌는가

## 11. 다음 구현 우선순위
1. NpcManager와 SpikyNpcSpawner 연결 자동화
2. 이동 범위를 맵 안으로 제한
3. 자원 오브젝트와 상호작용 추가
4. 저장고 입고 추가
5. 귀가/수면 루프 추가