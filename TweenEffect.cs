//
//  TweenEffect.cs
//
//  Author:
//       Andre Luis da Cunha <andreluiscunha81@gmail.com>
//
//  Copyright (c) 2017 Andre Luis da Cunha, 2MuchPines Studio.
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace _2MuchPines.PlugglableTweens
{
    /*[System.Serializable]
    public class MyintEvent : UnityEvent<int>
    {
    }*/


    //[HelpURL("")]
    [AddComponentMenu("2MuchPines/TweenEffect")]
    public class TweenEffect : MonoBehaviour
    {

        [Header("Tweener Curve")]
        [Tooltip("Ease Function")]
        [SerializeField]
        Ease ease = Ease.Linear;

        [ConditionalField("ease", Ease.Unset)]
        [SerializeField] Curve curve;

		[SerializeField] float _delayInSecs;

        [Tooltip("Play on Awake")]
        [SerializeField]
        bool playOnAwake;

        //[Tooltip("If true, the tweeners will be played simultaneosly")]
        [HideInInspector]
        public bool isSimultaneos;

		[Header("Tweener callbacks")]

		public bool _delayedOnPlay;
        [Tooltip("Function called on normal play")]
        public UnityEvent[] onPlay;

		public bool _delayedAfterPlay;
        [Tooltip("Function called after normal play")]
		public UnityEvent[] afterPlay;

		public bool _delayedOnRewind;
        [Tooltip("Function called on rewind")]
		public UnityEvent[] onRewind;
        
		public bool _delayedAfterRewind;
		[Tooltip("Function called after rewind")]
		public UnityEvent[] afterRewind;
        
        //[Header("Tween Move")]
        //[Tooltip("Use Move Tweener")]
        [HideInInspector]
        public bool doMove;
        //[Tooltip("Initial Position")]
        [HideInInspector]
        public Vector3 initialPos;
        //[Tooltip("Final Position")]
        [HideInInspector]
        public Vector3 finalPos;
        //[Tooltip("Duration")]
        [HideInInspector]
        public float moveDuration;

        //[Header("Tween Rotate")]
        //[Tooltip("Use Rotation Tweener")]
        [HideInInspector]
        public bool doRotate;
        //[Tooltip("Initial Totation")]
        [HideInInspector]
        public Vector3 initialAngle;
        //[Tooltip("Final Rotation")]
        [HideInInspector]
        public Vector3 finalAngle;
        //[Tooltip("Duration")]
        [HideInInspector]
        public float rotateDuration;

        //[Header("Tween Scale")]
        //[Tooltip("Use Scale Tweener")]
        [HideInInspector]
        public bool doScale;
        //[Tooltip("")]
        [HideInInspector]
        public Vector3 initialScale;
        //[Tooltip("")]
        [HideInInspector]
        public Vector3 finalScale;
        //[Tooltip("Duration")]
        [HideInInspector]
        public float scaleDuration;

		//[Header("Tween Alpha")]
        //[Tooltip("Use Alpha Tweener")]
        [HideInInspector]
        public bool doFadeAlpha;
        //[Tooltip("")]
        [HideInInspector]
        public float initialAlpha;
        //[Tooltip("")]
        [HideInInspector]
        public float finalAlpha;
        //[Tooltip("Duration")]
        [HideInInspector]
        public float fadeDuration;
        
		private CanvasRenderer _renderer;


        bool _isRewind;
        int _index;

        #region MonoBehavior_Methods
		void Start()
        {
            if (doMove)
                transform.localPosition = initialPos;
            if (doScale)
                transform.localScale = initialScale;
			if (doScale)
                transform.localScale = initialScale;
			if(doFadeAlpha)
			{
				_renderer = GetComponent<CanvasRenderer>();
				if (_renderer != null)
				{
					_renderer.SetAlpha (initialAlpha);
				}
				else
                {
                    doFadeAlpha = false;
                }            
			}

            if (playOnAwake)
            {
                StartTween();
            }
        }
		#endregion

        #region Public_Members
		/// <summary>
		/// Starts the tween.
		/// </summary>
		public void StartTween(int index=0)
        {
			ExecuteTween(index, _delayedOnPlay, _delayedAfterPlay);    
        }

        /// <summary>
        /// Rewinds the tween.
        /// </summary>
        public void RewindTween(int index=0)
        {
            _isRewind = true;
			//SwapInitialFinalValues();

			ExecuteTween(index, _delayedOnRewind, _delayedAfterRewind);
			//SwapInitialFinalValues();
        }

		public void SetDelay(float secs)
        {
            _delayInSecs = secs;
        }
		#endregion

        #region Private_Members
		void ExecuteTween(int index, bool useDelay, bool delayAfter)
		{
			_index = index;
            Sequence seq = DOTween.Sequence();
            if (doMove)
            {
                var tweener = transform.DOLocalMove(finalPos, moveDuration);
                if (ease == Ease.Unset)
                    tweener.SetEase(curve.curve);
                else
                    tweener.SetEase(ease);
                seq.Append(tweener);
            }

            if (doRotate)
            {
                var tweener = transform.DORotate(finalAngle, rotateDuration);
                if (ease == Ease.Unset)
                    tweener.SetEase(curve.curve);
                else
                    tweener.SetEase(ease);
                seq.Append(tweener);
            }

            if (doScale)
            {
				var tweener = transform.DOScale(finalScale, scaleDuration);
                if (ease == Ease.Unset)
                    tweener.SetEase(curve.curve);
                else
                    tweener.SetEase(ease);
                if (isSimultaneos && doMove)
                {
                    seq.Join(tweener);
                }
                else
                {
                    seq.Append(tweener);
                }
            }

            if (doFadeAlpha)
            {

                var tweener = DOTween.To(_renderer.GetAlpha, _renderer.SetAlpha, finalAlpha, fadeDuration);
                if (ease == Ease.Unset)
                    tweener.SetEase(curve.curve);
                else
                    tweener.SetEase(ease);
                if (isSimultaneos && doMove && doScale)
                {
                    seq.Join(tweener);
                }
                else
                {
                    seq.Append(tweener);
                }
            }
            if (doMove || doRotate || doScale || doFadeAlpha)
            {
                seq.PrependCallback(PlayAlongCallBack);
            }
			if (useDelay)
            {
                seq.PrependInterval(_delayInSecs);
            }
			if(delayAfter)
			{
				seq.AppendInterval(_delayInSecs);
			}
            seq.AppendCallback(PlayAfterCallBack);
			seq.AppendCallback(SwapInitialFinalValues);
		}

        /// <summary>
        /// Plays the after call back.
        /// </summary>
        void PlayAfterCallBack()
        {
            //Debug.Log("Play after");
            if (_isRewind)
            {
				//Debug.Log("rewind");
				if ( afterRewind.Length == 0)
                {
                    //Debug.Log("No callback to execute.");
                }
                else if (_index < afterRewind.Length)
                {
                    afterRewind[_index].Invoke();               
                }
                else
                {
                    Debug.LogWarning("Array index out of bounds: " + _index.ToString());                    
                }
				_isRewind = false;
            }
            else
            {
                if (afterPlay.Length == 0)
                {
                    //Debug.Log("No callback to execute.");
                    return;
                }
                else if ((_index < afterPlay.Length))
                {
                    afterPlay[_index].Invoke();
                    return;
                }
                else
                {
                    Debug.LogWarning("Array index out of bounds: " + _index.ToString());

                }
            }
        }

        void PlayAlongCallBack()
        {
            //Debug.Log("Play along");
            if (_isRewind)
            {
                if (onRewind.Length == 0)
                {
                    //Debug.Log("No callback to execute.");
                    return;
                }
                else if (_index < onRewind.Length)
                {
                    onRewind[_index].Invoke();
                    return;
                }
                else
                {
                    Debug.LogWarning("Array index out of bounds: " + _index.ToString());
                }
				_isRewind = false;
            }
            else
            {
                if (onPlay.Length == 0)
                {
                    //Debug.Log("No callback to execute.");
                    return;
                }
                else if (_index < onPlay.Length)
                {
                    onPlay[_index].Invoke();
                    return;
                }
                else
                {
                    Debug.LogWarning("Array index out of bounds: " + _index.ToString());
                }
            }
        }

        void OnSucess(){
            RewindTween();
        }

        void SwapInitialFinalValues()
		{
			Vector3 tmp = finalPos;
            finalPos = initialPos;
            initialPos = tmp;

            tmp = finalScale;
            finalScale = initialScale;
            initialScale = tmp;

			float tmp1 = finalAlpha;
			finalAlpha = initialAlpha;
			initialAlpha = tmp1;
		}
        #endregion        
    }
}