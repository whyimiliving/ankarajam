using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HandIKController : MonoBehaviour
{
    private Animator animator;

    [Header("Hand Targets")]
    public Transform rightHandTarget;
    public Transform leftHandTarget;

    [Header("Elbow (Dirsek) Hints")]
    public Transform rightElbowHint;
    public Transform leftElbowHint;

    [Header("IK Weights")]
    [Range(0, 1)] public float rightHandPositionWeight = 1.0f;
    [Range(0, 1)] public float rightHandRotationWeight = 1.0f;
    [Range(0, 1)] public float leftHandPositionWeight = 1.0f;
    [Range(0, 1)] public float leftHandRotationWeight = 1.0f;
    [Range(0, 1)] public float elbowHintWeight = 1.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator == null)
            return;

        // Sağ El
        if (rightHandTarget != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandPositionWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandRotationWeight);
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
        }

        // Sol El
        if (leftHandTarget != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandPositionWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandRotationWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
        }

        // Sağ Dirsek Hint
        if (rightElbowHint != null)
        {
            animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, elbowHintWeight);
            animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowHint.position);
        }

        // Sol Dirsek Hint
        if (leftElbowHint != null)
        {
            animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, elbowHintWeight);
            animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowHint.position);
        }
    }
}
