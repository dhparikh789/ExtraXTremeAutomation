# ExtraXTremeAutomation

[![NuGet](https://img.shields.io/nuget/v/ExtraXTremeAutomation.svg)](https://www.nuget.org/packages/ExtraXTremeAutomation/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ExtraXTremeAutomation.svg)](https://www.nuget.org/packages/ExtraXTremeAutomation/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

.NET automation library for automating mainframe terminal applications using **Micro Focus Extra!**.

ExtraXTremeAutomation is a C# wrapper around the **Extra! COM Automation API** that simplifies 3270 mainframe terminal automation.

The library provides a clean API for:

- Sending data to mainframe screens
- Automating keyboard actions
- Moving cursor positions
- Reading screen content
- Validating fields and screen values
- Waiting for screen conditions
- Building reusable mainframe automation workflows


---

# Features

- Extra! COM automation wrapper
- Send text to mainframe screens
- Press Enter / Tab / Escape / Backspace
- Support PF1 - PF24 function keys
- Support PA1 - PA3 keys
- Move cursor by row and column
- Read mainframe fields
- Read screen blocks
- Read complete screen text
- Screen validation
- Wait for screen changes
- Custom automation exceptions


---

# Supported Terminal

Currently supported:

- Micro Focus Extra!

Communication is done using the:

```
EXTRA.System
```

COM automation object model.


---

# Requirements

Before using this library:

- Windows Operating System
- Micro Focus Extra! installed
- Extra! COM Automation API available
- Active mainframe session
- Configured Extra! session file (.edp)

Example:

```
C:\Path\To\Your\Session.edp
```


---

# Installation

Install from NuGet:

```
Install-Package ExtraXTremeAutomation
```

or using .NET CLI:

```bash
dotnet add package ExtraXTremeAutomation
```
---

# How It Works


The application creates an Extra! COM session and passes it to `ExtraDriver`.

`ExtraDriver` provides a simplified wrapper around the Extra! COM API.


Architecture:


```
Automation Application

          |

          |

ExtraXTremeAutomation

          |

          |

       ExtraDriver

          |

          |

    Extra! COM API

    EXTRA.System

          |

          |

    Micro Focus Extra!

          |

          |

     Mainframe Host
```


Your automation code interacts only with `ExtraDriver`.

The library handles COM communication with Extra! internally and exposes a simplified automation API.


# Multi-Thread / Parallel Execution Support

`ExtraXTremeAutomation` supports parallel execution.

For running multiple automation flows at the same time, each thread should use its own:

- Extra! session
- ExtraDriver instance

Do not share the same `ExtraDriver` instance between multiple threads.
For running automation in multiple threads, the consuming framework can use:
- `ThreadLocal<ExtraDriver>` to maintain a separate driver instance per thread.
- `Parallel.For` to execute multiple automation flows in parallel.

---

# Complete Example


```csharp
using ExtraXTremeAutomation;
using System;

class Program
{
    static void Main(string[] args)
    {
		dynamic mfDriver = InitializeSession();
		var driver = new ExtraDriver(mfDriver);

        driver.SendKeys("username");
        driver.MoveTo(6,16);
        driver.SendKeys("password");
        driver.EnterButton(1);

        if(driver.ScreenContains(
            "WELCOME"))
        {
            Console.WriteLine(
                "Login Successful");
        }
    }

    public static dynamic InitializeSession()
	{
		try
		{
			dynamic mfDriver = null;
			Type systemType = Type.GetTypeFromProgID("EXTRA.System");
			dynamic system = Activator.CreateInstance(systemType);

			if (system == null)
			{
				Console.WriteLine("EXTRA.System not found");
				return mfDriver;
			}

			dynamic sessions = system.Sessions;

			string sessionPath = $@"C:\Micro Focus\Extra!\Sessions\Session1.edp";
			mfDriver = sessions.Open(sessionPath);
			mfDriver.WindowState = 2;  // 0 = minimized, 2 = maximized

			return mfDriver;
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			throw;
		}
	}
}
```
## Mainframe Keyboard and Screen Automation Method Documentation

This section describes each automation method, including its purpose, parameters, default values, and expected behavior.

### `SendKeys(string data)`
**Purpose:** Sends text to the current cursor position using the mainframe screen SendKeys API.

- `data` - Text to type.
- Empty or `null` values are ignored.
- Throws `ExtraAutomationException` if sending fails.

---

### `TypeSlowly(string text, int delayMilliseconds = 100)`
**Purpose:** Types text character by character with a delay between each character.

- `text` - Text to type.
- `delayMilliseconds` - Delay after each character. Default: `100 ms`.
- Useful when applications require slower typing speed.

---

### `TabButton(int numberOfTimes = 1)`
**Purpose:** Presses the TAB key one or more times.

- `numberOfTimes` - Number of TAB presses. Default: `1`.
- Must be greater than zero.
- Adds `100 ms` delay between presses.

---

### `EnterButton(int numberOfTimes = 1)`
**Purpose:** Presses ENTER key multiple times.

- `numberOfTimes` - Number of ENTER presses. Default: `1`.
- Adds `500 ms` delay between presses.

---

### `Backspace(int numberOfTimes = 1)`
**Purpose:** Presses BACKSPACE multiple times.

- `numberOfTimes` - Number of presses. Default: `1`.

---

### `PressFunctionKey(int keyNumber, int numberOfTimes = 1)`
**Purpose:** Presses a PF function key from PF1 to PF24.

- `keyNumber` - PF key number (`1-24`).
- `numberOfTimes` - Number of presses. Default: `1`.
- Adds a 2-second delay between presses.

---

### `BackTabButton(int numberOfTimes = 1)`
**Purpose:** Presses BACK TAB key.

- `numberOfTimes` - Number of presses. Default: `1`.

---

### `EscapeButton()`
**Purpose:** Presses ESC key.

---

### `DeleteButton()`
**Purpose:** Deletes the character at the current cursor position.

---

### `InsertButton()`
**Purpose:** Activates INSERT mode.

---

### `HomeButton()`
**Purpose:** Moves cursor to HOME position.

---

### `EndButton()`
**Purpose:** Moves cursor to END position.

---

### `ArrowUp()`
**Purpose:** Moves cursor up by one position.

---

### `ArrowDown()`
**Purpose:** Moves cursor down by one position.

---

### `ArrowLeft()`
**Purpose:** Moves cursor left by one position.

---

### `ArrowRight()`
**Purpose:** Moves cursor right by one position.

---

### `MoveTo(int row, int column)`
**Purpose:** Moves cursor to a specific screen location.

- `row` - Screen row.
- `column` - Screen column.
- Both values must be greater than zero.

---

### `GetStringFromTheField(int row, int column, int length)`
**Purpose:** Reads text from a specific screen area.

- `row` - Starting row.
- `column` - Starting column.
- `length` - Number of characters to read.

---

### `GetStringFromTheBlock(int startRow, int startCol, int endRow, int endCol)`
**Purpose:** Reads a rectangular area from the screen.

- Returns combined text separated by new lines.

---

### `GetScreenText()`
**Purpose:** Returns the complete screen text.

- Reads every row and column from the current screen.

---

### `IsTextPresentOnScreen(string text)`
**Purpose:** Checks if text exists anywhere on the screen.

- Returns `true` if found.

---

### `IsAnyMessagePresentOnScreen(string[] messages)`
**Purpose:** Checks whether any message from a list exists on screen.

- `messages` - Array of messages to search.

---

### `IsFieldValuePresent(row, column, length, expectedValue)`
**Purpose:** Checks whether a field contains an expected value.

- Reads the field and compares trimmed values ignoring case.

---

### `IsFieldEmpty(row, column, length)`
**Purpose:** Checks whether a field is empty.

- Returns `true` if the field contains only spaces or no text.

---

### `IsCursorAtPosition(row, column)`
**Purpose:** Checks cursor location.

- Returns `true` if cursor matches the provided row and column.

---

### `IsScreenReady()`
**Purpose:** Checks whether the screen can be read successfully.

- Returns `true` when screen text is available.

---

### `IsSessionConnected()`
**Purpose:** Checks if driver and screen objects exist.

- Returns `false` if session objects are unavailable.

---

### `IsSessionOpen()`
**Purpose:** Checks if mainframe session object exists.

- Returns `false` when the session is closed.

---

### `WaitForTextOnScreen(string text, int timeoutSeconds = 30)`
**Purpose:** Waits until text appears on the screen.

- `text` - Text to wait for.
- `timeoutSeconds` - Maximum wait time. Default: `30 seconds`.
- Checks every `500 ms`.

---

### `WaitForFieldText(row, column, length, expectedValue, timeoutSeconds = 30)`
**Purpose:** Waits until a screen field contains the expected value.

- Reads the field repeatedly until the value matches or timeout occurs.
- Default timeout: `30 seconds`.
- Checks every `500 ms`.

---

### `WriteAt(row, column, value)`
**Purpose:** Moves cursor and writes text.

- `row` / `column` - Target position.
- `value` - Text to write.

---

### `ClearField(row, column, length)`
**Purpose:** Clears a field by writing spaces.

- `length` determines the number of spaces written.

---

### `VerifyFieldText(row, column, expected)`
**Purpose:** Verifies text at a location.

- Reads text equal to expected length and compares values.

---

### `SendKeysAndEnter(string value)`
**Purpose:** Sends text and presses ENTER.

- Useful for entering commands or submitting forms.

---

### `GetCursorPosition()`
**Purpose:** Returns current cursor row and column.

**Returns:** `(Row, Column)`

---

### `PressPAKey(int keyNumber)`
**Purpose:** Presses PA1, PA2, or PA3.

- `keyNumber` must be `1`, `2`, or `3`.

---

### `CloseSession()`
**Purpose:** Closes the mainframe session.

---

### `Rows()`
**Purpose:** Returns screen row count.

---

### `Columns()`
**Purpose:** Returns screen column count.

---

## General Error Handling

Most methods catch exceptions and wrap them inside `ExtraAutomationException`, providing additional context such as method name and parameter values.
```

---

# Execution flow:

```
Application Code

        |

        |

driver.SendKeys("username")

        |

        |

ExtraDriver.SendKeys()

        |

        |

_mainframeDriver.Screen.SendKeys()

        |

        |

Extra! COM API

        |

        |

Micro Focus Extra!

        |

        |

Mainframe Screen

```


---

# Disclaimer

ExtraXTremeAutomation does not include or distribute Micro Focus Extra!.

A valid installation and license of Extra! is required.

This library communicates with Extra! through its COM Automation API.


# License

This project is open source and licensed under the MIT License.

Copyright © 2026 Dhaval Parikh

You are free to use, modify, and distribute this library according to the terms of the MIT License.


# Author

Created and maintained by Dhaval Parikh
