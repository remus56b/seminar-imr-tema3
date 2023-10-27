using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallController : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private Transform originalParent;
    public ScoreboardController scoreboardController;
    // Viteza de aruncare a obiectului
    public float throwForce = 5f;

    private void Start()
    {
        scoreboardController = FindObjectOfType<ScoreboardController>();
        Debug.Log("Hello\nWorld1111111");
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        if (grabInteractable == null || rb == null)
        {
            Debug.LogError("Componente lipsă. Asigură-te că ai XRGrabInteractable și Rigidbody pe acest obiect.");
            enabled = false;
            return;
        }

        originalParent = transform.parent;

        // Asociem funcțiile noastre la evenimentele de apucare și eliberare
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        // Salvează părintele original al obiectului și setează-l pe null pentru a permite aruncarea
        transform.parent = null;
    }

    private void CheckTarget()
    {
        GameObject militaryTarget = GameObject.FindWithTag("MilitaryTarget");

        // Verificăm dacă obiectul a fost găsit
        if (militaryTarget != null)
        {

            // Verificăm dacă distanța dintre Soccer Ball și Military Target este mică
            float distance = Vector3.Distance(transform.position, militaryTarget.transform.position);
            // Definește o valoare prag pentru a determina când considerăm că mingea a atins ținta
            Debug.Log(distance);
            float distanceThreshold = 1.10f;
            if (distance > distanceThreshold && distance < 2.8f)
            {
                
                // Atingere țintă, poți adăuga aici orice acțiuni dorite (de exemplu, adăugare puncte la scor)
                Debug.Log("Ai lovit tinta!");
                scoreboardController.UpdateScore(1);
                //score++;
                //Debug.Log("Scorul tău este acum: " + score);
            }
            RespawnBall();
        }
        else
        {
            Debug.LogError("Obiectul cu tag-ul 'MilitaryTarget' nu a fost găsit.");
        }
    }
    private void OnRelease(XRBaseInteractor interactor)
    {
        // Adaugăm forță de aruncare
        Rigidbody rb = GetComponent<Rigidbody>();

        // Verificăm dacă există o cameră principală
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 throwForce = mainCamera.transform.forward * 15.0f; // Mărește această valoare pentru o aruncare mai puternică
            rb.AddForce(throwForce, ForceMode.Impulse);

            // Resetăm părintele la cel original
            transform.parent = originalParent;
            Invoke("CheckTarget", 0.35f);
            

        }
        else
        {
            Debug.LogError("Camera principală nu a fost găsită.");
        }
    }
    private void RespawnBall()
    {
        Debug.Log("RESPAWN");
        // Respawnăm mingea la poziția inițială sau la o altă poziție dorită
        transform.position = originalParent.position;
        transform.rotation = originalParent.rotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }



}
