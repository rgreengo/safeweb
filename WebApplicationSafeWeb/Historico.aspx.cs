using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationSafeWeb
{
    public partial class Historico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {

                if (Request.QueryString["id"] != null)
                {
                    lblID.Text = Request.QueryString["id"].ToString();


                    string strcon = SqlDataSourceHistorico.ConnectionString.ToString();
                    SqlConnection conexao = new SqlConnection(strcon);
                    try
                    {
                        SqlCommand cmd = new SqlCommand("SELECT tbl_proposta_historico.Id, tbl_proposta_historico.id_usuario, tbl_proposta_historico.id_proposta, tbl_proposta_historico.data_aprovacao, tbl_proposta_historico.acao_realizada, tbl_usuarios.nome, tbl_propostas2.descricao, tbl_propostas2.valor, tbl_acao.acao, tbl_propostas2.nome AS propostaNome FROM tbl_proposta_historico INNER JOIN tbl_usuarios ON tbl_proposta_historico.id_usuario = tbl_usuarios.Id INNER JOIN tbl_propostas2 ON tbl_proposta_historico.id_proposta = tbl_propostas2.Id AND tbl_usuarios.Id = tbl_propostas2.usuario INNER JOIN tbl_acao ON tbl_proposta_historico.acao_realizada = tbl_acao.Id where tbl_proposta_historico.Id = @id", conexao);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                        SqlDataReader dr = null;

                        conexao.Open();
                        dr = cmd.ExecuteReader();



                        while (dr.Read())
                        {
                            txtNome.Text = dr["nome"].ToString();
                            txtAcao.Text = dr["acao"].ToString();
                            txtValorProposta.Text = dr["valor"].ToString();
                            txtDataModificacao.Text = dr["data_aprovacao"].ToString();
                        }


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


            }

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}