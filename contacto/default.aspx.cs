using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class _default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (Page.IsPostBack) return;
    }
	
    protected void formError(string estado)
    {
        lblEstado.Text = estado;
        lblEstado.Visible = true;
    }

    public static bool verificarCorreo(string correo)
    {
        string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        if (System.Text.RegularExpressions.Regex.IsMatch(correo, expresion))
        {
            if (System.Text.RegularExpressions.Regex.Replace(correo, expresion, String.Empty).Length == 0)
                return true;
            else
                return false;
        }
        else
            return false;
    }

	protected void btnAceptar_Click(object sender, EventArgs e)
	{
        if (txtNombre.Text.Trim() == "") { formError("Por favor completá tu nombre."); return; }
        if (txtCorreoElectronico.Text.Trim() == "") { formError("Por favor completá tu correo electrónico."); return; }
        if (!verificarCorreo(txtCorreoElectronico.Text.Trim())) { formError("El correo electrónico que ingresaste no es válido."); return; }
        if (txtComentarios.Text.Trim() == "") { formError("Por favor completá tus comentarios."); return; }
		
        string cuerpoMail = "<html><body style='font-family: Arial, Verdana; font-size: 13px'>" +
            "<b>Administrador de PronosticoExtendido.net,</b><br /><br />" +
            "Se ha enviado un comentario desde el formulario de contacto.<br/ ><br/ >" +
            "<b>Nombre:</b> " + txtNombre.Text + "<br />" +
            "<b>Correo electrónico:</b> " + txtCorreoElectronico.Text.ToLower().Trim() + "<br />" +
            "<b>Comentarios:</b> " + txtComentarios.Text + "<br /><br /><br /><br />" +
            "Nota: Este mensaje fue generado automáticamente, por favor no lo respondas.</body></html>";

        MailMessage FMensaje = new MailMessage();
        FMensaje.From = new MailAddress("no-responder@pronosticoextendido.net");
        FMensaje.Subject = "[PronosticoExtendido.net] Se envió un comentario";
        FMensaje.IsBodyHtml = true;
        FMensaje.Priority = System.Net.Mail.MailPriority.Normal;
        SmtpClient FSMTP = new SmtpClient();
        FSMTP.Host = "127.0.0.1";
        FSMTP.UseDefaultCredentials = true;
        FMensaje.To.Add("juanpabloventoso@gmail.com");
        FMensaje.Body = cuerpoMail;
        try
        {
            FSMTP.Send(FMensaje);
            FMensaje.To.Clear();
        }
        catch (Exception E)
        {
            formError("No pudimos enviar el comentario: " + E.Message + E.InnerException);
            return;
        }

        lblEstado.Visible = false;
        lblEstadoOK.Visible = true;
        txtNombre.Enabled = false;
        txtCorreoElectronico.Enabled = false;
        txtComentarios.Enabled = false;
        btnAceptar.Enabled = false;
		
	}
		
}
