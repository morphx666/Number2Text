' Convert any positive number from 0 up to 999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999,999
' to its Spanish written form

Imports System.Threading

Friend Class FormMain
    Inherits Form

    Private tmrCounter As Timer

    Private Function Num2Txt(number As Integer) As String
        Return Num2Txt(number.ToString)
    End Function

    Private Function Num2Txt(number As String) As String
        Dim tmpNumber As String = ""
        Dim result As String = ""

        ' Pad the number so that it is a multiple of 3 characters
        tmpNumber = tmpNumber.PadLeft(3 - (number.Length Mod 3)) + number

        If tmpNumber.Length < 4 Then
            Dim vs As Integer
            Integer.TryParse(tmpNumber, vs)
            ' This IIf is only useful to obtain "cero" when vs = 0
            ' Otherwise, we could use this: result = ParseNumber(vs)
            result = If(vs < 10, Unidad2Str(vs, False), ParseNumber(vs))
        Else
            Dim pos As Integer = 0
            Dim prefix As String
            Dim isThousand As Boolean
            ' WTF? I guess this is a "safety padding"? I just don't remember why I did this...
            tmpNumber = Space(3 * 10) + tmpNumber
            For strPos As Integer = tmpNumber.Length To 1 Step -3
                ' Again... WTF?
                If tmpNumber.Substring(strPos - 2 - 1, 3) = "   " Then Exit For
                isThousand = False
                Select Case pos
                    Case 1, 3, 5, 7, 9, 11, 13, 15, 17, 19
                        If CInt(tmpNumber.Substring(tmpNumber.Length - pos * 3 - 2 - 1, 3)) <> 0 Then result = "mil " + result
                        isThousand = True
                    Case 2, 4, 6, 8, 10, 12, 14, 16, 18
                        prefix = "illon"
                        Select Case pos
                            Case 2 : prefix = "m" + prefix
                            Case 4 : prefix = "b" + prefix
                            Case 6 : prefix = "tr" + prefix
                            Case 8 : prefix = "cuatr" + prefix
                            Case 10 : prefix = "quint" + prefix
                            Case 12 : prefix = "sext" + prefix
                            Case 14 : prefix = "sept" + prefix
                            Case 16 : prefix = "oct" + prefix
                            Case 18 : prefix = "non" + prefix
                        End Select
                        If CInt(tmpNumber.Substring(tmpNumber.Length - pos * 3 - 5 - 1, 6)) > 1 Then
                            prefix += "es"
                        Else
                            If CInt(tmpNumber.Substring(tmpNumber.Length - pos * 3 - 5 - 1, 6)) = 0 Then prefix = ""
                        End If
                        result = prefix + " " + result
                End Select
                If Not (isThousand And CInt(tmpNumber.Substring(strPos - 2 - 1, 3)) = 1) Then
                    result = ParseNumber(CInt(tmpNumber.Substring(strPos - 2 - 1, 3))) + " " + result
                End If
                pos += 1
            Next strPos
        End If

        ' Remove excessive spaces
        result = result.Trim()

        ' Fix some minor issues
        ' result = result.Replace("ciento ", "ciento").Replace("uno ", "un ")
        result = result.Replace("uno ", "un ")

        ' Make the word "mil" part of the previous word
        result = result.Replace("millon", "%").Replace(" mil", "mil").Replace("%", "millon")
        If result.Contains("illon ") OrElse result.EndsWith("illon") Then result = result.Replace("illon", "illón")

        result = StrConv(result, VbStrConv.ProperCase)
        Return result.Replace(" Y ", " y ")
    End Function

    Private Function ParseNumber(number As Integer) As String
        Dim sNumber As String = CStr(number)
        Dim position As Integer = 1
        Dim result As String = ""

        For strPos As Integer = sNumber.Length To 1 Step -1
            Select Case position
                Case 1
                    result = Unidad2Str(CInt(sNumber.Substring(strPos - 1, 1)), strPos = sNumber.Length) + result
                Case 2
                    result = Decena2Str(CInt(sNumber.Substring(strPos - 1, 1)), result = "") + result

                    result = result.Replace("diecicero", "diez")
                    result = result.Replace("dieciuno", "once")
                    result = result.Replace("diecidos", "doce")
                    result = result.Replace("diecitres", "trece")
                    result = result.Replace("diecicuatro", "catorce")
                    result = result.Replace("diecicinco", "quince")
                Case 3
                    result = Centena2Str(CInt(sNumber.Substring(strPos - 1, 1))) + " " + result
            End Select
            position += 1
        Next strPos

        result = Trim(result)
        If result.EndsWith("ciento") Then result = result.Substring(0, result.Length - 6) + "cien"

        Return result
    End Function

    Private Function Centena2Str(number As Integer) As String
        Select Case number
            Case 0
            Case 1
                Return "ciento"
            Case 5
                Return "quinientos"
            Case 7
                Return "setecientos"
            Case 9
                Return "novecientos"
            Case Else
                Return String.Format("{0}cientos", Unidad2Str(number, False))
        End Select
        Return ""
    End Function

    Private Function Decena2Str(number As Integer, useAlt As Boolean) As String
        Dim r As String = ""
        Select Case number
            Case 1
                r = If(useAlt, "diez", "dieci")
            Case 2
                r = If(useAlt, "veinte", "venti")
            Case 3
                r = "treinta"
            Case 4
                r = "cuarenta"
            Case 5
                r = "cincuenta"
            Case 6
                r = "sesenta"
            Case 7
                r = "setenta"
            Case 8
                r = "ochenta"
            Case 9
                r = "noventa"
        End Select

        If Not useAlt And number >= 3 Then r += " y "

        Return r
    End Function

    Private Function Unidad2Str(number As Integer, isFirst As Boolean) As String
        Select Case number
            Case 0
                If Not isFirst Then Return "cero"
            Case 1
                Return "uno"
            Case 2
                Return "dos"
            Case 3
                Return "tres"
            Case 4
                Return "cuatro"
            Case 5
                Return "cinco"
            Case 6
                Return "seis"
            Case 7
                Return "siete"
            Case 8
                Return "ocho"
            Case 9
                Return "nueve"
        End Select

        Return ""
    End Function

    Private Sub ButtonGuess_Click(eventSender As Object, eventArgs As EventArgs) Handles ButtonGuess.Click
        If tmrCounter IsNot Nothing Then ToggleAuto()
        Randomize(My.Computer.Clock.TickCount)
        Guess()
    End Sub

    Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If tmrCounter IsNot Nothing Then
            tmrCounter.Dispose()
            Application.DoEvents()
        End If
    End Sub

    Private Sub FormMain_Load(eventSender As Object, eventArgs As EventArgs) Handles MyBase.Load
        TextBoxNumber.Text = "0"
    End Sub

    Private Sub Guess()
        Dim rndNumber As String
        Dim level As Integer = 1
        Dim digit As String

        Do
            rndNumber = ""
            For randomizer As Integer = 1 To level
                Do
                    digit = Chr(CInt(Int(10 * Rnd())) + 48)
                    Application.DoEvents()
                Loop Until Int(Rnd(10)) = Int(Rnd(10000))
                rndNumber += digit
            Next

