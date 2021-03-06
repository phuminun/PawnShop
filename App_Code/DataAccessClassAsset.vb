﻿Imports Microsoft.VisualBasic
Imports System.Data
Imports Oracle.DataAccess.Client
Imports System.IO
Imports System.Exception
Namespace DataConnection
    Public Class DataAccessClassAsset
        Public con As OracleConnection

        Public Shared Function getConnection() As OracleConnection
            Dim con As New OracleConnection(ConfigurationManager.ConnectionStrings("PawnShopData").ToString)
            con.Open()
            Return con
        End Function
        Public Shared Function getPwAssetConnection() As OracleConnection
            Dim con As New OracleConnection(ConfigurationManager.ConnectionStrings("PawnAsset").ToString)
            con.Open()
            Return con
        End Function
        Public Shared Function fomatNumber(param As Integer) As String
            Return param.ToString("###,###.00")
        End Function
        Public Shared Function getEstimateTiket(ByVal ticketStatus As Integer, ByVal branch As Integer) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_EstimateTicketResult"""
            cmd.Parameters.Add(New OracleParameter("VTicketTypeID ", OracleDbType.Int32)).Value = ticketStatus
            cmd.Parameters.Add(New OracleParameter("VBranchId ", OracleDbType.Int32)).Value = branch
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt
        End Function
        Public Shared Function getEstimateDetial(ByVal ticketID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_TicketInfoDetail"""
            cmd.Parameters.Add(New OracleParameter("TicketID ", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getTicketInfoHeader(ByVal ticketID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_TicketInfoHeader"""
            cmd.Parameters.Add(New OracleParameter("TicketID ", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getEstimateLog(ByVal ticketID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getEstimateLog"""
            cmd.Parameters.Add(New OracleParameter("vTicketId ", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt
        End Function


        Public Shared Function InsertEstimate(ByVal ticketID As String, ByVal EstimatePrice As Int32, ByVal EstimateBy As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """InsertEstimate"""
            cmd.Parameters.Add(New OracleParameter("V_TicketId ", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("V_EstimatePrice ", OracleDbType.Int32)).Value = EstimatePrice
            cmd.Parameters.Add(New OracleParameter("V_EstimateBy ", OracleDbType.Varchar2)).Value = EstimateBy
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function
        Public Shared Function getEstimateByTicketID(ByVal ticketID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_EstimateByTicketID"""
            cmd.Parameters.Add(New OracleParameter("TicketID ", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function updateEstimateFirst(ByVal ticketID As String, ByVal EstimatePrice As Integer) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_updateEstimateFirst"""
            cmd.Parameters.Add(New OracleParameter("tkID", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("EstimatePrice", OracleDbType.Int32)).Value = EstimatePrice
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function


        Public Shared Function getAllBracnh() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getAllBranch"""
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt
        End Function
        Public Shared Function getAllCategoryTicket() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getCategoryTicket"""

            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getTicketForPrePreSet(ByVal ticketID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getTicketPrepareForSet"""
            cmd.Parameters.Add(New OracleParameter("tkID", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function prepareSet(ByVal ticketID As String, ByVal bookNoAndNumno As String, ByVal description As String, ByVal qty As Integer, ByVal weight As Integer, ByVal price As String, ByVal estimate As Integer) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_InsertPrepareSetGroup"""
            cmd.Parameters.Add(New OracleParameter("vSetID", OracleDbType.Varchar2)).Value = Guid.NewGuid.ToString("D").ToUpper()
            cmd.Parameters.Add(New OracleParameter("vTicketID", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("vBookNoAndNumNo", OracleDbType.Varchar2)).Value = bookNoAndNumno
            cmd.Parameters.Add(New OracleParameter("vDescription", OracleDbType.Varchar2)).Value = description
            cmd.Parameters.Add(New OracleParameter("vQuantity", OracleDbType.Int32)).Value = qty
            cmd.Parameters.Add(New OracleParameter("vWeight", OracleDbType.Int32)).Value = weight
            cmd.Parameters.Add(New OracleParameter("vPrice", OracleDbType.Int32)).Value = price
            cmd.Parameters.Add(New OracleParameter("vEstimatePrice", OracleDbType.Int32)).Value = estimate
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function getShowSetGroupAll() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_SetGroupAll"""
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getTicketBeforeSet(ByVal ticketID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_TicketInfoBeforeSet"""
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt
        End Function


        Public Shared Function updateSetIsEnable(ByVal vSetID As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_upDateSetIsEanble"""
            cmd.Parameters.Add(New OracleParameter("vSetID", OracleDbType.Varchar2)).Value = vSetID
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function SelectSet(ByVal vSetID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_SelectSetBySetID"""
            cmd.Parameters.Add(New OracleParameter("vSetID", OracleDbType.Varchar2)).Value = vSetID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function ResetSet() As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_ResetSetGroup"""
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function getCodeSecure(ByVal vUsername As String, ByVal vCode As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getCodeOperate"""
            cmd.Parameters.Add(New OracleParameter("VUserName", OracleDbType.Varchar2)).Value = vUsername
            cmd.Parameters.Add(New OracleParameter("VCode", OracleDbType.Varchar2)).Value = vCode
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function InsertSetGroupReal(ByVal branch As Integer, ByVal category As Integer, ByVal numthing As Decimal, ByVal weight As Decimal, ByVal estimatesum As Decimal, ByVal isSet As Integer, ByVal eventID As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_InsertSetGroupReal"""
            cmd.Parameters.Add(New OracleParameter("vSetGroupID", OracleDbType.Varchar2)).Value = Guid.NewGuid.ToString("D").ToUpper()
            cmd.Parameters.Add(New OracleParameter("vBranch", OracleDbType.Int32)).Value = branch
            cmd.Parameters.Add(New OracleParameter("vCategory", OracleDbType.Int32)).Value = category
            cmd.Parameters.Add(New OracleParameter("vNumThing", OracleDbType.Int32)).Value = numthing
            cmd.Parameters.Add(New OracleParameter("vWeight", OracleDbType.Int32)).Value = weight
            cmd.Parameters.Add(New OracleParameter("vEstimateSum", OracleDbType.Int32)).Value = estimatesum
            cmd.Parameters.Add(New OracleParameter("visSet", OracleDbType.Int32)).Value = isSet
            cmd.Parameters.Add(New OracleParameter("vEventID", OracleDbType.Varchar2)).Value = eventID

            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function


        Public Shared Function UpdateForSetGroup(ByVal SetGroupID As String, ByVal eventID As String, ByVal SetID As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_UpdateSetForGroup"""
            cmd.Parameters.Add(New OracleParameter("vSetGroupID", OracleDbType.Varchar2)).Value = SetGroupID
            cmd.Parameters.Add(New OracleParameter("vEventID", OracleDbType.Varchar2)).Value = eventID
            cmd.Parameters.Add(New OracleParameter("vSetID", OracleDbType.Varchar2)).Value = SetID
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function
        Public Shared Function getSetGroupID() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_GetSetGroupID"""
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getAllSet() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getAllSet"""
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function


        Public Shared Function getChildTicket(ByVal setGroupID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_ChildTicket"""
            cmd.Parameters.Add(New OracleParameter("vSetGroupID", OracleDbType.Varchar2)).Value = setGroupID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getSetGroupIDByID(ByVal setGroupID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getSEtGroupBySetGroupId"""
            cmd.Parameters.Add(New OracleParameter("vSetGroupID", OracleDbType.Varchar2)).Value = setGroupID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getCustomerInfo(ByVal vCitizenNo As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getCustomerInfo"""
            cmd.Parameters.Add(New OracleParameter("vCitizenNo", OracleDbType.Varchar2)).Value = vCitizenNo
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getTicketDetailFullPath(ByVal ticketID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getTicketDetailInfoFull"""
            cmd.Parameters.Add(New OracleParameter("TicketID", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getCustomerFromTicket(ByVal ticketID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getCustomerFromTicket"""
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getBuyBackInfoBranch(ByVal CitizenNo As String, ByVal ticketID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getBuyBackInfoBranch"""
            cmd.Parameters.Add(New OracleParameter("vCitizenNo", OracleDbType.Varchar2)).Value = CitizenNo
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getStateTicketBuyBack(ByVal ticketIDs As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_countStateBuyBack"""
            cmd.Parameters.Add(New OracleParameter("vTicketID", OracleDbType.Varchar2)).Value = ticketIDs
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getBuyAll() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_buyAll"""
            'cmd.Parameters.Add(New OracleParameter("vTicketID", OracleDbType.Varchar2)).Value = ticketIDs
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getBuySelect(ByVal ticketIDs As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_BuySelect"""
            cmd.Parameters.Add(New OracleParameter("vTicketID", OracleDbType.Varchar2)).Value = ticketIDs
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function InsertBuyBack(ByVal tiketID As String, ByVal price As Decimal, ByVal name As String, ByVal remark As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_InsertBuyBack"""
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = Guid.NewGuid.ToString("D").ToUpper()
            cmd.Parameters.Add(New OracleParameter("vTicketID", OracleDbType.Varchar2)).Value = tiketID
            cmd.Parameters.Add(New OracleParameter("vPrice", OracleDbType.Decimal)).Value = price
            cmd.Parameters.Add(New OracleParameter("vName", OracleDbType.Varchar2)).Value = name
            cmd.Parameters.Add(New OracleParameter("vRemark", OracleDbType.Varchar2)).Value = remark
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function
        Public Shared Function updateStateTicket(ByVal StateID As Integer, ByVal tiketID As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_UpdateTicketState"""
            cmd.Parameters.Add(New OracleParameter("vSetStateID", OracleDbType.Int32)).Value = StateID
            cmd.Parameters.Add(New OracleParameter("vTicket", OracleDbType.Varchar2)).Value = tiketID

            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function getAssetFall(ByVal Branchid As Integer, ByVal RoldID As Integer) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getAssestFall"""
            cmd.Parameters.Add(New OracleParameter("vBranchId", OracleDbType.Int32)).Value = Branchid
            cmd.Parameters.Add(New OracleParameter("vRoleID", OracleDbType.Int32)).Value = RoldID
            cmd.Parameters.Add(New OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getAssetFallDB(ByVal Branchid As Integer, ByVal RoldID As Integer) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getAssetFall"""
            cmd.Parameters.Add(New OracleParameter("vBranchId", OracleDbType.Int32)).Value = Branchid
            cmd.Parameters.Add(New OracleParameter("vRoleID", OracleDbType.Int32)).Value = RoldID
            cmd.Parameters.Add(New OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getAssetFallDBbyPeriod(ByVal Branchid As Integer, ByVal RoldID As Integer, ByVal month As String, ByVal year As String, ByVal period As Integer) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getAssetFallByPeriod"""
            cmd.Parameters.Add(New OracleParameter("vBranchId", OracleDbType.Int32)).Value = Branchid
            cmd.Parameters.Add(New OracleParameter("vRoleID", OracleDbType.Int32)).Value = RoldID
            cmd.Parameters.Add(New OracleParameter("vMonth", OracleDbType.Varchar2)).Value = month
            cmd.Parameters.Add(New OracleParameter("vYear", OracleDbType.Varchar2)).Value = year
            cmd.Parameters.Add(New OracleParameter("vPeriod", OracleDbType.Int32)).Value = period
            cmd.Parameters.Add(New OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function




        Public Shared Function updateBothEstimateTicket(ByVal firstEstimate As Decimal, ByVal secondEstimate As Decimal, ByVal ticketID As String, ByVal reportNo As Integer) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_UpdateAssetBothEstimate"""
            cmd.Parameters.Add(New OracleParameter("vFirstEstimate", OracleDbType.Decimal)).Value = firstEstimate
            cmd.Parameters.Add(New OracleParameter("vSecondEstimate", OracleDbType.Decimal)).Value = secondEstimate
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("vReportNo", OracleDbType.Int32)).Value = reportNo

            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function updateFirstEstimateTicket(ByVal firstEstimate As Decimal, ByVal ticketID As String, ByVal reportNo As Integer) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_UpdateAssetFirstEstimate"""
            cmd.Parameters.Add(New OracleParameter("vFirstEstimate", OracleDbType.Decimal)).Value = firstEstimate
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("vReportNo", OracleDbType.Int32)).Value = reportNo

            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function updateSecondEstimateTicket(ByVal secondEstimate As Decimal, ByVal ticketID As String, ByVal reportNo As Integer) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_UpdateAssetSecondEstimate"""
            cmd.Parameters.Add(New OracleParameter("vSecondEstimate", OracleDbType.Decimal)).Value = secondEstimate
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = ticketID
            cmd.Parameters.Add(New OracleParameter("vReportNo", OracleDbType.Int32)).Value = reportNo

            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function InsertLogEstimateFirst(ByVal ticketId As String, ByVal Username As String, ByVal Price As Decimal) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_InsertLogFirstEstimate"""
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = Guid.NewGuid.ToString("D").ToUpper()
            cmd.Parameters.Add(New OracleParameter("vTicketID", OracleDbType.Varchar2)).Value = ticketId
            cmd.Parameters.Add(New OracleParameter("vUserName", OracleDbType.Varchar2)).Value = Username
            cmd.Parameters.Add(New OracleParameter("vPrice", OracleDbType.Decimal)).Value = Price
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function InsertLogEstimateSecond(ByVal ticketId As String, ByVal Username As String, ByVal Price As Decimal) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_InsertLogSecondEstimate"""
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = Guid.NewGuid.ToString("D").ToUpper()
            cmd.Parameters.Add(New OracleParameter("vTicketID", OracleDbType.Varchar2)).Value = ticketId
            cmd.Parameters.Add(New OracleParameter("vUserName", OracleDbType.Varchar2)).Value = Username
            cmd.Parameters.Add(New OracleParameter("vPrice", OracleDbType.Decimal)).Value = Price
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function InsertSet(ByVal vId As String, ByVal vNo As Integer, ByVal vPriceSum As Decimal, ByVal vQuantity As Decimal, ByVal vWeight As Decimal, ByVal vSecondEstimate As Decimal, ByVal vBranchId As Integer, ByVal Username As String, ByVal EventId As String, ByVal Category As Integer) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_AddEstimateSet"""
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = vId
            cmd.Parameters.Add(New OracleParameter("vNo", OracleDbType.Int32)).Value = vNo
            cmd.Parameters.Add(New OracleParameter("vPriceSum", OracleDbType.Decimal)).Value = vPriceSum
            cmd.Parameters.Add(New OracleParameter("vQuantity", OracleDbType.Decimal)).Value = vQuantity
            cmd.Parameters.Add(New OracleParameter("vWeight", OracleDbType.Decimal)).Value = vWeight
            cmd.Parameters.Add(New OracleParameter("vSecondEstimate", OracleDbType.Decimal)).Value = vSecondEstimate
            cmd.Parameters.Add(New OracleParameter("vBranchId", OracleDbType.Int32)).Value = vBranchId
            cmd.Parameters.Add(New OracleParameter("vUserName", OracleDbType.Varchar2)).Value = Username
            cmd.Parameters.Add(New OracleParameter("vEventID", OracleDbType.Varchar2)).Value = EventId
            cmd.Parameters.Add(New OracleParameter("vCategory", OracleDbType.Int32)).Value = Category
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function
        Public Shared Function InsertSetChild(ByVal vSetID As String, ByVal TicketId As String, ByVal Username As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_AddSetChid"""
            cmd.Parameters.Add(New OracleParameter("vSetID", OracleDbType.Varchar2)).Value = vSetID
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = TicketId
            cmd.Parameters.Add(New OracleParameter("vUsername", OracleDbType.Varchar2)).Value = Username
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function

        Public Shared Function getEventCnt(ByVal eventid As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_CntEvent"""
            cmd.Parameters.Add(New OracleParameter("vEventID", OracleDbType.Varchar2)).Value = eventid
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function getMaxNoSet(ByVal eventid As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getMaxNo"""
            cmd.Parameters.Add(New OracleParameter("vEventID", OracleDbType.Varchar2)).Value = eventid
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function getAllSetFromBranch(ByVal BranchId As Integer) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_GetAllSet"""
            cmd.Parameters.Add(New OracleParameter("vBranchId", OracleDbType.Varchar2)).Value = BranchId
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function
        Public Shared Function getAllChild(ByVal setID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getAllChildFromSet"""
            cmd.Parameters.Add(New OracleParameter("vSetID", OracleDbType.Varchar2)).Value = setID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function UpdateSetToZero(ByVal vSetID As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_UpdateSetToZero"""
            cmd.Parameters.Add(New OracleParameter("vSetID", OracleDbType.Varchar2)).Value = vSetID
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function

        Public Shared Function UpdateChildToZero(ByVal vSetID As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_UpdateChildToZero"""
            cmd.Parameters.Add(New OracleParameter("vSetID", OracleDbType.Varchar2)).Value = vSetID
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function
        Public Shared Function getProductType() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getProductType"""
            cmd.Parameters.Add(New OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function getProductGroup() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getProductGroup"""
            cmd.Parameters.Add(New OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function getEventToDropdown() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getEventToDropDown"""
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function


        Public Shared Function getEventCommingToDropdown() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getEventComming"""
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function


        Public Shared Function AddTray(ByVal vid As String, ByVal TrayNo As Integer, ByVal productGroupId As Integer, ByVal amt As Decimal, ByVal estimate As Decimal, ByVal branchId As Integer, ByVal username As String, ByVal eventId As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_AddTray"""
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = vid
            cmd.Parameters.Add(New OracleParameter("vTrayNo", OracleDbType.Int32)).Value = TrayNo
            cmd.Parameters.Add(New OracleParameter("vProductGroupID", OracleDbType.Int32)).Value = productGroupId
            cmd.Parameters.Add(New OracleParameter("vAmount", OracleDbType.Decimal)).Value = amt
            cmd.Parameters.Add(New OracleParameter("vEstimate", OracleDbType.Decimal)).Value = estimate
            cmd.Parameters.Add(New OracleParameter("vBranchId", OracleDbType.Int32)).Value = branchId
            cmd.Parameters.Add(New OracleParameter("vUsername", OracleDbType.Varchar2)).Value = username
            cmd.Parameters.Add(New OracleParameter("vEventID", OracleDbType.Varchar2)).Value = eventId
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function
        Public Shared Function AddTrayChild(ByVal TrayId As String, ByVal SetID As String, ByVal username As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_AddTrayChild"""
            cmd.Parameters.Add(New OracleParameter("vTrayID", OracleDbType.Varchar2)).Value = TrayId
            cmd.Parameters.Add(New OracleParameter("vSetID", OracleDbType.Varchar2)).Value = SetID
            cmd.Parameters.Add(New OracleParameter("vUsername", OracleDbType.Varchar2)).Value = username

            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function


        Public Shared Function getMaxTrayNo(ByVal EventID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_GetMaxTrayNo"""
            cmd.Parameters.Add(New OracleParameter("vEventID", OracleDbType.Varchar2)).Value = EventID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function getAllTray(ByVal branchid As Integer) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getAllTray"""
            cmd.Parameters.Add(New OracleParameter("vUsername", OracleDbType.Int32)).Value = branchid
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function
        Public Shared Function getTrayForEdit(ByVal TrayId As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getDataForEditTray"""
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = TrayId
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function getSetAllDisplay() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getSetDisplay"""
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function getTrayAllDisplay() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getTrayAllDisplay"""
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function getTicketInSet(ByVal setID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getTicketInSet"""
            cmd.Parameters.Add(New OracleParameter("vSetID", OracleDbType.Varchar2)).Value = setID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function


        Public Shared Function UpdateTrayToZero(ByVal vID As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_updateTrayToZero"""
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = vID

            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function

        Public Shared Function UpdateChildDetail(ByVal vSetID As String, ByVal vTicketId As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_updateChildDetail"""
            cmd.Parameters.Add(New OracleParameter("vSetID", OracleDbType.Varchar2)).Value = vSetID
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = vTicketId
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function


        Public Shared Function UpdateTrayDetail(ByVal vProductGroupID As Integer, ByVal vAmount As Decimal, ByVal vEstimate As Decimal, ByVal vID As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_UpdateTray"""
            cmd.Parameters.Add(New OracleParameter("vProductGroupID", OracleDbType.Int32)).Value = vProductGroupID
            cmd.Parameters.Add(New OracleParameter("vAmount", OracleDbType.Decimal)).Value = vAmount
            cmd.Parameters.Add(New OracleParameter("vEstimate", OracleDbType.Decimal)).Value = vEstimate
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = vID
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function

        Public Shared Function getSetDetail(ByVal setID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_GetDetailSet"""
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = setID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function UpdateSetDetail(ByVal vID As String, ByVal vWeight As Decimal, ByVal vPriceSum As Decimal, ByVal vSecondEstimate As Decimal) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_updateDetailSet"""
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = vID
            cmd.Parameters.Add(New OracleParameter("vWeight", OracleDbType.Decimal)).Value = vWeight
            cmd.Parameters.Add(New OracleParameter("vPriceSum", OracleDbType.Decimal)).Value = vPriceSum
            cmd.Parameters.Add(New OracleParameter("vSecondEstimate", OracleDbType.Decimal)).Value = vSecondEstimate
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function

        Public Shared Function CheckIsAsset(ByVal vTicketID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_CheckIsAsset"""
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = vTicketID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function
        Public Shared Function CheckIsAsset2(ByVal vTicketID As String, ByVal vEventId As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_CheckIsAsset2"""
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = vTicketID
            cmd.Parameters.Add(New OracleParameter("vEventId", OracleDbType.Varchar2)).Value = vEventId
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function




        Public Shared Function CheckIsSet(ByVal vTicketID As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_CheckIsSet"""
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = vTicketID
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function

        Public Shared Function UpdateSettTray(ByVal vID As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_UpdateSetTrayZero"""
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = vID
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function

        Public Shared Function getAllEvent() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_eventForManage"""
            cmd.Parameters.Add(New OracleParameter("Eventinfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt

        End Function


        Public Shared Function UpdateIsEnableEvent(ByVal vEventID As String, ByVal isEnable As Integer) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_updateEventIsEnable"""
            cmd.Parameters.Add(New OracleParameter("vEventID", OracleDbType.Varchar2)).Value = vEventID
            cmd.Parameters.Add(New OracleParameter("visEnable", OracleDbType.Int32)).Value = isEnable
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()

        End Function

        Public Shared Function updateTicketBuyOut(ByVal ticketId As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_updateBuyOut"""
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = ticketId
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function CheckBuyOutDuplicate(ByVal ticketId As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_CheckDuplicateBuyBack"""
            cmd.Parameters.Add(New OracleParameter("vTicketID", OracleDbType.Varchar2)).Value = ticketId
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getSomeBranch(ByVal ConcatBranch As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getSomeBranch"""
            cmd.Parameters.Add(New OracleParameter("Branchs", OracleDbType.Varchar2)).Value = ConcatBranch
            cmd.Parameters.Add(New OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getTicketForEvent(ByVal Sdate As Date, ByVal SDate2 As Date) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getTicketForEvent"""
            cmd.Parameters.Add(New OracleParameter("sDate", OracleDbType.Date)).Value = Sdate
            cmd.Parameters.Add(New OracleParameter("sDate2", OracleDbType.Date)).Value = SDate2
            cmd.Parameters.Add(New OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function addTicketOnEvent(ByVal ticketid As String, ByVal eventId As String, ByVal username As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_AddTicketOnEvent"""
            cmd.Parameters.Add(New OracleParameter("vID", OracleDbType.Varchar2)).Value = Guid.NewGuid.ToString("D").ToUpper()
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = ticketid
            cmd.Parameters.Add(New OracleParameter("vEventId", OracleDbType.Varchar2)).Value = eventId
            cmd.Parameters.Add(New OracleParameter("vUsername", OracleDbType.Varchar2)).Value = username
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function getTicketFromEventId(ByVal eventid As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getTicketFromEvent"""
            cmd.Parameters.Add(New OracleParameter("vEventId", OracleDbType.Varchar2)).Value = eventid
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function updateCheckOut(ByVal username As String, ByVal eventid As String, ByVal ticketId As String) As Boolean
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_CheckOutTicket"""
            cmd.Parameters.Add(New OracleParameter("vUsername", OracleDbType.Varchar2)).Value = username
            cmd.Parameters.Add(New OracleParameter("vEventId", OracleDbType.Varchar2)).Value = eventid
            cmd.Parameters.Add(New OracleParameter("vTicketId", OracleDbType.Varchar2)).Value = ticketId
            Try
                cmd.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Return False
            End Try
            con.Close()
        End Function

        Public Shared Function getEventToDropDownByBranch(ByVal branchId As Integer) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getEventToDropDownByBranch"""
            cmd.Parameters.Add(New OracleParameter("vBranchID", OracleDbType.Int32)).Value = branchId
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getAssetFallByEvent(ByVal vbranchId As Integer, ByVal roleid As Integer, ByVal vEventid As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getAssetFallByEvent"""
            cmd.Parameters.Add(New OracleParameter("vBranchId", OracleDbType.Int32)).Value = vbranchId
            cmd.Parameters.Add(New OracleParameter("vRoleID", OracleDbType.Int32)).Value = roleid
            cmd.Parameters.Add(New OracleParameter("vEventId", OracleDbType.Varchar2)).Value = vEventid
            cmd.Parameters.Add(New OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getBranchByEvent(ByVal vEventId As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getBranchByEvent"""
            cmd.Parameters.Add(New OracleParameter("vEventId", OracleDbType.Varchar2)).Value = vEventId
            cmd.Parameters.Add(New OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function


        Public Shared Function CheckEstimateSecond(ByVal vUsername As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_CheckSecondEstimate"""
            cmd.Parameters.Add(New OracleParameter("vUsername", OracleDbType.Varchar2)).Value = vUsername
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getPeriodNo(ByVal pBranchID As Integer, ByVal pYear As String, ByVal pMonth As String) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """AP_Report5621GetPeriodNo"""
            cmd.Parameters.Add(New OracleParameter("vBranchID", OracleDbType.Int32)).Value = pBranchID
            cmd.Parameters.Add(New OracleParameter("vYear", OracleDbType.Varchar2)).Value = pYear
            cmd.Parameters.Add(New OracleParameter("vMonth", OracleDbType.Varchar2)).Value = pMonth
            cmd.Parameters.Add(New OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getYear() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getYearForTicketIn"""
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception

            Finally
                con.Close()
            End Try

            Return dt
        End Function
        Public Shared Function getMonthTicket() As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getMonthForTicketIn"""
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

        Public Shared Function getTicketForEvent2(ByVal pBranchID As Integer, ByVal pYear As String, ByVal pMonth As String, ByVal periodNo As Integer) As DataTable
            Dim dt As New DataTable
            Dim da As New OracleDataAdapter
            Dim con As New OracleConnection
            Dim cmd As New OracleCommand
            con = getPwAssetConnection()
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = """sp_getTicketForEvent2"""
            cmd.Parameters.Add(New OracleParameter("vBranchID", OracleDbType.Int32)).Value = pBranchID
            cmd.Parameters.Add(New OracleParameter("vMonth", OracleDbType.Varchar2)).Value = pMonth
            cmd.Parameters.Add(New OracleParameter("vYear", OracleDbType.Varchar2)).Value = pYear
            cmd.Parameters.Add(New OracleParameter("vPeriodNo", OracleDbType.Int32)).Value = periodNo
            cmd.Parameters.Add(New OracleParameter("TicketInfo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output

            Try
                da.SelectCommand = cmd
                da.Fill(dt)
            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.ToString)
            Finally
                con.Close()
            End Try

            Return dt
        End Function

    End Class
End Namespace


