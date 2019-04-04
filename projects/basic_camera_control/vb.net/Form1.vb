Imports System.IO
Imports System.Net


Public Class Form1
    Dim appPath As String = Application.StartupPath()
    Dim loaded As Boolean = False

#Region "Subs acting like functions"
    Private Sub sendToCamera(ByVal camIP As String, ByVal getString As String)
        'craft the full url using the variables sent to the sub
        Dim fullurl = "http://" & camIP & "/cgi-bin/ptzctrl.cgi?ptzcmd&" & getString
        Try
            Dim request As WebRequest = WebRequest.Create(fullurl)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            response.Close()
            'Since we use this same "function" for the stop event. We don't need a back to back reloading of the preview image. Only really need to reload at the stop event.
            If getString = "ptzstop" Or getString = "zoomstop" Or getString = "focusstop" Or getString.ToString.Contains("_mfocus") = True Then
                ReloadSnapshot(camIP)
            Else
                'Need a pause for the Pan/Tilt/Zoom to actually happen, after the pause and this "fuction" completes, it typically calls for a stop.
                System.Threading.Thread.Sleep(HoldTime.Text)

                '.net likes the & as code, so you have to cancel it out by using it twice
                fullurl = fullurl.Replace("&", "&&")
                'lets update the status text at the bottom with the URL used.
                statusLabel.Text = fullurl
            End If
        Catch ex As Exception
            MessageBox.Show("Better make sure your camera IP is correct")
        End Try
    End Sub
    Private Sub adjustCamera(ByVal camIP As String, ByVal getString As String)
        Dim fullurl = "http://" & camIP & "/cgi-bin/param.cgi?" & getString
        Try
            Dim request As WebRequest = WebRequest.Create(fullurl)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            response.Close()
            System.Threading.Thread.Sleep(1000)
            ReloadSnapshot(camIP)
            fullurl = fullurl.Replace("&", "&&")
            statusLabel.Text = fullurl
        Catch ex As Exception
            MessageBox.Show("Better make sure your camera IP is correct")
        End Try
    End Sub
    Private Sub deviceinfo(ByVal camIP As String, ByVal getString As String)
        Dim fullurl = "http://" & camIP & "/cgi-bin/param.cgi?" & getString
        Try
            Dim request As WebRequest = WebRequest.Create(fullurl)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            response.Close()
            RichTextBox1.Text = responseFromServer
            ReloadSnapshot(camIP)
            fullurl = fullurl.Replace("&", "&&")
            statusLabel.Text = fullurl
        Catch ex As Exception
            MessageBox.Show("Better make sure your camera IP is correct")
        End Try
    End Sub
    Private Sub preset(ByVal camIP As String, ByVal getString As String)
        Dim fullurl = "http://" & camIP & "/cgi-bin/ptzctrl.cgi?ptzcmd&" & getString
        Try
            Dim request As WebRequest = WebRequest.Create(fullurl)
            Dim response As WebResponse = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            response.Close()

            fullurl = fullurl.Replace("&", "&&")
            statusLabel.Text = fullurl

        Catch ex As Exception
            MessageBox.Show("Better make sure your camera IP is correct")
        End Try
    End Sub
