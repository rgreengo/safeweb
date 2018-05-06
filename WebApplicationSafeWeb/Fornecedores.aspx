<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fornecedores.aspx.cs" Inherits="WebApplicationSafeWeb.Fornecedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




    
    <div class="card card-register mx-auto mt-5" id="gridDiv" runat="server">
        <div class="card-header">Cadastro de fornecedores</div>
        <div class="card-body">


            <asp:Button ID="btnNovoUsuario" runat="server" CssClas="btn btn-primary btn-block" Text="Cadastrar novo fornecedor" OnClick="btnNovoUsuario_Click" />
            <br />
            <br />


            <asp:Repeater ID="rptFornecedores" runat="server" DataSourceID="SqlDataSourceFornecedores">
                <HeaderTemplate>
                    <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>CPF/CNPJ</th>
                                <th>Nome</th>
                                <th>Telefone</th>
                                <th>Email</th>
                                <th>Editar</th>
                                <th>Excluir</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# DataBinder.Eval(Container.DataItem, "id") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "cnpj") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "nome") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "telefone") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "email") %></td>
                        <td>
                            <a href="<%# "Fornecedores.aspx?id=" + DataBinder.Eval(Container.DataItem, "id")%>">Editar </a></td>
                        <td>
                            <a href="<%# "Fornecedores.aspx?excluir=" + DataBinder.Eval(Container.DataItem, "id")%>">Excluir </a></td>
                        </td>
                    </tr>
                </ItemTemplate>


                <FooterTemplate>
                    </table>
                </FooterTemplate>


            </asp:Repeater>

            <asp:SqlDataSource ID="SqlDataSourceFornecedores" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [tbl_fornecedores]"></asp:SqlDataSource>

        </div>
    </div>


    <div class="card card-register mx-auto mt-5" id="formCadastro" runat="server" visible="false">
        <div class="card-header">Cadastro de Usuários</div>
        <div class="card-body">

            <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
            <div class="form-group">
                <label for="exampleInputEmail1">CPF / CNPJ</label>
                <asp:TextBox ID="txtCnpj" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="exampleInputEmail1">Nome</label>
                <asp:TextBox ID="txtNome" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="exampleInputEmail1">Telefone</label>
                <asp:TextBox ID="txtTelefone" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

             <div class="form-group">
                <label for="exampleInputEmail1">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            

            <asp:Button ID="btnEnviar" runat="server" CssClas="btn btn-primary btn-block" Text="Enviar dados" OnClick="btnEnviar_Click" />
            <asp:Button ID="btnAtualizar" runat="server" Text="Atualizar fornecedor" CssClas="btn btn-primary btn-block" Visible="false" OnClick="btnAtualizar_Click" />


        </div>
    </div>
    <br />
    <div class="card card-register mx-auto mt-5">
        <asp:Label ID="lblInfo" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="Larger"></asp:Label>
    </div>
</asp:Content>
