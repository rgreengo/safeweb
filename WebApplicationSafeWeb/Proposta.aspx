<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proposta.aspx.cs" Inherits="WebApplicationSafeWeb.Propostas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




    <div class="card card-register mx-auto mt-5" id="gridDiv" runat="server">
        <div class="card-header">Cadastro de Propostas</div>
        <div class="card-body">


            <asp:Button ID="btnNovaProposta" runat="server" CssClas="btn btn-primary btn-block" Text="Cadastrar nova proposta" OnClick="btnNovaProposta_Click" />
            <br />
            <br />


            <asp:Repeater ID="rptPropostas" runat="server" DataSourceID="SqlDataSourceProposta">
                <HeaderTemplate>
                    <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Nome</th>
                                <th>Fornecedor</th>
                                <th>Data</th>
                                <th>Valor</th>
                                <th>Aprovada</th>
                                <th>Editar</th>
                                <th>Excluir</th>
                                <th>Arquivo Proposta</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# DataBinder.Eval(Container.DataItem, "id") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "NomeProposta") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "nome") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "data_proposta") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "valor") %></td>
                        <td><%# DataBinder.Eval(Container.DataItem, "aprovada") %></td>
                        <td>
                            <a href="<%# "Proposta.aspx?id=" + DataBinder.Eval(Container.DataItem, "id")%>">Editar</a></td>
                        <td>
                            <a href="<%# "Proposta.aspx?excluir=" + DataBinder.Eval(Container.DataItem, "id")%>">Excluir</a></td>
                        </td>
                        <td>
                            <a target="_blank" href="<%# "Propostas_Arquivos\\" + DataBinder.Eval(Container.DataItem, "arquivo")%>"><%# DataBinder.Eval(Container.DataItem, "arquivo") %></a>
                        </td>
                    </tr>
                </ItemTemplate>


                <FooterTemplate>
                    </table>
                </FooterTemplate>


            </asp:Repeater>

            <asp:SqlDataSource ID="SqlDataSourceProposta" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT tbl_categorias.descricao, tbl_fornecedores.nome, tbl_propostas2.Id, tbl_propostas2.data_proposta, tbl_propostas2.aprovada, tbl_propostas2.valor, tbl_propostas2.descricao AS descricao, tbl_propostas2.arquivo, tbl_propostas2.vencida, tbl_propostas2.nome AS NomeProposta FROM tbl_fornecedores INNER JOIN tbl_propostas2 ON tbl_fornecedores.Id = tbl_propostas2.fornecedor INNER JOIN tbl_categorias ON tbl_propostas2.categoria = tbl_categorias.Id"></asp:SqlDataSource>

        </div>
    </div>


    <div class="card card-register mx-auto mt-5" id="formCadastro" runat="server" visible="false">
        <div class="card-header">Cadastro de propostas</div>
        <div class="card-body">

            <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
            <div class="form-group">
                <label for="InputNome">Nome proposta</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNome" runat="server" ErrorMessage="campo requerido" ControlToValidate="txtNome"></asp:RequiredFieldValidator>
                &nbsp;<asp:TextBox ID="txtNome" CssClass="form-control" runat="server">
                </asp:TextBox>



            </div>

            <div class="form-group">
                <label for="InputValor">Valor</label>
                <asp:TextBox ID="txtValor" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group" id="divData" runat="server">
                <label for="InputData">Data proposta</label>
                <asp:TextBox ID="txtDataProposta" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>

            </div>

            <div class="form-group">
                <label for="InputFornecedor">Fornecedor</label>
                <asp:DropDownList ID="ddlFornecedores" runat="server" DataSourceID="SqlDataSourceFornecedores" DataTextField="nome" DataValueField="Id"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceFornecedores" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Id], [nome] FROM [tbl_fornecedores]"></asp:SqlDataSource>
            </div>

            <div class="form-group">
                <label for="InputFornecedor">Categoria</label>
                <asp:DropDownList ID="ddlCategorias" runat="server" DataSourceID="SqlDataSourceCategorias" DataTextField="descricao" DataValueField="Id"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceCategorias" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Id], [descricao] FROM [tbl_categorias]"></asp:SqlDataSource>
            </div>


            <div class="form-group">
                <label for="InputArquivo">Arquivo Proposta</label>
                <input type="file" id="FileArquivo" name="FileArquivo" class="form-control form-control-file" runat="server" />
            </div>

            <div class="form-group">
                <label for="InputAcao">Ação</label>
                <asp:DropDownList ID="ddlAcao" runat="server" DataSourceID="SqlDataSourceAcao" DataTextField="acao" DataValueField="Id"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceAcao" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Id], [acao] FROM [tbl_acao]"></asp:SqlDataSource>
            </div>


            <div class="form-group" id="divVencida" runat="server">
                <label for="InputAprovada">Vencida</label>
                <asp:CheckBox ID="chkVencida" runat="server" />
            </div>

            <div class="form-group">
                <label for="InputDesc">Descrição</label>
                <asp:TextBox ID="txtDescricao" TextMode="MultiLine" CssClass="form-control" runat="server" Height="225px"></asp:TextBox>
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