ReStart:
            If rndNumber.Length > 1 Then
                For strPos As Integer = 0 To rndNumber.Length - 1
                    If rndNumber.Substring(strPos, 1) <> "0" Then Exit For
                    rndNumber = rndNumber.Substring(strPos + 1)
                    GoTo ReStart
                Next
            End If

            TextBoxNumber.Visible = False
            TextBoxNumber.Text = rndNumber

            Dim msg As String = String.Format("Nivel: {0}{1}{1}Introduce la forma númerica de {1}{2}", level, Environment.NewLine, LabelResult.Text)
            Dim answer As String = InputBox(msg, "Number2Text Guess Game", "").Replace(",", "")
            If answer = "" Then Exit Do
            If rndNumber <> answer Then
                msg = String.Format("INCORRECTO{0}{0}La respuesta era:{0}{1} ({2}){0}{0}Tu escribiste:{0}{3} ({4})", Environment.NewLine, rndNumber, Num2Txt(rndNumber), answer, Num2Txt(answer))
                MsgBox(msg, MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Respuesta Incorrecta")
                If level > 1 Then level -= 1
            Else
                level += 1
            End If
        Loop

        TextBoxNumber.Visible = True
    End Sub

    'Private Sub AutoCount()
    '    Static IsBusy As Boolean
    '    If IsBusy Then Exit Sub
    '    IsBusy = True

    '    Dim number As Integer = CInt(GetIntPart(txtNumber.Text).Replace(",", ""))
    '    txtNumber.Invoke(New UpdateTextDel(AddressOf UpdateText), CStr(number + 1))

    '    IsBusy = False
    'End Sub

    ' This version is slower but it allows us to go beyond the limits of the largest number available in .NET (ULong.MaxValue / 18,446,744,073,709,551,615)
    ' We could define the variables as Double (which would allow us to go up to 10^308 but this would make the program too slow
    Private Sub AutoCount(state As Object)
        Static IsBusy As Boolean
        If IsBusy Then Exit Sub
        IsBusy = True

        Dim sNumber As String = GetIntPart(TextBoxNumber.Text).Replace(",", "")
        Dim number As Integer
        Dim carryFlag As Boolean = False

        For i As Integer = sNumber.Length To 1 Step -1
            Integer.TryParse(sNumber.Chars(i - 1), number)
            If i = sNumber.Length OrElse carryFlag Then
                number += 1
                If number >= 10 Then
                    number -= 10
                    carryFlag = True
                Else
                    carryFlag = False
                End If
            Else
                Exit For
            End If
            sNumber = sNumber.Substring(0, i - 1) + number.ToString() + sNumber.Substring(i)
            'Mid(sNumber, i, 1) = CStr(number)
        Next i
        If carryFlag Then sNumber = "1" + sNumber

        TextBoxNumber.Invoke(New MethodInvoker(Sub() TextBoxNumber.Text = sNumber))

        IsBusy = False
    End Sub

    Private Function GetFracPart(sNumber As String) As String
        If sNumber.IndexOf(".") <> -1 Then
            Return sNumber.Split("."c)(1)
        Else
            Return "0"
        End If
    End Function

    Private Function GetIntPart(sNumber As String) As String
        If sNumber.IndexOf(".") <> -1 Then
            Return sNumber.Split("."c)(0)
        Else
            Return sNumber
        End If
    End Function

    Private Sub TextBoxNumber_TextChanged(eventSender As Object, eventArgs As EventArgs) Handles TextBoxNumber.TextChanged
        Static IsBusy As Boolean
        If IsBusy Then Exit Sub
        IsBusy = True

        If TextBoxNumber.Text = "." Then TextBoxNumber.Text = "0.0"

        Dim intPart As String = GetIntPart(TextBoxNumber.Text)
        Dim frcPart As String = GetFracPart(TextBoxNumber.Text)
        Dim selStart As Integer = TextBoxNumber.SelectionStart - System.Text.RegularExpressions.Regex.Matches(intPart, ",").Count

        ' Automatically apply the thousands divisions with commas
        Dim sNumber As String = intPart.Replace(",", "")
        If sNumber.Length > 60 Then
            LabelResult.Text = "ERROR"
        Else
            If sNumber.Length > 3 Then
                Dim position As Integer = 0
                For i As Integer = sNumber.Length - 1 To 0 Step -1
                    If sNumber.Substring(i, 1) <> "," Then position += 1
                    If position Mod 3 = 0 And i > 0 Then
                        sNumber = String.Format("{0},{1}", sNumber.Substring(0, i), sNumber.Substring(i))
                        selStart += 1
                    End If
                Next i
            Else
                sNumber = intPart.Replace(",", "")
            End If
            If Val(sNumber) = 0 Then sNumber = "0"
            TextBoxNumber.Text = sNumber + "." + frcPart
            TextBoxNumber.SelectionStart = If(selStart <= 0, 1, selStart)

            ' Handle decimals
            LabelResult.Text = Num2Txt(intPart.Replace(",", ""))
            If Val(frcPart) <> 0 Then
                LabelResult.Text = LabelResult.Text + " punto"
                For i As Integer = 0 To frcPart.Length - 1
                    If frcPart.Substring(i, 1) <> "0" Then Exit For
                    LabelResult.Text = LabelResult.Text + " Cero"
                Next i

                ' Remove zeros at the right
                frcPart = frcPart.TrimEnd("0"c)
                LabelResult.Text = String.Format("{0} {1}", LabelResult.Text, Num2Txt(frcPart))

                ' Alternate Display
                Dim f As String = Num2Frac(CSng(TextBoxNumber.Text.Replace(",", "")))
                If f <> "" Then
                    Dim p As String() = f.Split("/"c)
                    Dim n As String = Num2Txt(p(0))
                    Dim d As String = Num2Txt(p(1))

                    LabelResult.Text = String.Format("{0}{1}ó{1}{2} sobre {3}", LabelResult.Text, Environment.NewLine, n, d)
                End If
            ElseIf intPart = "42" Then
                LabelResult.Text = String.Format("{0}{1}ó{1}El significado de la vida, el universo y todo lo demás...", LabelResult.Text, Environment.NewLine)
            End If

            If frcPart = "0" AndAlso intPart.Replace(",", "").Length <= 4 Then
                If intPart = "" Then intPart = "0"

                LabelResult.Text += vbCrLf + ToRomanNumeral(Integer.Parse(intPart.Replace(",", "")))
            End If
        End If

        IsBusy = False
    End Sub

    Private Function Num2Frac(decNumber As Single) As String
        Dim numerator As Single

        If Not decNumber.ToString.Contains("E") Then
            If CStr(decNumber).Contains(".") Then
                For denominator As Integer = 1 To 10000
                    numerator = (denominator * decNumber)
                    If CInt(numerator) - numerator = 0 Then Return String.Format("{0}/{1}", numerator, denominator)
                Next denominator
            End If
        End If

        Return ""
    End Function

    Private Sub TextBoxNumber_KeyPress(eventSender As Object, eventArgs As KeyPressEventArgs) Handles TextBoxNumber.KeyPress
        Dim KeyAscii As Integer = Asc(eventArgs.KeyChar)

        If (Not (KeyAscii >= Asc("0") And KeyAscii <= Asc("9"))) And KeyAscii <> Asc(".") And KeyAscii <> Keys.Back Then
            KeyAscii = 0
        End If

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then eventArgs.Handled = True
    End Sub

    Private Sub TextBoxAuto_Click(sender As Object, e As EventArgs) Handles ButtonAuto.Click
        ToggleAuto()
    End Sub

    Private Sub ToggleAuto()
        If tmrCounter Is Nothing Then
            tmrCounter = New Timer(AddressOf AutoCount, Nothing, 0, 1)
        Else
            tmrCounter.Dispose()
            tmrCounter = Nothing
        End If
    End Sub

    Private rnValues() As Integer = New Integer() {1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1}
    Private rNumerals() As String = New String() {"M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"}

    Private Function ToRomanNumeral(number As Integer) As String
        If number < 0 OrElse number > 3999 Then
            Return ""
        Else
            If number = 0 Then Return "N"

            Dim result As New System.Text.StringBuilder()
            For i As Integer = 0 To rnValues.Length - 1
                While number >= rnValues(i)
                    number -= rnValues(i)
                    result.Append(rNumerals(i))
                End While
            Next

            Return result.ToString()
        End If
    End Function
End Class