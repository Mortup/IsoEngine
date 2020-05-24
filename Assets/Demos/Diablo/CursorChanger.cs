﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.mortup.iso.demo.diablo {

    public class CursorChanger : MonoBehaviour {

        [SerializeField] private Texture2D cursorTexture;

        private void Start() {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }

}