/**
 * WiredIn - Visual Reminder of Suspended Tasks
 *
 * The MIT License (MIT)
 * Copyright (c) 2012 Yikun Liu, https://github.com/yikliu
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in the
 * Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

namespace WiredIn.UI.RadioButtonTile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class RadioButtonTileSubject.
    /// </summary>
    public class RadioButtonTileSubject : Subject
    {
        #region Fields

        /// <summary>
        /// The selected
        /// </summary>
        private Dictionary<string, bool> selected;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonTileSubject"/> class.
        /// </summary>
        public RadioButtonTileSubject()
        {
            this.selected = new Dictionary<string, bool>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the selected key.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetSelectedKey()
        {
            string[] ks = selected.Keys.ToArray();
            foreach (string k in ks)
            {
                if (selected[k])
                {
                    return k;
                }
            }
            return null;
        }

        /// <summary>
        /// Notifies this instance.
        /// </summary>
        public override void notify()
        {
            foreach (string k in observers.Keys)
            {
                observers[k].Update(selected[k]);
            }
        }

        /// <summary>
        /// Registers the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="o">The o.</param>
        public override void Register(string key, Observer o)
        {
            base.Register(key, o);
            selected.Add(key, false);
        }

        /// <summary>
        /// Tiles the selected.
        /// </summary>
        /// <param name="key">The key.</param>
        public void TileSelected(String key)
        {
            string[] ks = selected.Keys.ToArray();
            foreach (string k in ks)
            {
                selected[k] = false;
            }
            selected[key] = true;
            notify();
        }

        /// <summary>
        /// Unregisters all.
        /// </summary>
        public override void UnregisterAll()
        {
            base.UnregisterAll();
            selected.Clear();
        }

        #endregion Methods
    }
}