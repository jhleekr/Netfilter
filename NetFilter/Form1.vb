﻿Imports System.Security.Cryptography

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Items.Clear()
        Load_Data()
        MODIFYTXT.Visible = False
    End Sub

    Sub Load_Data()
        Dim readlist As ListBox.ObjectCollection
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText("C:\Windows\System32\drivers\etc\hosts")
        Dim fileSplit() As String = Split(fileReader, vbNewLine)
        Dim reset As Boolean = False

        If fileSplit.Length > 0 And Not (fileSplit(0) = "# Copyright (c) 2019-2020 BFY") Then
            MsgBox("프로그램이 처음 실행되었거나 설정 파일이 잘못되었습니다. 설정 파일을 초기화합니다.")
            reset = True
        ElseIf fileSplit.Length > 1 And Not (fileSplit(1) = "# This file is Generated by NetFilter") Then
            MsgBox("프로그램이 처음 실행되었거나 설정 파일이 잘못되었습니다. 설정 파일을 초기화합니다.")
            reset = True
        ElseIf fileSplit.Length > 2 And Not (fileSplit(2) = "# DO NOT MODIFY THIS FILE if you don't know what you are doing.") Then
            MsgBox("프로그램이 처음 실행되었거나 설정 파일이 잘못되었습니다. 설정 파일을 초기화합니다.")
            reset = True
        ElseIf fileSplit.Length > 3 And Not (fileSplit(3) = "# Modifying this file incorrectly can cause problem to system and BFY NetFilter.") Then
            MsgBox("프로그램이 처음 실행되었거나 설정 파일이 잘못되었습니다. 설정 파일을 초기화합니다.")
            reset = True
        End If

        If reset = True Then
            Dim towrite As String = "# Copyright (c) 2019-2020 BFY" + vbNewLine + "# This file is Generated by NetFilter" + vbNewLine + "# DO NOT MODIFY THIS FILE if you don't know what you are doing." + vbNewLine + "# Modifying this file incorrectly can cause problem to system and BFY NetFilter."
            My.Computer.FileSystem.WriteAllText("C:\Windows\System32\drivers\etc\hosts", towrite, False)
        End If

        Dim i As Integer = 4
        readlist = ListBox1.Items
        readlist.Clear()

        Dim listChk As Boolean = False
        Dim j As Long = 0

        While fileSplit.Length > i
            If Strings.Left(fileSplit(i), 9) = "127.0.0.1" Then
                For j = 0 To readlist.Count - 1
                    If readlist(j) = Strings.Mid(fileSplit(i), 11) Then listChk = True
                Next
                If listChk = False Then
                    readlist.Add(Strings.Mid(fileSplit(i), 11))
                Else
                    MsgBox("설정 파일이 잘못되었습니다. 설정 파일을 초기화합니다.")
                    reset = True
                    Exit While
                End If
            Else
                MsgBox("프로그램이 처음 실행되었거나 설정 파일이 잘못되었습니다. 설정 파일을 초기화합니다.")
                reset = True
                Exit While
            End If
            i += 1
        End While

        If reset = True Then
            readlist.Clear()
            Dim towrite As String = "# Copyright (c) 2019-2020 BFY" + vbNewLine + "# This file is Generated by NetFilter" + vbNewLine + "# DO NOT MODIFY THIS FILE if you don't know what you are doing." + vbNewLine + "# Modifying this file incorrectly can cause problem to system and BFY NetFilter."
            My.Computer.FileSystem.WriteAllText("C:\Windows\System32\drivers\etc\hosts", towrite, False)
        End If

        Return
    End Sub

    Dim point As New System.Drawing.Point()
    Dim X, Y As Integer

    Private Sub Bar_MouseDown(sender As Object, e As MouseEventArgs) Handles Bar.MouseDown
        X = Control.MousePosition.X - Me.Location.X
        Y = Control.MousePosition.Y - Me.Location.Y
    End Sub

    Private Sub Bar_MouseMove(sender As Object, e As MouseEventArgs) Handles Bar.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            point = Control.MousePosition
            point.X -= (X)
            point.Y -= (Y)
            Me.Location = point
        End If
    End Sub

    Private Sub ADDBTN_Click(sender As Object, e As EventArgs) Handles ADDBTN.Click
        Dim listChk As Boolean = False
        Dim i As Long = 0
        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i) = sitetxt.Text Then listChk = True
        Next
        If sitetxt.Text = "" Then
            MsgBox("값을 입력해주세요")
        ElseIf listChk = False Then
            ListBox1.Items.Add(sitetxt.Text)
        Else
            MsgBox("중복된 값이 존재합니다")
        End If
        MODIFYTXT.Visible = True
        SAVEDTXT.Visible = False
        sitetxt.Text = ""
    End Sub

    Private Sub APPLYBTN_Click(sender As Object, e As EventArgs) Handles APPLYBTN.Click
        Dim i As Long = 0
        Dim towrite As String
        towrite = "# Copyright (c) 2019-2020 BFY" + vbNewLine + "# This file is Generated by NetFilter" + vbNewLine + "# DO NOT MODIFY THIS FILE if you don't know what you are doing." + vbNewLine + "# Modifying this file incorrectly can cause problem to system and BFY NetFilter."
        My.Computer.FileSystem.WriteAllText("C:\Windows\System32\drivers\etc\hosts", towrite, False)
        For i = 0 To ListBox1.Items.Count - 1
            towrite = vbNewLine + "127.0.0.1 " + ListBox1.Items(i).ToString()
            My.Computer.FileSystem.WriteAllText("C:\Windows\System32\drivers\etc\hosts", towrite, True)
        Next
        MODIFYTXT.Visible = False
        SAVEDTXT.Visible = True
    End Sub

    Private Sub RELEASEBTN_Click(sender As Object, e As EventArgs) Handles RELEASEBTN.Click
        Dim str As String
        Try
            str = ListBox1.SelectedItem.ToString()
        Catch ex As NullReferenceException
            MsgBox("삭제할 항목을 먼저 선택해주세요")
            Return
        Finally
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End Try
        MODIFYTXT.Visible = True
        SAVEDTXT.Visible = False
    End Sub

    Private Sub QUITBTN_Click(sender As Object, e As EventArgs) Handles QUITBTN.Click
        Application.Exit()
    End Sub
End Class