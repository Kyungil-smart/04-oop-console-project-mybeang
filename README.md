# OOP Project - OOP 기반 Console 게임 제작 프로젝트

## 목표
- Global Goal: OOP 강의에서 들었던 많은 내용을 토대로, 콘솔 게임 제작해보기.
- KPI
  - LSP 에 어긋나지 않는 다형성 설계.
  - Class 별 책임 역할 분배 잘 나누기.  
  - Interface 활용.
  - 객체간 상호작용 경험.
  - Observer Pattern 사용해보기.
  - Stateful Design Pattern 사용해보기.

# 결과물

## 제목: Survive Console World

## 게임 내용

- 장르: 스테이지 기반 뱀파이벌서바이벌류

## Snapshot
### Title
![alt text](docs/img/title.png)

### Credits
![alt text](docs/img/Credits.png)
- 화면이 아래에서 위로 움직임.
- 완전히 멈추면 "엔터 입력시~~" 가 뜨며 씬간 이동 가능.

### 조작법
![alt text](docs/img/howToPlay.png)

### 게임 화면
![alt text](docs/img/gameScreen001.png)
- 좌측위: 플레이어 생명력
- 가운데: 스테이지 번호
- 우측위: 맵상 적 개체수 / 스테이지별 총 적 개체수

![alt text](docs/img/gameScreen002.png)
- Z키 입력시 위 화면과 같이 총알 발생
- 적`E`는 플레이어를 지속적으로 쫒아옴.

![alt text](docs/img/nextStage.png)
- 해당 스테이지 클리어시 위 화면 출력.
- 약 3초뒤 다음 스테이지로 이동.

### 스테이지 구성
#### Stage 1.
![alt text](docs/img/stage1.png) 
#### Stage 2.
![alt text](docs/img/stage2.png) 
#### Stage 3.
![alt text](docs/img/stage3.png) 
#### Stage 4.
![alt text](docs/img/stage4.png) 
#### Stage 5.
![alt text](docs/img/stage5.png)

### 게임 오버
![alt text](docs/img/gameOver.png)

### 게임 승리
![alt text](docs/img/victory.png)

# 남은 이슈
https://github.com/Kyungil-smart/04-oop-console-project-mybeang/issues

# 특이사항
## 적이 플레이어를 쫒아오는 알고리즘 관련
[문서 링크](/docs/traceToPlayer.md)
