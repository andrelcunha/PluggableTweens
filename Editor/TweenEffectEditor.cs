using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace _2MuchPines.PlugglableTweens
{
    [CustomEditor(typeof(TweenEffect))]

    public class TweenEffectEditor : Editor
    {
        private TweenEffect tweenEffect;

        private void OnEnable(){
            tweenEffect = (TweenEffect)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.LabelField("Tweeners settings", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            tweenEffect.doMove = GUILayout.Toggle(tweenEffect.doMove, "Use DOMove Tweener");
            tweenEffect.doRotate = GUILayout.Toggle(tweenEffect.doRotate, "Use DORotate Tweener");
            tweenEffect.doScale = GUILayout.Toggle(tweenEffect.doScale, "Use DOScale Tweener");
			tweenEffect.doFadeAlpha = GUILayout.Toggle(tweenEffect.doFadeAlpha, "Use DOFadeAlpha Tweener");

            using (new EditorGUI.DisabledScope(!(tweenEffect.doScale && tweenEffect.doMove)))
                tweenEffect.isSimultaneos = GUILayout.Toggle(tweenEffect.isSimultaneos, "Use Simultaneosly");

            #region doMove

            using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(tweenEffect.doMove)))
            {
                if (group.visible)
                {
                    EditorGUILayout.LabelField("DoMove Settings", EditorStyles.boldLabel);
                    EditorGUI.indentLevel++;
                    tweenEffect.initialPos = EditorGUILayout.Vector3Field("Initial Position", tweenEffect.initialPos);
                    tweenEffect.finalPos = EditorGUILayout.Vector3Field("Final Position", tweenEffect.finalPos);
                    //EditorGUILayout.PrefixLabel("Duration");
                    //tweenEffect.moveDuration = EditorGUILayout.Slider(tweenEffect.moveDuration, 0f, 5f);
                    tweenEffect.moveDuration = EditorGUILayout.Slider("Duration", tweenEffect.moveDuration, 0f, 5f);

                    EditorGUI.indentLevel--;
                }    
            }
            #endregion

            #region doRotate

            using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(tweenEffect.doRotate)))
            {
                if (group.visible)
                {
                    EditorGUILayout.LabelField("DoRotate Settings", EditorStyles.boldLabel);
                    EditorGUI.indentLevel++;
                    tweenEffect.initialAngle = EditorGUILayout.Vector3Field("Initial Angle", tweenEffect.initialAngle);
                    tweenEffect.finalAngle = EditorGUILayout.Vector3Field("Final Angle", tweenEffect.finalAngle);
                    //EditorGUILayout.PrefixLabel("Duration");
                    tweenEffect.rotateDuration = EditorGUILayout.Slider("Duration", tweenEffect.rotateDuration, 0f, 5f);

                    EditorGUI.indentLevel--;
                }
            }
            #endregion

            #region doScale
            if (tweenEffect.doScale)
            {
                EditorGUILayout.LabelField("DoScale Settings", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                tweenEffect.initialScale = EditorGUILayout.Vector3Field("Initial Scale", tweenEffect.initialScale);
                tweenEffect.finalScale = EditorGUILayout.Vector3Field("Final Scale", tweenEffect.finalScale);
                if (!tweenEffect.isSimultaneos)
                    tweenEffect.scaleDuration = EditorGUILayout.Slider("Duration", tweenEffect.scaleDuration, 0f, 5f);
                EditorGUI.indentLevel--;
            }
            #endregion

			#region doFadeAlpha
			if (tweenEffect.doFadeAlpha)
            {
                EditorGUILayout.LabelField("DoFadeAlpha Settings", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
				tweenEffect.initialAlpha = EditorGUILayout.Slider("Initial Alpha", tweenEffect.initialAlpha, 0f, 1f);
				tweenEffect.finalAlpha = EditorGUILayout.Slider("Final Alpha", tweenEffect.finalAlpha, 0f, 1f);

                if (!tweenEffect.isSimultaneos)
					tweenEffect.fadeDuration = EditorGUILayout.Slider("Duration", tweenEffect.fadeDuration, 0f, 5f);
                EditorGUI.indentLevel--;
            }
            #endregion

            EditorGUI.indentLevel--;

        }
    }
}
