﻿using System;
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
                        SqlCommand cmd = new SqlCommand("SELECT tbl_categorias.descricao, tbl_fornecedores.nome, tbl_propostas2.Id, tbl_propostas2.data_proposta, tbl_propostas2.aprovada, tbl_propostas2.valor, tbl_propostas2.descricao AS descricao, tbl_propostas2.arquivo, tbl_propostas2.vencida, tbl_propostas2.nome AS NomeProposta FROM tbl_fornecedores INNER JOIN tbl_propostas2 ON tbl_fornecedores.Id = tbl_propostas2.fornecedor INNER JOIN tbl_categorias ON tbl_propostas2.categoria = tbl_categorias.Id where tbl_propostas2.Id = @id", conexao);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["id"];
                        SqlDataReader dr = null;

                        conexao.Open();
                        dr = cmd.ExecuteReader();



                        while (dr.Read())
                        {
                            txtValor.Text = dr["valor"].ToString();
                            txtNome.Text = dr["NomeProposta"].ToString();
                            txtDescricao.Text = dr["descricao"].ToString();


                            chkAprovada.Checked = Convert.ToBoolean(dr["aprovada"].ToString());

                            var dataProposta = dr["data_proposta"].ToString();

                        }

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
                        SqlCommand cmd = new SqlCommand("delete from tbl_propostas where id = @id", conexao);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["excluir"];
                        conexao.Open();
                        cmd.ExecuteNonQuery();

                        Response.Redirect("Propostas.aspx?sucess=true");
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
                SqlCommand cmd = new SqlCommand("INSERT into tbl_propostas2 (categoria , fornecedor , data_proposta , aprovada , valor , descricao , arquivo , vencida , nome) VALUES (@categoria , @fornecedor , @data_proposta , @aprovada , @valor , @descricao , @arquivo , @vencida , @nome )", conexao);


                cmd.Parameters.Add("@categoria", SqlDbType.VarChar).Value = ddlCategorias.SelectedValue;

                cmd.Parameters.Add("@fornecedor", SqlDbType.VarChar).Value = ddlFornecedores.SelectedValue;


                cmd.Parameters.Add("@data_proposta", SqlDbType.VarChar).Value = txtDataProposta.Text;
                cmd.Parameters.Add("@aprovada", SqlDbType.Bit).Value = chkAprovada.Checked;

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

                conexao.Open();

                cmd.ExecuteNonQuery();





                Response.Redirect("Proposta.aspx?sucess=true");

            }
            catch (Exception ex)
            {
                lblInfo.Visible = true;
                lblInfo.Text = "Ops ocorreu um erro ao cadastrar proposta!!!";
                throw;
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
                SqlCommand cmd = new SqlCommand("Update tbl_propostas2 set categoria = @categoria , fornecedor = @fornecedor , data_proposta = data_proposta , aprovada = @aprovada , valor = @valor , descricao = @descricao , arquivo = @arquivo , vencida = @vencida , nome = @nome where Id = @id", conexao);


                cmd.Parameters.Add("@categoria", SqlDbType.VarChar).Value = ddlCategorias.SelectedValue;

                cmd.Parameters.Add("@fornecedor", SqlDbType.VarChar).Value = ddlFornecedores.SelectedValue;


                cmd.Parameters.Add("@data_proposta", SqlDbType.VarChar).Value = txtDataProposta.Text;
                cmd.Parameters.Add("@aprovada", SqlDbType.Bit).Value = chkAprovada.Checked;

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

                conexao.Open();

                cmd.ExecuteNonQuery();





                Response.Redirect("Proposta.aspx?sucess=true");

            }
            catch (Exception ex)
            {
                lblInfo.Visible = true;
                lblInfo.Text = "Ops ocorreu um erro ao cadastrar proposta!!!";
                throw;
            }
            finally
            {
                conexao.Close();

            }
        }
    }
}