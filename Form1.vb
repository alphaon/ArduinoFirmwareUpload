Imports System.IO
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' Show all available COM ports.
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ListBox1.Items.Add(sp)
        Next


    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fd As OpenFileDialog = New OpenFileDialog()


        fd.Title = "Open File Dialog"
        fd.InitialDirectory = "C:\"
        fd.Filter = "All HEX files (*.hex)|*.hex"
        fd.FilterIndex = 2
        fd.RestoreDirectory = True

        If fd.ShowDialog() = DialogResult.OK Then
            TextBox1.Text = fd.FileName
            CheckBox3.Checked = True
            If (CheckBox1.Checked And CheckBox2.Checked) Then
                Button3.Enabled = True
            End If
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        CheckBox2.Checked = True
        If (CheckBox1.Checked And CheckBox3.Checked) Then
            Button3.Enabled = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If File.Exists("avrdude.conf") And File.Exists("avrdude.exe") And File.Exists("libusb0.dll") Then
            CheckBox1.Checked = True
        End If
        Label3.Text = "Ожидание прошивки."
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim strPath As String = System.IO.Path.GetDirectoryName(
    System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
        Label3.Text = "Началась прошивка! НЕ ВЫКЛЮЧАЙТЕ ПК И НЕ ОТКЛЮЧАЙТЕ МОДУЛЬ ОТ ПК!!!"
        strPath = Mid(strPath, 7)
        TextBox2.Text = ControlChars.Quote & strPath & "\avrdude.exe" & ControlChars.Quote & " -C" & ControlChars.Quote & strPath & "\avrdude.conf" & ControlChars.Quote & " -l" & ControlChars.Quote & strPath & "\avrdude.log" & ControlChars.Quote & " -v -patmega328p -carduino -P" & ListBox1.SelectedItem.ToString & " -b115200 -D -Uflash:w:" & ControlChars.Quote & TextBox1.Text & ControlChars.Quote & ":i"
        'Shell(TextBox2.Text,, True)
        'Dim Counter As String = System.IO.File.ReadAllLines(strPath & "\avrdude.log")(77)
        'If (Counter = "avrdude.exe done.  Thank you.") Then
        'MsgBox("Прошивка успешно завершена! Модуль можно отключить.")
        'Label3.Text = "Ожидание прошивки."
        'Else
        'MsgBox("Что то пошло не так! Отправьте файл avrdude.log из папки с программой разработчику!")
        'l3.Text = "Ожидание прошивки."
        'End If
    End Sub

    Private Sub Button3_EnabledChanged(sender As Object, e As EventArgs) Handles Button3.EnabledChanged
        Label3.Text = "Все готово к прошивке!"
    End Sub
End Class
