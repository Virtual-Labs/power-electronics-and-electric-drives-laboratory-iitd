Imports System.Data
Imports System.IO
Imports System.Xml
Imports System.Xml.Xsl

Namespace ExcelUtil
    Public Class WorkbookEngine
        Public Shared Sub exportToExcel(ByVal source As DataSet, ByVal fileName As String)


            Dim excelDoc As System.IO.StreamWriter

            excelDoc = New System.IO.StreamWriter(fileName)
            Const startExcelXML As String = "<xml version>" & vbCr & vbLf & "<Workbook " & "xmlns=""urn:schemas-microsoft-com:office:spreadsheet""" & vbCr & vbLf & " xmlns:o=""urn:schemas-microsoft-com:office:office""" & vbCr & vbLf & " " & "xmlns:x=""urn:schemas-    microsoft-com:office:" & "excel""" & vbCr & vbLf & " xmlns:ss=""urn:schemas-microsoft-com:" & "office:spreadsheet"">" & vbCr & vbLf & " <Styles>" & vbCr & vbLf & " " & "<Style ss:ID=""Default"" ss:Name=""Normal"">" & vbCr & vbLf & " " & "<Alignment ss:Vertical=""Bottom""/>" & vbCr & vbLf & " <Borders/>" & vbCr & vbLf & " <Font/>" & vbCr & vbLf & " <Interior/>" & vbCr & vbLf & " <NumberFormat/>" & vbCr & vbLf & " <Protection/>" & vbCr & vbLf & " </Style>" & vbCr & vbLf & " " & "<Style ss:ID=""BoldColumn"">" & vbCr & vbLf & " <Font " & "x:Family=""Swiss"" ss:Bold=""1""/>" & vbCr & vbLf & " </Style>" & vbCr & vbLf & " " & "<Style     ss:ID=""StringLiteral"">" & vbCr & vbLf & " <NumberFormat" & " ss:Format=""@""/>" & vbCr & vbLf & " </Style>" & vbCr & vbLf & " <Style " & "ss:ID=""Decimal"">" & vbCr & vbLf & " <NumberFormat " & "ss:Format=""0.0000""/>" & vbCr & vbLf & " </Style>" & vbCr & vbLf & " " & "<Style ss:ID=""Integer"">" & vbCr & vbLf & " <NumberFormat " & "ss:Format=""0""/>" & vbCr & vbLf & " </Style>" & vbCr & vbLf & " <Style " & "ss:ID=""DateLiteral"">" & vbCr & vbLf & " <NumberFormat " & "ss:Format=""mm/dd/yyyy;@""/>" & vbCr & vbLf & " </Style>" & vbCr & vbLf & " " & "</Styles>" & vbCr & vbLf & " "
            Const endExcelXML As String = "</Workbook>"

            Dim rowCount As Integer = 0
            Dim sheetCount As Integer = 1
            '
            '    <xml version>
            '    <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
            '    xmlns:o="urn:schemas-microsoft-com:office:office"
            '    xmlns:x="urn:schemas-microsoft-com:office:excel"
            '    xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet">
            '    <Styles>
            '    <Style ss:ID="Default" ss:Name="Normal">
            '      <Alignment ss:Vertical="Bottom"/>
            '      <Borders/>
            '      <Font/>
            '      <Interior/>
            '      <NumberFormat/>
            '      <Protection/>
            '    </Style>
            '    <Style ss:ID="BoldColumn">
            '      <Font x:Family="Swiss" ss:Bold="1"/>
            '    </Style>
            '    <Style ss:ID="StringLiteral">
            '      <NumberFormat ss:Format="@"/>
            '    </Style>
            '    <Style ss:ID="Decimal">
            '      <NumberFormat ss:Format="0.0000"/>
            '    </Style>
            '    <Style ss:ID="Integer">
            '      <NumberFormat ss:Format="0"/>
            '    </Style>
            '    <Style ss:ID="DateLiteral">
            '      <NumberFormat ss:Format="mm/dd/yyyy;@"/>
            '    </Style>
            '    </Styles>
            '    <Worksheet ss:Name="Sheet1">
            '    </Worksheet>
            '    </Workbook>
            '    

            excelDoc.Write(startExcelXML)
            excelDoc.Write("<Worksheet ss:Name=""Sheet" & sheetCount & """>")
            excelDoc.Write("<Table>")
            excelDoc.Write("<Row>")
            For x As Integer = 0 To source.Tables(0).Columns.Count - 1
                excelDoc.Write("<Cell ss:StyleID=""BoldColumn""><Data ss:Type=""String"">")
                excelDoc.Write(source.Tables(0).Columns(x).ColumnName)
                excelDoc.Write("</Data></Cell>")
            Next
            excelDoc.Write("</Row>")
            For Each x As DataRow In source.Tables(0).Rows
                rowCount += 1
                'if the number of rows is > 64000 create a new page to continue output

                If rowCount = 64000 Then
                    rowCount = 0
                    sheetCount += 1
                    excelDoc.Write("</Table>")
                    excelDoc.Write(" </Worksheet>")
                    excelDoc.Write("<Worksheet ss:Name=""Sheet" & sheetCount & """>")
                    excelDoc.Write("<Table>")
                End If
                excelDoc.Write("<Row>")
                'ID=" + rowCount + "
                For y As Integer = 0 To source.Tables(0).Columns.Count - 1
                    Dim rowType As System.Type
                    rowType = x(y).[GetType]()
                    Select Case rowType.ToString()
                        Case "System.String"
                            Dim XMLstring As String = x(y).ToString()
                            XMLstring = XMLstring.Trim()
                            XMLstring = XMLstring.Replace("&", "&")
                            XMLstring = XMLstring.Replace(">", ">")
                            XMLstring = XMLstring.Replace("<", "<")
                            excelDoc.Write("<Cell ss:StyleID=""StringLiteral"">" & "<Data ss:Type=""String"">")
                            excelDoc.Write(XMLstring)
                            excelDoc.Write("</Data></Cell>")
                            Exit Select
                        Case "System.DateTime"
                            'Excel has a specific Date Format of YYYY-MM-DD followed by  

                            'the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000

                            'The Following Code puts the date stored in XMLDate 

                            'to the format above

                            Dim XMLDate As DateTime = DirectCast(x(y), DateTime)
                            Dim XMLDatetoString As String = ""
                            'Excel Converted Date
                            XMLDatetoString = XMLDate.Year.ToString() & "-" & (If(XMLDate.Month < 10, "0" & XMLDate.Month.ToString(), XMLDate.Month.ToString())) & "-" & (If(XMLDate.Day < 10, "0" & XMLDate.Day.ToString(), XMLDate.Day.ToString())) & "T" & (If(XMLDate.Hour < 10, "0" & XMLDate.Hour.ToString(), XMLDate.Hour.ToString())) & ":" & (If(XMLDate.Minute < 10, "0" & XMLDate.Minute.ToString(), XMLDate.Minute.ToString())) & ":" & (If(XMLDate.Second < 10, "0" & XMLDate.Second.ToString(), XMLDate.Second.ToString())) & ".000"
                            excelDoc.Write("<Cell ss:StyleID=""DateLiteral"">" & "<Data ss:Type=""DateTime"">")
                            excelDoc.Write(XMLDatetoString)
                            excelDoc.Write("</Data></Cell>")
                            Exit Select
                        Case "System.Boolean"
                            excelDoc.Write("<Cell ss:StyleID=""StringLiteral"">" & "<Data ss:Type=""String"">")
                            excelDoc.Write(x(y).ToString())
                            excelDoc.Write("</Data></Cell>")
                            Exit Select
                        Case "System.Int16", "System.Int32", "System.Int64", "System.Byte"
                            excelDoc.Write("<Cell ss:StyleID=""Integer"">" & "<Data ss:Type=""Number"">")
                            excelDoc.Write(x(y).ToString())
                            excelDoc.Write("</Data></Cell>")
                            Exit Select
                        Case "System.Decimal", "System.Double"
                            excelDoc.Write("<Cell ss:StyleID=""Decimal"">" & "<Data ss:Type=""Number"">")
                            excelDoc.Write(x(y).ToString())
                            excelDoc.Write("</Data></Cell>")
                            Exit Select
                        Case "System.DBNull"
                            excelDoc.Write("<Cell ss:StyleID=""StringLiteral"">" & "<Data ss:Type=""String"">")
                            excelDoc.Write("")
                            excelDoc.Write("</Data></Cell>")
                            Exit Select
                        Case Else
                            Throw (New Exception(rowType.ToString() & " not handled."))
                    End Select
                Next
                excelDoc.Write("</Row>")
            Next
            excelDoc.Write("</Table>")
            excelDoc.Write(" </Worksheet>")
            excelDoc.Write(endExcelXML)
            excelDoc.Close()
        End Sub

    End Class
End Namespace
