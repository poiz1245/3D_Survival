# Bloody Hell
## ⌨️개발 내용

### 1. 몬스터, 발사체 Object Pooling을 사용하여 관리
- 몬스터, 발사체, 경험치 오브젝트와 같이 생성과 파괴를 계속해서 반복하는 오브젝트별로 Object Pool을 만들어 관리하였음

  
### 2. 플레이어 무기와 각 무기 별 업그레이드 시스템 구현
- 플레이어가 사용하는 무기 4가지를 상위 클래스인 Weapon을 상속받고 WeaponUpGrade()를 override하여 각각 다르게 구현
- 기본 무기를 제외한 나머지 무기들은 업그레이드를 한번 해야 활성화
- power, shield와 같은 플레이어 스탯에 따라 무기 공격력과 받는 피해량 업데이트
  
◼ Gun
  - 기본무기, 플레이어 정면으로 날아감
  - 레벨에 따라 최대 4방향까지 발사체 생성
    
◼ Homing Launcher
  - Physics.OverlapColiider 사용하여 정해진 반경 내의 몬스터를 감지하고 최단거리에 있는 몬스터 저장
  - 최단거리 몬스터를 HomingMissile에서 참조하여 몬스터에게 날아감
  - 최단거리 몬스터의 변경을 감지하는 이벤트를 생성하고 HomingMissile에서 구독하여 변경될 때 생성돼 있는 발사체 비활성화
    
◼ Sword
  - 자식 오브젝트(칼)에서 몬스터와 충돌시 부모 오브젝트(회전 축)에게 충돌한 몬스터를 전달하여 부모에서 처리
  - 레벨에 따라 4개까지 생성
    
 ◼ AoE
  - Physics.OverlapColiider 사용하여 정해진 반경 내의 몬스터를 감지하여 쿨타임 마다 GetDamage() 호출
  - 레벨 4일 때 반경 내의 몬스터 이동속도 감소

    
### 3. 추적시스템 구현
- 주변의 가장 가까운 몬스터를 감지하여 공격하거나, 몬스터가 플레이어를 감지하여 공격하기 위해 사용
- 아래와 같은 폼으로 필요한 상황에 따라 조금씩 변환하여 사용하였음
- 타겟의 위치 정보를 참조하여 타겟 방향으로 이동할 수 있도록 함
```
 void ScanTargets()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, range, targetLayer);

        if (targets.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            foreach (Collider target in targets)
            {
                MeleeMonster meleeMonster = target.GetComponent<MeleeMonster>();
                if (meleeMonster != null)
                {
                    float distance = Vector3.Distance(transform.position, meleeMonster.transform.position);
                    if (distance < closestDistance && meleeMonster.hp > 0)
                    {
                        closestDistance = distance;
                        closestTargetObject = meleeMonster.gameObject;
                    }
                }
             }
            nearestTarget = closestTargetObject;
        }
        else
        {
            nearestTarget = null;
        }
    }
```


### 4. 무한맵 구현
- 4개의 Plan을 사용하여 이동시키면서 무한맵 구현
- 플레이어 위치와 각 ground오브젝트의 x, z좌표 차이를 구해서 더 많이 이동한 방향으로 일정 값 만큼 이동
- x, z좌표의 차이가 0.3이하일 경우 대각선으로 이동
- 방향은 Horizontal과 Vertical값을 받아서 상,하,좌,우 구분


### 5. 플레이어와 몬스터 움직임 구현
- rigidbody.velocity에 직접 값을 대입하여 움직이는 것은 velocity가 외부의 영향에 관계없이 고정되기 때문에 일반적인 캐릭터 움직임을 구현할 때 적절하지 않음, 이 프로젝트에서 플레이어와 몬스터의 움직임은 rigid.AddForce의 VelocityChange 모드 사용
- ForceMode.VelocityChange는 Rigidbody의 질량에 관계없이 직접적으로 속도에 변화를 주기 때문에 속도를 표현할 때 가장 직관적인 모드라고 생각하여 선택
- 하지만 일반적으로 오브젝트를 움직일 떄 AddForce를 사용하면 힘이 중첩되어 가속도가 붙게 되므로 이를 수정해 줄 필요가 있으며, TargetVelocity를 설정하고 (moveSpeed), TargetVelocity와 현재 Velocity의 차이를 AddForce의 force에 대입하여 중첩을 방지
- 회전은 Quaternion.LookRotation()에 moveDir.normalized해서 Quternion변수에 저장하고 rigid.MoveRotation으로 회전
- 몬스터는 플레이어의 위치를 참조하여 플레이어를 향해 이동, 공격 가능 거리에 플레이어가 감지될 때 정지 및 공격

  
### 6. UI 및 사운드
- 타이틀씬에서 게임씬으로 전환될 때 버튼클릭시 재생되는 사운드가 생략되는 것을 방지하기 위해 코루틴을 사용하여 씬을 로드하는 메서드 호출을 지연
- 플레이어가 레벨업을 할 때 8개의 업그레이드 옵션중 3개를 랜덤으로 선별하여 업그레이드 버튼을 활성화(List와 Array사용)
- 플레이어가 레벨업을 하면 무기 업그레이드 메서드를 호출하는 버튼을 검사하여 무기의 level이 4인 경우 배열에서 제거
- 플레이어 사망을 감지하는 이벤트를 생성하여 사망시 UIManager에서 GameOver Panel 활성화
- 보스 몬스터 사망 시 UIManager의 GameClear Panel 활성화
- 경험치와 플레이어 체력을 Slider로 생성하고 value에 플레이어의 currentExperience와 hp를 대입하여 업데이트
