using UnityEngine;

public class BunnyAnimator : StateMachineBehaviour
{
    // Массив с анимациями ушей
    [SerializeField]
    private AnimationClip[] earsAnimations;

    // Метод вызывается при входе в состояние
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Если есть анимации ушей
        if (earsAnimations != null && earsAnimations.Length > 0)
        {
            // Выбираем случайную анимацию из массива и применяем её
            int randomIndex = Random.Range(0, earsAnimations.Length);
            animator.Play(earsAnimations[randomIndex].name);
        }
        else
        {
            Debug.LogError("No ears animations assigned to the Idle state!");
        }
    }
}
