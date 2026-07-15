using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float m_MoveSpeed = 5f;

    private void Update()
    {

        Vector2 inputDir = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.A))
            inputDir.x = -1;
        if (Input.GetKey(KeyCode.D))
            inputDir.x = 1;
        if (Input.GetKey(KeyCode.W))
            inputDir.y = 1;
        if (Input.GetKey(KeyCode.S))
            inputDir.y = -1;

        inputDir = inputDir.normalized;

        Vector3 dir = new Vector3(inputDir.x, 0, inputDir.y);
        transform.position += dir * m_MoveSpeed * Time.deltaTime;
    }


}
