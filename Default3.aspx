﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default3.aspx.vb" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="js/jquery.js"></script>
    <script src="js/jquery.validate.js"></script>
    <script src="js/jquery.tabletojson.js"></script>
    <script src="js/uikit.min.js" type="text/javascript"></script>
    <link href="css/uikit.min.css" rel="stylesheet" type="text/css" />
    <link href="css/GridStyle.css" rel="stylesheet" />
    <style type="text/css">
        .error {
            color: red;
        }
    </style>
    <title>::ระบบจัดการทรัพย์หลุด::</title>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#loading').show();
            $.ajax({
                type: "POST",
                url: "ajax/Default3.aspx",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('#tableData tbody').empty();

                    for (i = 0 ; i < data.length; i++) {
                        $('#tableData tbody').append(
                        "<tr>" +
                            "<td style='text-align:center'>" + (i + 1) + "</td>" +
                            "<td style='text-align:center'>" + data[i].TicketId + "</td>" +
                            "<td style='text-align:center'>" + data[i].BookNo + "</td>" +
                            "<td style='text-align:center'>" + data[i].TicketNo + "</td>" +
                            "<td style='text-align:center'>" + data[i].CreatedDate + "</td>" +
                            "<td style='text-align:center'>" + data[i].Amount + "</td>" +
                            "<td style='text-align:center'>" + "<input id='FirstEstimate' type='text' name='firstEstimate' class='uk-form-width-medium' value='" + data[i].FirstEstimate + "' />" + "</td>" +
                            "<td style='text-align:center'>" + data[i].SecondEstimate + "</td>" +
                            "<td style='text-align:center;display:none;' class='reportNo' >" + data[i].ReportNo + "</td>" +
                            "<td style='text-align:center'><input type='button' value='รายละเอียด' style='color:#ffffff' class='uk-button uk-button-primary' onclick=\"getTicketDetail('" + data[i].TicketId + "')\"/> </td>" +
                            "<td style='text-align:center'><input type='button' value='ประวัติการประเมิน' style='color:#ffffff' class='uk-button uk-button-success' onclick=\"getEstimatDetail('" + data[i].TicketId + "')\"/> </td>" +
                        "</tr>"
                         );
                    }
                    $('#loading').hide();
                },
                error: function ajaxError(result) {
                    alert(result.status + ":" + result.statusText);
                }
            });

            $('#form15').validate({
                rules: {
                    firstEstimate: {
                        required: true,
                        digits: true
                    }
                },
                messages: {
                    firstEstimate: " กรุณากรอกราคาประเมินและต้องเป็นค่าตัวเลข",
                }
                    ,
                submitHandler: function (form) {
                    UpdateEstimate()
                    //confirmEdit();
                    //form.submit();
                }
            });

            //$('#btnUpdate').click(function () {

            //});
        });

        function UpdateEstimate() {
            var arrData = [];
            $('#tableData tbody tr').each(function () {
                var currentRow = $(this);

                var id = currentRow.find("td:eq(0)").text();
                var TicketId = currentRow.find("td:eq(1)").text();
                var BookNo = currentRow.find("td:eq(2)").text();
                var TicketNo = currentRow.find("td:eq(3)").text();
                //var CreateDate = currentRow.find("td:eq(4)").text();
                var Amount = currentRow.find("td:eq(5)").text();
                var FirstEstimate = currentRow.find('input[name=first]').val();
                var SecondEstimate = currentRow.find("td:eq(7)").text();
                var ReportNo = currentRow.find("td:eq(8)").text();

                var obj = {};
                obj.id = id;
                obj.TicketId = TicketId;
                obj.BookNo = BookNo;
                obj.TicketNo = TicketNo;
                //obj.CreateDate = CreateDate;
                obj.Amount = Amount;
                obj.FirstEstimate = FirstEstimate;
                obj.SecondEstimate = SecondEstimate;
                obj.ReportNo = ReportNo;

                arrData.push(obj);
            });
            $.ajax({
                url: "ajax/UpdateEstimate.aspx",
                method: "POST",
                dataType: "json",
                data: JSON.stringify(arrData),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if ($.trim(data).toString() == 'Success') {

                    }
                },
            });
            $('#lblAlert').text("ประเมินตั๋วเรียบร้อย");
            AlertModal("modalAlertSuccess");
            loadGrid();
        }



        function AlertModal(ModalName) {
            var modalName = "#" + ModalName;
            var modal = UIkit.modal(modalName);

            if (modal.isActive()) {
                modal.hide();
            } else {
                modal.show();
            }
        }
        function loadGrid() {
            $('#loading').show();
            $.ajax({
                type: "POST",
                url: "ajax/Default3.aspx",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('#tableData tbody').empty();

                    for (i = 0 ; i < data.length; i++) {
                        $('#tableData tbody').append(
                        "<tr>" +
                            "<td style='text-align:center'>" + (i + 1) + "</td>" +
                            "<td style='text-align:center'>" + data[i].TicketId + "</td>" +
                            "<td style='text-align:center'>" + data[i].BookNo + "</td>" +
                            "<td style='text-align:center'>" + data[i].TicketNo + "</td>" +
                            "<td style='text-align:center'>" + data[i].CreatedDate + "</td>" +
                            "<td style='text-align:center'>" + data[i].Amount + "</td>" +
                            "<td style='text-align:center'>" + data[i].FirstEstimate + "</td>" +
                            "<td style='text-align:center'>" + data[i].SecondEstimate + "</td>" +
                            "<td style='text-align:center;display:none'>" + data[i].ReportNo + "</td>" +
                            "<td style='text-align:center'><input type='button' value='รายละเอียด' style='color:#ffffff' class='uk-button uk-button-primary' onclick=\"getTicketDetail('" + data[i].TicketId + "')\"/> </td>" +
                            "<td style='text-align:center'><input type='button' value='ประวัติการประเมิน' style='color:#ffffff' class='uk-button uk-button-success' onclick=\"getEstimatDetail('" + data[i].TicketId + "')\"/> </td>" +
                        "</tr>"
                         );
                    }
                    $('#loading').hide();
                },
                error: function ajaxError(result) {
                    alert(result.status + ":" + result.statusText);
                }
            });
        }

        function getTicketDetail(TicketID) {

            data = "TicketID=" + TicketID;
            $.ajax({
                url: "ajax/DetailTicket.aspx",
                data: data,
                method: "POST",
                dataType:"json",
                success: function (data) {
                    $('#tableDetail tbody').empty();

                    for (i = 0 ; i < data.length; i++) {
                        $('#tableDetail tbody').append(
                        "<tr>" +
                            "<td style='text-align:center'>" + data[i].TicketLine + "</td>" +
                            "<td style='text-align:center'>" + data[i].Name + "</td>" +
                            "<td style='text-align:center'>" + data[i].Description + "</td>" +
                            "<td style='text-align:center'>" + data[i].Quantity + "</td>" +
                        "</tr>"
                         );
                    }
                    AlertModal("modalDetail");                 
                },
                error: function ajaxError(result) {
                    alert(result.status + ":" + result.statusText);
                }

            });
        }
        function getEstimatDetail(TicketID) {
            data = "TicketID=" + TicketID;
            $.ajax({
                url: "ajax/DetailEstimateLog.aspx",
                data: data,
                method: "POST",
                dataType: "json",
                success: function (data) {
                    $('#tableEstimateDetail tbody').empty();
                    for (i = 0 ; i < data.length; i++) {
                        $('#tableEstimateDetail tbody').append(
                        "<tr>" +
                            "<td style='text-align:center'>" + data[i].EstimateNo + "</td>" +
                            "<td style='text-align:center'>" + data[i].TicketId + "</td>" +
                            "<td style='text-align:center'>" + data[i].Price + "</td>" +
                            "<td style='text-align:center'>" + data[i].UserName + "</td>" +
                            "<td style='text-align:center'>" + data[i].DateCreated + "</td>" +
                        "</tr>"
                         );
                    }
                    AlertModal("modalEstimatelog");
                },
                error: function ajaxError(result) {
                    alert(result.status + ":" + result.statusText);
                }

            });
        }
    </script>
