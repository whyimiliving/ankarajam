using UnityEngine;

[CreateAssetMenu(fileName = "NewInputData", menuName = "ScriptableObjects/InputData")]
public class InputData : ScriptableObject
{
   private Vector2 _inputVector;
   // private bool _goFast;



   public Vector2 InputVector;
   
   public float InputVectorX
   {
      set => _inputVector.x = value;
   }

   public float InputVectorY
   {
      set => _inputVector.y = value;
   }
   
   
}
