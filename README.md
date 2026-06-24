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