#End Region

    'This is what refreshes the image. Obviously ways to get the live feed here but was trying to stick within the HTTP-CGI API.
    Private Sub ReloadSnapshot(ByVal camip As String)

        If WebBrowser1.Url Is Nothing Then
            WebBrowser1.Navigate("http://" & camip & "/snapshot.jpg?user=" & camUN.Text & "&pwd=" & camPW.Text)
        Else
            WebBrowser1.Refresh()
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Do we know the IP we want to connect to?
        If System.IO.File.Exists(appPath & "\ip.txt") = True Then
            Try
                Dim IPUNPW As String() = System.IO.File.ReadAllText(appPath & "\ip.txt").Split(":")
                camIPtxt.Text = IPUNPW(0)
                camUN.Text = IPUNPW(1)
                camPW.Text = IPUNPW(2)
            Catch ex As Exception
                camIPtxt.Text = "0.0.0.0"
            End Try


        Else
            camIPtxt.Text = "0.0.0.0"
        End If

        'Tell all the comboboxes to select something (prevents some errors this way)
        PanSpeed.SelectedIndex = 0
        TiltSpeed.SelectedIndex = 0
        ZoomSpeed.SelectedIndex = 0
        FocusSpeed.SelectedIndex = 0

        Brightness.SelectedIndex = 0
        Saturation.SelectedIndex = 0
        Contrast.SelectedIndex = 0
        Sharpness.SelectedIndex = 0
        Hue.SelectedIndex = 0
        Orientation.SelectedIndex = 0

        'We didn't have a IP saved from a prior run, so we will ask the user for one. Also need username and password
        If camIPtxt.Text = "0.0.0.0" Then
            camIPtxt.Text = InputBox("What is the IP address of the camera you'd like to control?", "IP address needed", "")
            camUN.Text = InputBox("What is the Username of the camera you'd like to control?", "Username needed", "admin")
            camPW.Text = InputBox("What is the Password of the camera you'd like to control?", "Password needed", "admin")
            SaveBtn.PerformClick()
        End If
        ReloadSnapshot(camIPtxt.Text)



        'Now that the comboboxes are set, we can tell the app it's finally loaded so it doesn't fire off the combobox changed event.
        loaded = True
        Timer1.Start()
    End Sub

#Region "PTZ Controls"
    'These call a sub that acts like a function. Since we don't really have a return value, I just handle the error in the sub.
    'They call the request (up) and then delay based on the user specified time and user specified speed. This lets the camera move up for that amount of time/speed
    'Then it tells it to stop moving by calling ptzstop
    Private Sub UpBtn_Click(sender As Object, e As EventArgs) Handles UpBtn.Click
        sendToCamera(camIPtxt.Text, "up&" & PanSpeed.Text & "&" & TiltSpeed.Text)
        sendToCamera(camIPtxt.Text, "ptzstop")
    End Sub
    Private Sub DownBtn_Click(sender As Object, e As EventArgs) Handles DownBtn.Click
        sendToCamera(camIPtxt.Text, "down&" & PanSpeed.Text & "&" & TiltSpeed.Text)
        sendToCamera(camIPtxt.Text, "ptzstop")
    End Sub
    Private Sub LeftBtn_Click(sender As Object, e As EventArgs) Handles LeftBtn.Click
        sendToCamera(camIPtxt.Text, "left&" & PanSpeed.Text & "&" & TiltSpeed.Text)
        sendToCamera(camIPtxt.Text, "ptzstop")
    End Sub
    Private Sub RightBtn_Click(sender As Object, e As EventArgs) Handles RightBtn.Click
        sendToCamera(camIPtxt.Text, "right&" & PanSpeed.Text & "&" & TiltSpeed.Text)
        sendToCamera(camIPtxt.Text, "ptzstop")
    End Sub
    Private Sub LeftUpBtn_Click(sender As Object, e As EventArgs) Handles LeftUpBtn.Click
        sendToCamera(camIPtxt.Text, "leftup&" & PanSpeed.Text & "&" & TiltSpeed.Text)
        sendToCamera(camIPtxt.Text, "ptzstop")
    End Sub
    Private Sub RightUpBtn_Click(sender As Object, e As EventArgs) Handles RightUpBtn.Click
        sendToCamera(camIPtxt.Text, "rightup&" & PanSpeed.Text & "&" & TiltSpeed.Text)
        sendToCamera(camIPtxt.Text, "ptzstop")
    End Sub
    Private Sub RightDownBtn_Click(sender As Object, e As EventArgs) Handles RightDownBtn.Click
        sendToCamera(camIPtxt.Text, "rightdown&" & PanSpeed.Text & "&" & TiltSpeed.Text)
        sendToCamera(camIPtxt.Text, "ptzstop")
    End Sub
    Private Sub LeftDownBtn_Click(sender As Object, e As EventArgs) Handles LeftDownBtn.Click
        sendToCamera(camIPtxt.Text, "leftdown&" & PanSpeed.Text & "&" & TiltSpeed.Text)
        sendToCamera(camIPtxt.Text, "ptzstop")
    End Sub
    Private Sub ZoomInBtn_Click(sender As Object, e As EventArgs) Handles ZoomInBtn.Click
        sendToCamera(camIPtxt.Text, "zoomin&" & ZoomSpeed.Text)
        sendToCamera(camIPtxt.Text, "zoomstop")
    End Sub
    Private Sub ZoomOutBtn_Click(sender As Object, e As EventArgs) Handles ZoomOutBtn.Click
        sendToCamera(camIPtxt.Text, "zoomout&" & ZoomSpeed.Text)
        sendToCamera(camIPtxt.Text, "zoomstop")
    End Sub
    Private Sub FocusInBtn_Click(sender As Object, e As EventArgs) Handles FocusInBtn.Click
        sendToCamera(camIPtxt.Text, "focusin&" & FocusSpeed.Text)
        sendToCamera(camIPtxt.Text, "focusstop")
    End Sub
    Private Sub FocusOutBtn_Click(sender As Object, e As EventArgs) Handles FocusOutBtn.Click
        sendToCamera(camIPtxt.Text, "focusout&" & FocusSpeed.Text)
        sendToCamera(camIPtxt.Text, "focusstop")
    End Sub
    Private Sub FocusLockBtn_Click(sender As Object, e As EventArgs) Handles FocusLockBtn.Click
        If FocusLockBtn.Text = "Lock" Then
            sendToCamera(camIPtxt.Text, "lock_mfocus")
            FocusLockBtn.Text = "Unlock"
        Else
            sendToCamera(camIPtxt.Text, "unlock_mfocus")
            FocusLockBtn.Text = "Lock"
        End If

    End Sub
    Private Sub HomeBtn_Click(sender As Object, e As EventArgs) Handles HomeBtn.Click
        sendToCamera(camIPtxt.Text, "home")
        ReloadSnapshot(camIPtxt.Text)
    End Sub
