# 오브젝트 아트 적용 계획

## 목표
현재 프로젝트의 집, 나무, 풀, 바위 오브젝트에 동일한 2.5D 픽셀아트 분위기의 스프라이트를 적용한다.

## 1차 적용 대상
- TreeObject
- GrassObject
- RockObject
- HouseObject
- StorageObject

## 적용 방식
- Unity `SpriteRenderer`에 오브젝트별 스프라이트 연결
- 투명 배경 PNG 사용
- 쿼터뷰 2.5D 픽셀아트 스타일 통일
- 큰 오브젝트는 `QuarterViewSorter` 사용

## 권장 파일 구조
- `Assets/Art/Sprites/Environment/Trees/`
- `Assets/Art/Sprites/Environment/Grass/`
- `Assets/Art/Sprites/Environment/Rocks/`
- `Assets/Art/Sprites/Buildings/Houses/`
- `Assets/Art/Sprites/Buildings/Storage/`

## 적용 순서
1. 오브젝트별 스프라이트 준비
2. Unity에서 Sprite Import 설정 적용
3. 각 프리팹의 SpriteRenderer에 연결
4. Collider와 Pivot 확인
5. QuarterViewSorter로 가림 순서 확인

## 체크포인트
- 오브젝트 분위기가 서로 어울리는가
- 크기 비율이 자연스러운가
- 집이 나무/바위/풀과 같은 세계관으로 보이는가
- 바닥 접지감이 맞는가
- 투명 배경이 깨끗한가

## 다음 검증 단계
- 1차: 기본 오브젝트 세트 적용
- 2차: 크기/피벗/정렬 조정
- 3차: 애니메이션 대상(나무/풀/횃불) 프레임화
- 4차: 전체 마을 배치 후 통일감 검수