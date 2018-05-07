<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Historico.aspx.cs" Inherits="WebApplicationSafeWeb.Historico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">





    <div class="card card-register mx-auto mt-5" id="gridDiv" runat="server">
        <div class="card-header">Histórico de propostas</div>
        <div class="card-body">


            <asp:Button ID="btnVoltar" runat="server" CssClas="btn btn-primary btn-block" Text="Voltar" OnClick="btnVoltar_Click" />
            <br />
            <br />


            <asp:Repeater ID="rptHistorico" runat="server" DataSourceID="SqlDataSourceHistorico">
                <HeaderTemplate>
                    <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Usuário</th>
                                <th>Ação realizada</th>
                                <th>Data Modificação</th>
                                <th>Visualizar</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# DataBinder.Eval(Container.DataItem, "Id") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "nome") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "acao") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "data_aprovacao") %></td>

                        <td>
                            <a href="<%# "Historico.aspx?id=" + DataBinder.Eval(Container.DataItem, "Id")%>">Visualizar </a></td>

                    </tr>
                </ItemTemplate>


                <FooterTemplate>
                    </table>
                </FooterTemplate>


            </asp:Repeater>

            <asp:SqlDataSource ID="SqlDataSourceHistorico" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT tbl_proposta_historico.Id, tbl_proposta_historico.id_usuario, tbl_proposta_historico.id_proposta, tbl_proposta_historico.data_aprovacao, tbl_proposta_historico.acao_realizada, tbl_usuarios.nome, tbl_propostas2.descricao, tbl_propostas2.valor, tbl_acao.acao, tbl_propostas2.nome AS propostaNome, tbl_perfis.tipo FROM tbl_proposta_historico INNER JOIN tbl_usuarios ON tbl_proposta_historico.id_usuario = tbl_usuarios.Id INNER JOIN tbl_propostas2 ON tbl_proposta_historico.id_proposta = tbl_propostas2.Id AND tbl_usuarios.Id = tbl_propostas2.usuario INNER JOIN tbl_acao ON tbl_proposta_historico.acao_realizada = tbl_acao.Id INNER JOIN tbl_perfis ON tbl_usuarios.perfil = tbl_perfis.Id"></asp:SqlDataSource>

        </div>
    </div>


    <div class="card card-register mx-auto mt-5" id="formCadastro" runat="server" visible="false">
        <div class="card-header">Cadastro de Usuários</div>
        <div class="card-body">

            <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
            <div class="form-group">
                <label for="exampleInputNome">Nome completo</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNome" runat="server" ErrorMessage="campo requerido" ControlToValidate="txtNome"></asp:RequiredFieldValidator>
                &nbsp;<asp:TextBox ID="txtNome" ReadOnly="true" CssClass="form-control" runat="server">
                </asp:TextBox>
            </div>



             <div class="form-group">
                <label for="InputNascimento">Ação realizada</label>
                <asp:TextBox ID="txtAcao" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
            </div>


            <div class="form-group">
                <label for="InputValor">Valor da proposta</label>
                <asp:TextBox ID="txtValorProposta" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="InputData">Data da modificação</label>
                <asp:TextBox ID="txtDataModificacao" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="InputData">Nível de quem alterou (Perfil)</label>
                <asp:TextBox ID="txtPerfil" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
            </div>


        </div>
    </div>
    <br />
    <div class="card card-register mx-auto mt-5">
        <asp:Label ID="lblInfo" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="Larger"></asp:Label>
    </div>
</asp:Content>
