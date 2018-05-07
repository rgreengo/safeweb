using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationSafeWeb
{
    public partial class Propostas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                if (Request.QueryString["sucess"] == "true")
                {
                    lblInfo.Visible = true;
                    lblInfo.Text = "Operação realizada com sucesso!!!";
                    formCadastro.Visible = false;
                }

                if (Request.QueryString["id"] != null)
                {
                    lblID.Text = Request.QueryString["id"].ToString();
                    btnAtualizar.Visible = true;
                    btnEnviar.Visible = false;


                    string strcon = SqlDataSourceFornecedores.ConnectionString.ToString();
                    SqlConnection conexao = new SqlConnection(strcon);
                    try
                    {
                        SqlCommand cmd = new SqlCommand("SELECT tbl_propostas2.acao , tbl_propostas2.categoria ,tbl_propostas2.fornecedor, tbl_categorias.descricao, tbl_fornecedores.nome, tbl_propostas2.Id, tbl_propostas2.data_proposta, tbl_propostas2.valor, tbl_propostas2.descricao AS descricao, tbl_propostas2.arquivo, tbl_propostas2.vencida, tbl_propostas2.nome AS NomeProposta FROM tbl_fornecedores INNER JOIN tbl_propostas2 ON tbl_fornecedores.Id = tbl_propostas2.fornecedor INNER JOIN tbl_categorias ON tbl_propostas2.categoria = tbl_categorias.Id where tbl_propostas2.Id = @id", conexao);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                        SqlDataReader dr = null;

                        conexao.Open();
                        dr = cmd.ExecuteReader();


                        //var nivelQueAprovou = "";
                        var idProposta = "";
                        //verifica se analista financeiro aprovou
                        var fornecedor_id = 0;
                        var categoria_id = 0;
                        var acao_id = 0;

                        //SqlCommand cmdPropostaHistorico = new SqlCommand("select tbl_usuarios.perfil , tbl_perfis.tipo  from tbl_proposta_aprovada join tbl_usuarios on tbl_proposta_aprovada.id_usuario = tbl_usuarios.Id join tbl_perfis on tbl_usuarios.perfil = tbl_perfis.Id ")

                        while (dr.Read())
                        {
                            txtValor.Text = dr["valor"].ToString();
                            txtNome.Text = dr["NomeProposta"].ToString();
                            txtDescricao.Text = dr["descricao"].ToString();
                            idProposta = dr["Id"].ToString();
                            var dataProposta = dr["data_proposta"].ToString();
                            fornecedor_id = Convert.ToInt32(dr["fornecedor"]);
                            categoria_id = Convert.ToInt32(dr["categoria"]);
                            acao_id = Convert.ToInt32(dr["acao"]);
                        }


                        lblID.Text = idProposta;

                        ddlFornecedores.SelectedIndex = Convert.ToInt32(fornecedor_id) - 1;
                        ddlCategorias.SelectedIndex = Convert.ToInt32(categoria_id) - 1;
                        ddlAcao.SelectedIndex = Convert.ToInt32(acao_id) - 1;

                        lblID.Visible = false;
                        divVencida.Visible = false;
                        divData.Visible = false;
                        formCadastro.Visible = true;

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    finally
                    {
                        conexao.Close();

                    }

                }

                if (Request.QueryString["excluir"] != null)
                {
                    string strcon = SqlDataSourceCategorias.ConnectionString.ToString();
                    SqlConnection conexao = new SqlConnection(strcon);
                    try
                    {
                        SqlCommand cmd = new SqlCommand("delete from tbl_propostas2 where id = @id", conexao);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["excluir"];
                        conexao.Open();
                        cmd.ExecuteNonQuery();

                        Response.Redirect("Proposta.aspx?sucess=true");
                    }

                    catch (Exception ex)
                    {

                        throw;
                    }
                    finally
                    {
                        conexao.Close();

                    }
                }

            }


        }

        protected void btnNovaProposta_Click(object sender, EventArgs e)
        {
            gridDiv.Visible = false;
            formCadastro.Visible = true;
            btnAtualizar.Visible = false;
            txtDescricao.Text = "";

            txtDataProposta.Text = "";

            txtValor.Text = "";
            txtNome.Text = "";

            divVencida.Visible = false;
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {


            string strcon = SqlDataSourceProposta.ConnectionString.ToString();
            SqlConnection conexao = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT into tbl_propostas2 (categoria , fornecedor , data_proposta , valor , descricao , arquivo , vencida , nome , acao , usuario) VALUES (@categoria , @fornecedor , @data_proposta ,  @valor , @descricao , @arquivo , @vencida , @nome , @acao , @usuario) SELECT SCOPE_IDENTITY() ", conexao);


                cmd.Parameters.Add("@categoria", SqlDbType.Int).Value = ddlCategorias.SelectedValue;

                cmd.Parameters.Add("@fornecedor", SqlDbType.Int).Value = ddlFornecedores.SelectedValue;


                cmd.Parameters.Add("@data_proposta", SqlDbType.DateTime).Value = txtDataProposta.Text;


                cmd.Parameters.Add("@valor", SqlDbType.VarChar).Value = txtValor.Text;

                cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = txtDescricao.Text;

                if ((FileArquivo.PostedFile != null) && (FileArquivo.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileArquivo.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Propostas_Arquivos") + "\\" + fn;


                    cmd.Parameters.Add("@arquivo", SqlDbType.VarChar).Value = FileArquivo.PostedFile.FileName;

                    try
                    {
                        FileArquivo.PostedFile.SaveAs(SaveLocation);

                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }


                }
                else
                {
                    cmd.Parameters.Add("@arquivo", SqlDbType.VarChar).Value = "";
                }






                cmd.Parameters.Add("@vencida", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtNome.Text;


                if (Convert.ToInt32(txtValor.Text) > 10000)
                {
                    if (Convert.ToInt32(Session["perfil"]) != 3) //se for diferente de diretor não deixa aprovar valor alto
                        cmd.Parameters.Add("@acao", SqlDbType.Int).Value = 3; //valor muito alto == pendente diretoria
                }
                else
                {
                    cmd.Parameters.Add("@acao", SqlDbType.Int).Value = ddlAcao.SelectedValue;
                }

                cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = Session["id_usuario"].ToString();

                conexao.Open();




                int id_inserido = Convert.ToInt32(cmd.ExecuteScalar());

                if (id_inserido > 0)
                {


                    //Cadastra no historico
                    SqlCommand cmdHistorico = new SqlCommand("INSERT into tbl_proposta_historico (id_usuario , id_proposta , data_aprovacao , acao_realizada) VALUES (@id_usuario , @id_proposta , @data_aprovacao , @acao_realizada )", conexao);

                    cmdHistorico.Parameters.Add("@id_usuario", SqlDbType.Int).Value = Session["id_usuario"].ToString();
                    cmdHistorico.Parameters.Add("@id_proposta", SqlDbType.Int).Value = id_inserido.ToString();

                    cmdHistorico.Parameters.Add("@data_aprovacao", SqlDbType.DateTime).Value = System.DateTime.Now.ToString();
                    cmdHistorico.Parameters.Add("@acao_realizada", SqlDbType.Int).Value = ddlAcao.SelectedValue;

                    cmdHistorico.ExecuteNonQuery();
                }

                Response.Redirect("Proposta.aspx?sucess=true");

            }
            catch (Exception ex)
            {
                lblInfo.Visible = true;
                lblInfo.Text = "Ops ocorreu um erro ao cadastrar proposta!!!";

            }
            finally
            {
                conexao.Close();

            }
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {



            string strcon = SqlDataSourceProposta.ConnectionString.ToString();
            SqlConnection conexao = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("Update tbl_propostas2 set categoria = @categoria , fornecedor = @fornecedor , data_proposta = data_proposta , valor = @valor , descricao = @descricao , arquivo = @arquivo , vencida = @vencida , nome = @nome , acao = @acao , usuario = @usuario where Id = @id", conexao);


                cmd.Parameters.Add("@categoria", SqlDbType.VarChar).Value = ddlCategorias.SelectedValue;

                cmd.Parameters.Add("@fornecedor", SqlDbType.VarChar).Value = ddlFornecedores.SelectedValue;

                cmd.Parameters.Add("@data_proposta", SqlDbType.VarChar).Value = txtDataProposta.Text;

                cmd.Parameters.Add("@valor", SqlDbType.VarChar).Value = txtValor.Text;

                cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = txtDescricao.Text;

                if ((FileArquivo.PostedFile != null) && (FileArquivo.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileArquivo.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Propostas_Arquivos") + "\\" + fn;


                    cmd.Parameters.Add("@arquivo", SqlDbType.VarChar).Value = FileArquivo.PostedFile.FileName;

                    try
                    {
                        FileArquivo.PostedFile.SaveAs(SaveLocation);

                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }


                }
                else
                {
                    cmd.Parameters.Add("@arquivo", SqlDbType.VarChar).Value = "";
                }



                cmd.Parameters.Add("@vencida", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtNome.Text;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(lblID.Text);

                if (Convert.ToInt32(txtValor.Text) > 10000)
                {
                    if (Convert.ToInt32(Session["perfil"]) != 3) //se for diferente de diretor não deixa aprovar valor alto
                        cmd.Parameters.Add("@acao", SqlDbType.Int).Value = 3; //valor muito alto == pendente diretoria
                }
                else
                {
                    cmd.Parameters.Add("@acao", SqlDbType.Int).Value = ddlAcao.SelectedValue;
                }


                cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = Session["id_usuario"].ToString();

                conexao.Open();

                cmd.ExecuteNonQuery();


                //insere na tabela historico
                SqlCommand cmdAcao = new SqlCommand("insert into tbl_proposta_historico (id_usuario , id_proposta ,  data_aprovacao , acao_realizada ) values ( @id_usuario , @id_proposta , @data_aprovacao , @acao_realizada)", conexao);
                cmdAcao.Parameters.Add("@id_usuario", SqlDbType.Int).Value = Session["id_usuario"].ToString();

                cmdAcao.Parameters.Add("@id_proposta", SqlDbType.Int).Value = Convert.ToInt32(lblID.Text);
                cmdAcao.Parameters.Add("@data_aprovacao", SqlDbType.DateTime).Value = System.DateTime.Now;

                cmdAcao.Parameters.Add("@acao_realizada", SqlDbType.Int).Value = ddlAcao.SelectedValue;

                cmdAcao.ExecuteNonQuery();

                Response.Redirect("Proposta.aspx?sucess=true");

            }
            catch (Exception ex)
            {
                lblInfo.Visible = true;
                lblInfo.Text = "Ops ocorreu um erro ao cadastrar proposta!!!";

            }
            finally
            {
                conexao.Close();

            }
        }
    }
}