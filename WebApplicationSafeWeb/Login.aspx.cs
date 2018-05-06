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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string strcon = SqlDataSourceLogin.ConnectionString.ToString();
            SqlConnection conexao = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("select nome , password , perfil from tbl_usuarios where nome = @nome", conexao);

                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = txtLogin.Text.Trim();
                SqlDataReader dr = null;

                conexao.Open();
                
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {

                    var password = "";
                    var perfil = "";

                    while (dr.Read())
                    {
                        password = dr["password"].ToString();
                        perfil = dr["perfil"].ToString();
                    }

                    if(password == txtPassword.Text)
                    {
                        Session["logado"] = "true";
                        Session["nome"] = txtLogin.Text;
                        Session["perfil"] = perfil.ToString();

                        Response.Redirect("Default.aspx");
                    }
                }
                else
                {
                    lblInfo.Text = "Ops ocorreu um erro ao efetuar login";
                    lblInfo.Visible = true;
                    Session["logado"] = "false";

                    Session["nome"] = "";
                    Session["perfil"] = "";

                    txtLogin.Text = "";
                    txtPassword.Text = "";
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
    }
}