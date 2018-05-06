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
    public partial class Categorias : System.Web.UI.Page
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


                    string strcon = SqlDataSourceCategorias.ConnectionString.ToString();
                    SqlConnection conexao = new SqlConnection(strcon);
                    try
                    {
                        SqlCommand cmd = new SqlCommand("select descricao from tbl_categorias where id = @id", conexao);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                        SqlDataReader dr = null;

                        conexao.Open();
                        dr = cmd.ExecuteReader();



                        while (dr.Read())
                        {
                            txtDescricao.Text = dr.GetString(0);
                            
                            formCadastro.Visible = true;
                        }


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
                        SqlCommand cmd = new SqlCommand("delete from tbl_categorias where id = @id", conexao);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["excluir"];
                        conexao.Open();
                        cmd.ExecuteNonQuery();

                        Response.Redirect("Categorias.aspx?sucess=true");
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

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string strcon = SqlDataSourceCategorias.ConnectionString.ToString();
            SqlConnection conexao = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_categorias (descricao) VALUES (@descricao )", conexao);
                cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = txtDescricao.Text;
                

                conexao.Open();
                cmd.ExecuteNonQuery();





                Response.Redirect("Categorias.aspx?sucess=true");

            }
            catch (Exception ex)
            {
                lblInfo.Visible = true;
                lblInfo.Text = "Ops ocorreu um erro ao cadastrar categoria!!!";
                throw;
            }
            finally
            {
                conexao.Close();

            }
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            string strcon = SqlDataSourceCategorias.ConnectionString.ToString();
            SqlConnection conexao = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_categorias set descricao = @descricao where id = @id", conexao);


                cmd.Parameters.Add("@descricao", SqlDbType.VarChar).Value = txtDescricao.Text;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["id"];

                conexao.Open();
                cmd.ExecuteNonQuery();

                txtDescricao.Text = "";


                btnAtualizar.Visible = false;
                btnEnviar.Visible = true;




                Response.Redirect("Categorias.aspx?sucess=true");

            }
            catch (Exception ex)
            {
                lblInfo.Visible = true;
                lblInfo.Text = "Ops ocorreu um erro ao cadastrar categoria!!!";
                throw;
            }
            finally
            {
                conexao.Close();

            }
        }

        protected void btnNovaCategoria_Click(object sender, EventArgs e)
        {
            gridDiv.Visible = false;
            formCadastro.Visible = true;
            btnAtualizar.Visible = false;
            txtDescricao.Text = "";
            
        }
    }
}