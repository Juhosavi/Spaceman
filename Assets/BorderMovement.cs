using UnityEngine;

public class BorderMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Liikkumisnopeus
    public Vector2 imageSize = new Vector2(10f, 10f); // Kuvan koko (leveys ja korkeus)

    private Vector2 topLeft;
    private Vector2 topRight;
    private Vector2 bottomRight;
    private Vector2 bottomLeft;

    private Vector2 targetPosition; // Nykyinen kohde
    private int currentCorner = 0; // Kulman indeksi (0 = topLeft, 1 = topRight, jne.)

    void Start()
    {
        // M‰‰rit‰ reuna-alueiden kulmat kuvan mukaan
        topLeft = new Vector2(-imageSize.x / 2, imageSize.y / 2);
        topRight = new Vector2(imageSize.x / 2, imageSize.y / 2);
        bottomRight = new Vector2(imageSize.x / 2, -imageSize.y / 2);
        bottomLeft = new Vector2(-imageSize.x / 2, -imageSize.y / 2);

        // Aseta aloituskulma
        targetPosition = topLeft;
        transform.position = topLeft;
    }

    void Update()
    {
        // Liikuta kohti seuraavaa kulmaa
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Jos saavutetaan kohde, siirry seuraavaan kulmaan
        if ((Vector2)transform.position == targetPosition)
        {
            UpdateTargetPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        currentCorner = (currentCorner + 1) % 4; // Siirry seuraavaan kulmaan (0, 1, 2, 3, ja takaisin 0)
        switch (currentCorner)
        {
            case 0:
                targetPosition = topLeft;
                break;
            case 1:
                targetPosition = topRight;
                break;
            case 2:
                targetPosition = bottomRight;
                break;
            case 3:
                targetPosition = bottomLeft;
                break;
        }
    }
}
