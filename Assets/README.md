# Unity Assets 구조

이 폴더는 쿼터뷰 마을 발전 시뮬레이션의 Unity 프로젝트 골격이다.

## 주요 디렉토리
- `Scenes/` : 메인 씬 저장
- `Scripts/Core/` : 시간, 월드, 시뮬레이션 핵심 매니저
- `Scripts/NPC/` : NPC 데이터와 제어 로직
- `Scripts/Objects/` : 집, 나무, 풀, 바위 등 개별 오브젝트
- `Scripts/AI/` : 행동 점수 계산과 후보 선택
- `Scripts/Data/` : 성격 프리셋, 데이터베이스
- `Scripts/UI/` : HUD, 로그, 상세 패널
- `Prefabs/` : NPC/오브젝트/건물 프리팹
- `Art/` : 스프라이트와 시각 자산

## 시작 권장 순서
1. `Scenes/VillageSimulation.unity` 생성
2. `Managers` 오브젝트에 Core 매니저 부착
3. `WorldObjectsRoot`, `NpcRoot`, `UIRoot` 구성
4. 스피키 A/B/C/D 프리팹 테스트
5. 나무/풀/바위/집 프리팹 테스트