#End Region
#Region "Camera Adjustments"
    'since these update on change I had to add a method to set these boxes (not empty) when the app opened and not trigger. So we set a boolean to true after they are set.
    'then we only adjust the camera if it's true
    Private Sub Brightness_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Brightness.SelectedIndexChanged

        If loaded = True Then
            adjustCamera(camIPtxt.Text, "post_image_value&bright&" & Brightness.Text)
        End If
    End Sub
    Private Sub Saturation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Saturation.SelectedIndexChanged
        If loaded = True Then
            adjustCamera(camIPtxt.Text, "post_image_value&saturation&" & Saturation.Text)
        End If
    End Sub
    Private Sub Contrast_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Contrast.SelectedIndexChanged
        If loaded = True Then
            adjustCamera(camIPtxt.Text, "post_image_value&contrast&" & Contrast.Text)
        End If
    End Sub
    Private Sub Sharpness_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Sharpness.SelectedIndexChanged
        If loaded = True Then
            adjustCamera(camIPtxt.Text, "post_image_value&sharpness&" & Sharpness.Text)
        End If
    End Sub
    Private Sub Hue_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Hue.SelectedIndexChanged
        If loaded = True Then
            adjustCamera(camIPtxt.Text, "post_image_value&hue&" & Hue.Text)
        End If
    End Sub
    Private Sub Orientation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Orientation.SelectedIndexChanged
        If loaded = True Then
            If Orientation.Text = "flip" Then
                adjustCamera(camIPtxt.Text, "post_image_value&flip&1")
            End If
            If Orientation.Text = "mirror" Then
                adjustCamera(camIPtxt.Text, "post_image_value&mirror&1")
            End If
            If Orientation.Text = "flip and mirror" Then
                adjustCamera(camIPtxt.Text, "post_image_value&flip&1")
                adjustCamera(camIPtxt.Text, "post_image_value&mirror&1")
            End If
            If Orientation.Text = "default" Then
                adjustCamera(camIPtxt.Text, "post_image_value&flip&0")
                adjustCamera(camIPtxt.Text, "post_image_value&mirror&0")
            End If
        End If
    End Sub
    Private Sub Reset_Click(sender As Object, e As EventArgs) Handles Reset.Click
        adjustCamera(camIPtxt.Text, "get_image_default_conf")
    End Sub
#End Region

