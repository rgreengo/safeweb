<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="WebApplicationSafeWeb.Categorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    
    
    <div class="card card-register mx-auto mt-5" id="gridDiv" runat="server">
        <div class="card-header">Cadastro de categorias</div>
        <div class="card-body">


            <asp:Button ID="btnNovaCategoria" runat="server" CssClas="btn btn-primary btn-block" Text="Cadastrar nova categoria" OnClick="btnNovaCategoria_Click"  />
            <br />
            <br />


            <asp:Repeater ID="rptCategorias" runat="server" DataSourceID="SqlDataSourceCategorias">
                <HeaderTemplate>
                    <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Descrição</th>                               
                                <th>Editar</th>
                                <th>Excluir</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# DataBinder.Eval(Container.DataItem, "id") %></td>
                        
                        <td><%# DataBinder.Eval(Container.DataItem, "descricao") %></td>
                        <td>
                            <a href="<%# "Categorias.aspx?id=" + DataBinder.Eval(Container.DataItem, "id")%>">Editar </a></td>
                        <td>
                            <a href="<%# "Categorias.aspx?excluir=" + DataBinder.Eval(Container.DataItem, "id")%>">Excluir </a></td>
                        </td>
                    </tr>
                </ItemTemplate>


                <FooterTemplate>
                    </table>
                </FooterTemplate>


            </asp:Repeater>

            <asp:SqlDataSource ID="SqlDataSourceCategorias" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [tbl_categorias]"></asp:SqlDataSource>

        </div>
    </div>


    <div class="card card-register mx-auto mt-5" id="formCadastro" runat="server" visible="false">
        <div class="card-header">Cadastro de Categorias</div>
        <div class="card-body">

            <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
            

            <div class="form-group">
                <label for="exampleInputDesc">Descrição</label>
                <asp:TextBox ID="txtDescricao" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescricao" runat="server"  ErrorMessage="Campo categoria requerido" ControlToValidate="txtDescricao" ></asp:RequiredFieldValidator>
            </div>

          

            

            <asp:Button ID="btnEnviar" runat="server" CssClas="btn btn-primary btn-block" Text="Enviar dados" OnClick="btnEnviar_Click" />
            <asp:Button ID="btnAtualizar" runat="server" Text="Atualizar categoria" CssClas="btn btn-primary btn-block" Visible="false" OnClick="btnAtualizar_Click"  />


        </div>
    </div>
    <br />
    <div class="card card-register mx-auto mt-5">
        <asp:Label ID="lblInfo" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="Larger"></asp:Label>
    </div>
</asp:Content>
