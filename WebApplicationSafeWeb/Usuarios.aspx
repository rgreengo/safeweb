<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="WebApplicationSafeWeb.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="card card-register mx-auto mt-5" id="gridDiv" runat="server">
        <div class="card-header">Cadastro de Usuários</div>
        <div class="card-body">


            <asp:Button ID="btnNovoUsuario" runat="server" CssClas="btn btn-primary btn-block" Text="Cadastrar novo usuário" OnClick="btnNovoUsuario_Click" />
            <br />
            <br />


            <asp:Repeater ID="rptUsuarios" runat="server" DataSourceID="SqlDataSourceUsuarios">
                <HeaderTemplate>
                    <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Nome</th>
                                <th>CPF</th>
                                <th>Nascimento</th>
                                <th>Perfil</th>
                                <th>Editar</th>
                                <th>Excluir</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# DataBinder.Eval(Container.DataItem, "id") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "Nome") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "cpf") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "data_nascimento") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "tipo") %></td>
                        <td>
                            <a href="<%# "Usuarios.aspx?id=" + DataBinder.Eval(Container.DataItem, "id")%>">Editar </a></td>
                        <td>
                            <a href="<%# "Usuarios.aspx?excluir=" + DataBinder.Eval(Container.DataItem, "id")%>">Excluir </a></td>
                        </td>
                    </tr>
                </ItemTemplate>


                <FooterTemplate>
                    </table>
                </FooterTemplate>


            </asp:Repeater>

            <asp:SqlDataSource ID="SqlDataSourceUsuarios" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT tbl_perfis.tipo, tbl_usuarios.Id, tbl_usuarios.nome, tbl_usuarios.cpf, tbl_usuarios.perfil, tbl_usuarios.data_nascimento FROM tbl_perfis INNER JOIN tbl_usuarios ON tbl_perfis.Id = tbl_usuarios.perfil"></asp:SqlDataSource>

        </div>
    </div>


    <div class="card card-register mx-auto mt-5" id="formCadastro" runat="server" visible="false">
        <div class="card-header">Cadastro de Usuários</div>
        <div class="card-body">

            <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
            <div class="form-group">
                <label for="exampleInputNome">Nome completo</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNome" runat="server" ErrorMessage="campo requerido" ControlToValidate="txtNome"></asp:RequiredFieldValidator>
&nbsp;<asp:TextBox ID="txtNome" CssClass="form-control" runat="server">
                </asp:TextBox>


                
            </div>

            <div class="form-group">
                <label for="InputNome">CPF</label>
                <asp:TextBox ID="txtCPF" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="InputNascimento">Data Nascimento</label>
                <asp:TextBox ID="txtNascimento" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
            </div>


            <div class="form-group">
                <label for="InputNascimento">Password</label>
                <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="InputProposta">Perfil</label>
                <asp:DropDownList ID="ddlPerfil" CssClass="form-control" runat="server"></asp:DropDownList>

                <asp:SqlDataSource ID="SqlDataSourcePerfis" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Id], [tipo] FROM [tbl_perfis]"></asp:SqlDataSource>

            </div>

            <asp:Button ID="btnEnviar" runat="server" CssClas="btn btn-primary btn-block" Text="Enviar dados" OnClick="btnEnviar_Click" />
            <asp:Button ID="btnAtualizar" runat="server" Text="Atualizar usuário" CssClas="btn btn-primary btn-block" Visible="false" OnClick="btnAtualizar_Click" />


        </div>
    </div>
    <br />
    <div class="card card-register mx-auto mt-5">
        <asp:Label ID="lblInfo" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="Larger"></asp:Label>
    </div>

</asp:Content>
