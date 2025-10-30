using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ItemBelt : MonoBehaviour
{

    // c#에서 기본으로 제공하는 연결리스트, 아이템 이름을 string으로 저장
    LinkedList<string> itemBelt;
    string lastUsedItem;

    void Start()
    {
        // 문자열의 LinkedList를 itemBelt라는 변수로 생성
        itemBelt = new LinkedList<string>();


        // 아이템들 생성        
        itemBelt.AddLast("HP 포션");
        itemBelt.AddLast("MP 포션");
        itemBelt.AddLast("검");
        itemBelt.AddLast("방패");
        itemBelt.AddLast("귀환 주문서");


        // 시작하면 띄워지는 메세지
        PrintCurrentBeltState(" 게임 시작 ! 현재아이템 벨트 상태 ");
    }


    void Update()
    {
        // Q 키를 누르면 나오는 CycleLeft() 함수
        if(Input.GetKeyDown(KeyCode.Q))
        {
            CycleLeft();
        }

        // E 키를 누르면 나오는 CycleRight() 함수
        if(Input.GetKeyDown(KeyCode.E))
        {
            CycleRight();
        }


        // 스페이스바 키를 누르면 나오는 UseCurrentItem() 함수
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UseCurrentItem();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            CreateItems();
        }



    }

    // 벨트를 왼쪽으로 도는 싸이클 
    void CycleLeft()
    {
        // 아이템 2개 미만이면 순환하지 않는다
        if(itemBelt.Count < 2)
        return;

        // 마지막에 나온 아이템을 가져온다
        LinkedListNode<string> lastNode = itemBelt.Last;

        // 벨트에서 마지막 아이템을 제거한 후
        itemBelt.RemoveLast();

        // 맨 앞에 제거한  아이템을 추가한다.
        itemBelt.AddFirst(lastNode);


        PrintCurrentBeltState("왼쪽으로 순환 (Q)");

    }


    void CycleRight()
    {
        // 아이템 2개 미만이면 순환하지 않음
        if(itemBelt.Count < 2)
        return;

        // 첫번째에 나온 아이템을 가져온다
        LinkedListNode<string> firstNode = itemBelt.First;

        // 벨트에서 첫번째 아이템을 제거한 후 
        itemBelt.RemoveFirst();

        // 맨 뒤 아이템을 가져온다.
        itemBelt.AddLast(firstNode);

        PrintCurrentBeltState("오른쪽으로 순환 (E)");

    }

	void UseCurrentItem()
	{
		if(itemBelt.Count == 0)
		{
			Debug.Log("사용할 아이템이 없습니다.");
			return;
		}

        // 현재 아이템 ( 첫번째 )의 이름을 가져오고
		string currentItem = itemBelt.First.Value;
		
		// 마지막으로 사용한 아이템 저장
		lastUsedItem = currentItem;

		// Debug.Log($"아이템 사용: {currentItem}");
        // 첫번째 아이템을 제거한다.
        itemBelt.RemoveFirst();

		PrintCurrentBeltState("아이템 사용 (Space)");
	}


    void CreateItems()
	{
		if(lastUsedItem == null)
		{
			Debug.Log("재생성할 아이템이 없습니다.");
			return;
		}

		// 마지막으로 사용한 아이템을 맨 앞에 다시 추가
		itemBelt.AddFirst(lastUsedItem);

		PrintCurrentBeltState("아이템 생성 (R)");

		// 여러 번 중복 생성되지 않도록 초기화
		lastUsedItem = null;
	}







    // 현재 벨트 상태를 보기 쉽게 콘솔로 출력시키는 함수
    void PrintCurrentBeltState(string action)
    {
        // 여러 문자열을 합칠 때 쓰는 함수
        StringBuilder sb = new StringBuilder();
        sb.Append($"[{action}]");

        // 만약 아이템의 개수가 0 이라면
        if(itemBelt.Count == 0)
        {
            sb.Append("벨트가 비어있습니다.");
        }

        // 그게 아니면
        else
        {
            // 노란색으로 강조해서 아이템을 보여준다.
            sb.Append($"<color=yellow><b> -> {itemBelt.First.Value}</b></color> | ");
        
            // 나머지 아이템들도 순서대로 보여준다
            int count = 0;
            foreach(string item in itemBelt)
            {
                // 만약 첫 아이템이 보여줬으면
                if(count > 0)
                {
                    sb.Append(item + " | ");
                }
                count++;
            }
        }

        Debug.Log(sb.ToString());



    }

}