#Region "Camera information links"
    'Gathers the web request information and displays it in a rich text box
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        RichTextBox1.Text = ""
        deviceinfo(camIPtxt.Text, "get_media_video")
    End Sub
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        RichTextBox1.Text = ""
        deviceinfo(camIPtxt.Text, "get_media_audio")
    End Sub
    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        RichTextBox1.Text = ""
        deviceinfo(camIPtxt.Text, "get_network_conf")
    End Sub
    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        RichTextBox1.Text = ""
        deviceinfo(camIPtxt.Text, "get_device_conf")
    End Sub
    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        RichTextBox1.Text = ""
        deviceinfo(camIPtxt.Text, "get_serial_number")
    End Sub
#End Region

#Region "Save button and form closing"
    'Handle what happens when we click the save button. I have it writing to a file. Also having it ask if they'd like to save the IP when closing the app
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        System.IO.File.WriteAllText(appPath & "\ip.txt", camIPtxt.Text & ":" & camUN.Text & ":" & camPW.Text)
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim result As Integer = MessageBox.Show("Do you want to save the camera IP?", "Save first?", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            SaveBtn.PerformClick()
            End
        End If
    End Sub
#End Region

    'Refresh the image if you click the preview text
    Private Sub GroupBox4_Click(sender As Object, e As EventArgs) Handles GroupBox4.Click
        ReloadSnapshot(camIPtxt.Text)
    End Sub


    'All things presets
    Private Sub PresetSetCall(ByVal presetnumber As String)
        'check if we are doing a call or if it's setting 
        If RadioCall.Checked = True Then
            'call
            preset(camIPtxt.Text, "poscall&" & presetnumber)
            ReloadSnapshot(camIPtxt.Text)
        Else
            'not call (set)
            preset(camIPtxt.Text, "posset&" & presetnumber)
            statusLabel.Text = "Preset " & presetnumber & " has been set"
        End If
    End Sub
#Region "Preset buttons"
    Private Sub Preset1_Click(sender As Object, e As EventArgs) Handles Preset1.Click
        PresetSetCall("1")
    End Sub
    Private Sub Preset2_Click(sender As Object, e As EventArgs) Handles Preset2.Click
        PresetSetCall("2")
    End Sub
    Private Sub Preset3_Click(sender As Object, e As EventArgs) Handles Preset3.Click
        PresetSetCall("3")
    End Sub
    Private Sub Preset4_Click(sender As Object, e As EventArgs) Handles Preset4.Click
        PresetSetCall("4")
    End Sub
    Private Sub Preset5_Click(sender As Object, e As EventArgs) Handles Preset5.Click
        PresetSetCall("5")
    End Sub
    Private Sub Preset6_Click(sender As Object, e As EventArgs) Handles Preset6.Click
        PresetSetCall("6")
    End Sub
    Private Sub Preset7_Click(sender As Object, e As EventArgs) Handles Preset7.Click
        PresetSetCall("7")
    End Sub
    Private Sub Preset8_Click(sender As Object, e As EventArgs) Handles Preset8.Click
        PresetSetCall("8")
    End Sub
    Private Sub Preset9_Click(sender As Object, e As EventArgs) Handles Preset9.Click
        PresetSetCall("9")
    End Sub
    Private Sub Preset10_Click(sender As Object, e As EventArgs) Handles Preset10.Click
        PresetSetCall("10")
    End Sub
    Private Sub Preset11_Click(sender As Object, e As EventArgs) Handles Preset11.Click
        PresetSetCall("11")
    End Sub
    Private Sub Preset12_Click(sender As Object, e As EventArgs) Handles Preset12.Click
        PresetSetCall("12")
    End Sub
    Private Sub Preset13_Click(sender As Object, e As EventArgs) Handles Preset13.Click
        PresetSetCall("13")
    End Sub
    Private Sub Preset14_Click(sender As Object, e As EventArgs) Handles Preset14.Click
        PresetSetCall("14")
    End Sub
    Private Sub Preset15_Click(sender As Object, e As EventArgs) Handles Preset15.Click
        PresetSetCall("15")
    End Sub
    Private Sub Preset16_Click(sender As Object, e As EventArgs) Handles Preset16.Click
        PresetSetCall("16")
    End Sub
    Private Sub Preset17_Click(sender As Object, e As EventArgs) Handles Preset17.Click
        PresetSetCall("17")
    End Sub
    Private Sub Preset18_Click(sender As Object, e As EventArgs) Handles Preset18.Click
        PresetSetCall("18")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ReloadSnapshot(camIPtxt.Text)
    End Sub




#End Region

End Class
