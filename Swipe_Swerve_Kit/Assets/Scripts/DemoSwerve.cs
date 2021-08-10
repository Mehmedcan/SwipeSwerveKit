using UnityEngine;
using Listeners;

public class DemoSwerve : MonoBehaviour
{
    [SerializeField] private SwerveListener swerveListener;
    public Transform target;
    [SerializeField] float speed;

    private void OnEnable()
    {
        swerveListener.RunFunction += Swerve;
    }

    private void OnDisable()
    {
        swerveListener.RunFunction -= Swerve;
    }

    private void Swerve(float xSwerveValue, float ySwerveValue)
    {
        target.transform.Translate(xSwerveValue * transform.right * speed * Time.deltaTime);
        target.transform.Translate(ySwerveValue * transform.forward * speed * Time.deltaTime);
    }
}