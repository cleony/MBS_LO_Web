<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="mixproadmin.aspx.vb" Inherits="MBS_Loan.mixproadmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">กำหนดค่าสำหรับ mixpro</h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <label class="col-sm-3 control-label">สาขา</label>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddlBranch" runat="server"   class="form-control" Style="width: 100%;">
                              
                            </asp:DropDownList>
                        </div>

                        <div class="col-sm-2">
                            <asp:Button ID="btnUpdate" runat="server" Text="update" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                     <div class="form-group">
                        <label class="col-sm-3 control-label"></label>
                        
                        <div class="col-sm-2">
                            <asp:Button ID="btnAlterDB" runat="server" Text="ปรับปรุงดาต้าเบส" OnClick="btnAlterDB_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</asp:Content>
