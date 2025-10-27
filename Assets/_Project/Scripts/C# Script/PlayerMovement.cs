using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CharacterController controller;
    private Animator animator;

    // 마지막으로 누른 키 기억용
    private KeyCode lastKey = KeyCode.None;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float h = 0f;
        float v = 0f;

        // 🔄 입력 감지 (방향키 부호 반전: 동숲 시점용)
        if (Input.GetKey(KeyCode.W)) { v -= 1; lastKey = KeyCode.W; }
        if (Input.GetKey(KeyCode.S)) { v += 1; lastKey = KeyCode.S; }
        if (Input.GetKey(KeyCode.A)) { h += 1; lastKey = KeyCode.A; }
        if (Input.GetKey(KeyCode.D)) { h -= 1; lastKey = KeyCode.D; }

        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        if (moveDir != Vector3.zero)
        {
            // 🎯 회전 처리: 마지막으로 누른 방향 기준
            float targetY = transform.localEulerAngles.y;

            switch (lastKey)
            {
                case KeyCode.W: targetY = 180f; break;   // 위쪽(시각적으로 앞)
                case KeyCode.S: targetY = 0f; break;     // 아래쪽(시각적으로 뒤)
                case KeyCode.A: targetY = 90f; break;   // 왼쪽
                case KeyCode.D: targetY = -90f; break;    // 오른쪽
            }

            transform.localEulerAngles = new Vector3(0, targetY, 0);

            controller.Move(moveDir * moveSpeed * Time.deltaTime);
            animator?.SetBool("isWalking", true);
        }
        else
        {
            animator?.SetBool("isWalking", false);
        }
    }
}
