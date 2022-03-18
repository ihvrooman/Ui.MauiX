using DataCollections.Models;
using Ui.Enums;
using Ui.MauiX.CustomControls.ViewModels;
using Ui.MauiX.Helpers;
using Ui.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ui.MauiX.CustomControls
{
    /// <summary>
    /// An on-screen keyboard.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KeyboardPopup : BaseInputControl
    {
        #region Fields
        #region Button string fields
        private List<List<List<string>>> _buttonTextAndImageSourceMatrix = new List<List<List<string>>>()
        {
            //Row0
            new List<List<string>>()
            {
                new List<string>()
                {
                        "Q",
                        "q",
                        "1",
                        "Q",
                        "$",
                },
                new List<string>()
                {
                        "W",
                        "w",
                        "2",
                        "W",
                        "€",
                },
                new List<string>()
                {
                        "E",
                        "e",
                        "3",
                        "E",
                        "£",
                },
                new List<string>()
                {
                        "R",
                        "r",
                        "4",
                        "R",
                        "á",
                },
                new List<string>()
                {
                        "T",
                        "t",
                        "5",
                        "T",
                        "â",
                },
                new List<string>()
                {
                        "Y",
                        "y",
                        "6",
                        "Y",
                        "ã",
                },
                new List<string>()
                {
                        "U",
                        "u",
                        "7",
                        "U",
                        "è",
                },
                new List<string>()
                {
                        "I",
                        "i",
                        "8",
                        "I",
                        "é",
                },
                new List<string>()
                {
                        "O",
                        "o",
                        "9",
                        "O",
                        "•",
                },
                new List<string>()
                {
                        "P",
                        "p",
                        "0",
                        "P",
                        "©",
                },
            },
            
            //Row1
            new List<List<string>>()
            {
                new List<string>()
                {
                        "A",
                        "a",
                        "_",
                        "A",
                        "®",
                },
                new List<string>()
                {
                        "S",
                        "s",
                        "-",
                        "S",
                        "±",
                },
                new List<string>()
                {
                        "D",
                        "d",
                        "+",
                        "D",
                        "~",
                },
                new List<string>()
                {
                        "F",
                        "f",
                        "/",
                        "F",
                        "\\",
                },
                new List<string>()
                {
                        "G",
                        "g",
                        "*",
                        "G",
                        "`",
                },
                new List<string>()
                {
                        "H",
                        "h",
                        "%",
                        "H",
                        "|",
                },
                new List<string>()
                {
                        "J",
                        "j",
                        "&",
                        "J",
                        "'",
                },
                new List<string>()
                {
                        "K",
                        "k",
                        "(",
                        "K",
                        "{",
                },
                new List<string>()
                {
                        "L",
                        "l",
                        ")",
                        "L",
                        "}",
                },
            },
            
            //Row2
            new List<List<string>>()
            {
                //Special shift button for numbers and special characters pages
                new List<string>()
                {
                        "",
                        "",
                        "#$=",
                        "",
                        "123",
                },
                //Regular shift button
                new List<string>()
                {
                        "shift-filled.png",
                        "shift.png",
                        "",
                        "caps-lock-on-50.png",
                        "",
                },
                new List<string>()
                {
                        "Z",
                        "z",
                        ".",
                        "Z",
                        "<",
                },
                new List<string>()
                {
                        "X",
                        "x",
                        ",",
                        "X",
                        ">",
                },
                new List<string>()
                {
                        "C",
                        "c",
                        "?",
                        "C",
                        "=",
                },
                new List<string>()
                {
                        "V",
                        "v",
                        "!",
                        "V",
                        "^",
                },
                new List<string>()
                {
                        "B",
                        "b",
                        ":",
                        "B",
                        "#",
                },
                new List<string>()
                {
                        "N",
                        "n",
                        ";",
                        "N",
                        "@",
                },
                new List<string>()
                {
                        "M",
                        "m",
                        "°",
                        "M",
                        "\"",
                },
            },
            
            //Row3
            new List<List<string>>()
            {
                //Button for toggling between alphabetic and numeric/special characters
                new List<string>()
                {
                        "123",
                        "123",
                        "ABC",
                        "123",
                        "ABC",
                },
            },
        };

        #region Row0
        private string _row0Button0String;
        private string _row0Button1String;
        private string _row0Button2String;
        private string _row0Button3String;
        private string _row0Button4String;
        private string _row0Button5String;
        private string _row0Button6String;
        private string _row0Button7String;
        private string _row0Button8String;
        private string _row0Button9String;
        #endregion

        #region Row1
        private string _row1Button0String;
        private string _row1Button1String;
        private string _row1Button2String;
        private string _row1Button3String;
        private string _row1Button4String;
        private string _row1Button5String;
        private string _row1Button6String;
        private string _row1Button7String;
        private string _row1Button8String;
        #endregion

        #region Row2
        private string _row2Button0String;
        private string _row2Button1String;
        private string _row2Button2String;
        private string _row2Button3String;
        private string _row2Button4String;
        private string _row2Button5String;
        private string _row2Button6String;
        private string _row2Button7String;
        private string _row2Button8String;
        #endregion

        #region Row3
        private string _row3Button0String;
        #endregion
        #endregion

        private Command _shiftCommand;
        private Command _toggleNumbersCommand;
        private Command _appendSpaceToValueCommand;
        private KeyboardMode _keyboardMode = KeyboardMode.Uppercase;
        private bool _isKeyboardShowingAlphabeticCharacters = true;
        private int _previousLengthOfValue;
        private List<string> _alphabetCharacters = new List<string>()
        {
            "a",
            "b",
            "c",
            "d",
            "e",
            "f",
            "g",
            "h",
            "i",
            "j",
            "k",
            "l",
            "m",
            "n",
            "o",
            "p",
            "q",
            "r",
            "s",
            "t",
            "u",
            "v",
            "w",
            "x",
            "y",
            "z",
        };
        private DateTime _timeShiftWasLastPressed = DateTime.MinValue;
        private DateTime _timeSpaceWasLastPressed = DateTime.MinValue;
        private bool _restoreCapsLock;
        private bool _restoreUppercase;
        #endregion

        #region Properties

        #region Button string properties
        #region Row0
        public string Row0Button0String { get => _row0Button0String; set => SetProperty(ref _row0Button0String, value); }

        public string Row0Button1String { get => _row0Button1String; set => SetProperty(ref _row0Button1String, value); }

        public string Row0Button2String { get => _row0Button2String; set => SetProperty(ref _row0Button2String, value); }

        public string Row0Button3String { get => _row0Button3String; set => SetProperty(ref _row0Button3String, value); }

        public string Row0Button4String { get => _row0Button4String; set => SetProperty(ref _row0Button4String, value); }

        public string Row0Button5String { get => _row0Button5String; set => SetProperty(ref _row0Button5String, value); }

        public string Row0Button6String { get => _row0Button6String; set => SetProperty(ref _row0Button6String, value); }

        public string Row0Button7String { get => _row0Button7String; set => SetProperty(ref _row0Button7String, value); }

        public string Row0Button8String { get => _row0Button8String; set => SetProperty(ref _row0Button8String, value); }

        public string Row0Button9String { get => _row0Button9String; set => SetProperty(ref _row0Button9String, value); }
        #endregion

        #region Row1
        public string Row1Button0String { get => _row1Button0String; set => SetProperty(ref _row1Button0String, value); }

        public string Row1Button1String { get => _row1Button1String; set => SetProperty(ref _row1Button1String, value); }

        public string Row1Button2String { get => _row1Button2String; set => SetProperty(ref _row1Button2String, value); }

        public string Row1Button3String { get => _row1Button3String; set => SetProperty(ref _row1Button3String, value); }

        public string Row1Button4String { get => _row1Button4String; set => SetProperty(ref _row1Button4String, value); }

        public string Row1Button5String { get => _row1Button5String; set => SetProperty(ref _row1Button5String, value); }

        public string Row1Button6String { get => _row1Button6String; set => SetProperty(ref _row1Button6String, value); }

        public string Row1Button7String { get => _row1Button7String; set => SetProperty(ref _row1Button7String, value); }

        public string Row1Button8String { get => _row1Button8String; set => SetProperty(ref _row1Button8String, value); }
        #endregion

        #region Row2
        public string Row2Button0String { get => _row2Button0String; set => SetProperty(ref _row2Button0String, value); }

        public string Row2Button1String { get => _row2Button1String; set => SetProperty(ref _row2Button1String, value); }

        public string Row2Button2String { get => _row2Button2String; set => SetProperty(ref _row2Button2String, value); }

        public string Row2Button3String { get => _row2Button3String; set => SetProperty(ref _row2Button3String, value); }

        public string Row2Button4String { get => _row2Button4String; set => SetProperty(ref _row2Button4String, value); }

        public string Row2Button5String { get => _row2Button5String; set => SetProperty(ref _row2Button5String, value); }

        public string Row2Button6String { get => _row2Button6String; set => SetProperty(ref _row2Button6String, value); }

        public string Row2Button7String { get => _row2Button7String; set => SetProperty(ref _row2Button7String, value); }

        public string Row2Button8String { get => _row2Button8String; set => SetProperty(ref _row2Button8String, value); }
        #endregion

        #region Row3
        public string Row3Button0String { get => _row3Button0String; set => SetProperty(ref _row3Button0String, value); }
        #endregion
        #endregion

        public Command ShiftCommand
        {
            get
            {
                if (_shiftCommand == null)
                {
                    _shiftCommand = new Command(Shift);
                }
                return _shiftCommand;
            }
        }

        public Command ToggleNumbersCommand
        {
            get
            {
                if (_toggleNumbersCommand == null)
                {
                    _toggleNumbersCommand = new Command(ToggleNumbers);
                }
                return _toggleNumbersCommand;
            }
        }

        public Command AppendSpaceToValueCommand
        {
            get
            {
                if (_appendSpaceToValueCommand == null)
                {
                    _appendSpaceToValueCommand = new Command(AppendSpaceToValue);
                }
                return _appendSpaceToValueCommand;
            }
        }

        public override object Value
        {
            get => StringDataItem.Value;
            set
            {
                if (value == null)
                {
                    value = ClearedValue;
                }

                var valueString = (string)value;

                if (Value == value || !StringDataItem.CharacterValid(valueString))
                {
                    return;
                }

                StringDataItem.Value = valueString;
                OnPropertyChanged();
                AdjustKeyboardMode();
            }
        }

        public StringDataItem StringDataItem => (StringDataItem)DataItem;

        public KeyboardMode KeyboardMode
        {
            get => _keyboardMode;
            private set
            {
                if (!SetProperty(ref _keyboardMode, value))
                {
                    return;
                }
                SetButtonStrings();
                IsKeyboardShowingAlphabeticCharacters = KeyboardMode != KeyboardMode.NumbersAndSpecialCharacters && KeyboardMode != KeyboardMode.AdditionalSpecialCharacters;
                if (KeyboardMode == KeyboardMode.CapsLock)
                {
                    _restoreCapsLock = false;
                }
                else if (KeyboardMode == KeyboardMode.Uppercase)
                {
                    _restoreUppercase = false;
                }
            }
        }

        public bool IsKeyboardShowingAlphabeticCharacters { get => _isKeyboardShowingAlphabeticCharacters; set => SetProperty(ref _isKeyboardShowingAlphabeticCharacters, value); }

        internal override string ClearedValue => string.Empty;

        public static Thickness DefaultPadding { get; set; } = new Thickness(0, 50, 0, 0);

        public new Thickness Padding => DefaultPadding;

        public override CornerRadius CornerRadius => new CornerRadius(5, 5, 0, 0);
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new <see cref="KeyboardPopup"/>.
        /// </summary>
        /// <param name="reservedCharacters">The reserved characters constraint.</param>
        /// <param name="patternMask">The pattern mask constraint.</param>
        /// <param name="min">The minimum number of characters constraint.</param>
        /// <param name="max">The maximum number of characters constraint.</param>
        internal KeyboardPopup(string reservedCharacters, string patternMask, double? min, double? max)
        {
            DataItem = new StringDataItem();
            InitializeComponent();
            SetButtonStrings();
            var tip = $"Min: {min} | Max: {max}";

            if (reservedCharacters != null)
            {
                StringDataItem.AddConstraint(ConstraintTypes.ReservedCharacters, reservedCharacters);
            }

            if (patternMask != null)
            {
                StringDataItem.AddConstraint(ConstraintTypes.PatternMask, patternMask, priority: 1);
            }

            if (min != null)
            {
                StringDataItem.AddConstraint(ConstraintTypes.Min, min.ToString(), tip, 2);
            }

            if (max != null)
            {
                StringDataItem.AddConstraint(ConstraintTypes.Max, max.ToString(), tip, 3);
            }
        }

        /// <summary>
        /// Creates a new <see cref="KeyboardPopup"/>.
        /// </summary>
        internal KeyboardPopup(StringDataItem stringDataItem)
        {
            DataItem = stringDataItem;
            InitializeComponent();
            SetButtonStrings();
        }
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Shows a keyboard to the user to collect alpha-numeric input.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the popup is to be displayed.</param>
        /// <param name="iconResourceId">The icon resource id.</param>
        /// <param name="label">The label which describes the value that is being collected from the user.</param>
        /// <param name="initialValue">The initial value to show.</param>
        /// <param name="valueSuffix">The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para></param>
        /// <param name="reservedCharacters">The reserved characters constraint.</param>
        /// <param name="patternMask">The pattern mask constraint.</param>
        /// <param name="min">The minimum number of characters constraint.</param>
        /// <param name="max">The maximum number of characters constraint.</param>
        /// <returns></returns>
        public static async Task<PopupResult> Show(NavigableElement navigableElement, string iconResourceId = null, string label = null, string initialValue = null, string valueSuffix = null, string reservedCharacters = null, string patternMask = null, double? min = null, double? max = null)
        {
            return await Device.InvokeOnMainThreadAsync(async () =>
            {
                var popupPushed = false;
                KeyboardPopup popup = null;
                try
                {
                    popup = new KeyboardPopup(reservedCharacters, patternMask, min, max)
                    {
                        Label = label,
                        IconResourceId = iconResourceId,
                        ValueSuffix = valueSuffix,
                        Value = initialValue,
                    };
                    await navigableElement.OpenModalPopup(popup);
                    popupPushed = true;

                    await popup.WaitForResultToBeSet();

                    return popup.Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (popupPushed)
                    {
                        await navigableElement.CloseModalPopup(popup);
                    }
                }
            });
        }

        /// <summary>
        /// Shows a keyboard to the user to collect alpha-numeric input.
        /// </summary>
        /// <param name="navigableElement">The <see cref="NavigableElement"/> over which the popup is to be displayed.</param>
        /// <param name="stringDataItem">The <see cref="DataCollections.Models.StringDataItem"/>.</param>
        /// <param name="iconResourceId">The icon resource id.</param>
        /// <param name="label">The label which describes the value that is being collected from the user.</param>
        /// <param name="initialValue">The initial value to show.</param>
        /// <param name="valueSuffix">The suffix of the value.
        /// <para>The unit of measure would be a common value for this property.</para></param>
        /// <returns></returns>
        public static async Task<PopupResult> Show(NavigableElement navigableElement, StringDataItem stringDataItem, string iconResourceId = null, string label = null, string initialValue = null, string valueSuffix = null)
        {
            return await Device.InvokeOnMainThreadAsync(async () =>
            {
                var popupPushed = false;
                KeyboardPopup popup = null;
                try
                {
                    var iValue = stringDataItem.Value;
                    popup = new KeyboardPopup(stringDataItem)
                    {
                        Label = label,
                        IconResourceId = iconResourceId,
                        ValueSuffix = valueSuffix,
                        Value = initialValue,
                    };
                    await navigableElement.OpenModalPopup(popup);
                    popupPushed = true;

                    await popup.WaitForResultToBeSet();

                    if (popup.Result.UserCancelled)
                    {
                        //If the user cancels their operation, revert the value to it's original value
                        stringDataItem.Value = iValue;
                    }

                    popup.DataItem = null;

                    return popup.Result;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (popupPushed)
                    {
                        await navigableElement.CloseModalPopup(popup);
                    }
                }
            });
        }
        #endregion

        #region Private
        private void AdjustKeyboardMode()
        {
            //TODO: See if there's a better way to implement this stuff
            var characterAdded = StringValue.Length > _previousLengthOfValue;
            _previousLengthOfValue = StringValue.Length;
            if (KeyboardMode != KeyboardMode.CapsLock && (string.IsNullOrEmpty(StringValue) || StringValue.EndsWith(". ")))
            {
                /* If the user types a period followed by a space
                 * or if user deletes all text, switch to uppercase mode 
                 * to auto-capitalize the first word of the next sentence
                 * as long as they caps lock isn't on.
                 */
                KeyboardMode = _restoreCapsLock ? KeyboardMode.CapsLock : KeyboardMode.Uppercase;
            }
            else if (KeyboardMode == KeyboardMode.NumbersAndSpecialCharacters && characterAdded && Value != null && StringValue.Length > 1)
            {
                var secondToLastCharacter = StringValue.Substring(StringValue.Length - 2, 2).ToLower();
                if ((StringValue.EndsWith(" ") && !_alphabetCharacters.Contains(secondToLastCharacter)) || StringValue.EndsWith("'"))
                {
                    /* If user types special character or a number followed by 
                     * a space OR if user types an apostrophe,
                     * switch to alphabet keyboard mode.
                     */
                    KeyboardMode = _restoreCapsLock ? KeyboardMode.CapsLock : KeyboardMode.Lowercase;
                }
            }
            else if (KeyboardMode == KeyboardMode.Uppercase)
            {
                /* Once user types a single character in uppercase, revert back 
                 * to lowercase.
                 */
                KeyboardMode = KeyboardMode.Lowercase;
            }
        }

        private void Shift(object obj)
        {
            if (KeyboardMode != KeyboardMode.NumbersAndSpecialCharacters && KeyboardMode != KeyboardMode.AdditionalSpecialCharacters && WasKeyDoublePressed(ref _timeShiftWasLastPressed))
            {
                // If shift was double clicked, turn on caps lock
                KeyboardMode = KeyboardMode.CapsLock;
            }
            else
            {
                switch (KeyboardMode)
                {
                    case KeyboardMode.Uppercase:
                        KeyboardMode = KeyboardMode.Lowercase;
                        break;
                    case KeyboardMode.Lowercase:
                        KeyboardMode = KeyboardMode.Uppercase;
                        break;
                    case KeyboardMode.CapsLock:
                        KeyboardMode = KeyboardMode.Lowercase;
                        break;
                    case KeyboardMode.NumbersAndSpecialCharacters:
                        KeyboardMode = KeyboardMode.AdditionalSpecialCharacters;
                        break;
                    case KeyboardMode.AdditionalSpecialCharacters:
                        KeyboardMode = KeyboardMode.NumbersAndSpecialCharacters;
                        break;
                    default:
                        break;
                }
            }
        }

        private void ToggleNumbers(object obj)
        {
            if (KeyboardMode != KeyboardMode.NumbersAndSpecialCharacters && KeyboardMode != KeyboardMode.AdditionalSpecialCharacters)
            {
                if (KeyboardMode == KeyboardMode.CapsLock)
                {
                    _restoreCapsLock = true;
                }
                else if (KeyboardMode == KeyboardMode.Uppercase)
                {
                    _restoreUppercase = true;
                }
                KeyboardMode = KeyboardMode.NumbersAndSpecialCharacters;
            }
            else if (_restoreUppercase || Value == null)
            {
                KeyboardMode = _restoreCapsLock ? KeyboardMode.CapsLock : KeyboardMode.Uppercase;
            }
            else
            {
                KeyboardMode = _restoreCapsLock ? KeyboardMode.CapsLock : KeyboardMode.Lowercase;
            }
        }

        private void AppendSpaceToValue(object obj)
        {
            if (WasKeyDoublePressed(ref _timeSpaceWasLastPressed) && Value != null && StringValue.Length > 1)
            {
                var secondToLastCharacter = StringValue.Substring(StringValue.Length - 2, 1).ToLower();
                if (_alphabetCharacters.Contains(secondToLastCharacter))
                {
                    /* If space was double clicked and second to last 
                     * character is in alphabet,
                     * turn the last space into a period followed by a space.
                     */
                    Value = StringValue.Substring(0, StringValue.Length - 1) + ". ";
                }
            }
            else
            {
                Value += " ";
            }
        }

        private bool WasKeyDoublePressed(ref DateTime timeKeyWasLastPressed)
        {
            var timeSinceKeyWasLastPressed = DateTime.Now - timeKeyWasLastPressed;
            timeKeyWasLastPressed = DateTime.Now;
            return timeSinceKeyWasLastPressed.TotalMilliseconds < 500;
        }

        private void SetButtonStrings()
        {
            for (int rowNumber = 0; rowNumber < _buttonTextAndImageSourceMatrix.Count; rowNumber++)
            {
                var row = _buttonTextAndImageSourceMatrix[rowNumber];
                for (int buttonNumber = 0; buttonNumber < row.Count; buttonNumber++)
                {
                    var button = row[buttonNumber];
                    var buttonString = button[(int)KeyboardMode];
                    typeof(KeyboardPopup).GetProperty($"Row{rowNumber}Button{buttonNumber}String").SetValue(this, buttonString);
                }
            }
        }
        #endregion
        #endregion
    }
}