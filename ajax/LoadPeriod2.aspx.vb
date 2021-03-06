﻿Imports System.Data
Imports System.Web.UI
Imports PSCS.Libary.Models
Imports DataConnection.DataAccessClassAsset
Imports Newtonsoft.Json
Partial Class ajax_LoadPeriod2
    Inherits System.Web.UI.Page

    Private Sub ajax_LoadPeriod2_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim tokenOb As New PSCS.Libary.Models.TokenClass
        If Not PawnUtilFn.GetSessionUserObj(tokenOb) Then
            Response.Redirect("../../../login.aspx")
            Exit Sub
        End If
        Dim BranchId As Integer = tokenOb.BranchId

        'Dim branchId As Integer = Integer.Parse(Request.QueryString("branch"))
        Dim month As String = Request.QueryString("month")
        Dim year As String = Request.QueryString("year")

        Dim dt As New DataTable
        dt = DataConnection.DataAccessClassAsset.getPeriodNo(BranchId, year, month)
        Dim json = JsonConvert.SerializeObject(dt, Formatting.Indented)
        Response.Write(json)
    End Sub
End Class