</head>
<body>
    <form id="form15" runat="server">
        <div class="uk-form">
            <div id='loading' style='display: none'>
                <img src="img/ajax-loader.gif" />
            </div><br />
            <b>การประเมินครั้งที่ 1 (ผู้จัดการสาขา)</b>
            <hr />
            <table id="tableData" class="uk-table" border="1">
                <thead>
                    <tr>
                        <td style="text-align: center">ลำดับ</td>
                        <td style="text-align: center">ตั๋ว</td>
                        <td style="text-align: center">เล่มที่</td>
                        <td style="text-align: center">เลขที่</td>
                        <td style="text-align: center">เดือน</td>
                        <td style="text-align: center">ราคารับจำนำ</td>
                        <td style="text-align: center">ประเมินราคาครั้งที่หนึ่ง</td>
                        <td style="text-align: center">ประเมินราคาครั้งที่สอง</td>
                        <td style="text-align: center; display: none">ReportNo</td>
                        <td style="text-align: center">รายละเอียดตั๋ว</td>
                        <td style="text-align: center">ประวัติการประเมิน</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <%--<input id="btnUpdate" type="button" class="uk-button uk-button-success" style="color: #ffffff" value="Update" />--%>
            <button type="submit" class="uk-button uk-button-success" style="color: #ffffff">Update</button>
        </div>
        <br />
        <br />
        <br />
        <div class="uk-modal" id="modalAlertSuccess">
            <div class="uk-modal-dialog">
                <div class="uk-modal-header uk-alert-success">แจ้งเตือน</div>
                <asp:Label ID="lblAlert" runat="server"></asp:Label>
            </div>
        </div>
        <div class="uk-modal" id="modalDetail">
            <div class="uk-modal-dialog">
                <div class="uk-modal-header uk-alert-success">รายละเอียดตั๋ว</div>
                <div id="detailTicket">
                     <table id="tableDetail" class="uk-table" border="1">
                         <thead>
                             <tr>
                                 <td style="text-align: center">
                                     ลำดับ
                                 </td>
                                 <td style="text-align: center">
                                     ประเภท
                                 </td>
                                 <td style="text-align: center">
                                     รายละเอียด
                                 </td>
                                 <td style="text-align: center">
                                     จำนวน
                                 </td>
                             </tr>
                         </thead>
                         <tbody>

                         </tbody>
                     </table>
                </div>
            </div>
        </div>
        <div class="uk-modal" id="modalEstimatelog">
            <div class="uk-modal-dialog">
                <div class="uk-modal-header uk-alert-success">รายละเอียดตั๋ว</div>
                <div id="detailTicketEstimate">
                     <table id="tableEstimateDetail" class="uk-table" border="1">
                         <thead>
                             <tr>
                                 <td style="text-align: center">
                                    ประเมินครั้งที่
                                 </td>
                                 <td style="text-align: center">
                                     ตั๋ว
                                 </td>
                                 <td style="text-align: center">
                                     ราคา
                                 </td>
                                 <td style="text-align: center">
                                     ผู้ประเมิน
                                 </td>
                                 <td style="text-align: center">
                                     วันที่
                                 </td>
                             </tr>
                         </thead>
                         <tbody>

                         </tbody>
                     </table>
                </div>
            </div>
        </div>

        <div class="uk-modal" id="modalSecret">
            <div class="uk-modal-dialog">
                <div class="uk-modal-header uk-alert-success">กรุณากรอกรหัสความปลอดภัย</div>
                <div class="uk-form uk-form-horizontal">
                    <table>
                        <tr>
                            <td></td>
                            <td>
                                <input type="text" id="confirmCode" class="uk-form-width-medium" /><input type="button" id="btnPrivateCode" class="uk-button uk-button-success" value="ยินยัน" onclick="Buy()" />
                            </td>
                        </tr>
                        <tr>
                            <td>

                            </td>
                            <td>
                                <div id='loadingmessage2' style='display: none'>
                                    <img src="img/ajax-loader.gif" />
                                </div>
                            </td>
                            <td>
                                 <p id="lblmodal"></p>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
