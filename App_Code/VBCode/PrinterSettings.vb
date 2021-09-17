Imports Microsoft.VisualBasic
Imports System.Drawing.Printing

Namespace Raj.EC.Printers

    Public Class Printer_Settings

        Public _pDoc As New PrintDocument
        Private _printerName As String
        Private _pSize As String

        Public Sub New(ByVal Printer_ID As Integer, ByVal customPaperName As String)
            _printerName = Get_Printer_Name(Printer_ID)
            _pSize = customPaperName
            PrinterName = _printerName
        End Sub

        Private Function Get_Printer_Name(ByVal Printer_ID As Integer)
            Dim Printer_Name As String
            If Printer_ID = 2 Then
                Printer_Name = "Epson LX-300+"
            ElseIf Printer_ID = 1 Then
                'Printer_Name = "EPSON LX-300+II ESC/P"
                Printer_Name = "Epson LX-300+"
            Else
                Printer_Name = "Epson LX-300+"
            End If
            Return Printer_Name
        End Function

        Public Property PrinterName()
            Get
                Return _printerName
            End Get
            Set(ByVal value)
                _pDoc.PrinterSettings.PrinterName = value
            End Set
        End Property


        Public ReadOnly Property Papersize() As CrystalDecisions.Shared.PaperSize
            Get
                Dim i As Integer
                Dim pkSize As New PaperSize

                For i = 0 To _pDoc.PrinterSettings.PaperSizes.Count - 1
                    If LCase(_pDoc.PrinterSettings.PaperSizes.Item(i).PaperName) = LCase(_pSize) Then
                        pkSize = _pDoc.PrinterSettings.PaperSizes.Item(i)
                        Exit For
                    End If
                Next
                Return CType(pkSize.RawKind, CrystalDecisions.Shared.PaperSize)
            End Get
        End Property
    End Class
End Namespace
