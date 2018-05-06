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
    public partial class Usuarios : System.Web.UI.Page
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


                    string strcon = SqlDataSourcePerfis.ConnectionString.ToString();
                    SqlConnection conexao = new SqlConnection(strcon);
                    try
                    {
                        SqlCommand cmd = new SqlCommand("select nome, cpf, data_nascimento, perfil , password from tbl_usuarios where id = @id", conexao);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                        SqlDataReader dr = null;

                        conexao.Open();
                        dr = cmd.ExecuteReader();


                        carregaDropDownListPerfis();
                        var perfil = "";

                        while (dr.Read())
                        {
                            txtNome.Text = dr["nome"].ToString();
                            txtCPF.Text = dr["cpf"].ToString();
                            txtNascimento.Text = dr["data_nascimento"].ToString();
                            txtPassword.Text = dr["password"].ToString();

                            perfil = dr["perfil"].ToString();


                            formCadastro.Visible = true;
                        }


                        ddlPerfil.SelectedIndex = Convert.ToInt32(perfil) - 1;

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
                    string strcon = SqlDataSourcePerfis.ConnectionString.ToString();
                    SqlConnection conexao = new SqlConnection(strcon);
                    try
                    {
                        SqlCommand cmd = new SqlCommand("delete from tbl_usuarios where id = @id", conexao);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["excluir"];
                        conexao.Open();
                        cmd.ExecuteNonQuery();

                        Response.Redirect("Usuarios.aspx?sucess=true");
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
            string strcon = SqlDataSourcePerfis.ConnectionString.ToString();
            SqlConnection conexao = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_usuarios (nome,cpf,data_nascimento,perfil , password) VALUES (@nome, @cpf , @data_nascimento , @perfil , @password)", conexao);
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtNome.Text;
                cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = txtCPF.Text;
                cmd.Parameters.Add("@data_nascimento", SqlDbType.VarChar).Value = txtNascimento.Text;
                cmd.Parameters.Add("@perfil", SqlDbType.Int).Value = ddlPerfil.SelectedValue;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = txtPassword.Text;

                conexao.Open();
                cmd.ExecuteNonQuery();

                txtCPF.Text = "";
                txtNascimento.Text = "";
                txtNome.Text = "";



                Response.Redirect("Usuarios.aspx?sucess=true");

            }
            catch (Exception ex)
            {
                lblInfo.Visible = true;
                lblInfo.Text = "Ops ocorreu um erro ao cadastrar usuário!!!";
                throw;
            }
            finally
            {
                conexao.Close();

            }

        }


        public void carregaDropDownListPerfis()
        {


            string strcon = SqlDataSourcePerfis.ConnectionString.ToString();
            SqlConnection conexao = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("select * from tbl_perfis", conexao);
                SqlDataReader dr = null;

                conexao.Open();
                ddlPerfil.DataSource = cmd.ExecuteReader();

                ddlPerfil.DataTextField = "tipo";
                ddlPerfil.DataValueField = "id";
                ddlPerfil.DataBind();


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

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            string strcon = SqlDataSourcePerfis.ConnectionString.ToString();
            SqlConnection conexao = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_usuarios set nome = @nome ,cpf = @cpf , data_nascimento = @data_nascimento ,perfil= @perfil , password = @password where id = @id", conexao);
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtNome.Text;
                cmd.Parameters.Add("@cpf", SqlDbType.VarChar).Value = txtCPF.Text;
                cmd.Parameters.Add("@data_nascimento", SqlDbType.VarChar).Value = txtNascimento.Text;
                cmd.Parameters.Add("@perfil", SqlDbType.Int).Value = ddlPerfil.SelectedValue;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = txtPassword.Text;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["id"];

                conexao.Open();
                cmd.ExecuteNonQuery();

                txtCPF.Text = "";
                txtNascimento.Text = "";
                txtNome.Text = "";


                btnAtualizar.Visible = false;
                btnEnviar.Visible = true;

                Response.Redirect("Usuarios.aspx?sucess=true");

            }
            catch (Exception ex)
            {
                lblInfo.Visible = true;
                lblInfo.Text = "Ops ocorreu um erro ao cadastrar usuário!!!";
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
            txtCPF.Text = "";
            txtNascimento.Text = "";
            txtNome.Text = "";
            txtPassword.Text = "";
            carregaDropDownListPerfis();
        }
    }
}