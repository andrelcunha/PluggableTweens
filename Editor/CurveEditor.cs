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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace _2MuchPines.PlugglableTweens
{
    /// <summary>
    /// Curve Data Model Custom Inspector
    /// </summary>
    [CustomEditor(typeof(Curve))]
    public class CurveEditor : Editor
    {

        [SerializeField] private Curve curve;

        private void OnEnable()
        {
            curve = (Curve)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (curve.curve.length == 0)
                EditorGUILayout.HelpBox("Empty Curve", MessageType.Info);

            EditorUtility.SetDirty(curve);
        }
    }
}