Imports System.Web.UI.DataVisualization.Charting

Public Class EEVirtualLab
    Inherits System.Web.UI.Page

    Public dsPersistentOutputData As New DataSet

    <Serializable()>
    Public Class clsExperimentInputVariable
        Property Display_Name As String
        Property Value As String
        Property Variable_Name_In_PSIM As String
        Property Range_From As String
        Property Range_To As String
        Property Data_Type As String
        Property Validation_Message As String
    End Class
    <Serializable()>
    Public Class clsExperimentOutputVariable
        Property Display_Name As String
        Property Value As String
        Property Variable_Name_In_PSIM As String
    End Class
    <Serializable()>
    Public Class clsExperiment
        Property Name As String
        Property Image_Path As String
        Property Sim_File As String
        Property ListOfInputVariables As New List(Of clsExperimentInputVariable)
        Property ListOfOutputVariables As New List(Of clsExperimentOutputVariable)
    End Class


    Public objTestExperiment As New clsExperiment
    Public objAllExperiments As New List(Of clsExperiment)



    Public Sub ReadAllData()

        Dim strsomeString As String()
        Dim strstream As System.IO.StreamReader = System.IO.File.OpenText(Server.MapPath("Expt_PSim_Files\AllExperiments.txt"))

        Dim intNoofExperiments As Integer = strstream.ReadLine()
        For i = 1 To intNoofExperiments
            objTestExperiment = New clsExperiment
            strsomeString = DecodeCSV(strstream.ReadLine)
            objTestExperiment.Name = strsomeString(0)
            objTestExperiment.Sim_File = strsomeString(1)
            objTestExperiment.Image_Path = strsomeString(2)

            Dim intNoofInputVariables As Integer = strstream.ReadLine
            For j = 1 To intNoofInputVariables
                strsomeString = DecodeCSV(strstream.ReadLine)
                Dim objNewInputVariable As New clsExperimentInputVariable
                objNewInputVariable.Display_Name = strsomeString(0)
                objNewInputVariable.Variable_Name_In_PSIM = strsomeString(1)
                objNewInputVariable.Value = strsomeString(2)
                objNewInputVariable.Range_From = strsomeString(3)
                objNewInputVariable.Range_To = strsomeString(4)
                objNewInputVariable.Data_Type = strsomeString(5)
                objTestExperiment.ListOfInputVariables.Add(objNewInputVariable)
            Next
            Dim intNoofOutputVariables As Integer = strstream.ReadLine
            For j = 1 To intNoofOutputVariables
                strsomeString = DecodeCSV(strstream.ReadLine)
                Dim objNewOutputVariable As New clsExperimentOutputVariable
                objNewOutputVariable.Display_Name = strsomeString(0)
                objNewOutputVariable.Variable_Name_In_PSIM = strsomeString(1)
                objNewOutputVariable.Value = strsomeString(2)
                objTestExperiment.ListOfOutputVariables.Add(objNewOutputVariable)
            Next
            strsomeString = DecodeCSV(strstream.ReadLine) ' This is the end of experiment line which we do not need to use
            objAllExperiments.Add(objTestExperiment)
        Next
        strstream.Close()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        
        If Not IsPostBack Or ViewState("OldSelectedIndex") <> lstExperiments.SelectedIndex Then
            ReadAllData()
            If Not IsPostBack Then
                lstExperiments.DataSource = objAllExperiments
                lstExperiments.DataBind()
                Dim intSelectedExperiment As Integer = Request.QueryString("expt")
                If intSelectedExperiment <= lstExperiments.Items.Count Then lstExperiments.SelectedIndex = intSelectedExperiment - 1

            End If

            ViewState.Add("AllExperiments", objAllExperiments)

        Else
            objAllExperiments = ViewState("AllExperiments")
            For Each objRow As GridViewRow In dgParameters.Rows
                For Each objVariable As clsExperimentInputVariable In objAllExperiments(lstExperiments.SelectedIndex).ListOfInputVariables
                    If objVariable.Variable_Name_In_PSIM = objRow.Cells(1).Text Then
                        objVariable.Value = CType(objRow.Cells(2).Controls(1), TextBox).Text
                    End If
                Next
            Next

            For Each objRow As GridViewRow In dgOutput.Rows
                For Each objVariable As clsExperimentOutputVariable In objAllExperiments(lstExperiments.SelectedIndex).ListOfOutputVariables
                    If objVariable.Display_Name = objRow.Cells(0).Text Then
                        objVariable.Value = CType(objRow.Cells(1).Controls(1), DropDownList).Text
                    End If
                Next
            Next

        End If

        ViewState.Add("OldSelectedIndex", lstExperiments.SelectedIndex)

        'The following sub does error Validations on the input parameters

        Dim IsValidated As Boolean
        IsValidated = ValidateErrors(objAllExperiments(lstExperiments.SelectedIndex).ListOfInputVariables)

        dgParameters.DataSource = objAllExperiments(lstExperiments.SelectedIndex).ListOfInputVariables
        dgParameters.DataBind()
        dgOutput.DataSource = objAllExperiments(lstExperiments.SelectedIndex).ListOfOutputVariables
        dgOutput.DataBind()
        ChartCircuit.BackImage = "Expt_Images\" & objAllExperiments(lstExperiments.SelectedIndex).Image_Path
        ChartCircuit.BackImageWrapMode = ChartImageWrapMode.Scaled

        If IsValidated Then
            RunSimulation()
        Else

            CreateMessageboxMessage("There are validation errors. Please refer to the parameter box for further details.")
        End If
    End Sub

    Protected Sub btnRunSimulation_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRunSimulation.Click

    End Sub


    Public Sub RunSimulation()



        Try
            Dim strPSIMPath As String = """" & Server.MapPath("Psim\PsimCmd.exe") & " """ ' """C:\Program Files\Powersim\PSIM9.0.3_Demo\PsimCmd.exe """
            Dim strPSIMInput As String
            Dim strPSIMOutput As String
            Dim strPSIMError As String
            Dim strPSIMVariables As String = ""
            Dim strPSIMTime As String = ""
            Dim strGUID As String = System.Guid.NewGuid.ToString

            strPSIMInput = " -i """ & Server.MapPath("Expt_PSIM_Files\" & objAllExperiments(lstExperiments.SelectedIndex).Sim_File) & """"

            strPSIMOutput = " -o """ & Server.MapPath("Temp\" & strGUID & "_Output.txt") & """"

            strPSIMError = " -m """ & Server.MapPath("Temp\" & strGUID & "_Error.txt") & """"

            'Simulation Time is always the first variable
            strPSIMTime = " -t """ & objAllExperiments(lstExperiments.SelectedIndex).ListOfInputVariables(0).Value & """ "

            For Each objVariable In objAllExperiments(lstExperiments.SelectedIndex).ListOfInputVariables
                If objVariable.Variable_Name_In_PSIM <> "Simulation Duration" Then
                    strPSIMVariables = strPSIMVariables & " -v """ & objVariable.Variable_Name_In_PSIM & "=" & objVariable.Value & """"
                    If objVariable.Data_Type = "Firing Angle" Then
                        strPSIMVariables = strPSIMVariables & " -v """ & objVariable.Variable_Name_In_PSIM & "e" & "=" & (objVariable.Value + 10) & """"
                    End If
                End If
            Next

            Dim strFinalCommandLine = strPSIMInput & strPSIMOutput & strPSIMVariables & strPSIMTime & strPSIMError

            Dim p As New System.Diagnostics.Process
            p.StartInfo.UseShellExecute = True
            p.StartInfo.CreateNoWindow = True
            p.StartInfo.FileName = strPSIMPath
            p.StartInfo.Arguments = strFinalCommandLine

            p.Start()
            p.WaitForExit()

            Dim strsomeString As String
            Dim strstream As System.IO.StreamReader = System.IO.File.OpenText(Server.MapPath("Temp\" & strGUID & "_Output.txt"))


            strsomeString = strstream.ReadLine()
            Dim intNoofOutputVars As Integer = Len(strsomeString) / 18
            Dim dsOutputDataSet As New DataSet

            dsOutputDataSet.Tables.Add("OutputData")

            For count = 1 To intNoofOutputVars
                dsOutputDataSet.Tables(0).Columns.Add(strsomeString.Substring((count - 1) * 18, 18).Trim, GetType(Double))
            Next



            Dim intRowNum = 0
            While strstream.Peek() <> -1
                dsOutputDataSet.Tables(0).Rows.Add()
                strsomeString = strstream.ReadLine()

                For count = 1 To intNoofOutputVars
                    dsOutputDataSet.Tables(0).Rows(intRowNum)(count - 1) = strsomeString.Substring((count - 1) * 18, 18)
                Next
                intRowNum += 1
            End While
            dsPersistentOutputData = dsOutputDataSet
            'Populate charts
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Time (ms)"
            Chart1.Series.Clear()
            Chart2.ChartAreas("ChartArea2").AxisX.Title = "Time (ms)"
            Chart2.Series.Clear()
            Chart3.ChartAreas("ChartArea3").AxisX.Title = "Time (ms)"
            Chart3.Series.Clear()
            For Each objVariable In objAllExperiments(lstExperiments.SelectedIndex).ListOfOutputVariables
                'Populate Chart 1
                If objVariable.Value = "Graph 1" Then
                    For count = 1 To intNoofOutputVars - 1
                        If objVariable.Variable_Name_In_PSIM = (dsOutputDataSet.Tables(0).Columns(count).ColumnName) Then
                            BindDataToChart(Chart1, objVariable.Display_Name, dsOutputDataSet, count)
                        End If
                    Next
                End If

                'Populate Chart 2
                If objVariable.Value = "Graph 2" Then
                    For count = 1 To intNoofOutputVars - 1
                        If objVariable.Variable_Name_In_PSIM = (dsOutputDataSet.Tables(0).Columns(count).ColumnName) Then
                            BindDataToChart(Chart2, objVariable.Display_Name, dsOutputDataSet, count)
                        End If
                    Next
                End If
                'Populate Chart 3
                If objVariable.Value = "Graph 3" Then
                    For count = 1 To intNoofOutputVars - 1
                        If objVariable.Variable_Name_In_PSIM = (dsOutputDataSet.Tables(0).Columns(count).ColumnName) Then
                            BindDataToChart(Chart3, objVariable.Display_Name, dsOutputDataSet, count)
                        End If
                    Next
                End If
            Next

            If Chart1.Series.Count = 0 Then

                Chart1.Visible = False
            End If

            If Chart2.Series.Count = 0 Then
                Chart2.Visible = False
            End If

            If Chart3.Series.Count = 0 Then
                Chart3.Visible = False
            End If

            strstream.Close()
            'shruti: undelete these files after testing
            'System.IO.File.Delete(Server.MapPath("Temp\" & strGUID & "_Output.txt"))
            'System.IO.File.Delete(Server.MapPath("Temp\" & strGUID & "_Error.txt"))



        Catch ex As Exception
            Dim ErrorMessage As String
            ErrorMessage = "An Error has occured. Kindly contact Wizda Solutions at support@wizda.in if the error persists, with the message below." + ex.Message
            CreateMessageboxMessage(ErrorMessage)
            'Response.Write("An Error has occured. Kindly contact Wizda Solutions at support@wizda.in if the error persists, with the message below.")
            'Response.Write(ex.Message)

        Finally

        End Try

    End Sub

    Public Sub BindDataToChart(ByVal chartToBeBound As Chart, ByVal strChartName As String, _
                               ByVal dsOutputDataSet As DataSet, ByVal intDataColumnToBeBound As Integer)

        Dim objChartSeries As New Series(dsOutputDataSet.Tables(0).Columns(intDataColumnToBeBound).ColumnName)
        objChartSeries.ChartType = DataVisualization.Charting.SeriesChartType.FastLine
        objChartSeries.Name = strChartName
        objChartSeries.XValueMember = dsOutputDataSet.Tables(0).Columns(0).ToString
        objChartSeries.YValueMembers = dsOutputDataSet.Tables(0).Columns(intDataColumnToBeBound).ToString
        objChartSeries.BorderWidth = 1

        For intRowNum = 0 To dsOutputDataSet.Tables(0).Rows.Count - 1
            objChartSeries.Points.AddXY(dsOutputDataSet.Tables(0).Rows(intRowNum)(0), _
                                              dsOutputDataSet.Tables(0).Rows(intRowNum)(intDataColumnToBeBound))
        Next

        chartToBeBound.Series.Add(objChartSeries)
        chartToBeBound.DataBind()
        chartToBeBound.Visible = True

    End Sub



    Protected Sub ChartCircuit_Load(ByVal sender As Object, ByVal e As EventArgs) Handles ChartCircuit.Load

    End Sub

    Private Function ValidateErrors(ByVal lstInputVariables As List(Of clsExperimentInputVariable)) As Boolean
        Dim IsValidated As Boolean
        IsValidated = True
        For Each objvariable In lstInputVariables
            objvariable.Validation_Message = Nothing
        Next
        For Each objvariable In lstInputVariables
            Select Case (objvariable.Data_Type).ToString.ToLower
                Case "integer"

                    If Not IsNumeric(objvariable.Value, System.Globalization.NumberStyles.Integer) Then
                        objvariable.Validation_Message = objvariable.Validation_Message + " " + objvariable.Display_Name + " should be an integer. "
                        IsValidated = False
                        'Response.Write(objvariable.Display_Name + "should be an integer")
                    End If

                Case "decimal"
                    If Not IsNumeric(objvariable.Value, System.Globalization.NumberStyles.Float) Then
                        objvariable.Validation_Message = objvariable.Validation_Message + " " + objvariable.Display_Name + " should be decimal. "
                        IsValidated = False
                        'Response.Write(objvariable.Display_Name + "should be an decimal")
                    End If

                Case "exponent"
                    If Not IsNumeric(objvariable.Value, System.Globalization.NumberStyles.Float) Then
                        objvariable.Validation_Message = objvariable.Validation_Message + " " + objvariable.Display_Name + " should be an exponent. "
                        IsValidated = False
                        'Response.Write(objvariable.Display_Name + "should be an exponent")
                    End If

            End Select

            If IsValidated = True Then
                Dim strvalue As String
                Dim strRange_From As String
                Dim strRange_To As String

                Dim dblvalue As Double
                Dim dblRange_From As Double
                Dim dblRange_To As Double

                strvalue = objvariable.Value
                strRange_From = objvariable.Range_From
                strRange_To = objvariable.Range_To

                dblvalue = System.Convert.ToDouble(strvalue)
                dblRange_From = System.Convert.ToDouble(strRange_From)
                dblRange_To = System.Convert.ToDouble(strRange_To)

                If dblvalue < dblRange_From Or dblvalue > dblRange_To Then
                    objvariable.Validation_Message = objvariable.Validation_Message + " " + objvariable.Display_Name + " should be between " + objvariable.Range_From +
                        " and " + objvariable.Range_To
                    IsValidated = False
                End If

            End If

        Next
        Return IsValidated
    End Function

    Private Sub CreateMessageboxMessage(ByVal MessageString As String)

        Dim Message As New System.Text.StringBuilder
        Message.Append(MessageString)
        MessageBox(Message.ToString, "Validation Errors", "OK", "300", "180", Me.Page)
        
    End Sub

    Public Sub MessageBox(ByVal Message As String, _
                              ByVal MessageTitle As String, _
                              ByVal ButtonTitle As String, _
                              ByVal NewWidth As String, _
                              ByVal NewHeight As String, _
                              ByVal SentPage As Page)



        If MessageTitle.Length = 0 Then
            MessageTitle = "Message"
        End If

        If ButtonTitle.Length = 0 Then
            ButtonTitle = "Ok"
        End If

        'Pad the Button...Modify to your liking.
        If ButtonTitle.Length < 4 Then
            ButtonTitle = "'" & Space(5) & ButtonTitle & Space(5) & "'"
        Else
            ButtonTitle = "'" & Space(2) & ButtonTitle & Space(2) & "'"
        End If

        'Default Width and Height
        If NewWidth.Length = 0 Then
            NewWidth = 200
        End If
        If NewHeight.Length = 0 Then
            NewHeight = 100
        End If


        Dim js_Script As New System.Text.StringBuilder("")
        js_Script.Append("<Script LANGUAGE='JavaScript'>" & vbCrLf)
        js_Script.Append("function popup() {")
        js_Script.Append("msg=window.open("""",""msg"",""height=" & NewHeight & ",width=" & NewWidth & ",left=80,top=80"");")
        js_Script.Append("msg.document.write(""<html><title>" & MessageTitle & "</title>"");")
        js_Script.Append("msg.document.write(""<body bgcolor='#F7F6F3' onblur=window.close()>"");")
        js_Script.Append("msg.document.write(""<center><font face='trebuchet ms' size='2'>" & Message & "</font></center>"");")
        js_Script.Append("msg.document.write(""<center><form>"");")
        js_Script.Append("msg.document.write(""<input type=button value=" & ButtonTitle & "onClick='javascript:window.close();'>"");")
        js_Script.Append("msg.document.write(""</form></center>"");")
        js_Script.Append("msg.document.write(""</body></html>"");")
        js_Script.Append("};")
        js_Script.Append("function returnnow() {;")
        js_Script.Append("};")
        js_Script.Append("</SCRIPT>")
        js_Script.Append("</head>")
        js_Script.Append("<body onload='popup();'>")
        js_Script.Append("</body>")


        ClientScript.RegisterClientScriptBlock(Me.GetType, "MsgString", js_Script.ToString())

    End Sub



    Public Shared Function DecodeCSV(ByVal strLine As String) As String()

        Dim strPattern As String
        Dim objMatch As Match

        ' build a pattern
        strPattern = "^" ' anchor to start of the string
        strPattern += "(?:""(?<value>(?:""""|[^""\f\r])*)""|(?<value>[^,\f\r""]*))"
        strPattern += "(?:,(?:[ \t]*""(?<value>(?:""""|[^""\f\r])*)""|(?<value>[^,\f\r""]*)))*"
        strPattern += "$" ' anchor to the end of the string

        ' get the match
        objMatch = Regex.Match(strLine, strPattern)

        ' if RegEx match was ok
        If objMatch.Success Then
            Dim objGroup As Group = objMatch.Groups("value")
            Dim intCount As Integer = objGroup.Captures.Count
            Dim arrOutput(intCount - 1) As String

            ' transfer data to array
            For i As Integer = 0 To intCount - 1
                Dim objCapture As Capture = objGroup.Captures.Item(i)
                arrOutput(i) = objCapture.Value

                ' replace double-escaped quotes
                arrOutput(i) = arrOutput(i).Replace("""""", """")
            Next

            ' return the array
            Return arrOutput
        Else
            Throw New ApplicationException("Bad CSV line: " & strLine)
        End If

    End Function
    Public Function IsNumeric(ByVal val As String, ByVal NumberStyle As System.Globalization.NumberStyles) As Boolean

        Dim result As Double
        Return Double.TryParse(val, NumberStyle,
            System.Globalization.CultureInfo.CurrentCulture, result)
    End Function

    Protected Sub btnSaveResults_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveResults.Click
        ExcelUtil.WorkbookEngine.exportToExcel(dsPersistentOutputData, Server.MapPath("test.xls"))

        Response.ContentType = "doc/excel"
        Response.AppendHeader("Content-Disposition", "attachment; filename=Output.xls")
        Response.TransmitFile(Server.MapPath("test.xls"))
        Response.End()

    End Sub
End Class