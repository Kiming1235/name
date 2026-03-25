# Quarter View Village Simulation

쿼터뷰 마을 발전 시뮬레이션 프로젝트 설계 저장소입니다.

## 핵심 방향
- 쿼터뷰 2.5D 표현
- 집, 나무, 풀, 바위 등 주요 오브젝트 개별 인스턴스화
- 모든 NPC는 동일 규칙으로 동작
- 스피키 A/B/C/D는 이름만 고정하고 성격 프리셋은 게임 시작 시 랜덤 배정
- 플레이어는 관찰자

## 문서 목록
- `docs/architecture.md` : 전체 시스템 구조
- `docs/world-model.md` : 월드, 오브젝트, NPC 데이터 모델
- `docs/simulation-loop.md` : 시뮬레이션 루프와 행동 선택 구조
- `docs/mvp-roadmap.md` : MVP 범위와 단계별 개발 계획
- `docs/unity-scene-plan.md` : Unity 씬/레이어/프리팹 구성안
