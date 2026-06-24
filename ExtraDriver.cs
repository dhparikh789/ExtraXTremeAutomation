
namespace ExtraXTremeAutomation
{
	public class ExtraDriver
	{
		private readonly dynamic _mainframeDriver;

		public ExtraDriver(dynamic mainframeDriver)
		{
			_mainframeDriver = mainframeDriver ?? throw new ArgumentNullException(nameof(mainframeDriver));
		}

		/// <summary>
		/// Sends text to the current cursor position.
		/// </summary>
		public void SendKeys(string data)
		{
			//if (string.IsNullOrEmpty(data))
			//	throw new ArgumentException("Data cannot be null or empty.", nameof(data));

			try
			{
				if (!string.IsNullOrEmpty(data))
					_mainframeDriver.Screen.SendKeys(data);
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"SendKeys failed. Data='{data}'.", ex);
			}
		}

		/// <summary>
		/// Sends text followed by ENTER.
		/// </summary>
		public void SendKeysAndEnter(string value)
		{
			if (string.IsNullOrEmpty(value))
				throw new ArgumentException("Value cannot be null or empty.", nameof(value));

			try
			{
				SendKeys(value);
				EnterButton();
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"SendKeysAndEnter failed. Value='{value}'.", ex);
			}
		}

		public void TypeSlowly(
		string text,
		int delayMilliseconds = 100)
		{
			try
			{
				foreach (char c in text)
				{
					SendKeys(c.ToString());
					Thread.Sleep(delayMilliseconds);
				}
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"TypeSlowly failed. Text={text}", ex);
			}
		}

		#region Keyboard Actions

		/// <summary>
		/// Presses TAB key one or more times.
		/// </summary>
		public void TabButton(int numberOfTimes = 1)
		{
			if (numberOfTimes <= 0)
				throw new ArgumentOutOfRangeException(nameof(numberOfTimes), "Value must be greater than zero.");

			try
			{
				for (int i = 0; i < numberOfTimes; i++)
				{
					_mainframeDriver.Screen.SendKeys("<Tab>");
					Thread.Sleep(100);
				}
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"TabButton failed. Count={numberOfTimes}.", ex);
			}
		}

		/// <summary>
		/// Presses ENTER key one or more times.
		/// </summary>
		public void EnterButton(int numberOfTimes = 1)
		{
			if (numberOfTimes <= 0)
				throw new ArgumentOutOfRangeException(nameof(numberOfTimes), "Value must be greater than zero.");

			try
			{
				for (int i = 0; i < numberOfTimes; i++)
				{
					_mainframeDriver.Screen.SendKeys("<Enter>");
					Thread.Sleep(500);
				}
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"EnterButton failed. Count={numberOfTimes}.", ex);
			}
		}

		public void Backspace(int numberOfTimes = 1)
		{
			if (numberOfTimes <= 0)
				throw new ArgumentOutOfRangeException(nameof(numberOfTimes), "Value must be greater than zero.");

			try
			{
				for (int i = 0; i < numberOfTimes; i++)
				{
					_mainframeDriver.Screen.SendKeys("<Backspace>");
				}
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"Backspace failed. Count={numberOfTimes}", ex);
			}
		}

		private static string GetPFKey(int keyNumber)
		{
			if (keyNumber < 1 || keyNumber > 24)
			{
				throw new ArgumentOutOfRangeException(nameof(keyNumber), "Function key must be between 1 and 24.");
			}

			return $"<Pf{keyNumber}>";
		}

		/// <summary>
		/// Presses a PF key (PF1-PF24).
		/// </summary>
		public void PressFunctionKey(int keyNumber, int numberOfTimes = 1)
		{
			if (numberOfTimes <= 0)
				throw new ArgumentOutOfRangeException(nameof(numberOfTimes));

			try
			{
				string key = GetPFKey(keyNumber);

				for (int i = 0; i < numberOfTimes; i++)
				{
					_mainframeDriver.Screen.SendKeys(key);

					if (i < numberOfTimes - 1)
					{
						Thread.Sleep(2000);
					}
				}
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"PressFunctionKey failed. PF{keyNumber}, Count={numberOfTimes}.", ex);
			}
		}

		/// <summary>
		/// Presses BACK TAB key.
		/// </summary>
		public void BackTabButton(int numberOfTimes = 1)
		{
			try
			{
				for (int i = 0; i < numberOfTimes; i++)
				{
					_mainframeDriver.Screen.SendKeys("<BackTab>");
					Thread.Sleep(100);
				}
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					$"BackTabButton failed. Count={numberOfTimes}.",
					ex);
			}
		}


		/// <summary>
		/// Presses ESC key.
		/// </summary>
		public void EscapeButton()
		{
			try
			{
				_mainframeDriver.Screen.SendKeys("<Esc>");
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"EscapeButton failed.",
					ex);
			}
		}

		/// <summary>
		/// Deletes character at cursor.
		/// </summary>
		public void DeleteButton()
		{
			try
			{
				_mainframeDriver.Screen.SendKeys("<Delete>");
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"DeleteButton failed.",
					ex);
			}
		}


		/// <summary>
		/// Inserts character mode.
		/// </summary>
		public void InsertButton()
		{
			try
			{
				_mainframeDriver.Screen.SendKeys("<Insert>");
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"InsertButton failed.",
					ex);
			}
		}


		/// <summary>
		/// Moves cursor HOME.
		/// </summary>
		public void HomeButton()
		{
			try
			{
				_mainframeDriver.Screen.SendKeys("<Home>");
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"HomeButton failed.",
					ex);
			}
		}


		/// <summary>
		/// Moves cursor END.
		/// </summary>
		public void EndButton()
		{
			try
			{
				_mainframeDriver.Screen.SendKeys("<End>");
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"EndButton failed.",
					ex);
			}
		}


		/// <summary>
		/// Moves cursor UP.
		/// </summary>
		public void ArrowUp()
		{
			try
			{
				_mainframeDriver.Screen.SendKeys("<Up>");
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"ArrowUp failed.",
					ex);
			}
		}


		/// <summary>
		/// Moves cursor DOWN.
		/// </summary>
		public void ArrowDown()
		{
			try
			{
				_mainframeDriver.Screen.SendKeys("<Down>");
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"ArrowDown failed.",
					ex);
			}
		}


		/// <summary>
		/// Moves cursor LEFT.
		/// </summary>
		public void ArrowLeft()
		{
			try
			{
				_mainframeDriver.Screen.SendKeys("<Left>");
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"ArrowLeft failed.",
					ex);
			}
		}


		/// <summary>
		/// Moves cursor RIGHT.
		/// </summary>
		public void ArrowRight()
		{
			try
			{
				_mainframeDriver.Screen.SendKeys("<Right>");
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"ArrowRight failed.",
					ex);
			}
		}

		/// <summary>
		/// Presses PA1, PA2, or PA3.
		/// </summary>
		public void PressPAKey(int keyNumber)
		{
			if (keyNumber < 1 || keyNumber > 3)
			{
				throw new ArgumentOutOfRangeException(nameof(keyNumber), "PA key must be between 1 and 3.");
			}

			try
			{
				_mainframeDriver.Screen.SendKeys($"<Pa{keyNumber}>");
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"PressPAKey failed. PA{keyNumber}.", ex);
			}
		}

		#endregion

		/// <summary>
		/// Moves cursor to specified row and column.
		/// </summary>
		public void MoveTo(int row, int column)
		{
			if (row <= 0)
				throw new ArgumentOutOfRangeException(nameof(row));

			if (column <= 0)
				throw new ArgumentOutOfRangeException(nameof(column));

			try
			{
				_mainframeDriver.Screen.MoveTo(row, column);
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"MoveTo failed. Row={row}, Column={column}.", ex);
			}
		}

		/// <summary>
		/// Reads text from screen coordinates.
		/// </summary>
		public string GetStringFromTheField(
			int row,
			int column,
			int length)
		{
			if (row <= 0)
				throw new ArgumentOutOfRangeException(nameof(row));

			if (column <= 0)
				throw new ArgumentOutOfRangeException(nameof(column));

			if (length <= 0)
				throw new ArgumentOutOfRangeException(nameof(length));

			try
			{
				return _mainframeDriver.Screen.GetString(
					row,
					column,
					length);
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"GetStringFromTheField failed. Row={row}, Column={column}, Length={length}.", ex);
			}
		}

		/// <summary>
		/// Reads a rectangular screen area.
		/// </summary>
		public string GetStringFromTheBlock(
			int startRow,
			int startCol,
			int endRow,
			int endCol)
		{
			try
			{
				dynamic screen = _mainframeDriver.Screen;

				int rows = screen.Rows;
				int cols = screen.Cols;

				if (startRow < 1 ||
					startCol < 1 ||
					endRow < startRow ||
					endCol < startCol ||
					endRow > rows ||
					endCol > cols)
				{
					throw new ArgumentOutOfRangeException("Invalid screen coordinates.");
				}

				List<string> lines = new();

				for (int r = startRow; r <= endRow; r++)
				{
					int length;

					if (startRow == endRow)
					{
						length = endCol - startCol + 1;

						lines.Add(
							screen.GetString(
								r,
								startCol,
								length));
					}
					else if (r == startRow)
					{
						length = cols - startCol + 1;

						lines.Add(
							screen.GetString(
								r,
								startCol,
								length));
					}
					else if (r == endRow)
					{
						lines.Add(
							screen.GetString(
								r,
								1,
								endCol));
					}
					else
					{
						lines.Add(
							screen.GetString(
								r,
								1,
								cols));
					}
				}

				return string.Join(Environment.NewLine, lines).Trim();
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"ReadBlock failed. Start=({startRow},{startCol}), End=({endRow},{endCol}).", ex);
			}
		}

		/// <summary>
		/// Returns complete screen text.
		/// </summary>
		public string GetScreenText()
		{
			try
			{
				dynamic screen = _mainframeDriver.Screen;

				int rows = screen.Rows;
				int cols = screen.Cols;

				string[] lines = new string[rows];

				for (int row = 1; row <= rows; row++)
				{
					lines[row - 1] =
						screen.GetString(
							row,
							1,
							cols);
				}

				return string.Join(
					Environment.NewLine,
					lines);
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException("GetScreenText failed.", ex);
			}
		}

		/// <summary>
		/// Checks whether the current screen contains the specified text.
		/// </summary>
		public bool IsTextPresentOnScreen(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentException("Search text cannot be null or empty.", nameof(text));
			}

			try
			{
				return GetScreenText().Contains(text, StringComparison.OrdinalIgnoreCase);
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"ScreenContains failed. SearchText='{text}'.", ex);
			}
		}

		/// <summary>
		/// Checks whether any of the provided messages exist on the current screen.
		/// </summary>
		public bool IsAnyMessagePresentOnScreen(string[] messages)
		{
			if (messages == null || messages.Length == 0)
			{
				throw new ArgumentException(
					"Messages list cannot be null or empty.",
					nameof(messages));
			}

			try
			{
				string screenText = GetScreenText();

				foreach (string message in messages)
				{
					if (string.IsNullOrWhiteSpace(message))
						continue;

					if (screenText.Contains(
						message,
						StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}

				return false;
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"IsAnyMessagePresentOnScreen failed.",
					ex);
			}
		}

		/// <summary>
		/// Checks whether a field contains the expected value.
		/// </summary>
		public bool IsFieldValuePresent(
			int row,
			int column,
			int length,
			string expectedValue)
		{
			if (string.IsNullOrWhiteSpace(expectedValue))
				throw new ArgumentException(
					"Expected value cannot be null or empty.",
					nameof(expectedValue));

			try
			{
				string actual =
					GetStringFromTheField(
						row,
						column,
						length);

				return actual.Trim()
					.Equals(
						expectedValue.Trim(),
						StringComparison.OrdinalIgnoreCase);
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"IsFieldValuePresent failed.",
					ex);
			}
		}


		/// <summary>
		/// Checks whether a field is empty.
		/// </summary>
		public bool IsFieldEmpty(
			int row,
			int column,
			int length)
		{
			try
			{
				string value =
					GetStringFromTheField(
						row,
						column,
						length);

				return string.IsNullOrWhiteSpace(value);
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"IsFieldEmpty failed.",
					ex);
			}
		}

		/// <summary>
		/// Returns current cursor position.
		/// </summary>
		public (int Row, int Col) GetCursorPosition()
		{
			try
			{
				return
				(
					(int)_mainframeDriver.Screen.Row,
					(int)_mainframeDriver.Screen.Col
				);
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException("GetCursorPosition failed.", ex);
			}
		}

		/// <summary>
		/// Checks whether cursor is at specified position.
		/// </summary>
		public bool IsCursorAtPosition(
			int row,
			int col)
		{
			try
			{
				var position =
					GetCursorPosition();

				return position.Row == row &&
					   position.Col == col;
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"IsCursorAtPosition failed.",
					ex);
			}
		}


		/// <summary>
		/// Checks whether screen is available and responding.
		/// </summary>
		public bool IsScreenReady()
		{
			try
			{
				string screen =
					GetScreenText();

				return !string.IsNullOrEmpty(screen);
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"IsScreenReady failed.",
					ex);
			}
		}


		/// <summary>
		/// Checks whether Extra session is connected.
		/// </summary>
		public bool IsSessionConnected()
		{
			try
			{
				return _mainframeDriver != null &&
					   _mainframeDriver.Screen != null;
			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// Checks whether Extra session is open.
		/// </summary>
		public bool IsSessionOpen()
		{
			try
			{
				return _mainframeDriver != null;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Waits until specified text appears anywhere on the screen.
		/// </summary>
		public bool WaitForTextOnScreen(
			string text,
			int timeoutSeconds = 30)
		{
			if (string.IsNullOrWhiteSpace(text))
				throw new ArgumentException("Search text cannot be null or empty.", nameof(text));

			if (timeoutSeconds <= 0)
				throw new ArgumentOutOfRangeException(nameof(timeoutSeconds), "Timeout must be greater than zero.");

			try
			{
				DateTime endTime = DateTime.Now.AddSeconds(timeoutSeconds);

				while (DateTime.Now < endTime)
				{
					if (GetScreenText().Contains(text, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}

					Thread.Sleep(500);
				}

				return false;
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"WaitForText failed. Text='{text}', Timeout={timeoutSeconds}.", ex);
			}
		}

		/// <summary>
		/// Waits until a field contains the expected value.
		/// </summary>
		public bool WaitForFieldText(
			int row,
			int column,
			int length,
			string expectedValue,
			int timeoutSeconds = 30)
		{
			if (row <= 0)
				throw new ArgumentOutOfRangeException(nameof(row));

			if (column <= 0)
				throw new ArgumentOutOfRangeException(nameof(column));

			if (length <= 0)
				throw new ArgumentOutOfRangeException(nameof(length));

			if (string.IsNullOrWhiteSpace(expectedValue))
				throw new ArgumentException(
					"Expected value cannot be null or empty.",
					nameof(expectedValue));

			if (timeoutSeconds <= 0)
				throw new ArgumentOutOfRangeException(nameof(timeoutSeconds));

			try
			{
				DateTime endTime = DateTime.Now.AddSeconds(timeoutSeconds);

				while (DateTime.Now < endTime)
				{
					string actual =
						GetStringFromTheField(
							row,
							column,
							length);

					if (actual.Trim()
						.Equals(
							expectedValue.Trim(),
							StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}

					Thread.Sleep(500);
				}

				return false;
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					$"WaitForField failed. Row={row}, Column={column}, Length={length}, ExpectedValue='{expectedValue}', Timeout={timeoutSeconds}.",
					ex);
			}
		}

		/// <summary>
		/// Moves cursor and writes text.
		/// </summary>
		public void WriteAt(
			int row,
			int column,
			string value)
		{
			if (row <= 0)
				throw new ArgumentOutOfRangeException(nameof(row));

			if (column <= 0)
				throw new ArgumentOutOfRangeException(nameof(column));

			if (string.IsNullOrEmpty(value))
				throw new ArgumentException(
					"Value cannot be null or empty.",
					nameof(value));

			try
			{
				MoveTo(row, column);
				SendKeys(value);
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"WriteAt failed. Row={row}, Column={column}, Value='{value}'.", ex);
			}
		}

		/// <summary>
		/// Clears a field by replacing it with spaces.
		/// </summary>
		public void ClearField(
			int row,
			int column,
			int length)
		{
			if (row <= 0)
				throw new ArgumentOutOfRangeException(nameof(row));

			if (column <= 0)
				throw new ArgumentOutOfRangeException(nameof(column));

			if (length <= 0)
				throw new ArgumentOutOfRangeException(nameof(length));

			try
			{
				MoveTo(row, column);
				SendKeys(new string(' ', length));
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"ClearField failed. Row={row}, Column={column}, Length={length}.", ex);
			}
		}

		/// <summary>
		/// Verifies text at specified coordinates.
		/// </summary>
		public bool VerifyFieldText(
			int row,
			int column,
			string expected)
		{
			if (row <= 0)
				throw new ArgumentOutOfRangeException(nameof(row));

			if (column <= 0)
				throw new ArgumentOutOfRangeException(nameof(column));

			if (string.IsNullOrWhiteSpace(expected))
				throw new ArgumentException("Expected text cannot be null or empty.", nameof(expected));

			try
			{
				string actual =
					GetStringFromTheField(
						row,
						column,
						expected.Length);

				return actual.Trim()
					.Equals(
						expected.Trim(),
						StringComparison.OrdinalIgnoreCase);
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException($"VerifyText failed. Row={row}, Column={column}, Expected='{expected}'.", ex);
			}
		}

		public void CloseSession()
		{
			try
			{
				_mainframeDriver?.Close();
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException(
					"CloseSession failed.",
					ex);
			}
		}

		/// <summary>
		/// Returns screen row count.
		/// </summary>
		public int Rows()
		{
			try
			{
				return _mainframeDriver.Screen.Rows;
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException("Unable to retrieve screen rows.", ex);
			}
		}

		/// <summary>
		/// Returns screen column count.
		/// </summary>
		public int Columns()
		{
			try
			{
				return _mainframeDriver.Screen.Cols;
			}
			catch (Exception ex)
			{
				throw new ExtraAutomationException("Unable to retrieve screen columns.", ex);
			}
		}
	}
}