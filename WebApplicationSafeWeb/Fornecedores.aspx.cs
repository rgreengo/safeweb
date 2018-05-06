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
    public partial class Fornecedores : System.Web.UI.Page
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
                        SqlCommand cmd = new SqlCommand("select cnpj , nome , telefone , email from tbl_fornecedores where id = @id", conexao);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                        SqlDataReader dr = null;

                        conexao.Open();
                        dr = cmd.ExecuteReader();



                        while (dr.Read())
                        {
                            
                            txtCnpj.Text = dr.GetString(0);
                            txtNome.Text = dr.GetString(1);
                            txtTelefone.Text = dr.GetString(2);
                            txtEmail.Text = dr.GetString(3);

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
                    string strcon = SqlDataSourceFornecedores.ConnectionString.ToString();
                    SqlConnection conexao = new SqlConnection(strcon);
                    try
                    {
                        SqlCommand cmd = new SqlCommand("delete from tbl_fornecedores where id = @id", conexao);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["excluir"];
                        conexao.Open();
                        cmd.ExecuteNonQuery();

                        Response.Redirect("Fornecedores.aspx?sucess=true");
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
            string strcon = SqlDataSourceFornecedores.ConnectionString.ToString();
            SqlConnection conexao = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_fornecedores (cnpj ,nome ,telefone ,email) VALUES (@cnpj, @nome , @telefone , @email )", conexao);
                cmd.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = txtCnpj.Text;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtNome.Text;
                cmd.Parameters.Add("@telefone", SqlDbType.VarChar).Value = txtTelefone.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;

                conexao.Open();
                cmd.ExecuteNonQuery();

               



                Response.Redirect("Fornecedores.aspx?sucess=true");

            }
            catch (Exception ex)
            {
                lblInfo.Visible = true;
                lblInfo.Text = "Ops ocorreu um erro ao cadastrar fornecedor!!!";
                throw;
            }
            finally
            {
                conexao.Close();

            }
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {


            string strcon = SqlDataSourceFornecedores.ConnectionString.ToString();
            SqlConnection conexao = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_fornecedores set cnpj = @cnpj , nome = @nome , telefone = @telefone , email = @email where id = @id", conexao);
                cmd.Parameters.Add("@cnpj", SqlDbType.VarChar).Value = txtCnpj.Text;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtNome.Text;
                cmd.Parameters.Add("@telefone", SqlDbType.VarChar).Value = txtTelefone.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["id"];

                conexao.Open();
                cmd.ExecuteNonQuery();


                txtEmail.Text = "";
                txtCnpj.Text = "";
                txtTelefone.Text = "";
                txtNome.Text = "";


                btnAtualizar.Visible = false;
                btnEnviar.Visible = true;

                


                Response.Redirect("Fornecedores.aspx?sucess=true");

            }
            catch (Exception ex)
            {
                lblInfo.Visible = true;
                lblInfo.Text = "Ops ocorreu um erro ao cadastrar fornecedor!!!";
                throw;
            }
            finally
            {
                conexao.Close();

            }
        }

        protected void btnNovoUsuario_Click(object sender, EventArgs e)
        {
            gridDiv.Visible = false;
            formCadastro.Visible = true;
            btnAtualizar.Visible = false;
            txtTelefone.Text = "";
            txtCnpj.Text = "";
            txtNome.Text = "";
           
        }
    }
}